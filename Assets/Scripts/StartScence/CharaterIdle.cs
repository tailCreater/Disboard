using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaterIdle : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        PlayIdle("arthur_idle_01");
    }
    void PlayIdle(string aniName)
    {
        GetComponent<Animation>().CrossFade(aniName);
    }
}
