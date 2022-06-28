using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseMap : MonoBehaviour
{
    public GameObject MoveMapPanel;
    

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if(!GameData.Instance.playerdata.InBaseCamp)
            { 
                MoveMapPanel.transform.GetChild(0).GetComponent<Text>().text = "º£ÀÌ½º Ä·ÇÁ";
                MoveMapPanel.gameObject.SetActive(true);
                GameData.Instance.playerdata.InBaseCamp = !GameData.Instance.playerdata.InBaseCamp;
                GameData.Instance.playerdata.SaveMap = "º£ÀÌ½º Ä·ÇÁ";
            }
        }
    }
}
