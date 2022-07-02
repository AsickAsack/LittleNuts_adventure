using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Item
{
    public uint UID;
    public int itemCount;
    public ItemData itemdata;

    //������϶� ������ ������ȣ UID�� �߱���
    public Item(uint UID, ItemData myData)
    {
        this.UID = UID;
        this.itemCount = 1;
        this.itemdata = myData;
    }

    //������ ���϶��� UID���� ����Ÿ�� ����
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
