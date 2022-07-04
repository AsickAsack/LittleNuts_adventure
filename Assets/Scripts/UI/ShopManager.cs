using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public enum ShopType
{
    Sell,Buy

}

public class ShopManager : MonoBehaviour,  IPointerClickHandler
{
    [Header("[오른쪽 구매&판매 패널관련]")]
    public Image Icon;
    public Text Item_NameTx;
    public Text Item_DescriptionTx;
    public Text MyGold;
    public Text CountText;
    public Text CurCirGold;     
    public Button SellButton;
    public Button BuyButton;

    public ShopSellSlotManager shopSellSlotManager;

    [Header("[확인 패널]")]
    public Text Popup_Label;
    public Text Popup_detail;
    public GameObject[] Popup;

    BuySlot CurBuy_Item;
    GameObject SelectImage = null;
    ShopMyItemSlot CurSell_Item;
    ShopType shoptype = ShopType.Buy;

    //샵 열었을때 골드 세팅
    public void OpenShopSet()
    {
        MyGold.text = "보유 골드 " +GameData.Instance.playerdata.money.ToString("N0");
    }

    public void ExipShop()
    {
        SelectImage?.SetActive(false);
        SetAct(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //판매 클릭
        if (eventData.pointerCurrentRaycast.gameObject.GetComponent<ShopMyItemSlot>() != null)
        {

            CurBuy_Item = null;
            shoptype = ShopType.Sell;
            ShopMyItemSlot temp = eventData.pointerCurrentRaycast.gameObject.GetComponent<ShopMyItemSlot>();
            CurSell_Item = temp;
            SetAct(true);
            Icon.sprite = temp.myItem.itemdata.Image;
            Item_NameTx.text = temp.myItem.itemdata.ItemName;
            Item_DescriptionTx.text = temp.myItem.itemdata.Description;
            ChangeShop(false);
            CurCirGold.gameObject.SetActive(true);            
            CountText.text = "1";
            CirCulateGold();
            SelectImage?.SetActive(false);
            SelectImage = temp.SelectImage.gameObject;
            SelectImage.SetActive(true);
        }

        //구매 클릭
        if (eventData.pointerCurrentRaycast.gameObject.GetComponent<BuySlot>() != null)
        {
            CurSell_Item = null;
            shoptype = ShopType.Buy;
            BuySlot temp = eventData.pointerCurrentRaycast.gameObject.GetComponent<BuySlot>();
            CurBuy_Item = temp;
            SetAct(true);
            Icon.sprite = temp.buyItem.Image;
            Item_NameTx.text = temp.buyItem.ItemName;
            Item_DescriptionTx.text = temp.buyItem.Description;
            ChangeShop(true);
            CurCirGold.gameObject.SetActive(true);
            CountText.text = "1";
            CirCulateGold();
            SelectImage?.SetActive(false);
            SelectImage = temp.SelectImage.gameObject;
            SelectImage.SetActive(true);
        }
    }

    //설명 판 켜기
    public void SetAct(bool check)
    {
        Icon.gameObject.SetActive(check);
        Item_NameTx.gameObject.SetActive(check);
        Item_DescriptionTx.gameObject.SetActive(check);
    }


    //판매 -> 구매 혹은 그 반대로 바꿨을때
    public void ChangeShop(bool check)
    {
        BuyButton.gameObject.SetActive(check);
        SellButton.gameObject.SetActive(!check);

    }


    //수량 버튼 클릭
    public void ClickArrow(int index)
    {
        int count = int.Parse(CountText.text);

        switch (index)
        {
            //왼쪽 클릭
            case 0:
                if(count > 1)
                {
                    count--;
                    CountText.text = count.ToString();
                    CirCulateGold();
                }
                break;

            //오른쪽 클릭
            case 1:
                switch (shoptype)
                {
                    case ShopType.Sell:
                        if(CurSell_Item != null)
                        { 
                            if (CurSell_Item.myItem.itemCount >= count+1 )
                            {
                                count++;
                                CountText.text = count.ToString();
                                CirCulateGold();
                            }
                            else
                            {
                                //안됨
                            }
                        }
                        break;

                    case ShopType.Buy:
                        if (CurBuy_Item != null)
                        {
                            if (GameData.Instance.playerdata.money >= CurBuy_Item.buyItem.BuyPrice * (count+1))
                            {
                                count++;
                                CountText.text = count.ToString();
                                CirCulateGold();
                            }
                            else
                            {
                                //d안됨
                            }
                        }
                        break;
                }
                break;
        }

    }

    //누를때마다 돈 계산
    public void CirCulateGold()
    {
        switch (shoptype)
        {
            case ShopType.Sell:
                if(CurSell_Item !=null)
                {   
                    CurCirGold.text = (CurSell_Item.myItem.itemdata.SellPrice *int.Parse(CountText.text)).ToString("N0") +" 골드";
                }
                break;

            case ShopType.Buy:
                if(CurBuy_Item != null)
                {
                    if(GameData.Instance.playerdata.money >= CurBuy_Item.buyItem.BuyPrice)
                    CurCirGold.text = (CurBuy_Item.buyItem.BuyPrice * int.Parse(CountText.text)).ToString("N0") + " 골드";
                    else
                    {
                        CurCirGold.text = "돈이 부족합니다.";
                    }

                }
                break;

        }
    }

    //판매 로직
    public void SellBtn()
    {
       if (CurSell_Item != null)
        {
            GameData.Instance.playerdata.money += CurSell_Item.myItem.itemdata.SellPrice* int.Parse(CountText.text);

            if(CurSell_Item.myItem.itemdata.myType == ItemType.etc || CurSell_Item.myItem.itemdata.myType == ItemType.UseItem)
            {
                Item obj = GameData.Instance.playerdata.myItems.Find(x => x.itemdata == CurSell_Item.myItem.itemdata);
                obj.itemCount -= int.Parse(CountText.text);
                if (obj.itemCount <= 0)
                {
                    GameData.Instance.playerdata.myItems.Remove(obj);
                }
            }
            else
            {
                Item obj = GameData.Instance.playerdata.myItems.Find(x => x.UID == CurSell_Item.myItem.UID);
                GameData.Instance.playerdata.myItems.Remove(obj);
            }

            shopSellSlotManager.myItemList();
            MyGold.text = "보유 골드 " +GameData.Instance.playerdata.money.ToString("N0");

            if(CurSell_Item.myItem.itemCount <= 0)
            {
                CurSell_Item = null;
                SetAct(false);
                SelectImage?.SetActive(false);
            }
        }
    }

    //구매 팝업
    public void BuyPopup_set()
    {
        if (CurBuy_Item != null)
        {
            if (GameData.Instance.playerdata.money >= CurBuy_Item.buyItem.BuyPrice* int.Parse(CountText.text))
            {
                Popup_Label.text = "구매하기";
                Popup_detail.text = CurBuy_Item.buyItem.ItemName + "\n총 " + (CurBuy_Item.buyItem.BuyPrice * int.Parse(CountText.text)).ToString("N0") + "골드 입니다.\n 정말 구매 하시겠습니까?";
                Popup[0].SetActive(true);
                Popup[1].SetActive(true);
            }
        }
    }

    //판매 팝업 
    public void Sell_Popup_set()
    {
        if (CurSell_Item != null)
        {
            Popup_Label.text = "판매하기";
            Popup_detail.text = CurSell_Item.myItem.itemdata.ItemName + "\n총 " + (CurSell_Item.myItem.itemdata.SellPrice * int.Parse(CountText.text)).ToString("N0") + "골드 입니다.\n 정말 판매 하시겠습니까?";
            Popup[0].SetActive(true);
            Popup[1].SetActive(true);
        }
    }



    //마지막으로 구매 or 판매 확인버튼
    public void FinalConfirm()
    {
        switch(shoptype)
        {
            //구매
            case ShopType.Buy:
                BuyBtn();
                break;

            //판매
            case ShopType.Sell:
                SellBtn();
                break;
        }
    }



    // 구매 로직
    public void BuyBtn()
    {
        if(CurBuy_Item != null)
        {
            if (GameData.Instance.playerdata.money >= CurBuy_Item.buyItem.BuyPrice * int.Parse(CountText.text))
            {
                GameData.Instance.playerdata.money -= CurBuy_Item.buyItem.BuyPrice* int.Parse(CountText.text);

                if (CurBuy_Item.buyItem.myType == ItemType.etc || CurBuy_Item.buyItem.myType == ItemType.UseItem)
                {
                    Item obj = GameData.Instance.playerdata.myItems.Find(x => x.itemdata == CurBuy_Item.buyItem);
                    if (obj == null)
                    {
                        GameData.Instance.playerdata.myItems.Add(new Item(CurBuy_Item.buyItem, int.Parse(CountText.text)));
                    }
                    else
                    {
                        obj.itemCount += int.Parse(CountText.text);
                    }
                }
                else
                {
                    for(int i=0;i< int.Parse(CountText.text);i++)
                    GameData.Instance.playerdata.myItems.Add(new Item((uint)Random.Range(uint.MinValue, uint.MaxValue), CurBuy_Item.buyItem));
                }

                shopSellSlotManager.myItemList();
                MyGold.text = "보유 골드 "+GameData.Instance.playerdata.money.ToString("N0");
            }
            else
            {
                //돈없으면 out
            }

        }
    }

}
