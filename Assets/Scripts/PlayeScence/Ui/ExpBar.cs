using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpBar : MonoBehaviour
{
    public static ExpBar instance;
    private UISlider expBar;
    private UILabel expLabel;


    void Awake()
    {
        instance = this;     
    }

    void Start()
    {
        expBar = transform.GetComponent<UISlider>();
        expLabel = transform.Find("Thumb/Label").GetComponent<UILabel>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateShow()
    {
        expBar.value = PlayerStatus.instance.expNow / (PlayerStatus.instance.level * 20);
        expLabel.text = PlayerStatus.instance.expNow + "/" + PlayerStatus.instance.level * 20;
    }
}
