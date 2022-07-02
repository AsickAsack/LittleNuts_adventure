using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum MenuType
{
    Weapon=0,Use,Etc
}


public class SlotManager : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    public ItemSlot[] mySlots;
    public GameObject SelectImage;
    public ItemSlot CurSlot;
    private GameObject SelectType;
    public Image DesImage;
    public Text ItemNameText;
    public Text ItemDesText;
    public Text GodlText;
    public GameObject[] MenuSelectObject;
    private GameObject MenuSelectImage;
    public GameObject[] QuickSlot;
    public GameObject[] CheckQuickSlotPopup;

    public MenuType menutype = MenuType.Weapon;

    private void Awake()
    {
        mySlots = this.GetComponentsInChildren<ItemSlot>();
        InitIndex();
        SelectImage = mySlots[0].SelectImage;
    }

    public void InitIndex()
    {
        for (int i = 0; i < mySlots.Length; i++)
        {
            mySlots[i].MyIndex = i;
        }
    }



    //인벤토리 열때
    public void OpenInventory()
    {

        GodlText.text = GameData.Instance.playerdata.money.ToString("N0");

        ChangeType((int)menutype);
        SetMenuSelectImage();


        if (GameData.Instance.playerdata.myItems.Count == 0)
        {
            ActiveDesCript(false);
        }
    }




    //인벤토리 아이템 타입 바꿀때
    public void ChangeType(int index)
    {
        InitSlots();
        CurSlot = null;

        switch (index)
        {
            case 0:

                menutype = MenuType.Weapon;

                for (int i = 0; i < GameData.Instance.playerdata.myItems.Count; i++)
                {
                    if (GameData.Instance.playerdata.myItems[i].itemdata.myType == ItemType.Weapon ||
                        GameData.Instance.playerdata.myItems[i].itemdata.myType == ItemType.Armor ||
                        GameData.Instance.playerdata.myItems[i].itemdata.myType == ItemType.Shoes)
                    {
                        for (int j = 0; j < mySlots.Length; j++)
                        {
                            if (!mySlots[j].Icon.gameObject.activeSelf)
                            {
                                mySlots[j].myItem = GameData.Instance.playerdata.myItems[i];
                                mySlots[j].Icon.gameObject.SetActive(true);
                                mySlots[j].Icon.sprite = GameData.Instance.playerdata.myItems[i].itemdata.Image;
                                break;
                            }
                        }
                    }

                }


                break;
            case 1:

                menutype = MenuType.Use;

                for (int i = 0; i < GameData.Instance.playerdata.myItems.Count; i++)
                {
                    if (GameData.Instance.playerdata.myItems[i].itemdata.myType == ItemType.UseItem)

                    {
                        for (int j = 0; j < mySlots.Length; j++)
                        {
                            if (!mySlots[j].Icon.gameObject.activeSelf)
                            {
                                mySlots[j].myItem = GameData.Instance.playerdata.myItems[i];
                                mySlots[j].Icon.gameObject.SetActive(true);
                                mySlots[j].Icon.sprite = GameData.Instance.playerdata.myItems[i].itemdata.Image;
                                mySlots[j].itemCountText.gameObject.SetActive(true);
                                mySlots[j].itemCountText.text = GameData.Instance.playerdata.myItems[i].itemCount.ToString();
                                break;
                            }
                        }
                    }

                }

                break;
            case 2:

                menutype = MenuType.Etc;

                for (int i = 0; i < GameData.Instance.playerdata.myItems.Count; i++)
                {
                    if (GameData.Instance.playerdata.myItems[i].itemdata.myType == ItemType.etc)

                    {
                        for (int j = 0; j < mySlots.Length; j++)
                        {
                            if (!mySlots[j].Icon.gameObject.activeSelf)
                            {
                                mySlots[j].myItem = GameData.Instance.playerdata.myItems[i];
                                mySlots[j].Icon.gameObject.SetActive(true);
                                mySlots[j].Icon.sprite = GameData.Instance.playerdata.myItems[i].itemdata.Image;
                                mySlots[j].itemCountText.gameObject.SetActive(true);
                                mySlots[j].itemCountText.text = GameData.Instance.playerdata.myItems[i].itemCount.ToString();
                                break;
                            }
                        }
                    }

                }

                break;

        }

        SetMenuSelectImage();
    }

    //아이템 초기화하고 다시 그려줌
    void InitSlots()
    {
        if (SelectImage != null)
            SelectImage.SetActive(false);

        for (int i = 0; i < mySlots.Length; i++)
        {
            mySlots[i].Icon.gameObject.SetActive(false);
            mySlots[i].itemCountText.gameObject.SetActive(false);

        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {

        if (eventData.pointerCurrentRaycast.gameObject.GetComponent<ItemSlot>().Icon.gameObject.activeSelf)
        {

            CurSlot = eventData.pointerCurrentRaycast.gameObject.GetComponent<ItemSlot>();

            DesImage.gameObject.SetActive(true);
            ItemNameText.gameObject.SetActive(true);
            ItemDesText.gameObject.SetActive(true);

            DesImage.sprite = CurSlot.myItem.itemdata.Image;
            ItemNameText.text = CurSlot.myItem.itemdata.ItemName;
            ItemDesText.text = CurSlot.myItem.itemdata.Description;


            if (SelectImage == null)
            {
                SelectImage = eventData.pointerCurrentRaycast.gameObject.GetComponent<ItemSlot>().SelectImage;
                SelectImage.SetActive(true);
            }
            else
            {
                SelectImage.SetActive(false);
                SelectImage = eventData.pointerCurrentRaycast.gameObject.GetComponent<ItemSlot>().SelectImage;
                SelectImage.SetActive(true);
            }




        }
    }

    public bool setDescription(int index)
    {
        switch (menutype)
        {
            case MenuType.Weapon:



                break;
            case MenuType.Use:


                break;
            case MenuType.Etc:


                break;
        }
        return false;
    }

    //아이템 설명창 끄고 닫기
    public void ActiveDesCript(bool Check)
    {
        DesImage.gameObject.SetActive(Check);
        ItemNameText.gameObject.SetActive(Check);
        ItemDesText.gameObject.SetActive(Check);
    }


    //장비,소비,기타템 버튼 선택시 이미지 활성화
    public void SetMenuSelectImage()
    {
        if (MenuSelectImage != null)
        {
            MenuSelectImage.SetActive(false);
            MenuSelectImage = MenuSelectObject[(int)menutype];
            MenuSelectImage.SetActive(true);
        }
        else
        {
            MenuSelectImage = MenuSelectObject[(int)menutype];
            MenuSelectImage.SetActive(true);
        }
    }



    public void ExitInventory()
    {
        InitSlots();
    }


    //UseItem인지 확인
    public void CheckIsUseItem()
    {
        if (CurSlot.myItem.itemdata.myType == ItemType.UseItem)
        {
            CheckQuickSlotPopup[0].SetActive(true);
            CheckQuickSlotPopup[1].SetActive(true);
        }
        else
        {
            // 안된다는 효과음 재생
        }


    }


    public void QuickSlotRegister(int index)
    {
        QuickSlot tempQuick = QuickSlot[index].GetComponent<QuickSlot>();

        tempQuick.myItem = CurSlot.myItem;
        tempQuick.Icon.gameObject.SetActive(true);
        tempQuick.Icon.sprite = CurSlot.myItem.itemdata.Image;
        tempQuick.ItemCount.gameObject.SetActive(true);
        tempQuick.ItemCount.text = CurSlot.myItem.itemCount.ToString();

    }


    public void UseButton()
    {
        if (CurSlot != null)
        {
            if (CurSlot.myItem.itemdata.myType == ItemType.UseItem)
            {
                ItemSlot temp = CurSlot;
                CurSlot.myItem.itemdata.UseItem();
                ChangeType((int)menutype);
                if (temp.myItem.itemCount == 0)
                {
                    ActiveDesCript(false);
                }
                else
                {
                    CurSlot = temp;
                    SelectImage = CurSlot.SelectImage;
                    SelectImage.SetActive(true);
                }

            }
            else
            {
                //안된다는 효과음 재생
            }
        }
    }
}
