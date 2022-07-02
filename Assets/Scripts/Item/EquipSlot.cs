using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EquipSlot : MonoBehaviour , IDropHandler
{
    public Image Icon;
    public SlotManager slotmanager;
    public ItemType itemtype;
    private Item CurSlotItem;

    public void OnDrop(PointerEventData eventData)
    {
        if (slotmanager.CurSlot != null)
        {
            if(slotmanager.CurSlot.myItem.itemdata.myType == itemtype)
            {
                Debug.Log(GameData.Instance.playerdata.ATK);

                if(CurSlotItem != null)
                {
                    UnEquipItem();
                    EquipItem();
                }
                else
                {
                    EquipItem();
                }

                Debug.Log(GameData.Instance.playerdata.ATK);
                Icon.gameObject.SetActive(true);
                Icon.sprite = slotmanager.CurSlot.myItem.itemdata.Image;
                CurSlotItem = slotmanager.CurSlot.myItem;



            }

        }
    }


    void EquipItem()
    {
        switch (itemtype)
        {
            case ItemType.Weapon:
                GameData.Instance.playerdata.ATK += (int)slotmanager.CurSlot.myItem.itemdata.ATK;
                break;
            case ItemType.Armor:
                GameData.Instance.playerdata.MaxHP += (int)slotmanager.CurSlot.myItem.itemdata.HP;
                GameData.Instance.playerdata.DEF += (int)slotmanager.CurSlot.myItem.itemdata.DEF;
                break;
            case ItemType.Shoes:
                GameData.Instance.playerdata.MoveSpeed += (int)slotmanager.CurSlot.myItem.itemdata.Value;
                break;
        }

    }

    void UnEquipItem()
    {
        switch (itemtype)
        {
            case ItemType.Weapon:
                GameData.Instance.playerdata.ATK -= (int)CurSlotItem.itemdata.ATK;
                break;
            case ItemType.Armor:
                GameData.Instance.playerdata.MaxHP -= (int)CurSlotItem.itemdata.HP;
                GameData.Instance.playerdata.DEF -= (int)CurSlotItem.itemdata.DEF;
                break;
            case ItemType.Shoes:
                GameData.Instance.playerdata.MoveSpeed -= (int)CurSlotItem.itemdata.Value;
                break;
        }
    }
}
