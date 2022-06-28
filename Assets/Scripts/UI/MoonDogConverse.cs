using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.IO;



[System.Serializable]
public class MoonDogConverse : MonoBehaviour, IPointerClickHandler
{
    private bool ConversationEnd = false;
    public string[] mychat = new string[3];
    private Coroutine myco = null;
    public GameObject Arrow = null;
    int index = 1;
    public GameObject Cam;
    public GameObject notuch;


    string nickName = "문독전사";

    public Dictionary<int, string[]> ConversationDic = new Dictionary<int, string[]>();

    private void Awake()
    {
        TextAsset myConversation = Resources.Load("DogConverseindex") as TextAsset;
        string[] splitData = myConversation.text.Split('\n');

        for(int i=1;i< splitData.Length-1;i++)
        {
            string[] SplitConversation = splitData[i].Split(',');
            ConversationDic.Add(int.Parse(SplitConversation[0]), SplitConversation);
        }
    
    }



    public void chat()
    {
        mychat = ConversationDic[GameData.Instance.playerdata.DogProgress];
     
        if(myco == null)
        myco = StartCoroutine(Chatgo());
        else
        {
            StopCoroutine(myco);
            myco = StartCoroutine(Chatgo());
        }
    }

    IEnumerator Chatgo()
    {
        this.transform.GetChild(0).GetComponent<Text>().text = nickName;

        string temp = "";

        for (int i = 0; i < mychat[index].Length; i++)
        {

            temp += mychat[index][i];
            this.transform.GetChild(1).GetComponent<Text>().text = temp;
            if (ConversationEnd)
            {
                this.transform.GetChild(1).GetComponent<Text>().text = mychat[index];
                break;
            }
            yield return new WaitForSeconds(0.05f);
        }
            
            index++;
            ConversationEnd = true;
            Arrow.gameObject.SetActive(true);
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if(!ConversationEnd)
        {
            ConversationEnd = true;
        }
        else
        {
            if (index < mychat.Length && mychat[index] == "\r")
                index++;

            if(index < mychat.Length)
            {
                ConversationEnd = false;
                Arrow.gameObject.SetActive(false);

                if (myco == null) 
                { 
                    myco = StartCoroutine(Chatgo());
                }
                else
                {
                    StopCoroutine(myco);
                    myco = StartCoroutine(Chatgo());
                }
            }
            else
            {                
                index = 1;
                ConversationEnd = false;
                Camera.main.transform.gameObject.SetActive(true);
                this.gameObject.SetActive(false);
                Cam.gameObject.SetActive(false);
                notuch.SetActive(false);
            }
        }
    }
}
