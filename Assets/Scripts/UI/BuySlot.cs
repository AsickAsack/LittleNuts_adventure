using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuySlot : MonoBehaviour
{
    public ItemData buyItem;
    public Image Icon;
    public Text ItemNameTx;
    public Text BuyPriceTx;
    public Image SelectImage;

    // Start is called before the first frame update
    private void Awake()
    {
        Icon.sprite = buyItem.Image;
        ItemNameTx.text = buyItem.ItemName;
        BuyPriceTx.text = buyItem.BuyPrice.ToString("N0") + " °ñµå";
    }
}
