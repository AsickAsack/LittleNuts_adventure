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

    private void Awake()
    {
        switch(itemtype)
        {
            case ItemType.Weapon:
                if(GameData.Instance.playerdata.CurWeapon !=null)
                    CurSlotItem = GameData.Instance.playerdata.CurWeapon;
                break;
            case ItemType.Armor:
                if (GameData.Instance.playerdata.CurDefenceSphere != null)
                    CurSlotItem = GameData.Instance.playerdata.CurDefenceSphere;
                break;
            case ItemType.Shoes:
                if (GameData.Instance.playerdata.CurShoes != null)
                    CurSlotItem = GameData.Instance.playerdata.CurShoes;
                break;
        }

        if(!Icon.gameObject.activeSelf && CurSlotItem !=null)
        {
            Icon.gameObject.SetActive(true);
            Icon.sprite = CurSlotItem.itemdata.Image;
        }

    }



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
                GameData.Instance.playerdata.CurWeapon = slotmanager.CurSlot.myItem;
                break;
            case ItemType.Armor:
                GameData.Instance.playerdata.MaxHP += (int)slotmanager.CurSlot.myItem.itemdata.HP;
                GameData.Instance.playerdata.DEF += (int)slotmanager.CurSlot.myItem.itemdata.DEF;
                GameData.Instance.playerdata.CurDefenceSphere = slotmanager.CurSlot.myItem;
                break;
            case ItemType.Shoes:
                GameData.Instance.playerdata.MoveSpeed += (int)slotmanager.CurSlot.myItem.itemdata.Value;
                GameData.Instance.playerdata.CurShoes = slotmanager.CurSlot.myItem;
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
