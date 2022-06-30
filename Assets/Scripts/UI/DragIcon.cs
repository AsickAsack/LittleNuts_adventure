using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragIcon : MonoBehaviour, IDragHandler, IDropHandler, IEndDragHandler, IBeginDragHandler , IPointerDownHandler
{
    Transform myParent;
    GameObject myIcon;
    public Text ItemNameTx;
    public Text ItemDescriptionTx;
    public Image ItemImage;
    private static GameObject SelectImage = null;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject.GetComponent<DragIcon>() != null)
        {
            myIcon = eventData.pointerCurrentRaycast.gameObject;
            myParent = myIcon.transform.parent;
        }

        if (eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<ItemSlot>() != null &&
           eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<ItemSlot>().mydata != null)
        {
            ItemSlot tempSlotData = eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<ItemSlot>();
            GameObject myParent = tempSlotData.gameObject;
            
            ItemNameTx.text = tempSlotData.mydata.ItemName;
            ItemDescriptionTx.text = tempSlotData.mydata.Description;
            ItemImage.sprite = tempSlotData.mydata.Image;

            if(SelectImage == null)
            {
                SelectImage = myParent.GetComponent<ItemSlot>().SelectImage.gameObject;
                SelectImage.SetActive(true);
            }
            else
            {
                SelectImage.SetActive(false);
                SelectImage = myParent.GetComponent<ItemSlot>().SelectImage.gameObject;
                SelectImage.SetActive(true);
            }





            //버튼 해야함

        }

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject.GetComponent<DragIcon>() != null)
        {
            myIcon.transform.SetParent(myParent.parent.parent);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(myIcon != null)
        { 
            myIcon.transform.position = Input.mousePosition;
            myIcon.GetComponent<Image>().raycastTarget = false;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
       // throw new System.NotImplementedException();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject.GetComponent<DragIcon>() != null)
        {
            myIcon.transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent);
            eventData.pointerCurrentRaycast.gameObject.transform.SetParent(myParent);
            myIcon.GetComponent<Image>().raycastTarget = true;
            eventData.pointerCurrentRaycast.gameObject.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            myIcon.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            SwapItemSlot(myParent, myIcon.transform.parent);
            SelectImage.SetActive(false);
            SelectImage = myIcon.GetComponentInParent<ItemSlot>().SelectImage.gameObject;
            SelectImage.SetActive(true);

        }
        else
        { 
            myIcon.transform.SetParent(myParent);
            myIcon.GetComponent<Image>().raycastTarget = true;
            myIcon.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }
    }

 

    public void SwapItemSlot(Transform A , Transform B)
    {
        int intTemp = A.GetComponent<ItemSlot>().ItemCount;
        ItemData ItemDataTemp = A.GetComponent<ItemSlot>().mydata;
        A.GetComponent<ItemSlot>().ItemCount = B.GetComponent<ItemSlot>().ItemCount;
        A.GetComponent<ItemSlot>().mydata = B.GetComponent<ItemSlot>().mydata;
        B.GetComponent<ItemSlot>().ItemCount = intTemp;
        B.GetComponent<ItemSlot>().mydata = ItemDataTemp;

        if (A.GetComponent<ItemSlot>().ItemCount != 0)
        { 
            A.GetComponent<ItemSlot>().MyText.gameObject.SetActive(true);
            A.GetComponent<ItemSlot>().MyText.text = A.GetComponent<ItemSlot>().ItemCount.ToString();
        }
        else
        {
            A.GetComponent<ItemSlot>().MyText.gameObject.SetActive(false);
        }

        if (B.GetComponent<ItemSlot>().ItemCount != 0)
        {
            B.GetComponent<ItemSlot>().MyText.gameObject.SetActive(true);
            B.GetComponent<ItemSlot>().MyText.text = B.GetComponent<ItemSlot>().ItemCount.ToString();
        }
        else

        {
            B.GetComponent<ItemSlot>().MyText.gameObject.SetActive(false);
        }
        
    }
}
