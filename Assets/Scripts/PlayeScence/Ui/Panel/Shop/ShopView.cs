using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShopView : MonoBehaviour
{
    public static ShopView instance;

    private int hpNumBuyBuyOrSell = 0;
    private int mpNumBuyBuyOrSell = 0;
    private int helmetNumBuyOrSell = 0;
    private int armorNumBuyOrSell = 0;
    private int pantNumBuyOrSell = 0;
    private int shieldNumBuyOrSell = 0;
    private int price = 0;

    private UILabel hpNum;
    private UILabel mpNum;
    private UILabel helmetNum;
    private UILabel armorNum;
    private UILabel pantNum;
    private UILabel shieldNum;
    private UILabel priceBuy;

    private GameObject buyButton;
    private GameObject sellButton;
    private GameObject priceView;

    public bool shopIsOpen = false;
    public bool isBuy = false;
    public bool isSell = false;

    //设置更改窗容口的大小
    private Vector3 min = new Vector3(0, 0, 0);
    //设置更改窗容口的大小
    private Vector3 max = new Vector3(1, 1, 1);
    //设置变化时间
    private float time = 0.5f;

    void Awake()
    {
        instance = this;
        hpNum = GameObject.Find("HpNum").GetComponent<UILabel>();
        mpNum = GameObject.Find("MpNum").GetComponent<UILabel>();
        helmetNum = GameObject.Find("HelmetNum").GetComponent<UILabel>();
        armorNum = GameObject.Find("ArmorNum").GetComponent<UILabel>();
        pantNum = GameObject.Find("PantNum").GetComponent<UILabel>();
        shieldNum = GameObject.Find("ShieldNum").GetComponent<UILabel>();
        priceBuy = GameObject.Find("PriceBuyOrSell").GetComponent<UILabel>();
        buyButton = GameObject.Find("Buy");
        sellButton = GameObject.Find("Sell");
        priceView = GameObject.Find("DetermineButton");
        priceView.gameObject.SetActive(false);
    }



    public void OnBuyButtonClick()
    {
        buyButton.gameObject.SetActive(false);
        sellButton.gameObject.SetActive(false);
        priceView.gameObject.SetActive(true);
        ShowNum();
        isBuy = true;
    }

    public void OnSellButtonClick()
    {
        buyButton.gameObject.SetActive(false);
        sellButton.gameObject.SetActive(false);
        priceView.gameObject.SetActive(true);
        ShowNum();
        isSell = true;
    }

    public void ShowShopPanel() 
    {       
        Transform shop = GameObject.Find("ShopPanel").transform;
        if (shopIsOpen == false)
        {
            shop.DOScale(max, time);
            shopIsOpen = true;
            Talk.instance.CloseTalkPanel();
        }
    }

    public void CloseShopPanel()
    {
        Transform shop = GameObject.Find("ShopPanel").transform;
        shop.DOScale(min, time);
        ClearShopDate();
        buyButton.gameObject.SetActive(true);
        sellButton.gameObject.SetActive(true);
        priceView.gameObject.SetActive(false);
        isBuy = false;
        isSell = false;
        ShowNum();
        shopIsOpen = false;
    }

    /// <summary>
    /// 更新显示
    /// </summary>
    private void ShowNum()
    {
        if (isBuy)
        {
            price = hpNumBuyBuyOrSell * 50 + mpNumBuyBuyOrSell * 50 + helmetNumBuyOrSell * 500 + armorNumBuyOrSell * 500 + pantNumBuyOrSell * 500 + shieldNumBuyOrSell * 1000;
        }
        if(isSell)
        {
            price = hpNumBuyBuyOrSell * 20 + mpNumBuyBuyOrSell * 20 + helmetNumBuyOrSell * 100 + armorNumBuyOrSell * 100 + pantNumBuyOrSell * 100 + shieldNumBuyOrSell * 200;
        }
        
        hpNum.text = hpNumBuyBuyOrSell.ToString();
        mpNum.text = mpNumBuyBuyOrSell.ToString();
        helmetNum.text = helmetNumBuyOrSell.ToString();
        armorNum.text = armorNumBuyOrSell.ToString();
        pantNum.text = pantNumBuyOrSell.ToString();
        shieldNum.text = shieldNumBuyOrSell.ToString();
        priceBuy.text = price.ToString();
    }

    

    /// <summary>
    /// 购买或者售卖的最终确定
    /// </summary>
    public void OnDetermineButtonClick()
    {
        //购买
        if(isBuy && isSell == false)
        {
            if (PlayerStatus.instance.coin >= price)
            {
                PlayerStatus.instance.coin -= price;
                Inventory.instance.GetItemInNum(1001, hpNumBuyBuyOrSell);
                Inventory.instance.GetItemInNum(1002, mpNumBuyBuyOrSell);
                Inventory.instance.GetItemInNum(2003, helmetNumBuyOrSell);
                Inventory.instance.GetItemInNum(2004, armorNumBuyOrSell);
                Inventory.instance.GetItemInNum(2006, pantNumBuyOrSell);
                Inventory.instance.GetItemInNum(2002, shieldNumBuyOrSell);
            }
            //钱不够
            else
            {
                
            }
        }
        //售卖
        if(isSell && isBuy == false)
        {
            PlayerStatus.instance.coin += price;
            Inventory.instance.LoseItemInNum(1001, hpNumBuyBuyOrSell);
            Inventory.instance.LoseItemInNum(1002, mpNumBuyBuyOrSell);
            Inventory.instance.LoseItemInNum(2003, helmetNumBuyOrSell);
            Inventory.instance.LoseItemInNum(2004, armorNumBuyOrSell);
            Inventory.instance.LoseItemInNum(2006, pantNumBuyOrSell);
            Inventory.instance.LoseItemInNum(2002, shieldNumBuyOrSell);
        }
        ClearShopDate();
        buyButton.gameObject.SetActive(true);
        sellButton.gameObject.SetActive(true);
        priceView.gameObject.SetActive(false);
        isBuy = false;
        isSell = false;
        ShowNum();
    }


    /// <summary>
    /// 情况商店购买的所有物品数量与花费数据
    /// </summary>
    private void ClearShopDate()
    {
        hpNumBuyBuyOrSell = 0;
        mpNumBuyBuyOrSell = 0;
        helmetNumBuyOrSell = 0;
        armorNumBuyOrSell = 0;
        pantNumBuyOrSell = 0;
        shieldNumBuyOrSell = 0;
        price = 0;
    }

    /// <summary>
    /// 血瓶购买的点击控制数量
    /// </summary>
    public void OnHpNumPlusButtonClick()
    {
        if (isBuy)
        {
            hpNumBuyBuyOrSell += 1;
        }
        if(isSell)
        {
            if (Inventory.instance.GetNumInItem(1001) > hpNumBuyBuyOrSell)
            {
                hpNumBuyBuyOrSell += 1;
            }
        }
        ShowNum();
    }
    public void OnHpNumReduceButtonClick()
    {
        if(hpNumBuyBuyOrSell > 0)
        {
            hpNumBuyBuyOrSell -= 1;
        }
        ShowNum();
    }

    /// <summary>
    /// 蓝瓶购买的点击控制数量
    /// </summary>
    public void OnMpNumPlusButtonClick()
    {
        if (isBuy)
        {
            mpNumBuyBuyOrSell += 1;
        }
        if (isSell)
        {
            if (Inventory.instance.GetNumInItem(1002) > mpNumBuyBuyOrSell)
            {
                mpNumBuyBuyOrSell += 1;
            }
        }
        ShowNum();
    }
    public void OnMpNumReduceButtonClick()
    {
        if (mpNumBuyBuyOrSell > 0)
        {
            mpNumBuyBuyOrSell -= 1;
        }
        ShowNum();
    }

    /// <summary>
    /// 头盔购买的点击控制数量
    /// </summary>
    public void OnHelmetNumPlusButtonClick()
    {
        if (isBuy)
        {
            helmetNumBuyOrSell += 1;
        }
        if (isSell)
        {
            if (Inventory.instance.GetNumInItem(2003) > helmetNumBuyOrSell)
            {
                helmetNumBuyOrSell += 1;
            }
        }
        ShowNum();
    }
    public void OnHelmetNumReduceButtonClick()
    {
        if (helmetNumBuyOrSell > 0)
        {
            helmetNumBuyOrSell -= 1;
        }
        ShowNum();
    }

    /// <summary>
    /// 皮甲购买的点击控制数量
    /// </summary>
    public void OnArmorNumPlusButtonClick()
    {
        if (isBuy)
        {
            armorNumBuyOrSell += 1;
        }
        if (isSell)
        {
            if (Inventory.instance.GetNumInItem(2004) > armorNumBuyOrSell)
            {
                armorNumBuyOrSell += 1;
            }
        }
        ShowNum();
    }
    public void OnArmorNumReduceButtonClick()
    {
        if (armorNumBuyOrSell > 0)
        {
            armorNumBuyOrSell -= 1;
        }
        ShowNum();
    }

    /// <summary>
    /// 腿甲购买的点击控制数量
    /// </summary>
    public void OnPantNumPlusButtonClick()
    {
        if (isBuy)
        {
            pantNumBuyOrSell += 1;
        }
        if (isSell)
        {
            if (Inventory.instance.GetNumInItem(2006) > pantNumBuyOrSell)
            {
                pantNumBuyOrSell += 1;
            }
        }
        ShowNum();
    }
    public void OnPantNumReduceButtonClick()
    {
        if (pantNumBuyOrSell > 0)
        {
            pantNumBuyOrSell -= 1;
        }
        ShowNum();
    }

    /// <summary>
    /// 盾牌购买的点击控制数量
    /// </summary>
    public void OnShieldNumPlusButtonClick()
    {
        if (isBuy)
        {
            shieldNumBuyOrSell += 1;
        }
        if (isSell)
        {
            if (Inventory.instance.GetNumInItem(2002) > shieldNumBuyOrSell)
            {
                shieldNumBuyOrSell += 1;
            }
        }
        ShowNum();
    }
    public void OnShieldNumReduceButtonClick()
    {
        if (shieldNumBuyOrSell > 0)
        {
            shieldNumBuyOrSell -= 1;
        }
        ShowNum();
    }



}
