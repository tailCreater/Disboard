using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortCutGird : MonoBehaviour
{
    public KeyCode keyCode;
    private ShortCutType type = ShortCutType.None;
    private UISprite icon;
    public int id;
    public int num;
    private UILabel itemNumLabel;

    private ObjectInfo objectInfo;
    private static PlayerStatus player;

    void Start()
    {
        icon = transform.Find("icon").GetComponent<UISprite>();
        icon.gameObject.SetActive(false);
        itemNumLabel = transform.Find("ItemLabel").GetComponent<UILabel>();
        itemNumLabel.gameObject.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(keyCode))
        {
            if(type == ShortCutType.Drug)
            {
                OnDrugUse();
                if(num == 0)
                {
                    ClearInfo();
                }
            }
        }
    }

    public void SetItemInShourtCut(int id,int num)
    {
        this.id = id;
        this.num = num;
        objectInfo = ObjectsInfo.instance.GetObjectInfoById(id);
        if(objectInfo.type == ObjectType.Drug)
        {
            icon.gameObject.SetActive(true);
            icon.spriteName = objectInfo.iconName;
            itemNumLabel.gameObject.SetActive(true);
            itemNumLabel.text = num.ToString();
            type = ShortCutType.Drug;
        }
    }

    public void OnDrugUse()
    {
        //如果物品栏有该药品
        if (Inventory.instance.GetNumInItem(id) > 0)
        {
            //使用物品，物品在物品栏的数量减一
            Inventory.instance.LoseItemInNum(id, 1);
            if(id == 1001)
            {
                player.Recover(50,0);
            }
            if (id == 1002)
            {
                player.Recover(0, 10);
            }
            this.num = Inventory.instance.GetNumInItem(id);
            itemNumLabel.text = num.ToString();
        }
        else
        {
 
        }
    }

    public void ClearInfo()
    {
        id = 0;
        icon.gameObject.SetActive(false);
        num = 0;
        itemNumLabel.gameObject.SetActive(false);
    }

}


public enum ShortCutType { 
    Drug,
    None
}
