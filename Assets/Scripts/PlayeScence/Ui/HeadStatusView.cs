using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadStatusView : MonoBehaviour
{
    public static HeadStatusView instance;
    private UILabel playerName;
    private UILabel playerLevel;

    private UISlider hpBar;
    private UISlider mpBar;
    private UILabel hpLabel;
    private UILabel mpLabel;


    void Awake()
    {
        instance = this;
        playerName = transform.Find("PlayerName").GetComponent<UILabel>();
        playerLevel = transform.Find("PlayerLevel").GetComponent<UILabel>();
        hpBar = transform.Find("Hp").GetComponent<UISlider>();
        mpBar = transform.Find("Mp").GetComponent<UISlider>();
        hpLabel = transform.Find("Hp/Thumb/Label").GetComponent<UILabel>();
        mpLabel = transform.Find("Mp/Thumb/Label").GetComponent<UILabel>();
        
    }

    void Start()
    {
        UpdateShow();
    }

    public void UpdateShow()
    {
        playerName.text = PlayerStatus.instance.name;
        playerLevel.text = "Lv " + PlayerStatus.instance.level;
        hpBar.value = PlayerStatus.instance.hpNow / PlayerStatus.instance.hp;
        mpBar.value = PlayerStatus.instance.mpNow / PlayerStatus.instance.mp;
        hpLabel.text = PlayerStatus.instance.hpNow + "/" + PlayerStatus.instance.hp;
        mpLabel.text = PlayerStatus.instance.mpNow + "/" + PlayerStatus.instance.mp;
    }

}
