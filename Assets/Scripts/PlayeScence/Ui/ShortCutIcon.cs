using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortCutIcon : UIDragDropItem
{
    public int id;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //物品拖拽功能
    protected override void OnDragDropRelease(GameObject surface)
    {
        base.OnDragDropRelease(surface);
        if (surface != null)
        {
            if (surface.tag == Tags.ShortCut) //拖到空格子上
            {
                if (surface == this.transform.parent.gameObject) //拖到原来格子上
                {

                }
                else
                {
                    //先得到旧格子
                    ShortCutGird oldParent = this.transform.parent.GetComponent<ShortCutGird>();
                    //得到新格子
                    ShortCutGird newParent = surface.transform.GetComponent<ShortCutGird>();
                    //将旧格子的id和num赋值给新格子
                    newParent.SetItemInShourtCut(oldParent.id, oldParent.num);
                    //旧格子id和num清空
                    oldParent.ClearInfo();
                }
            }
            //拖到有物体的格子上
            else if (surface.tag == Tags.ShortCutIcon)
            {
                //只需要将两个格子的信息交换再更新显示
                ShortCutGird gird1 = this.transform.parent.GetComponent<ShortCutGird>();
                ShortCutGird gird2 = surface.transform.parent.GetComponent<ShortCutGird>();
                int id = gird1.id;
                int num = gird1.num;
                gird1.SetItemInShourtCut(gird2.id, gird2.num);
                gird2.SetItemInShourtCut(id, num);
            }
            else
            {
                ShortCutGird Gird = this.transform.parent.GetComponent<ShortCutGird>();
                //旧格子id和num清空
                Gird.ClearInfo();
            }
        }
        else
        {
            ShortCutGird Gird = this.transform.parent.GetComponent<ShortCutGird>();
            //旧格子id和num清空
            Gird.ClearInfo();
        }
        ResetPosition();
    }

    void ResetPosition()
    {
        transform.localPosition = Vector3.zero;
    }
}
