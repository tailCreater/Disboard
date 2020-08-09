using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaterCreation : MonoBehaviour
{
    private PlayerStatus playerStatus; 
    //得到输入的角色名字
    public UIInput nameInput;


    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonDone()
    {
        StartMenuButton.instance.createNew = true;
        PlayerPrefs.SetInt("DateFromSave", 1);
        //存储角色名字
        PlayerPrefs.SetString("name",nameInput.value);
        //加载到下一个场景
        Application.LoadLevel(1);
    }
}
