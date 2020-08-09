using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusView : MonoBehaviour
{
    public static StatusView instance;
    private UILabel level;
    private UILabel hpPoint;
    private UILabel mpPoint;
    private UILabel attackPoint;
    private UILabel defPoint;
    private UILabel strengthPoint;
    private UILabel powerPoint;
    private UILabel endurancePoint;

    private UILabel strengthPlusPoint;
    private UILabel powerPlusPoint;
    private UILabel endurancePlusPoint;

    
    private UILabel remainPoint;
    private UILabel expPoint;

    void Awake()
    {
        instance = this;
        level = GameObject.Find("Level").GetComponent<UILabel>();
        hpPoint = GameObject.Find("HpPoint").GetComponent<UILabel>();
        mpPoint = GameObject.Find("MpPoint").GetComponent<UILabel>();
        attackPoint = GameObject.Find("AttackPoint").GetComponent<UILabel>();
        defPoint = GameObject.Find("DefPoint").GetComponent<UILabel>();
        strengthPoint = GameObject.Find("StrengthPoint").GetComponent<UILabel>();
        powerPoint = GameObject.Find("PowerPoint").GetComponent<UILabel>();
        endurancePoint = GameObject.Find("EndurancePoint").GetComponent<UILabel>();
        strengthPlusPoint = GameObject.Find("StrengthPlusPoint").GetComponent<UILabel>();
        powerPlusPoint = GameObject.Find("PowerPlusPoint").GetComponent<UILabel>();
        endurancePlusPoint = GameObject.Find("EndurancePlusPoint").GetComponent<UILabel>();
        remainPoint = GameObject.Find("RemainPoint").GetComponent<UILabel>();
        expPoint = GameObject.Find("ExpNow").GetComponent<UILabel>();
    }

    void Start()
    {
        PlayerStatus.instance.hp = PlayerStatus.instance.hpBase + PlayerStatus.instance.strengthPoint * 2;
        PlayerStatus.instance.hpNow = PlayerStatus.instance.hp;
        ShowStatus();
    }

    // Update is called once per frame
    void Update()
    {
    }


    //更新属性面板
    public void ShowStatus()
    {
        level.text = PlayerStatus.instance.level.ToString();
        hpPoint.text = PlayerStatus.instance.hpNow + "/" + PlayerStatus.instance.hp;
        mpPoint.text = PlayerStatus.instance.mpNow + "/" + PlayerStatus.instance.mp;
        strengthPoint.text = PlayerStatus.instance.strengthPoint.ToString();
        powerPoint.text = PlayerStatus.instance.powerPoint.ToString();
        endurancePoint.text = PlayerStatus.instance.endurancePoint.ToString();
        strengthPlusPoint.text = PlayerStatus.instance.strengthPlusPoint.ToString();
        powerPlusPoint.text = PlayerStatus.instance.powerPlusPoint.ToString();
        endurancePlusPoint.text = PlayerStatus.instance.endurancePlusPoint.ToString();
        attackPoint.text = (PlayerStatus.instance.attack + PlayerStatus.instance.powerPoint * 2).ToString();
        defPoint.text = (PlayerStatus.instance.def + PlayerStatus.instance.endurancePoint * 1).ToString();
        remainPoint.text = PlayerStatus.instance.pointRemain.ToString();
        expPoint.text = PlayerStatus.instance.expNow + "/" + PlayerStatus.instance.level * 20;
    }


    //更新加点减点显示
    private void ShowChange()
    {
        strengthPlusPoint.text = PlayerStatus.instance.strengthPlusPoint.ToString();
        powerPlusPoint.text = PlayerStatus.instance.powerPlusPoint.ToString();
        endurancePlusPoint.text = PlayerStatus.instance.endurancePlusPoint.ToString();
        remainPoint.text = PlayerStatus.instance.pointRemain.ToString();
    }


    //升级按钮
    public void OnLevelUpButtonClick()
    {
        if (PlayerStatus.instance.expNow >= PlayerStatus.instance.level * 20)
        {
            PlayerStatus.instance.expNow -= PlayerStatus.instance.level * 20;
            PlayerStatus.instance.level += 1;
            PlayerStatus.instance.hpBase += 10;
            PlayerStatus.instance.strengthPoint += 1;
            PlayerStatus.instance.powerPoint += 1;
            PlayerStatus.instance.endurancePoint += 1;
            PlayerStatus.instance.pointRemain += 5;
            PlayerStatus.instance.hp = PlayerStatus.instance.hpBase + PlayerStatus.instance.strengthPoint * 2;
            PlayerStatus.instance.hpNow = PlayerStatus.instance.hp;
            ShowStatus();
            HeadStatusView.instance.UpdateShow();
            ExpBar.instance.UpdateShow();
        }
    }


    //体力加减点按钮
    public void OnStrengthPlusButtonClick()
    {
        if (PlayerStatus.instance.pointRemain > 0)
        {
            PlayerStatus.instance.strengthPlusPoint += 1;
            PlayerStatus.instance.pointRemain -= 1;
            ShowChange();
        }
    }

    public void OnStrengthReduceButtonClick()
    {
        if (PlayerStatus.instance.strengthPlusPoint > 0)
        {
            PlayerStatus.instance.strengthPlusPoint -= 1;
            PlayerStatus.instance.pointRemain += 1;
            ShowChange();
        }
 
    }

    //力量加减点按钮
    public void OnPowerPlusButtonClick()
    {
        if (PlayerStatus.instance.pointRemain > 0)
        {
            PlayerStatus.instance.powerPlusPoint += 1;
            PlayerStatus.instance.pointRemain -= 1;
            ShowChange();
        }
    }

    public void OnPowerReduceButtonClick()
    {
        if (PlayerStatus.instance.powerPlusPoint > 0)
        {
            PlayerStatus.instance.powerPlusPoint -= 1;
            PlayerStatus.instance.pointRemain += 1;
            ShowChange();
        }

    }

    //耐力加减点按钮
    public void OnEndurancePlusButtonClick()
    {
        if (PlayerStatus.instance.pointRemain > 0)
        {
            PlayerStatus.instance.endurancePlusPoint += 1;
            PlayerStatus.instance.pointRemain -= 1;
            ShowChange();
        }
    }

    public void OnEnduranceReduceButtonClick()
    {
        if (PlayerStatus.instance.endurancePlusPoint > 0)
        {
            PlayerStatus.instance.endurancePlusPoint -= 1;
            PlayerStatus.instance.pointRemain += 1;
            ShowChange();
        }
    }

    //确定加点按钮
    public void OnDetermineButtonClick()
    {
        PlayerStatus.instance.strengthPoint += PlayerStatus.instance.strengthPlusPoint;
        PlayerStatus.instance.strengthPlusPoint = 0;
        PlayerStatus.instance.powerPoint += PlayerStatus.instance.powerPlusPoint;
        PlayerStatus.instance.powerPlusPoint = 0;
        PlayerStatus.instance.endurancePoint += PlayerStatus.instance.endurancePlusPoint;
        PlayerStatus.instance.endurancePlusPoint = 0;
        PlayerStatus.instance.hp = PlayerStatus.instance.hpBase + PlayerStatus.instance.strengthPoint * 2;
        PlayerStatus.instance.hpNow = PlayerStatus.instance.hp;
        ShowStatus();
    }

}
