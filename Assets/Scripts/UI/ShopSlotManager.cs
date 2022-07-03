using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSlotManager : MonoBehaviour
{

    public ShopMyItemSlot[] myItemSlots;


    private void Awake()
    {
        myItemSlots = this.GetComponentsInChildren<ShopMyItemSlot>();
        for (int j = 0; j < myItemSlots.Length; j++)
        {
            myItemSlots[j].gameObject.SetActive(false);
        }
    }

    //openShop


    //�� �κ��丮 ��� �ʱ�ȭ
    public void myItemList()
    {
        for(int i = 0; i<GameData.Instance.playerdata.myItems.Count; i++)
        {
            for(int j = 0; j<myItemSlots.Length;j++)
            { 
                if(!myItemSlots[j].gameObject.activeSelf)
                {   
                    Item Temp = GameData.Instance.playerdata.myItems[i];
                    ShopMyItemSlot myslot = myItemSlots[j].GetComponent<ShopMyItemSlot>();

                    myItemSlots[j].gameObject.SetActive(true);
                    myslot.myItem = Temp;
                    myslot.Icon.sprite = Temp.itemdata.Image;
                    myslot.ItemName.text = Temp.itemdata.ItemName;
                    myslot.ItemName.gameObject.SetActive(true);
                    myslot.Icon.gameObject.SetActive(true);
                    myslot.SellPrice.gameObject.SetActive(true);

                    if (Temp.itemdata.myType == ItemType.UseItem || Temp.itemdata.myType == ItemType.etc)
                        myItemSlots[j].GetComponent<ShopMyItemSlot>().SellPrice.text = Temp.itemdata.SellPrice.ToString() + "��\t" + Temp.itemCount + "�� ����";
                    else
                        myItemSlots[j].GetComponent<ShopMyItemSlot>().SellPrice.text = Temp.itemdata.SellPrice.ToString() + "��";

                    break;

                }
            }
        }
    }


}
