using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState {
    PlayerWalk,
    Attack,
    Death
}

public class PlayerAttack : MonoBehaviour
{
    public PlayerState state = PlayerState.PlayerWalk;

    private string aniAttack = "arthur_attack_01";

    private string aniDeath = "arthur_dead_01";

    public float attackTime = 2;     //攻击间隔时间
    public float attackDistance = 5;  //攻击距离
    public float distance = 0;  //距离
    private float timer = 2;
    private float soundCounter = 0;
    public float missRate = 0.1f;    //闪避几率

    public AudioClip hitSound ;         //击中怪物的音效
    private bool firstAttack = true;
    private Transform targetEnemy;
    private SkeletonNoraml target;
    private Vector3 nowTargetPos = Vector3.zero; //当前目标

    private PlayerMove move;
    public UILabel attackPoint;

    public bool isMiss = false;

    void Awake()
    {
        move = this.GetComponent<PlayerMove>();
        attackPoint = GameObject.Find("UI Root/StatusPanel/Attack/AttackPointGird/AttackPoint").GetComponent<UILabel>();
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            //射线检测
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            bool isCollider = Physics.Raycast(ray, out hitInfo);
            if (isCollider && hitInfo.collider.tag == Tags.Enemy && UICamera.isOverUI == false)
            {
                //点击到敌人
                state = PlayerState.Attack;
                targetEnemy = hitInfo.collider.transform;  //获得目标
                target = hitInfo.collider.gameObject.GetComponent<SkeletonNoraml>();
                if (targetEnemy.position != nowTargetPos)   //若点击的目标与当前目标的位置不同
                {
                    //切换当前目标
                    firstAttack = true;
                    nowTargetPos = targetEnemy.position;
                    timer = 2;
                    soundCounter = 0;
                }
                
            }
            else
            {
                //移动
                state = PlayerState.PlayerWalk; 
                targetEnemy = null;
                firstAttack = true;
            }
        }

        if(state == PlayerState.Attack)
        {
            distance = Vector3.Distance(targetEnemy.position, transform.position);
            transform.LookAt(targetEnemy);//旋转的时候就保证已自己Y轴为轴心旋转，朝向敌人
            if (distance > attackDistance)
            {
                move.SimpleMove(targetEnemy.position);
                firstAttack = true;
            }
            else 
            {
                AutoAttack();
            }
        }

        if (PlayerStatus.instance.hpNow <= 0)
        {
            state = PlayerState.Death;
            //SkeletonNoraml.instance.isHaveTarget = false;
            //SkeletonNoraml.instance.AnimationPlay("Idle");
            //播放死亡动画
            AnimationPlay(aniDeath);
            Destroy(this.gameObject, 2.1f);
        }
    }

    /// <summary>
    /// 播放动画
    /// </summary>
    /// <param name="aniName"></param>
    void AnimationPlay(string aniName)
    {
        GetComponent<Animation>().CrossFade(aniName);
    }

    private void AutoAttack()
    {
        AnimationPlay(aniAttack);
        timer += Time.deltaTime;
        if(timer >= attackTime)
        {
            soundCounter += Time.deltaTime;
            if (firstAttack)
            {
                if (soundCounter >= 1.0f)
                {
                    // SkeletonNoraml.instance.TakeDamage(Damage());
                    target.TakeDamage(Damage());
                    if (SkeletonNoraml.instance.isMiss == true)  //怪物闪避
                    {
                        SkeletonNoraml.instance.isMiss = false;
                    }
                    else
                    {
                        AudioSource.PlayClipAtPoint(hitSound, transform.position);
                    }
                    soundCounter = 0;
                    firstAttack = false;
                }
            }
            else
            {
                if (soundCounter >= 1.667f)
                {
                    //SkeletonNoraml.instance.TakeDamage(Damage());
                    target.TakeDamage(Damage());
                    if (SkeletonNoraml.instance.isMiss == true)  //怪物闪避
                    {
                        SkeletonNoraml.instance.isMiss = false;
                    }
                    else
                    {
                        AudioSource.PlayClipAtPoint(hitSound, transform.position);
                    }
                    soundCounter = 0;
                }
            }
        }
 
    }

    /// <summary>
    /// 受到伤害
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        float value = Random.Range(0f, 1f);
        if (value > missRate) //击中
        {
            if (PlayerStatus.instance.hpNow > 0)
            {
                PlayerStatus.instance.hpNow -= damage;
            }

        }
        else //闪避
        {
            if (PlayerStatus.instance.hpNow > 0)
            {
                isMiss = true;
            }
        }
    }

    private int Damage()
    {
        return (int.Parse(attackPoint.text) - SkeletonNoraml.instance.def) / 2;
    }
}
