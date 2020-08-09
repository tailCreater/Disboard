using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public GameObject inventoryItem;

    public List<InventoryItemGird> itemGirdList = new List<InventoryItemGird>();

    void Awake()
    {
        instance = this;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            GetItem(2002);
            GetItem(3001);
            GetItem(2001);
        }
    }

    /// <summary>
    /// 通过id获取物品
    /// </summary>
    /// <param name="id"></param>
    private void GetItem(int id)
    { 
        //查找是否存在该物品
        InventoryItemGird gird = null;
        foreach(InventoryItemGird temp in itemGirdList)
        {
            if(temp.id == id)
            {
                gird = temp;
                break;
            }
        }
        //若存在 num+1
        if (gird != null)
        {
            gird.PlusNumber();
        }
        //不存在 查找新空格子添加进去
        else 
        {
            foreach (InventoryItemGird temp in itemGirdList)
            {
                if (temp.id == 0)
                {
                    gird = temp;
                    break;
                }
            }
            //这里已经找到了新的空格，只需要放进去
            if (gird != null)
            {
                GameObject itemGo =  NGUITools.AddChild(gird.gameObject, inventoryItem);
                itemGo.transform.localPosition = Vector3.zero;
                itemGo.GetComponent<UISprite>().depth = 7;
                gird.SetId(id,1);
            }
        }
    }

    private void LoseId(int id)
    {
        //查找是否存在该物品
        InventoryItemGird gird = null;
        foreach (InventoryItemGird temp in itemGirdList)
        {
            if (temp.id == id)
            {
                gird = temp;
                break;
            }
        }
        //若存在 num-1
        if (gird != null)
        {
            gird.ReduceNumber();
        }
    }

    /// <summary>
    /// 根据num获取多个某id的物品
    /// </summary>
    /// <param name="id"></param>
    /// <param name="num"></param>
    public void GetItemInNum(int id, int num)
    {
        for (int i = 0; i < num; i++)
        {
            GetItem(id);
        }
    }

    /// <summary>
    /// 根据num失去多个某id的物品
    /// </summary>
    /// <param name="id"></param>
    /// <param name="num"></param>
    public void LoseItemInNum(int id, int num)
    {
        for (int i = 0; i < num; i++)
        {
            LoseId(id);
        }
    }

    /// <summary>
    /// 获取背包中某id物品有多少个
    /// </summary>
    public int GetNumInItem(int id)
    {
        int num = 0;
        //查找是否存在该物品
        InventoryItemGird gird = null;
        foreach (InventoryItemGird temp in itemGirdList)
        {
            if (temp.id == id)
            {
                gird = temp;
                break;
            }
        }
        //若存在 num+1
        if (gird != null)
        {
            num = gird.num;
        }
        //不存在就会直接返回0
        return num;
    }

    /// <summary>
    /// 查找背包内有没有某id物品
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool GetItemInInventory(int id)
    {
        //查找是否存在该物品
        InventoryItemGird gird = null;
        foreach (InventoryItemGird temp in itemGirdList)
        {
            if (temp.id == id)
            {
                gird = temp;
                break;
            }
        }
        if (gird == null)
        {
            return false;
        }
        else
        {
            return true;
        } 
    }

}
