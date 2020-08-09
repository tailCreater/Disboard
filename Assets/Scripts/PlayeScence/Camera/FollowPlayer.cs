using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Vector3 offsetPosion;
    private PlayerAttack playerAttack;
    private Transform player;
    public float distance = 0;
    public float scrollSpeed = 5;
    private bool isRotating = false;
    public float rotateSpeed = 1;

    void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag(Tags.Player).transform;
        this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 16, player.transform.position.z - 15);
        playerAttack = player.GetComponent<PlayerAttack>();
        transform.LookAt(player.position);
        offsetPosion = transform.position - player.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerAttack.state != PlayerState.Death)
        {
            transform.position = offsetPosion + player.position;
            //鼠标右键来控制视野旋转
            RotateView();
            //鼠标滑动控制视角远近
            ScorllView();
        }
        
    }


    //鼠标滑动控制视角远近
    //向前拉近向后拉远
    //向后是负值，向前是正值
    void ScorllView()
    {
        distance = offsetPosion.magnitude;
        distance -= Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
        distance = Mathf.Clamp(distance, 11, 30);
        offsetPosion = offsetPosion.normalized * distance;
    }

    //鼠标右键来控制视野旋转
    void RotateView()
    {
        //Input.GetAxis("Mouse X"); //鼠标水平方向的滑动
        //Input.GetAxis("Mouse Y"); //鼠标垂直方向的滑动
        if (Input.GetMouseButtonDown(1))
        {
            isRotating = true;
        }
        if(Input.GetMouseButtonUp(1))
        {
            isRotating = false;
        }

        if (isRotating)
        {
            transform.RotateAround(player.position,player.up, Input.GetAxis("Mouse X") * rotateSpeed);

            Vector3 originPosition = transform.position;
            Quaternion oringinRotation = transform.rotation;

            transform.RotateAround(player.position, transform.right, -Input.GetAxis("Mouse Y") * rotateSpeed);
            float x = transform.eulerAngles.x;
            if (x < 10 || x >80)
            {
                transform.position = originPosition;
                transform.rotation = oringinRotation;
            }
            x = Mathf.Clamp(x, 10, 80);
            transform.eulerAngles = new Vector3(x,transform.eulerAngles.y,transform.eulerAngles.z);
        }
        offsetPosion = transform.position - player.position;
    }
}
