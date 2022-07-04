using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    public GameObject MissionUi;

    public Text MissionName;
    public Text MissionDetail;

    public Text QuestPage_name;
    public Text QuestPage_detail;

    public GameObject FirstGameCollider;

    public bool IsQuest;
    private int QuestIndex = 0;
    private int QuestIndex2 = 0;


    private void Awake()
    {
        Instance = this;
        if(GameData.Instance.playerdata.StoryProcess > 1)
        {
            FirstGameCollider.SetActive(false);
        }
    }



    public void setMission(int index)
    {
        string[] MissionString = DialogueManager.Instance.Story[index + 11];
        

        MissionName.text = MissionString[1];
        QuestPage_name.text = MissionString[1];
        QuestPage_detail.text = MissionString[2].Replace('`','\n');

        MissionUi.SetActive(true);

        IsQuest = true;
        CheckQuest(index);

        GameData.Instance.playerdata.StoryProcess++;

        if (index == 2)
        FirstGameCollider.SetActive(false);

    }

    public void CheckQuest(int index)
    {

        switch (index + 11)
        {
            case 13:
                MissionDetail.text = "두더지 처치  "+QuestIndex + " / 5";
                break;
            case 15:
                MissionDetail.text = "슬라임 처치  "+QuestIndex + " / 5  " + "터틀쉘 처치  " + QuestIndex2 + " / 5";
                break;
        }
    }





    public void CheckQuest(GameObject obj)
    {

        switch(GameData.Instance.playerdata.StoryProcess+10)
        {
            case 13:
                if (obj.GetComponent<Mole>() != null)
                {
                    QuestIndex++;

                    if (QuestIndex >= 5)
                    {
                        QuestIndex = 0;
                        IsQuest = false;
                        MissionDetail.text = "퀘스트 완료!";
                        GameData.Instance.playerdata.StoryProcess++;
                    }
                    else
                    {
                        MissionDetail.text = "두더지 처치  " + QuestIndex + " / 5";
                    }
                }
                    break;
            case 15:
                if (obj.GetComponent<Slime>() != null)
                {
                    QuestIndex++;
                    MissionDetail.text = "슬라임 처치  " + QuestIndex + " / 5  " + "터틀쉘 처치  " + QuestIndex2 + " / 5";
                }
                else if(obj.GetComponent<TurtleShell>() != null)
                {
                    QuestIndex2++;
                    MissionDetail.text = "슬라임 처치  " + QuestIndex + " / 5  " + "터틀쉘 처치  " + QuestIndex2 + " / 5";
                }
                break;
    }

        



    }


}
