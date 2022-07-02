using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour ,IDragHandler,IEndDragHandler,IDropHandler,IBeginDragHandler
{
    public Image Icon;
    public Text itemCountText;
    public GameObject SelectImage;
    public int MyIndex;
    public Image DragImage;
    public SlotManager slotmanager;
    public static ItemSlot CurSlot = null;
    public Item myItem = null;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject.GetComponent<ItemSlot>().Icon.gameObject.activeSelf && eventData.pointerCurrentRaycast.gameObject.GetComponent<ItemSlot>() != null)
        {

            CurSlot = eventData.pointerCurrentRaycast.gameObject.GetComponent<ItemSlot>();

            DragImage.sprite = CurSlot.myItem.itemdata.Image;
            DragImage.gameObject.SetActive(true);
            DragImage.transform.position = eventData.position;


        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(DragImage.gameObject.activeSelf)
        DragImage.transform.position = eventData.position;
    }

    public void OnDrop(PointerEventData eventData)
    {
        //if (eventData.pointerCurrentRaycast.gameObject.GetComponent<ItemSlot>() != null && CurSlot != null)
        //{

        //    int SwapA = CurSlot.MyIndex;
        //    int SwapB = eventData.pointerCurrentRaycast.gameObject.GetComponent<ItemSlot>().MyIndex;



      

        //    slotmanager.ChangeType((int)slotmanager.menutype);

        //    slotmanager.SelectImage.SetActive(false);
        //    slotmanager.SelectImage = slotmanager.mySlots[SwapB].SelectImage;
        //    slotmanager.SelectImage.SetActive(true);
        //}
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        DragImage.gameObject.SetActive(false);
        CurSlot = null;
    }

  



}

   


