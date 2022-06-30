using System.Collections;
using System.Collections.Generic;


public class Item
{
    public int UID;
    public ItemData itemdata;

    public Item(int UID, ItemData myData)
    {
        this.UID = UID;
        this.itemdata = myData;
}


}
