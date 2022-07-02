using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class QuickSlot : MonoBehaviour,IPointerUpHandler
{
    public Item myItem;
    public Image Icon;
    public Text ItemCount;

    public void OnPointerUp(PointerEventData eventData)
    {
        if(myItem != null)
        { 
            myItem.itemdata.UseItem();
            if(myItem.itemCount == 0)
            {
                Icon.gameObject.SetActive(false);
                ItemCount.gameObject.SetActive(false);
                myItem = null;
            }
            else
            { 
                ItemCount.text = myItem.itemCount.ToString();
            }

        }
    }
}
