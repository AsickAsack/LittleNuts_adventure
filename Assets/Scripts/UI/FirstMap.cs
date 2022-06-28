using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstMap : MonoBehaviour
{
    public GameObject MoveMapPanel;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if(GameData.Instance.playerdata.InBaseCamp)
            { 
                MoveMapPanel.transform.GetChild(0).GetComponent<Text>().text = "두더지의 숲";
                MoveMapPanel.gameObject.SetActive(true);
                GameData.Instance.playerdata.InBaseCamp = !GameData.Instance.playerdata.InBaseCamp;
                GameData.Instance.playerdata.SaveMap = "두더지의 숲";
            }

        }
    }

    public void test()
    {
        GameData.Instance.Set_SaveData(1);
    }
}
