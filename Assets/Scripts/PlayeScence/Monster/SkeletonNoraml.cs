using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterState { 
    Walk,
    Attack,
    Death,
    patrol
}

public class SkeletonNoraml : MonoBehaviour
{
    public static SkeletonNoraml instance;
    private PlayerAttack playerAttack;
    public MonsterState state = MonsterState.patrol;  //怪物生成之后先是巡逻状态
    private Transform player;
    private UITextList chatContext;
    private string aniIdle = "Idle";
    private string aniDeath = "Death";
    private string aniWalk = "Walk";
    private string aniAttack = "Attack";
    public string aniNameNow;

    public float time = 2;        //巡逻时间
    private float timer = 0;      //巡逻计时
    private CharacterController skeletorConroller;
    //属性
    public int hp = 100;             //怪物血量
    public float missRate = 0.1f;    //闪避几率
    public int attack = 50;          //攻击力
    public int def = 10;             //防御力
    public float speed = 2;          //移动速度
    public float attackTime = 1.38f;     //设置定时器时间 2.767秒攻击一次(攻速1.38s动画播放1.38s)
    private float attackCounter = 0;    //攻击计时器，计时攻击间隔
    public float attackDistance = 4;    //攻击目标的距离，也是寻路的目标距离
    public Vector3 targetPos = Vector3.zero;  //玩家的位置
    public float distance = 0;         //怪物与玩家的距离
    public AudioClip hitSound;         //击中玩家的音效
    private float soundCounter = 0;    //计时音效间隔
    private  bool firstAttack = true;  //是否为第一下攻击

    private GameObject hudTextFollow;  //怪物的受伤和闪避显示
    private GameObject hudTextGo;      //显示的位置
    public GameObject hudTextPrefab; 
    private HUDText hudText;
    private UIFollowTarget target;
    private bool isKilled = true;

    private UILabel playerDef;
    public bool isMiss = false;
    public bool isHaveTarget = false;
    private bool isHit = false;

    void Awake()
    {
        instance = this;
        aniNameNow = aniIdle;
        state = MonsterState.patrol;
        skeletorConroller = GetComponent<CharacterController>();   
        hudTextFollow = transform.Find("HudText").gameObject;
        attackCounter = attackTime;            //一开始只要抵达目标立即攻击
        chatContext = GameObject.Find("UI Root/FunctionBar/Chat/Label").GetComponent<UITextList>();
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag(Tags.Player).transform;
        playerAttack = player.GetComponent<PlayerAttack>();
        playerDef = GameObject.Find("UI Root/StatusPanel/Def/DefPointGird/DefPoint").GetComponent<UILabel>();
        hudTextGo = GameObject.Instantiate(hudTextPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        hudTextGo.transform.parent = HudTextPanel.instance.gameObject.transform;
        hudTextGo.transform.localScale = new Vector3(1,1,1);
        //hudTextGo = NGUITools.AddChild(HudTextPanel.instance.gameObject.transform, hudTextPrefab);
        hudText = hudTextGo.GetComponent<HUDText>();
        target = hudTextGo.GetComponent<UIFollowTarget>();
        target.target = hudTextFollow.transform;
        target.gameCamera = Camera.main;
        target.uiCamera = UICamera.currentCamera;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerAttack.state != PlayerState.Death)
        {
            targetPos = player.position;  //更新玩家位置
            distance = Vector3.Distance(targetPos, transform.position);   //计算距离
            //计算距离，靠近到一定距离发现玩家
            if (distance <= 20)
            {
                isHaveTarget = true;
            }
            else
            {
                isHaveTarget = false;
            }
        }
        else
        {
            isHaveTarget = false;
        }
        if (isHaveTarget)
        {
            state = MonsterState.Attack;
        }
        else 
        {
            state = MonsterState.patrol;
        }

        if (state == MonsterState.Attack)
        {
            AutoAttack();
        }
        else if (state == MonsterState.patrol)
        {
            if (isHit || instance.transform.localPosition.z < 320 || instance.transform.localPosition.z > 450 || instance.transform.localPosition.x < 400 || instance.transform.localPosition.x > 580)
            {
                //碰撞到物体停下换方向
                AnimationPlay(aniIdle);
                transform.Rotate(transform.up * Random.Range(90, 270));
                aniNameNow = aniWalk;
                isHit = false;
            }
            //播放巡逻动画
            AnimationPlay(aniNameNow);
            if (aniNameNow == aniWalk)
            {
                skeletorConroller.SimpleMove(transform.forward * speed);
            }
            timer += Time.deltaTime;
            //计时切换状态
            if (timer >= time)
            {
                timer = 0;
                RandomState();
            }
        }
        if (hp <= 0)
        {
            state = MonsterState.Death;
            aniNameNow = aniDeath;
            playerAttack.state = PlayerState.PlayerWalk;
            if (isKilled)
            {
                if(Random.Range(0,100) > 50)
                {
                    Inventory.instance.GetItemInNum(3001,1);
                    chatContext.Add("获得蓝水晶");
                }
                PlayerStatus.instance.expNow += 10;
                PlayerStatus.instance.monsterNumKill++;
                chatContext.Add("获得10经验");
                ExpBar.instance.UpdateShow();
                StatusView.instance.ShowStatus();
                isKilled = false;
            }
            soundCounter = 0;
            //播放死亡动画
            AnimationPlay(aniNameNow);
            Destroy(this.gameObject, 2.1f);
            Destroy(hudTextGo.gameObject, 2.1f);
        }

    }

    /// <summary>
    /// 播放动画
    /// </summary>
    /// <param name="aniName"></param>
    public void AnimationPlay(string aniName)
    {
        GetComponent<Animation>().CrossFade(aniName);
    }


    /// <summary>
    /// 巡逻时选择方向
    /// </summary>
    void RandomState()
    {
        int value = Random.Range(0, 2);
        if (value < 0.4)
        {
            aniNameNow = aniIdle;
        }
        else
        {
            if (aniNameNow == aniIdle)
            {
                //停下再走动时换方向
                transform.Rotate(transform.up * Random.Range(0, 360));
            }
            aniNameNow = aniWalk;
        }
    }

    /// <summary>
    /// 自动攻击
    /// </summary>
    private void AutoAttack()
    {
        targetPos.y = transform.position.y;//此处的作用将自身的Y轴值赋值给目标Y值
        transform.LookAt(targetPos);//旋转的时候就保证已自己Y轴为轴心旋转，朝向玩家
        if (distance <= attackDistance)
        {
            aniNameNow = aniAttack;
            attackCounter += Time.deltaTime;
            if (attackCounter > attackTime)//定时器功能实现
            {
                //播放攻击动画
                AnimationPlay(aniNameNow);
                attackCounter = 0;
            }
            soundCounter += Time.deltaTime;
            if (firstAttack)
            {
                if (soundCounter >= 1.0f)
                {     
                    playerAttack.TakeDamage(Damage());
                    if (playerAttack.isMiss)
                    {
                        playerAttack.isMiss = false;
                    }
                    else
                    {
                        AudioSource.PlayClipAtPoint(hitSound, transform.position);
                    }
                    HeadStatusView.instance.UpdateShow();
                    soundCounter = 0;
                    firstAttack = false;
                }
            }
            else
            {
                if (soundCounter >= 3.0f)
                {
                    playerAttack.TakeDamage(Damage());
                    if (playerAttack.isMiss)
                    {
                        playerAttack.isMiss = false;
                    }
                    else
                    {
                        AudioSource.PlayClipAtPoint(hitSound, transform.position);
                    }
                    HeadStatusView.instance.UpdateShow();
                    soundCounter = 0;
                }
            }
        }
        else
        {
            aniNameNow = aniWalk;
            //播放追踪动画
            AnimationPlay(aniNameNow);
            soundCounter = 0;
            firstAttack = true;
            attackCounter = attackTime;
            skeletorConroller.SimpleMove(transform.forward * speed);
        }
    }

    /// <summary>
    /// 碰撞到物体
    /// </summary>
    /// <param name="hit"></param>
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Collider collider = hit.collider;
        if (collider == null || hit.transform.tag == Tags.Ground)
        {
            return;
        }

        isHit = true;
    }

    /// <summary>
    /// 受到伤害
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        float value = Random.Range(0f,1f);
        if (value > missRate) //击中
        {
            if (this.hp > 0)
            {
                this.hp -= damage;
                this.hudText.Add("-" + damage, Color.white, 1);
            }
        }
        else //闪避
        {
            if (this.hp > 0)
            {
                this.hudText.Add("Miss", Color.gray, 1);
                isMiss = true;
            }    
        }
    }

    private int Damage()
    {
        return (this.attack - int.Parse(playerDef.text)) / 2;
    }

}
