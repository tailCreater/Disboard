using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public static PlayerStatus instance;
    public string name = "";
    public int hp = 130;
    public int hpBase = 100;
    public float hpNow = 130;
    public int mp = 100;
    public float mpNow = 100;
    public int coin = 1000;
    public int level = 1;
    public int monsterNumKill = 0;
    //力量
    public int powerPoint = 15;
    public int powerPlusPoint = 0;
    //耐力
    public int endurancePoint = 15;
    public int endurancePlusPoint = 0;
    //体力
    public int strengthPoint = 15;
    public int strengthPlusPoint = 0;
    //现有经验
    public float expNow = 0;

    //攻击力
    public int attack = 10;
    //防御力
    public int def = 10;

    //升级获得的剩余点数
    public int pointRemain= 0;

    //目前已完成的任务数
    public int taskCode = 0;
    //是否在任务中
    public bool isDuringTask = false;

    void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// 获取金币
    /// </summary>
    /// <param name="num"></param>
    public void GetCoin(int num)
    {
        coin += num;
    }

    /// <summary>
    /// 获得治疗
    /// </summary>
    /// <param name="hp"></param>
    /// <param name="mp"></param>
    public void Recover(int hp,int mp)
    {
        hpNow += hp;
        mpNow += mp;
        if(hpNow > this.hp)
        {
            hpNow = this.hp;
        }
        if (mpNow > this.mp)
        {
            mpNow = this.mp;
        }
    }

    /// <summary>
    /// 获取经验
    /// </summary>
    public void GetExp(int exp)
    {
        expNow += exp;
    }
}
