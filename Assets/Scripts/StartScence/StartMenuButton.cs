using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class StartMenuButton : MonoBehaviour
{
    public GameObject charaterCreationPanel;
    public static StartMenuButton instance;
    public bool createNew = false;
    private string dirpath;
    public GameObject fire1;
    public GameObject fire2;
    //场景数据的保存以及场景数据的传递使用PlayerPrefs

    //场景分类
       //开始场景
       //选择角色或创建角色
       //游戏场景

    void Awake()
    {
        instance = this;
        dirpath = Application.streamingAssetsPath + "/";
    }

    /// <summary>
    /// 开始新游戏，创建角色
    /// </summary>
    public void NewGame()
    {
        GameObject.Find("startMenuPanel").SetActive(false);
        fire1.SetActive(true);
        fire2.SetActive(true);
        charaterCreationPanel.SetActive(true);
    }

    /// <summary>
    /// 继续游戏，读取唯一的存档
    /// </summary>
    public void LoadGame()
    {
        if (!Directory.Exists(dirpath))
        {
            print("nofile");
        }
        else 
        {
            PlayerPrefs.SetInt("DateFromSave", 1);
            Application.LoadLevel(1);
        }
        
    }
}
