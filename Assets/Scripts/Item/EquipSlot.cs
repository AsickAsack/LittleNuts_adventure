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

    public void OnDrop(PointerEventData eventData)
    {
        if (slotmanager.CurSlot != null)
        {
            if(slotmanager.CurSlot.myItem.itemdata.myType == itemtype)
            {
                Icon.gameObject.SetActive(true);
                Icon.sprite = slotmanager.CurSlot.myItem.itemdata.Image;

            }

        }
    }
}
