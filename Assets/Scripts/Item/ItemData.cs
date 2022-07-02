using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum ItemType
{
    Weapon=0,Armor,Shoes,UseItem,etc
}

[CreateAssetMenu(fileName = "ItemData", menuName = "ItemData", order = int.MinValue+1)]
public class ItemData : ScriptableObject
{

    public ItemType myType;
    public string ItemName;
    public string Description;
    public float HP;
    public float ATK;
    public float DEF;
    public float Value;
    public Sprite Image;
    public int BuyPrice;
    public int SellPrice;
    public UnityAction myAction;



}
