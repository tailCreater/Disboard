using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentItem : MonoBehaviour
{
    private UISprite sprite;
    public int id;
    public bool isDressing = false;

    void Awake()
    {
        sprite = this.GetComponent<UISprite>();
    }


    public void SetId(int id)
    {
        this.id = id;
        ObjectInfo info = ObjectsInfo.instance.GetObjectInfoById(id);
        SetInfo(info);
    }

    public void SetInfo(ObjectInfo info)
    {
        this.id = info.id;
        sprite.spriteName = info.iconName;
    }
}
