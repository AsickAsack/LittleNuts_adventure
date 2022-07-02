using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum ItemType
{
    Weapon=0,Armor,Shoes,UseItem,etc
}


[System.Serializable]
public abstract class ItemData : ScriptableObject
{
    public ItemType myType;
    public int ItemCode;
    public string ItemName;
    [TextArea]
    public string Description;
    public float HP;
    public float ATK;
    public float DEF;
    public float Value;
    public Sprite Image;
    public int BuyPrice;
    public int SellPrice;
    public Item RefItem;

    public abstract void UseItem();

 

}
