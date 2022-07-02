using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Item
{
    public uint UID;
    public int itemCount;
    public ItemData itemdata;

    //장비템일때 각자의 고유번호 UID를 발급함
    public Item(uint UID, ItemData myData)
    {
        this.UID = UID;
        this.itemCount = 1;
        this.itemdata = myData;
    }

    //나머지 템일때는 UID없이 데이타만 생성
    public Item(ItemData myData)
    {
        this.itemdata = myData;
        this.itemCount = 1;
    }

    public Item(uint UID, ItemData myData, int itemCount)
    {
        this.UID = UID;
        this.itemCount = itemCount;
        this.itemdata = myData;
    }

}
