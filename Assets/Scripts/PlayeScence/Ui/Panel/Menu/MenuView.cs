using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MenuView : MonoBehaviour
{
    private UITextList chatContext;
    private bool isOpen = false;
    //设置更改窗容口的大小
    private Vector3 min = new Vector3(0, 0, 0);
    //设置更改窗容口的大小
    private Vector3 max = new Vector3(1, 1, 1);
    //设置变化时间
    private float time = 0.5f;
    // Start is called before the first frame update

    void Awake()
    {
        chatContext = GameObject.Find("UI Root/FunctionBar/Chat/Label").GetComponent<UITextList>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    /// <summary>
    /// 保存游戏
    /// </summary>
    public void SaveGame()
    {
        ShowAndCloseMenuView();
        chatContext.Add("保存成功");
    }

    /// <summary>
    /// 退出游戏
    /// </summary>
    public void CloseGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// 窗口控制
    /// </summary>
    public void ShowAndCloseMenuView()
    {
        if (isOpen)
        {
            this.transform.DOScale(min, time);
            isOpen = false;
        }
        else
        {
            this.transform.DOScale(max, time);
            isOpen = true;
        }
    }
}
