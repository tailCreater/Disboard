using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcGray : MonoBehaviour
{
    public static NpcGray instance;
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
                    Talk.instance.taskName.text = "格雷";
                    Talk.instance.shopButton.gameObject.SetActive(true);
                    if (PlayerStatus.instance.isDuringTask)
                    {
                        Talk.instance.taskContent.text = "哟,小伙子\n要来买东西吗\n\n\n\n\n\n\n\n";
                        if (PlayerStatus.instance.taskCode > 3)
                        {
                            Talk.instance.completeButton.gameObject.SetActive(true);
                        }
                    }
                    else
                    {
                        if (PlayerStatus.instance.taskCode >= 3)
                        {
                            Talk.instance.taskContent.text = "小伙子\n有蓝水晶吗我无限收\n\n\n\n\n\n";
                            Talk.instance.taskButton.gameObject.SetActive(true);
                        }
                        else
                        {
                            Talk.instance.taskContent.text = "哟,小伙子是冒险者吧\n要不要来买点装备啊\n\n\n\n\n\n\n\n";
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
                ShopView.instance.CloseShopPanel();
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
        int crystalNum = Inventory.instance.GetNumInItem(3001);
        //如果数量足够任务完成
        if (crystalNum >= 5)
        {
            Inventory.instance.LoseItemInNum(3001,5);
            chatContext.Add("失去蓝水晶*5");
            Talk.instance.CloseTalkPanel();
            PlayerStatus.instance.expNow += 20;
            chatContext.Add("获得20经验");
            PlayerStatus.instance.coin += 500;
            chatContext.Add("获得500金币");
            ExpBar.instance.UpdateShow();
            PlayerStatus.instance.isDuringTask = false;
            isTalking = false;
        }
        else
        {
            Talk.instance.taskName.text = "格雷";
            Talk.instance.taskContent.text = "你身上水晶不够呀\n\n\n击杀骷髅兵有几率获得蓝水晶\n\n\n\n\n\n";
        }
    }
}
