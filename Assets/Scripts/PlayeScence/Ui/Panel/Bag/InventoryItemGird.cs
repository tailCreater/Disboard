using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemGird : MonoBehaviour
{
    public int id = 0;
    private ObjectInfo info = null;
    public int num = 0;
    public UILabel numLabel;

    // Start is called before the first frame update
    void Awake()
    {
        numLabel = this.GetComponentInChildren<UILabel>();
    }


    public void SetId(int id, int num)
    {
        this.id = id;
        info = ObjectsInfo.instance.GetObjectInfoById(id);
        InventoryItem item = this.GetComponentInChildren<InventoryItem>();
        item.SetIconName(info.id,info.iconName);
        this.num = num;  //局部变量赋值给属性
        numLabel.enabled = true;
        numLabel.text = this.num.ToString();
    }

    /// <summary>
    /// 物品数量加一
    /// </summary>
    /// <param name="num"></param>
    public void PlusNumber(int num = 1)
    {
        this.num += num;
        numLabel.text = this.num.ToString();
    }

    /// <summary>
    /// 物品数量减一
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public bool ReduceNumber(int num = 1)
    {
        if (this.num >= num)
        {
            this.num -= num;
            numLabel.text = this.num.ToString();
            if (this.num == 0)
            {
                ClearInfo();
                GameObject.Destroy(this.GetComponentInChildren<InventoryItem>().gameObject);
            }
            return true;
        }
        return false;
    }

    /// <summary>
    /// 清空格子存的物品信息
    /// </summary>
    public void ClearInfo()
    {
        id = 0;
        info = null;
        num = 0;
        numLabel.enabled = false;
    }
   

}
