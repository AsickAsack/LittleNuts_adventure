using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour 
{
    public GameObject[] itemSlot = null;
    public Text ItemNameTx;
    public Text ItemDescriptionTx;
    public Image ItemImage;
    public GameObject UseButton;
    public GameObject QuickButton;
    public Text GoldTx;


    public void SetInventory()
    {
        GoldTx.text = GameData.Instance.playerdata.money.ToString("N0");

        for (int i = 0; i < GameData.Instance.playerdata.myItems.Count; i++)
        {
            ItemData MyIventoryData = GameData.Instance.playerdata.myItems[i].itemdata;

            for (int j = 0; j < itemSlot.Length; j++)
            {

                Image IconImage = itemSlot[j].GetComponent<ItemSlot>().MyIcon;

                if (itemSlot[j].GetComponent<ItemSlot>().mydata == null)
                {

                    itemSlot[j].GetComponent<ItemSlot>().mydata = MyIventoryData;


                    IconImage.color = new Color(IconImage.color.r, IconImage.color.g, IconImage.color.b, 1);
                    IconImage.sprite = MyIventoryData.Image;
                    itemSlot[j].GetComponent<ItemSlot>().ItemCount++;
                    itemSlot[j].GetComponent<ItemSlot>().MyText.gameObject.SetActive(true);
                    itemSlot[j].GetComponent<ItemSlot>().MyText.GetComponent<Text>().text = itemSlot[j].GetComponent<ItemSlot>().ItemCount.ToString();
                    break;
                }
                else
                {
                    if (itemSlot[j].GetComponent<ItemSlot>().mydata == MyIventoryData)
                    {
                        itemSlot[j].GetComponent<ItemSlot>().ItemCount++;
                        itemSlot[j].GetComponent<ItemSlot>().MyText.GetComponent<Text>().text = itemSlot[j].GetComponent<ItemSlot>().ItemCount.ToString();
                        break;
                    }
                }
            }

            UseButton.SetActive(false);
            QuickButton.SetActive(false);


        }

        if (GameData.Instance.playerdata.myItems.Count != 0)
        {

            DragIcon.SelectImage = itemSlot[0].GetComponent<ItemSlot>().SelectImage;
            DragIcon.SelectImage.SetActive(true);
            ItemNameTx.gameObject.SetActive(true);
            ItemDescriptionTx.gameObject.SetActive(true);
            ItemImage.gameObject.SetActive(true);
            ItemNameTx.text = itemSlot[0].GetComponent<ItemSlot>()?.mydata.ItemName;
            ItemDescriptionTx.text = itemSlot[0].GetComponent<ItemSlot>()?.mydata.Description;
            ItemImage.sprite = itemSlot[0].GetComponent<ItemSlot>()?.mydata.Image;
        }
        else
        {
            ItemNameTx.gameObject.SetActive(false);
            ItemDescriptionTx.gameObject.SetActive(false);
            ItemImage.gameObject.SetActive(false);
        }    
    }

    
    public void Exitiventory()
    {
        for (int j = 0; j < itemSlot.Length; j++)
        {
            itemSlot[j].GetComponent<ItemSlot>().ItemCount = 0;
        }
    }



}
