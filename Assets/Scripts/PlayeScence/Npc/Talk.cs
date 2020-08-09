using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Talk : MonoBehaviour
{
    public static Talk instance;
    //任务内容
    private QuestList questList;
    public UILabel taskContent;
    public UILabel taskName;

    //对话按钮
    public GameObject acceptButton;
    public GameObject refuseButton;
    public GameObject taskButton;
    public GameObject shopButton;
    public GameObject completeButton;

    public bool isOpen = false;

    //设置更改窗容口的大小
    private Vector3 min = new Vector3(0, 0, 0);
    //设置更改窗容口的大小
    private Vector3 max = new Vector3(1, 1, 1);
    //设置窗口变化时间
    private float time = 0.5f;

    void Awake()
    {
        instance = this;
        questList = GameObject.Find("GameSetting").GetComponent<QuestList>();
        acceptButton.gameObject.SetActive(false);
        refuseButton.gameObject.SetActive(false);
        taskButton.gameObject.SetActive(false);
        completeButton.gameObject.SetActive(false);
        shopButton.gameObject.SetActive(false);      
    }

    /// <summary>
    /// 打开窗口
    /// </summary>
    public void OpenTalkPanel()
    {
        this.transform.DOScale(max, time);
        isOpen = true;
    }

    /// <summary>
    /// 关闭窗口
    /// </summary>
    public void CloseTalkPanel()
    {
        this.transform.DOScale(min, time);
        acceptButton.gameObject.SetActive(false);
        refuseButton.gameObject.SetActive(false);
        shopButton.gameObject.SetActive(false);
        taskButton.gameObject.SetActive(false);
        completeButton.gameObject.SetActive(false);
        isOpen = false;
    }


    /// <summary>
    /// 显示任务内容
    /// </summary>
    public void ShowTaskContent()
    {
        Talk.instance.taskButton.gameObject.SetActive(false);
        Talk.instance.shopButton.gameObject.SetActive(false);
        Talk.instance.acceptButton.gameObject.SetActive(true);
        Talk.instance.refuseButton.gameObject.SetActive(true);
        if (PlayerStatus.instance.taskCode == (int)TaskCode.zero)
        {
            Talk.instance.taskContent.text = questList.TaskContent(TaskCode.zero);
            Talk.instance.taskName.text = "送信";
        }
        else if (PlayerStatus.instance.taskCode == (int)TaskCode.first)
        {
            Talk.instance.taskContent.text = questList.TaskContent(TaskCode.first);
            Talk.instance.taskName.text = "送信";
        }
        else if (PlayerStatus.instance.taskCode == (int)TaskCode.second)
        {
            Talk.instance.taskContent.text = questList.TaskContent(TaskCode.second);
            Talk.instance.taskName.text = "击杀骷髅兵";
        }
        else if (PlayerStatus.instance.taskCode == (int)TaskCode.third)
        {
            Talk.instance.taskContent.text = questList.TaskContent(TaskCode.third);
            Talk.instance.taskName.text = "收集物资";
        }
        else
        {
            Talk.instance.taskContent.text = questList.TaskContent(TaskCode.third);
            Talk.instance.taskName.text = "收集物资";
        }
    }

    /// <summary>
    /// 接受任务
    /// </summary>
    public void OnAcceptButtonClick()
    {
        CloseTalkPanel();
        PlayerStatus.instance.taskCode += 1;
        PlayerStatus.instance.isDuringTask = true;
        acceptButton.gameObject.SetActive(false);
        refuseButton.gameObject.SetActive(false);
    }

    /// <summary>
    /// 拒绝任务
    /// </summary>
    public void OnRefuseButtonClick()
    {
        CloseTalkPanel();
        acceptButton.gameObject.SetActive(false);
        taskButton.gameObject.SetActive(false);
    }

    /// <summary>
    /// 完成任务
    /// </summary>
    public void OnCompleteButtonClick()
    {
        if (NpcSoilder.instance.isTalking)
        {
            NpcSoilder.instance.TaskReward();
        }
        else if (NpcGray.instance.isTalking)
        {
            NpcGray.instance.TaskReward();
        }
        else if (NpcClaude.instance.isTalking)
        {
            NpcClaude.instance.TaskReward();
        }
        else if (NpcAlasi.instance.isTalking)
        {
            NpcAlasi.instance.TaskReward();
        }
    }

}
