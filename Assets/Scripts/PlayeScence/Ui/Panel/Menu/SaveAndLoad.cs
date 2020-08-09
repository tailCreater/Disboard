using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;




public class SaveAndLoad : MonoBehaviour
{
    [Serializable]
    public class SaveDataPlayer
    {
        public string name;
        public int hp;
        public float hpNow;
        public int mp;
        public float mpNow;
        public int coin;
        public int level;
        public int monsterNumKill;
        public int powerPoint;
        public int powerPlusPoint;
        public int endurancePoint;
        public int endurancePlusPoint;
        public int strengthPoint;
        public int strengthPlusPoint;
        public float expNow;
        public int pointRemain;
        public int taskCode;
        public bool isDuringTask;
        //public int id;
        //public int num;
        public string itemId;
        public string itemNum;

        public List<int> Ints = new List<int>()
        {
            1,
            2,
            3,
            4,
            5,
            6,
            7,
            8,
            9,
            10,
            11,
            12,
            13,
            14,
            15,
            16,
            17,
            18,
            19,
            20
        };
    }



    public GameObject playerPrefab;
    public SaveDataPlayer t1;
    public string jsonName = "SaveData.json";
    private string dirpath;
    public string path;

    void Awake()
    {
        GameObject player = null;
        player = GameObject.Instantiate(playerPrefab) as GameObject;
    }

    void Start()
    {
        dirpath = Application.streamingAssetsPath + "/";
        path = dirpath + jsonName;
        //建立新存档
        if (StartMenuButton.instance.createNew)
        {
            if (!Directory.Exists(dirpath))
            {
                Directory.CreateDirectory(dirpath);
            }
            string name = PlayerPrefs.GetString("name");
            PlayerStatus.instance.name = name;
            StartMenuButton.instance.createNew = false;
            Save();
        }
        //读档
        else
        {
            if (!Directory.Exists(dirpath))
            {
                print("nofile");
            }
            else 
            {
                t1 = LoadDataFromJson(path);
                PlayerStatus.instance.name = t1.name;
                PlayerStatus.instance.hp = t1.hp;
                PlayerStatus.instance.hpNow = t1.hpNow;
                PlayerStatus.instance.mp = t1.mp;
                PlayerStatus.instance.mpNow = t1.mpNow;
                PlayerStatus.instance.coin = t1.coin;
                PlayerStatus.instance.level = t1.level;
                PlayerStatus.instance.monsterNumKill = t1.monsterNumKill;
                PlayerStatus.instance.powerPoint = t1.powerPoint;
                PlayerStatus.instance.powerPlusPoint = t1.powerPlusPoint;
                PlayerStatus.instance.endurancePoint = t1.endurancePoint;
                PlayerStatus.instance.endurancePlusPoint = t1.endurancePlusPoint;
                PlayerStatus.instance.strengthPoint = t1.strengthPoint;
                PlayerStatus.instance.strengthPlusPoint = t1.strengthPlusPoint;
                PlayerStatus.instance.expNow = t1.expNow;
                PlayerStatus.instance.pointRemain = t1.pointRemain;
                PlayerStatus.instance.taskCode = t1.taskCode;
                PlayerStatus.instance.isDuringTask = t1.isDuringTask;
                //拆分
                string[] strItemId = t1.itemId.Split(',');
                string[] strItemNum = t1.itemNum.Split(',');
                int i = 1;
                foreach (InventoryItemGird temp in Inventory.instance.itemGirdList)
                {
                    Inventory.instance.GetItemInNum(int.Parse(strItemId[i]), int.Parse(strItemNum[i]));
                    i++;
                }
                
            }
        }
        ExpBar.instance.UpdateShow();
        //SaveDataToJson(path ,t1);
    }


    public void Save()
    {
        t1.itemId = null;
        t1.itemNum = null;
        //存储玩家角色信息
        t1.name = PlayerStatus.instance.name;
        t1.hp = PlayerStatus.instance.hp;
        t1.hpNow = PlayerStatus.instance.hpNow;
        t1.mp = PlayerStatus.instance.mp;
        t1.mpNow = PlayerStatus.instance.mpNow;
        t1.coin = PlayerStatus.instance.coin;
        t1.level = PlayerStatus.instance.level;
        t1.monsterNumKill = PlayerStatus.instance.monsterNumKill;
        t1.powerPoint = PlayerStatus.instance.powerPoint;
        t1.powerPlusPoint = PlayerStatus.instance.powerPlusPoint;
        t1.endurancePoint = PlayerStatus.instance.endurancePoint;
        t1.endurancePlusPoint = PlayerStatus.instance.endurancePlusPoint;
        t1.strengthPoint = PlayerStatus.instance.strengthPoint;
        t1.strengthPlusPoint = PlayerStatus.instance.strengthPlusPoint;
        t1.expNow = PlayerStatus.instance.expNow;
        t1.pointRemain = PlayerStatus.instance.pointRemain;
        t1.taskCode = PlayerStatus.instance.taskCode;
        t1.isDuringTask = PlayerStatus.instance.isDuringTask;
        foreach (InventoryItemGird temp in Inventory.instance.itemGirdList)
        {
            t1.itemId = t1.itemId + "," + temp.id;
            t1.itemNum = t1.itemNum + "," + Inventory.instance.GetNumInItem(temp.id);
        }
        SaveDataToJson(path, t1);
    }

    /// <summary>
    /// 从json里面读取数据
    /// </summary>
    private SaveDataPlayer LoadDataFromJson(string p)
    {
        if (File.Exists(p))
        {
            string json = File.ReadAllText(p);
            return JsonUtility.FromJson<SaveDataPlayer>(json);
        }
        else
        {
            SaveDataPlayer a = new SaveDataPlayer();
            a.name = PlayerStatus.instance.name;
            a.hp = PlayerStatus.instance.hp;
            a.hpNow = PlayerStatus.instance.hpNow;
            a.mp = PlayerStatus.instance.mp;
            a.mpNow = PlayerStatus.instance.mpNow;
            a.coin = PlayerStatus.instance.coin;
            a.level = PlayerStatus.instance.level;
            a.monsterNumKill = PlayerStatus.instance.monsterNumKill;
            a.powerPoint = PlayerStatus.instance.powerPoint;
            a.powerPlusPoint = PlayerStatus.instance.powerPlusPoint;
            a.endurancePoint = PlayerStatus.instance.endurancePoint;
            a.endurancePlusPoint = PlayerStatus.instance.endurancePlusPoint;
            a.strengthPoint = PlayerStatus.instance.strengthPoint;
            a.strengthPlusPoint = PlayerStatus.instance.strengthPlusPoint;
            a.expNow = PlayerStatus.instance.expNow;
            a.pointRemain = PlayerStatus.instance.pointRemain;
            a.taskCode = PlayerStatus.instance.taskCode;
            a.isDuringTask = PlayerStatus.instance.isDuringTask;
            foreach (InventoryItemGird temp in Inventory.instance.itemGirdList)
            {
                a.itemId = a.itemId + "," + temp.id;
                a.itemNum = a.itemNum + "," + Inventory.instance.GetNumInItem(temp.id);
            }  
            return a;
        }
    }

    /// <summary>
    /// 储存数据到json
    /// </summary>
    public void SaveDataToJson(string path, SaveDataPlayer saveData)
    {
        string json = JsonUtility.ToJson(saveData, true);
        StreamWriter sw = File.CreateText(path);
        sw.Close();
        File.WriteAllText(path, json);
        print("SaveComplete");
    }
}
