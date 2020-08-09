using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : UIDragDropItem
{
    private UISprite sprite;
    private bool isHover = false;
    public int id;
    public bool inBag = true;

    void Awake()
    {
        base.Awake();
        sprite = this.GetComponent<UISprite>();
    }


    void Update()
    {
        base.Update();
        if(isHover)
        {
            InventoryDes.instance.Show(id);
            if(Input.GetMouseButtonDown(1))
            {
                ObjectInfo info = ObjectsInfo.instance.GetObjectInfoById(id);
                //如果是右键背包栏内的物品
                if (inBag)
                {
                    //如果右击还没穿戴的装备类型
                    if (this.GetComponent<EquipmentItem>().isDressing != true)
                    {
                        //先判断是否可以穿戴
                        bool canDress = EquipmentUi.instance.DressIn(id);
                        //可穿戴
                        if (canDress)
                        {
                            //穿戴上去背包栏此物品数量减一
                            transform.parent.GetComponent<InventoryItemGird>().ReduceNumber();
                            DressUpEquipment(info.id);
                        }
                    }
                    else
                    {
                    }
                }
                //右键装备栏上的装备
                else
                {
                    //判断物品在装备栏上还是在背包上
                    //如果右击已经穿上的装备就卸下装备
                    if (this.GetComponent<EquipmentItem>().isDressing == true)
                    {
                        Inventory.instance.GetItemInNum(info.id, 1);
                        DressOffEquipment(info.id);
                        this.GetComponent<EquipmentItem>().isDressing = false;
                        GameObject.Destroy(this.gameObject);
                    }
                }          
            }
        }
    }

    /// <summary>
    /// 物品拖拽功能
    /// </summary>
    /// <param name="surface"></param>
    protected override void OnDragDropRelease(GameObject surface)
    {
        base.OnDragDropRelease(surface);
        if (surface != null)
        {
            if(surface.tag == Tags.InventoryItemGird) //拖到空格子上
            {
                if (surface == this.transform.parent.gameObject) //拖到原来格子上
                {

                }
                else 
                {         
                    //先得到旧格子
                    InventoryItemGird oldParent = this.transform.parent.GetComponent<InventoryItemGird>();
                    //把物品的父类从旧格子换到新格子
                    this.transform.parent = surface.transform;
                    //得到新格子
                    InventoryItemGird newParent = this.transform.parent.GetComponent<InventoryItemGird>();
                    //将旧格子的id和num赋值给新格子
                    newParent.SetId(oldParent.id,oldParent.num);
                    //旧格子id和num清空
                    oldParent.ClearInfo();             
                }
            }
            //拖到有物体的格子上
            else if (surface.tag == Tags.InventoryItem)
            {
                //只需要将两个格子的信息交换再更新显示
                InventoryItemGird gird1 = this.transform.parent.GetComponent<InventoryItemGird>();
                InventoryItemGird gird2 = surface.transform.parent.GetComponent<InventoryItemGird>();
                int id = gird1.id;
                int num = gird1.num;
                gird1.SetId(gird2.id,gird2.num);
                gird2.SetId(id, num);
            }
            //拖到快捷方式里面
            else if(surface.tag == Tags.ShortCut)
            {
                print("ok");
                surface.GetComponent<ShortCutGird>().SetItemInShourtCut(id,Inventory.instance.GetNumInItem(id));
            }
        }
        ResetPosition();
    }

    /// <summary>
    /// 确保物品放在格子中间
    /// </summary>
    void ResetPosition()
    {
        transform.localPosition = Vector3.zero;
    }

    /// <summary>
    /// 根据id显示对应的贴图
    /// </summary>
    public void SetId(int id)
    {
        ObjectInfo info = ObjectsInfo.instance.GetObjectInfoById(id);
        sprite.spriteName = info.iconName;
    }

    public void SetIconName(int id ,string iconName)
    {
        sprite.spriteName = iconName;
        this.id = id;
    }

    public void OnHoverEnter()
    {
        isHover = true;
    }
    public void OnHoverOut()
    {
        isHover = false;
    }

    /// <summary>
    /// 穿上装备
    /// </summary>
    /// <param name="id"></param>
    private void DressUpEquipment(int id)
    {
        ObjectInfo info = ObjectsInfo.instance.GetObjectInfoById(id);
        PlayerStatus.instance.attack += info.attack;
        PlayerStatus.instance.def += info.def;
        StatusView.instance.ShowStatus();
    }

    /// <summary>
    /// 卸下装备
    /// </summary>
    /// <param name="id"></param>
    public void DressOffEquipment(int id)
    {
        ObjectInfo info = ObjectsInfo.instance.GetObjectInfoById(id);
        PlayerStatus.instance.attack -= info.attack;
        PlayerStatus.instance.def -= info.def;
        StatusView.instance.ShowStatus();
    }
}
