using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;

public class ObjectsInfo : MonoBehaviour
{

    public static ObjectsInfo instance;

    public TextAsset objectInfoListText;

    private Dictionary<int, ObjectInfo> objectInfoDict = new Dictionary<int, ObjectInfo>();

    void Awake()
    {
        instance = this;
        ReadInfo();
    }


    //通过id在字典内查找ObjectInfo
    public ObjectInfo GetObjectInfoById(int id)
    {
        ObjectInfo info = null;
        objectInfoDict.TryGetValue(id, out info);
        return info;
    }

    void ReadInfo()
    {
        //取到文本文件所有字符
        string text = objectInfoListText.text;

        //根据行拆分
        string[] strArray = text.Split('\n');

        foreach (string str in strArray)
        {
            string[] proArray = str.Split(',');
            ObjectInfo info = new ObjectInfo();
            int id = int.Parse(proArray[0]);
            string name = proArray[1];
            string iconName = proArray[2];
            string str_type = proArray[3];
            ObjectType type = ObjectType.Drug;
            switch (str_type) { 
                case "Drug":
                    type = ObjectType.Drug;
                    break;
                case"Equipment":
                    type = ObjectType.Equipment;
                    break;
                case"Mat":
                    type = ObjectType.Mat;
                    break;
            }
            info.id = id;
            info.name = name;
            info.iconName = iconName;
            info.type = type;
            if(type == ObjectType.Drug)
            {
                int hp = int.Parse(proArray[4]);
                int mp = int.Parse(proArray[5]);
                int priceSell = int.Parse(proArray[6]);
                int priceBuy = int.Parse(proArray[7]);
                info.hp = hp;
                info.mp = mp;
                info.priceSell = priceSell;
                info.priceBuy = priceBuy;
            }
            else if (type == ObjectType.Equipment)
            {
                info.attack  = int.Parse(proArray[4]);
                info.def  = int.Parse(proArray[5]);
                info.priceSell  = int.Parse(proArray[6]);
                info.priceBuy = int.Parse(proArray[7]);
                //string strDressType = proArray[8];
                switch (proArray[0].ToString())
                {
                    case "2001":
                        info.dressType = DressType.RightHand;
                        break;
                    case "2002":
                        info.dressType = DressType.LeftHand;
                        break;
                    case "2003":
                        info.dressType = DressType.Head;
                        break;
                    case "2004":
                        info.dressType = DressType.Armor;
                        break;
                    case "2005":
                        info.dressType = DressType.Armor;
                        break;
                    case "2006":
                        info.dressType = DressType.Leg;
                        break;
                }
            }
            else if (type == ObjectType.Mat)
            {
            }


            //通过id作为key将info存进字典内
            objectInfoDict.Add(id, info);
        }
    }

}



//id
//名称      
//icon名     
//类型                 
//加血量
//加魔法量         
//出售价      
//购买价格

public enum ObjectType { 
    Drug,
    Equipment,
    Mat
}

public enum DressType { 
    RightHand,
    LeftHand,
    Head,
    Armor,
    Leg
}

public class ObjectInfo
{
    public int id;
    public string name;
    public string iconName;
    public ObjectType type;
    public int hp;
    public int mp;  
    public int priceSell;
    public int priceBuy;

    public int attack;
    public int def;
    public DressType dressType;
}
