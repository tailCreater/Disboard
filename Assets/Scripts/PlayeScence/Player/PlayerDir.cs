using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerDir : MonoBehaviour
{
    
    public GameObject move_ClickEffect;
    //鼠标点击的目标位置
    public Vector3 targetPosion ;
    //鼠标是否按下
    private bool isMouseClickDown;
    //角色是否在行走
    public bool isMoving = false;

    private PlayerMove playerMove;

    void Awake()
    {
        targetPosion = transform.position;
        playerMove = this.GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            bool isCollider = Physics.Raycast(ray, out hitInfo);
            if (isCollider && hitInfo.collider.tag == Tags.Ground && UICamera.isOverUI == false)
            {
                isMouseClickDown = true;
                playerMove.isHit = false;
                ShowClickEffect(hitInfo.point);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isMouseClickDown = false;
        }

        if (isMouseClickDown)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            bool isCollider = Physics.Raycast(ray, out hitInfo);
            if (isCollider && hitInfo.collider.tag == Tags.Ground)
            {
                PlayerFaceToClickPoint(hitInfo.point);
            }
        }
        else
        {
            if (isMoving)
            {
                PlayerFaceToClickPoint(targetPosion);
            }
        }
    }

    //显示点击特效
    void ShowClickEffect(Vector3 hitPoint)
    {
        hitPoint = new Vector3(hitPoint.x,hitPoint.y - 0.1f,hitPoint.z);
        GameObject obj = Instantiate(move_ClickEffect, hitPoint, Quaternion.identity);
        obj.transform.position = hitPoint;
        Destroy(obj ,0.3f);
    }

    //玩家朝向鼠标点击的目标位置
    void PlayerFaceToClickPoint(Vector3 hitPoint)
    {
        targetPosion = hitPoint;
        targetPosion = new Vector3(targetPosion.x,transform.position.y,targetPosion.z);
        this.transform.LookAt(targetPosion);
    }
}

