using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcAlasi : MonoBehaviour
{
    public static NpcAlasi instance;
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
                    Talk.instance.taskName.text = "阿拉什";
                    if (PlayerStatus.instance.isDuringTask)
                    {
                        if (PlayerStatus.instance.taskCode == 1)
                        {
                            Talk.instance.taskContent.text = "信送到了吗\n答复呢\n请您快把信送个去\n\n\n\n\n\n\n\n\n";
                        }
                        if (PlayerStatus.instance.taskCode == 2)
                        {
                            Talk.instance.taskContent.text = "答复终于来了\n请拿给我看下\n\n\n\n\n\n\n\n\n";
                            Talk.instance.completeButton.gameObject.SetActive(true);
                        }
                        if (PlayerStatus.instance.taskCode == 3)
                        {
                            if (PlayerStatus.instance.monsterNumKill < 10)
                            {
                                Talk.instance.taskContent.text = "骷髅兵 " + PlayerStatus.instance.monsterNumKill + "/10\n\n需要装备和药水的话去找对面的格雷吧\n他可以帮到你\n\n\n\n\n\n\n\n\n";
                            }
                            else
                            {
                                Talk.instance.taskContent.text = "真的是非常感谢您\n这是进城的通行证请拿好\n\n\n\n\n\n\n\n\n";
                            }
                            Talk.instance.completeButton.gameObject.SetActive(true);
                        }
                    }
                    else
                    {
                        if (PlayerStatus.instance.taskCode == 0)
                        {
                            Talk.instance.taskContent.text = "欢迎来到棋盘村\n我是村长阿拉什,您是冒险者吧,我有事情想要委托你\n\n\n\n\n\n\n\n";
                            Talk.instance.taskButton.gameObject.SetActive(true);
                        }
                        else if (PlayerStatus.instance.taskCode == 2)
                        {
                            Talk.instance.taskContent.text = "现在村子面临着危机\n需要借助冒险者的力量才能化解\n可以接受委托吗\n\n\n\n\n\n\n\n\n";
                            Talk.instance.taskButton.gameObject.SetActive(true);
                        }
                        else
                        {
                            Talk.instance.taskContent.text = "欢迎来到棋盘村\n有什么需要帮助吗\n\n\n\n\n\n\n\n\n";
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
        //送信
        if (PlayerStatus.instance.taskCode == 2)
        {
            PlayerStatus.instance.expNow += 20;
            chatContext.Add("获得20经验");
            PlayerStatus.instance.coin += 500;
            chatContext.Add("获得500金币");
            PlayerStatus.instance.isDuringTask = false;
            Talk.instance.CloseTalkPanel();
        }
        //骷髅兵
        if (PlayerStatus.instance.taskCode == 3)
        {
            if (PlayerStatus.instance.monsterNumKill >= 10)
            {
                PlayerStatus.instance.expNow += 20;
                chatContext.Add("获得20经验");
                PlayerStatus.instance.coin += 200; 
                chatContext.Add("获得200金币");
                PlayerStatus.instance.isDuringTask = false;
                Inventory.instance.GetItemInNum(2001, 1);
                chatContext.Add("获得长剑");
                Inventory.instance.GetItemInNum(2002, 1);
                chatContext.Add("获得盾牌");
                Inventory.instance.GetItemInNum(3002, 1);
                chatContext.Add("获得通行证");
                PlayerStatus.instance.monsterNumKill = 0;
                Talk.instance.CloseTalkPanel();
            }
            else
            {
                Talk.instance.taskContent.text = "需要击杀10只骷髅兵\n\n\n\n\n\n\n\n\n\n";
            }
        }
        ExpBar.instance.UpdateShow();
    }
}
