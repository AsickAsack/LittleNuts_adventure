using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MoonDogConverse : MonoBehaviour, IPointerClickHandler
{
    private bool ConverseEnd = false;
    private string[] mychat = new string[2];
    private Coroutine myco = null;
    public GameObject Arrow = null;
    int index = 0;
    public GameObject Cam;
    public GameObject notuch;

    public void chat()
    {
        mychat[0] = "���� �������� �� �̻����� �þ�..";
        mychat[1] = "�����̶� ��� �ұ�..?";

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
        this.transform.GetChild(0).GetComponent<Text>().text = "�޵�����";
        string temp = "";

        for(int i=0 ;i<mychat[index].Length;i++)
        {
            temp += mychat[index][i];
            this.transform.GetChild(1).GetComponent<Text>().text = temp;
            if (ConverseEnd)
            { 
                this.transform.GetChild(1).GetComponent<Text>().text = mychat[index];
                Arrow.gameObject.SetActive(true);
                index++;
                
                break;
            }
            yield return new WaitForSeconds(0.05f);
        }
        if(!ConverseEnd)
        index++;

        ConverseEnd = true;
        Arrow.gameObject.SetActive(true);
    }




    public void OnPointerClick(PointerEventData eventData)
    {
        if(!ConverseEnd)
        {
            ConverseEnd = true;
        }
        else
        {
            if(index < mychat.Length)
            {
                ConverseEnd = false;
                Arrow.gameObject.SetActive(false);
                if (myco == null)
                    myco = StartCoroutine(Chatgo());
                else
                {
                    StopCoroutine(myco);
                    myco = StartCoroutine(Chatgo());
                }
            }
            else
            {
                ConverseEnd = false;
                index = 0;
                this.gameObject.SetActive(false);
                Cam.gameObject.SetActive(false);
                Camera.main.transform.gameObject.SetActive(true);
                notuch.SetActive(false);
            }
        }
    }
}
