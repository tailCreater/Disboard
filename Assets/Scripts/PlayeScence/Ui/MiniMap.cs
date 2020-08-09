using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    private Camera miniMap;

    void Start()
    {
        miniMap = GameObject.FindGameObjectWithTag(Tags.MiniMap).GetComponent<Camera>();
    }

    //放大
    public void OnMapUpClick()
    {
        miniMap.orthographicSize--;
    }

    //缩小
    public void OnMapDownClick()
    {
        miniMap.orthographicSize++;
    }

}
