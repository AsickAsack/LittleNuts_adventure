using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;


    public Canvas DialogueCanvas;
    public GameObject NextArrow;
    public GameObject DialoguePanel;
    public GameObject MissionButton;
    public Text CharName;
    public Text DialogueText;
    public GameObject NewCamera;
    public GameObject NoTouchPanel;

    private string[] MyChat;
    private bool IsChat;
    private bool IsQuest;
    private int ChatFirstIndex = 2;
    private int ChatSecondIndex = 0;
    string tempstring;

    public Dictionary<int, string[]> Story = new Dictionary<int, string[]>();
    

    private void Awake()
    {
        Instance = this;

        TextAsset Dialogue = Resources.Load("Dialogue1") as TextAsset;

        string[] firstData = Dialogue.text.Split('\n');

        for(int i=1; i<firstData.Length;i++)
        {
            string[] SecondData = firstData[i].Split(',');
            Story.Add(int.Parse(SecondData[0]), SecondData);
        }

 
    }
    
    public void SetDialogue(int index)
    {   

        MyChat = Story[index];
        CharName.text = MyChat[1];
        StartDialogue();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && DialoguePanel.gameObject.activeSelf)
        {
            if (!IsQuest)
            {
                if (IsChat)
                {
                    CancelInvoke("Dialogue_ing");
                    End_Dialogue();
                }
                else
                {
                    if (MyChat.Length == ChatFirstIndex)
                    {
                        DialoguePanel.gameObject.SetActive(false);
                        NewCamera.SetActive(false);
                        NoTouchPanel.SetActive(false);
                    }
                    else
                    {
                        IsChat = true;
                        NextArrow.SetActive(false);
                        Dialogue_ing();
                    }
                }
            }
        }
    }

    public void StartDialogue()
    {
        tempstring = "";
        ChatFirstIndex = 2;
        ChatSecondIndex = 0;
        NextArrow.SetActive(false);
        DialoguePanel.SetActive(true);
        Dialogue_ing();
        IsChat = true;
    }

    public void Dialogue_ing()
    {
        if(MyChat[ChatFirstIndex][ChatSecondIndex] == '*')
        {
            char a = '\n';
            tempstring += a;
        }
        else if (MyChat[ChatFirstIndex][ChatSecondIndex] == '`')
        {
            char a = '\n';
            tempstring += a;
        }
        else { 
            tempstring += MyChat[ChatFirstIndex][ChatSecondIndex];
        }

        DialogueText.text = tempstring;
        ChatSecondIndex++;

        if(MyChat[ChatFirstIndex].Length == ChatSecondIndex)
        {
            if(MyChat.Length == ChatFirstIndex+1)
            {
                End_Dialogue();
                return;
            }
            End_Dialogue();
        }
        else
        {
            float Inerval = 1.0f / 15;
            Invoke("Dialogue_ing", Inerval);
        }
    }

    public void End_Dialogue()
    {
        
        if (MyChat[ChatFirstIndex].Contains('*'))
        {
            IsQuest = true;
            MissionButton.SetActive(true);
            NextArrow.gameObject.SetActive(false);
        }
        else
            NextArrow.gameObject.SetActive(true);

        string A = MyChat[ChatFirstIndex].Replace('`', '\n');
        A = A.Replace('*', ' ');
        DialogueText.text = A;
        
        ChatSecondIndex = 0;
        ChatFirstIndex++; 
        tempstring = "";
        IsChat = false;
    }


    public void MissionNoButton()
    {
        MissionButton.SetActive(false);
        IsQuest = false;
        IsChat = true;
        NextArrow.SetActive(false);
        Dialogue_ing();
    }

    public void MissionYesButton()
    {
        GameData.Instance.playerdata.StoryProcess++;
        MissionButton.SetActive(false);
        SetDialogue(GameData.Instance.playerdata.StoryProcess);
        IsQuest = false;
        QuestManager.Instance.setMission(GameData.Instance.playerdata.StoryProcess);
    }


}
