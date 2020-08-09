using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BarNpc : MonoBehaviour
{
    public static BarNpc instance;
    private GameObject player;
    //小地图任务标记
    private Transform talkPanel;
    public GameObject taskMarkA;
    public GameObject taskMarkC;
    public GameObject taskMarkG;
 

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        player = GameObject.Find("arthur_Player");
    }
  
    void Update()
    {
        ShowTaskMark();
    }

    

    /// <summary>
    ///显示任务有关任务的标记
    /// </summary>
    public void ShowTaskMark()
    {
        if (PlayerStatus.instance.isDuringTask)
        {
            if (PlayerStatus.instance.taskCode == 1)
            {
                taskMarkA.gameObject.SetActive(false);
                taskMarkC.gameObject.SetActive(true);
                taskMarkG.gameObject.SetActive(false);
            }
            if (PlayerStatus.instance.taskCode == 2)
            {
                taskMarkA.gameObject.SetActive(true);
                taskMarkC.gameObject.SetActive(false);
                taskMarkG.gameObject.SetActive(false);
            }
            if (PlayerStatus.instance.taskCode == 3)
            {
                taskMarkG.gameObject.SetActive(false);
                taskMarkC.gameObject.SetActive(false);
                taskMarkA.gameObject.SetActive(true);
            }
            if (PlayerStatus.instance.taskCode > 3)
            {
                taskMarkA.gameObject.SetActive(false);
                taskMarkC.gameObject.SetActive(false);
                taskMarkG.gameObject.SetActive(true);
            }
        }
        else
        {
            if (PlayerStatus.instance.taskCode == 0)
            {
                taskMarkA.gameObject.SetActive(true);
                taskMarkC.gameObject.SetActive(false);
                taskMarkG.gameObject.SetActive(false);
            }
            if (PlayerStatus.instance.taskCode == 1)
            {
                taskMarkC.gameObject.SetActive(true);
                taskMarkA.gameObject.SetActive(false);
                taskMarkG.gameObject.SetActive(false);
            }
            if (PlayerStatus.instance.taskCode == 2)
            {
                taskMarkA.gameObject.SetActive(true);
                taskMarkC.gameObject.SetActive(false);
                taskMarkG.gameObject.SetActive(false);
            }
            if (PlayerStatus.instance.taskCode > 2)
            {                
                taskMarkA.gameObject.SetActive(false);
                taskMarkC.gameObject.SetActive(false);
                taskMarkG.gameObject.SetActive(true);
            }
        }
    }

 


}
