using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonSpawn : MonoBehaviour
{
    private int maxNum = 10;
    private int currentNum = 0;
    public float creatTime = 3;
    public float timer = 0;
    public GameObject monster;

    // Update is called once per frame
    void Update()
    {
        if(currentNum < maxNum)
        {
            timer += Time.deltaTime;
            if(timer > creatTime)
            {
                Vector3 pos = transform.position;
                pos.x += Random.Range(-40, 40);
                pos.z += Random.Range(-40, 40);
                GameObject.Instantiate(monster,pos,Quaternion.identity);
                timer = 0;
                currentNum++;
            }
        }
    }
}
