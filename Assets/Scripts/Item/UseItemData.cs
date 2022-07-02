using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UseItemData", menuName = "UseItemData", order = int.MinValue + 2)]
public class UseItemData : ItemData
{
    public override void UseItem()
    {
        switch (this.ItemCode)
        {
            case 0:

                for (int i = 0; i < GameData.Instance.playerdata.myItems.Count; i++)
                {
                    if (GameData.Instance.playerdata.myItems[i].itemdata == this)
                    {
                        GameData.Instance.playerdata.myItems[i].itemCount--;
                        RefItem = GameData.Instance.playerdata.myItems[i];
                        GameData.Instance.playerdata.CurHP += this.Value;
                        //물약빠는 효과음
                        break;
                    }

                }
                break;
            case 1:

                for (int i = 0; i < GameData.Instance.playerdata.myItems.Count; i++)
                {
                    if (GameData.Instance.playerdata.myItems[i].itemdata == this)
                    {
                        GameData.Instance.playerdata.myItems[i].itemCount--;
                        RefItem = GameData.Instance.playerdata.myItems[i];
                        GameData.Instance.playerdata.CurMP += this.Value;
                        //물약빠는 효과음
                        break;
                    }
                }
                    break;
            case 2:
                break;

        }

        if(RefItem.itemCount == 0)
        {
            GameData.Instance.playerdata.myItems.Remove(RefItem);
        }

    }

}
        