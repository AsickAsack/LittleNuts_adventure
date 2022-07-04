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
    int index = 2;
    public GameObject Cam;
    public GameObject notuch;


    string nickName = "��������";

    public Dictionary<int, string[]> ConversationDic = new Dictionary<int, string[]>();
    public List<string> test = new List<string>();

    private void Awake()
    {
        TextAsset myConversation = Resources.Load("DogConverseindex") as TextAsset;
        string[] splitData = myConversation.text.Split('*');

        for(int i=0;i< splitData.Length;i++)
        {
            string[] SplitConversation = splitData[i].Split(',');
            for (int j = 0; j < SplitConversation.Length; j++)
            {
                if (SplitConversation[j] != "")
                {
                    test.Add(SplitConversation[j]);
                }
            }
        }

        foreach(string test1 in test)
        {
            Debug.Log(test1);
        }



    }



    public void chat()
    {
        //mychat = ConversationDic[GameData.Instance.playerdata.DogProgress];
     
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
                index = 2;
                ConversationEnd = false;
                Camera.main.transform.gameObject.SetActive(true);
                this.gameObject.SetActive(false);
                Cam.gameObject.SetActive(false);
                notuch.SetActive(false);
            }
        }
    }
}
