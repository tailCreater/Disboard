using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentUi : MonoBehaviour
{
    public static EquipmentUi instance;

    private GameObject sword;
    private GameObject shield;
    private GameObject helmets;
    private GameObject armor;
    private GameObject pants;
    public GameObject equipmentItem;
    public bool helmetsDress = false;
    public bool pantsDress = false;
    public bool armorDress = false;
    public bool shieldDress = false;
    public bool swordDress = false;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        sword = GameObject.Find("Equipment_Sword").gameObject;
        shield = GameObject.Find("Equipment_Shield").gameObject;
        helmets = GameObject.Find("Equipment_Helmets").gameObject;
        armor = GameObject.Find("Equipment_Armor").gameObject;
        pants = GameObject.Find("Equipment_Pants").gameObject;
    }


    public bool DressIn(int id)
    {
        ObjectInfo info = ObjectsInfo.instance.GetObjectInfoById(id);
        
        if (info.type != ObjectType.Equipment) //不是装备无法穿戴
        {
            return false;
        }
        GameObject parent = null;
        switch (info.dressType) {
            case DressType.Head:
                parent = helmets;
                break;
            case DressType.Leg:
                parent = pants;
                break;
            case DressType.Armor:
                parent = armor;
                break;
            case DressType.LeftHand:
                parent = shield;
                break;
            case DressType.RightHand:
                parent = sword;
                break;
        }
        EquipmentItem item = parent.GetComponentInChildren<EquipmentItem>();
        //已经穿戴同类型的装备
        if (item != null)
        {
            //同类型装备互换
            return false;
        }
        //没有穿戴同类型的装备
        else
        {
            GameObject itemGo =  NGUITools.AddChild(parent, equipmentItem);
            itemGo.GetComponent<InventoryItem>().id = info.id;
            itemGo.transform.localPosition = Vector3.zero;
            itemGo.GetComponent<EquipmentItem>().SetInfo(info);
            itemGo.GetComponent<EquipmentItem>().isDressing = true;
            itemGo.GetComponent<InventoryItem>().inBag = false;
            return true;
        }
        
    }
}
