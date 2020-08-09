using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum TaskCode
{
    zero = 0,
    first = 1,
    second = 2,
    third = 3,
    fourth = 4,
    fifth = 5
}



public class QuestList : MonoBehaviour
{
    public bool talkPanelIsOpen = false;
    public TaskCode taskCode;

    void Start()
    {
        
    }

    void Update()
    {
    }

    public string TaskContent(TaskCode taskCode)
    {
        string content = "";
        if (taskCode == TaskCode.zero)
        {
            content = "现在这个村子发生了一些奇怪的事情,我需要你现在帮我把这封信交给村外的一位魔法师克劳德,我会给你相应的报酬\n\n\n\n\n\n\n";
        }
        if (taskCode == TaskCode.first)
        {
            content = "把这封信送回到村长阿拉什的手上,我会给你相应的报酬\n\n\n\n\n\n\n";
        }
        if (taskCode == TaskCode.second)
        {
            content = "其实最近村子附近的怪物突然变多了,前几天甚至有村民外出受到了袭击\n我想委托你去清理一下附近的骷髅兵\n如果你需要装备和药水的话\n对面武器商格雷那里可以购买\n\n\n\n\n\n\n";
        }
        if (taskCode == TaskCode.third)
        {
            content = "这位小哥听说你帮村子解决了不少外面的骷髅,我现在还差5个蓝宝石来打造装备\n击杀村外的骷髅兵有几率掉下\n给5个蓝宝石我就给你相应的报酬\n很划算吧\n\n\n\n\n";
        }
        if (taskCode == TaskCode.fourth)
        {
            content = "我现在还差5个蓝宝石\n击杀村外的骷髅兵有几率掉下\n给5个蓝宝石我就给你相应的报酬\n很划算吧\n\n\n\n\n";
        }
        if (taskCode == TaskCode.fifth)
        {
            content = "我现在还差5个蓝宝石\n击杀村外的骷髅兵有几率掉下\n给5个蓝宝石我就给你相应的报酬\n很划算吧\n\n\n\n\n";
        }
        return content;
    }
    
}

