using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcClaude : MonoBehaviour
{
    public static NpcClaude instance;
    private GameObject player;
    private UITextList chatContext;
    //玩家与NPC的距离
    private Vector3 distance;
    //正在谈话的NPC与玩家的距离
    private float distanceTalking;
    //正在谈话的NPC
    //private GameObject talkingNpc;
    private Vector3 talkPosition = Vector3.zero;

    public bool isTalking = false;

    void Awake()
    {
        instance = this;
        chatContext = GameObject.Find("UI Root/FunctionBar/Chat/Label").GetComponent<UITextList>();
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag(Tags.Player);
    }

    //鼠标位于这个collider上会自动每帧调用这个方法
    void OnMouseOver()
    {
        distance = player.transform.position - this.transform.position;
        //点击NPC显示对话
        if (Input.GetMouseButton(0))
        {
            if (ShopView.instance.shopIsOpen == false)
            {
                if (Mathf.Abs(distance.magnitude) < 15)
                {
                    ShowNpcTalkPanel();
                    talkPosition = player.transform.position;
                    Talk.instance.taskName.text = "克劳德";
                    if (PlayerStatus.instance.isDuringTask)
                    {
                        if (PlayerStatus.instance.taskCode == 1)
                        {
                            Talk.instance.taskContent.text = "冒险者,找我有什么事吗\n村长的来信,快让我看看\n\n\n\n\n\n\n\n";
                            Talk.instance.completeButton.gameObject.SetActive(true);
                        }
                        if (PlayerStatus.instance.taskCode == 2)
                        {
                            Talk.instance.taskContent.text = "快把信送回村长手上\n之后可能村长还会委托你的\n到时还请你帮助村子度过这次难关\n\n\n\n\n\n\n\n\n";
                        }
                    }
                    else
                    {
                        if (PlayerStatus.instance.taskCode == 1)
                        {
                            Talk.instance.taskContent.text = "冒险者\n我有事委托你\n\n\n\n\n\n\n\n";
                            Talk.instance.taskButton.gameObject.SetActive(true);
                        }
                        else
                        {
                            Talk.instance.taskContent.text = "冒险者\n村子出来右边的路走到尽头的城门就是国家的边界,通过那里你就可以进入帝国\n虽然路上附加会有怪物不过就你的身手没什么问题\n\n\n\n\n\n\n";
                        }

                    }
                }
            }
        }
    }

    void Update()
    {
        if (isTalking)
        {
            distanceTalking = Mathf.Abs((player.transform.position - talkPosition + distance).magnitude);
            if (distanceTalking > 20)
            {
                Talk.instance.CloseTalkPanel();
                isTalking = false;
            }
        }

    }

    /// <summary>
    /// 打开显示NPC谈话窗口
    /// </summary>
    void ShowNpcTalkPanel()
    {
        Talk.instance.OpenTalkPanel();
        isTalking = true;
    }

    /// <summary>
    /// 任务奖励
    /// </summary>
    public void TaskReward()
    {
        PlayerStatus.instance.expNow += 20;
        chatContext.Add("获得20经验");
        PlayerStatus.instance.coin += 500;
        chatContext.Add("获得500金币");
        ExpBar.instance.UpdateShow();
        PlayerStatus.instance.isDuringTask = false;
        Talk.instance.CloseTalkPanel();
    }
}
