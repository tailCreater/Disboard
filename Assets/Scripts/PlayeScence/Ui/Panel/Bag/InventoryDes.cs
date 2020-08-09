using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDes : MonoBehaviour
{
    public static InventoryDes instance;
    private UILabel inventoryDes;
    private float timer = 0;

    void Awake()
    {
        instance = this;
        inventoryDes = this.GetComponentInChildren<UILabel>();
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //如果鼠标一直指着timer就一直不会小于0.1。若鼠标离开timer立刻小于0
        if(this.gameObject.activeInHierarchy == true)
        {
            timer -= Time.deltaTime;
        }
        if(timer <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void Show(int id)
    {
        this.gameObject.SetActive(true);
        timer = 0.1f;
        transform.position =  UICamera.currentCamera.ScreenToWorldPoint(Input.mousePosition);
        ObjectInfo info = ObjectsInfo.instance.GetObjectInfoById(id);
        string des = "";
        switch (info.type) { 
            case ObjectType.Drug:
                des = GetDrugDes(info);
                break;
            case ObjectType.Equipment:
                des = GetEquip(info);
                break;
            case ObjectType.Mat:
                des = GetMatDes(info);
                break;
        }
        inventoryDes.text = des;
    }


    string GetDrugDes(ObjectInfo info)
    {
        string str = "";
        str += "名称: " + info.name + "\n";
        str += "回复HP: " + info.hp + "\n";
        str += "回复MP: " + info.mp + "\n";
        str += "出售价: " + info.priceSell + "\n";
        str += "购买价: " + info.priceBuy + "\n";
        return str;
    }

    string GetEquip(ObjectInfo info)
    {
        string str = "";
        str += "名称: " + info.name + "\n";
        str += "增加攻击: " + info.attack + "\n";
        str += "增加防御: " + info.def + "\n";
        switch (info.dressType) { 
            case DressType.Head:
                str += "穿戴类型: 头部\n";
                break;
            case DressType.Leg:
                str += "穿戴类型: 腿部\n";
                break;
            case DressType.Armor:
                str += "穿戴类型: 胸部\n";
                break;
            case DressType.LeftHand:
                str += "穿戴类型: 左手部\n";
                break;
            case DressType.RightHand:
                str += "穿戴类型: 右手部\n";
                break;
        }
        str += "出售价: " + info.priceSell + "\n";
        str += "购买价: " + info.priceBuy + "\n";
        return str;
    }

    string GetMatDes(ObjectInfo info)
    {
        string str = "";
        str += "名称: " + info.name + "\n";
        if(info.id == 3001)
        {
            str += "用处:用于打造装备\n";
            str += "\n可以通过和克劳德交换获取金币";
        }
        if (info.id == 3002)
        {
            str += "\n用处:进城需要的通行证\n";
        }
        
        return str;
    }

}
