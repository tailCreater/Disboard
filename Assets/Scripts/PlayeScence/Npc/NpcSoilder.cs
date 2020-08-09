using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcSoilder : MonoBehaviour
{
    public static NpcSoilder instance;
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
                    Talk.instance.taskName.text = "士兵";
                    Talk.instance.taskContent.text = "想要通过这里进城你需要通行证\n\n\n\n\n\n\n\n";
                    Talk.instance.completeButton.gameObject.SetActive(true);
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
        bool isHave = Inventory.instance.GetItemInInventory(3002);
        if (isHave)
        {
            this.gameObject.SetActive(false);
            Talk.instance.CloseTalkPanel();
            isTalking = false;
        }
        else
        {
            chatContext.Add("需要通行证");
            Talk.instance.taskName.text = "士兵";
            Talk.instance.taskContent.text = "你身上没有通行证\n\n\n你可以通过解决村长的烦恼获得通行证\n\n\n\n\n\n";
        }
    }
}
