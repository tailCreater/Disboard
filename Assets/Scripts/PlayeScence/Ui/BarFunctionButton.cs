using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BarFunctionButton : MonoBehaviour
{
    private bool taskIsOpen = false;
    private bool bagIsOpen = false;
    private bool statusIsOpen = false;

    //任务系统
    public UILabel taskContent;
    public UILabel taskName;
    private QuestList questList;

    //背包系统
    public UILabel coinsNumber;


    //设置更改窗容口的大小
    private Vector3 min = new Vector3(0, 0, 0);
    //设置更改窗容口的大小
    private Vector3 max = new Vector3(1, 1, 1);
    //设置变化时间
    private float time = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        questList = GameObject.Find("GameSetting").GetComponent<QuestList>();
    }

    
    void Update()
    {
        coinsNumber.text = PlayerStatus.instance.coin.ToString();
    }
    
    
    //任务窗口----------------------------------

    /// <summary>
    /// 打开任务窗口
    /// </summary>
    public void ShowAndCloseTaskPanel()
    {
        Transform task = GameObject.Find("ShowTaskPanl").transform;
        if (taskIsOpen == false)
        {
            task.DOScale(max, time);
            taskIsOpen = true;
            if (PlayerStatus.instance.isDuringTask == true)
            {
                if (PlayerStatus.instance.taskCode == 1)
                {
                    taskContent.text = "把信送到村外的克劳德手上\n\n\n\n\n\n\n奖励:200金币";
                    taskName.text = "送信";
                }
                if (PlayerStatus.instance.taskCode == 2)
                {
                    taskContent.text = "把信送到村长阿拉什手上\n\n\n\n\n\n\n奖励:200金币";
                    taskName.text = "送信";
                }
                if (PlayerStatus.instance.taskCode == 3)
                {
                    taskContent.text = "击败村外的10只骷髅兵\n\n\n\n\n" + PlayerStatus.instance.monsterNumKill + "/10\n\n奖励:500金币与通行证";
                    taskName.text = "击杀骷髅兵";
                }
                if (PlayerStatus.instance.taskCode > 3)
                {
                    taskContent.text = "售卖5个蓝宝石\n蓝水晶可以通过击杀骷髅兵有机会获取\n\n\n\n\n\n奖励:200金币";
                    taskName.text = "交换蓝水晶";
                }
            }
        }
        else
        {
            task.DOScale(min, time);
            taskIsOpen = false;
        }
    }



    //背包窗口-------------------------------------------

    /// <summary>
    /// 背包窗口的打开与关闭
    /// </summary>
    public void ShowAndCloseBagPanel()
    {
        Transform bag = GameObject.Find("BagPanel").transform;
        if (bagIsOpen == false)
        {
            bag.DOScale(max, time);
            bagIsOpen = true;
        }
        else
        {
            bag.DOScale(min, time);
            bagIsOpen = false;
        }
    }


    //属性窗口-------------------------------------------

    public void ShowAndCloseStatusPanel()
    {
        Transform status = GameObject.Find("StatusPanel").transform;
        if (statusIsOpen == false)
        {
            status.DOScale(max, time);
            StatusView.instance.ShowStatus();
            statusIsOpen = true;
        }
        else
        {
            status.DOScale(min, time);
            statusIsOpen = false;
        }
 
    }

}
