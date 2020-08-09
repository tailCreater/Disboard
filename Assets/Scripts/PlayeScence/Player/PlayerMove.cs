using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerWalkState
{
    Idle,
    Moving
}

public class PlayerMove : MonoBehaviour
{
    private PlayerDir playerDir;
    private CharacterController controller;
    private PlayerWalkState playerState = PlayerWalkState.Idle;
    private PlayerAttack attack;
    public float speed = 8;
    public bool isHit = false;
    public AudioClip playerMoveSound;
    private float walkCounter = 0;

    void Awake()
    {
        playerDir = this.GetComponent<PlayerDir>();
        controller = this.GetComponent<CharacterController>();
        attack = this.GetComponent<PlayerAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        if(attack.state == PlayerState.PlayerWalk)
        {
            float distance = Vector3.Distance(playerDir.targetPosion, this.transform.position);
            if (isHit)
            {
                distance = Vector3.Distance(transform.position, transform.position);
            }
            if (distance > 0.3f)
            {
                playerState = PlayerWalkState.Moving;
                controller.SimpleMove(transform.forward * speed);

                AnimationPlayer("arthur_walk_01");
                walkCounter += Time.deltaTime;
                if (walkCounter > 0.4f)
                {
                    AudioSource.PlayClipAtPoint(playerMoveSound, transform.position);
                    walkCounter = 0;
                }
                playerDir.isMoving = true;
            }
            else
            {
                playerState = PlayerWalkState.Idle;
                AnimationPlayer("arthur_idle_01");
                playerDir.isMoving = false;
            }
        }
    }


    void AnimationPlayer(string aniName)
    {
        GetComponent<Animation>().CrossFade(aniName);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Collider collider = hit.collider;
        if (collider == null || hit.transform.tag == Tags.Ground)
        {
            return;
        }

        isHit = true;
        playerState = PlayerWalkState.Idle;
        AnimationPlayer("arthur_idle_01");
        playerDir.isMoving = false;
    }

    public void SimpleMove(Vector3 targetPos)
    {
        controller.SimpleMove(transform.forward * speed);
        AnimationPlayer("arthur_walk_01");
    }
}
