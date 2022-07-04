using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneManager : MonoBehaviour
{
    [Header("[�ɼ� ����]")]
   
    public GameObject[] SelectImage;
    public GameObject[] OptionPanel;
    public Canvas OptionCanvas;

    public Text myDiff;

    [Header("[���̺� ���]")]
    public Text[] Savedate;
    public Text[] PlayTime;
    public Text[] SaveMap;
    public Text SaveCheckText;

    [Header("[���� ���]")]
    public Text BgmVolume_Text;
    public Text EffectVolume_Text;
    public Slider BgmSlider;
    public Slider EffectSlider;

    int myIndex = 0;
    Coroutine MyTextco = null;
    int saveIndex = 0;

    [Header("[�˸� ���]")]
    public Text[] EventText = null;
    public GameObject[] TextPanel = null;
    public AudioSource PlayerAudio = null;
    RectTransform[] EventTextRect = new RectTransform[2];
    Vector2[] EventText_OrgPos = new Vector2[2];
    public GameObject LevelUpText;

    [Header("[�κ��丮 ���]")]
    public GameObject[] InvenSelectImage;

    //���߿� ��������.
    #region ���̵� ���

    private void Awake()
    {
        myIndex = (int)GameData.Instance.playerdata.difficulty;
        for(int i=0;i< TextPanel.Length;i++)
        { 
            EventTextRect[i] = TextPanel[i].GetComponent<RectTransform>();
            EventText_OrgPos[i] = EventTextRect[i].anchoredPosition;
            TextPanel[i].SetActive(false);
        }
        ChanegeText();
        GameData.Instance.LevelUpAction += LevelUpEffect;
    }

    public void LevelUpEffect()
    {
        LevelUpText.SetActive(true);
        StartCoroutine(WaitEffet());
        PlayerAudio.PlayOneShot(SoundManager.Instance.myEffectClip[3]);

    }

    IEnumerator WaitEffet()
    {
        yield return new WaitForSeconds(1.0f);
        LevelUpText.SetActive(false);
    }

    private void Update()
    {
        if (GameData.Instance.EventString.Count != 0)
        {
            if (!TextPanel[0].activeSelf)
            {
                StartCoroutine(DequeueEventString(0));
            }
            else if(!TextPanel[1].activeSelf)
            { 
                StartCoroutine(DequeueEventString(1));
            }
        }
    }

    IEnumerator DequeueEventString(int index)
    {
        TextPanel[index].gameObject.SetActive(true);
        EventText[index].text = GameData.Instance.EventString.Dequeue();
        PlayerAudio.PlayOneShot(SoundManager.Instance.myEffectClip[2]);

        while (EventTextRect[index].anchoredPosition.y > -100.0f)
        {
             EventTextRect[index].anchoredPosition -= Vector2.up * Time.deltaTime * 250.0f;
            yield return null;
        }
        while (EventTextRect[index].anchoredPosition.y < 0.0f)
        {
            EventTextRect[index].anchoredPosition += Vector2.up * Time.deltaTime * 70.0f;
            yield return null;
        }
        
        EventTextRect[index].anchoredPosition = EventText_OrgPos[index];
        TextPanel[index].gameObject.SetActive(false);
        
    }

    public void DifficutyBtn(int index)
    {


        switch (index)
        {
            //����
            case 0:
                {
                    if (myIndex == 0)
                        myIndex = (myIndex - 1 + 3);
                    else
                        myIndex = (myIndex - 1);

                    if (MyTextco == null)
                        MyTextco = StartCoroutine(MoveTextLeft());
                    else
                    {
                        StopCoroutine(MyTextco);
                        MyTextco = StartCoroutine(MoveTextLeft());
                    }
                }
                break;

            //������
            case 1:
                myIndex = (myIndex + 1) % 3;
                if (MyTextco == null)
                    MyTextco = StartCoroutine(MoveTextRight());
                else
                {
                    StopCoroutine(MyTextco);
                    MyTextco = StartCoroutine(MoveTextRight());
                }
                break;
        }

        GameData.Instance.playerdata.difficulty = (GameData.PlayerData.Difficulty)myIndex;

    }



    IEnumerator MoveTextRight()
    {
        while (myDiff.rectTransform.anchoredPosition.x < 266)
        {
            myDiff.rectTransform.anchoredPosition += Vector2.right * Time.deltaTime * 1000.0f;

            yield return null;
        }

        myDiff.rectTransform.anchoredPosition = new Vector2(-85, myDiff.rectTransform.anchoredPosition.y);
        ChanegeText();

        while (myDiff.rectTransform.anchoredPosition.x < 90)
        {
            myDiff.rectTransform.anchoredPosition += Vector2.right * Time.deltaTime * 1000.0f;

            yield return null;
        }
        myDiff.rectTransform.anchoredPosition = new Vector2(90, myDiff.rectTransform.anchoredPosition.y);

    }

    IEnumerator MoveTextLeft()
    {
        while (myDiff.rectTransform.anchoredPosition.x > -85.0f)
        {
            myDiff.rectTransform.anchoredPosition += Vector2.left * Time.deltaTime * 1000.0f;

            yield return null;
        }

        myDiff.rectTransform.anchoredPosition = new Vector2(266, myDiff.rectTransform.anchoredPosition.y);
        ChanegeText();

        while (myDiff.rectTransform.anchoredPosition.x > 90)
        {
            myDiff.rectTransform.anchoredPosition += Vector2.left * Time.deltaTime * 1000.0f;

            yield return null;
        }
        myDiff.rectTransform.anchoredPosition = new Vector2(90, myDiff.rectTransform.anchoredPosition.y);

    }

    void ChanegeText()
    {
        switch (myIndex)
        {
            case 0:
                myDiff.text = "����";
                break;
            case 1:
                myDiff.text = "����";
                break;
            case 2:
                myDiff.text = "�����";
                break;
        }
    }

    #endregion



    //��ư�� ���� ������ �ǳ�
    public void SetOptionMenu(int index)
    {
        for (int i = 0; i < SelectImage.Length; i++)
        {
            SelectImage[i].SetActive(i == index);
            OptionPanel[i].SetActive(i == index);
        }

    }


    //���̺� �ؽ�Ʈ �ʱ�ȭ
    public void SetSaveText()
    {
        for (int i = 0; i < Savedate.Length; i++)
        {
            if (GameData.Instance.savedata.SaveData_date[i + 1] != null)
            {
                Savedate[i].text = "���� ����\n" + GameData.Instance.savedata.SaveData_date[i + 1];
                PlayTime[i].text = "�� �÷���Ÿ��\n" + GameData.Instance.savedata.SaveData_PlayTime[i + 1];
                SaveMap[i].text = GameData.Instance.savedata.SaveData_Map[i + 1];
            }
            else
            {
                SaveMap[i].text = "���� ������ ����";
            }
        }
    }




    //���̺� Ȯ�� �Լ�
    public void CheckSave(int index)
    {
        saveIndex = index;

        if (System.IO.File.Exists(Application.dataPath+"GameData" + index + ".json"))
        {
            SaveCheckText.text = "���� ������ �����մϴ�.\n���� ���� �Ͻðڽ��ϱ�?";
        }
        else
        {
            SaveCheckText.text = "���� ���� �Ͻðڽ��ϱ�?";
        }
    }


    //���̺� �Լ�
    public void Saving()
    {
        OptionCanvas.gameObject.SetActive(false);
        GameData.Instance.Set_SaveData(saveIndex);
        StartCoroutine(DelayScreenShot());
        SetSaveText();
    }

    //���� ��� �Լ�
    //���߿� �ð������� ī�޶�θ� ���
    IEnumerator DelayScreenShot()
    {
        yield return new WaitForSeconds(0.1f);
        OptionCanvas.gameObject.SetActive(true);
    }

    //Bgm���� ���� �Լ�
    public void ChangeVolume(int index)
    {
        switch(index)
        {
            //Bgm �����̴�
            case 0:
                SetBgmVolume();
                break;
            // ����Ʈ �����̴�
            case 1:
                SetEffectVolume();
                break;
            //�ɼ�â �������� �ʱ�ȭ
            case 2:
                BgmSlider.value = SoundManager.Instance.BgmVolume;
                EffectSlider.value = SoundManager.Instance.EffectVolume;
                BgmVolume_Text.text = (SoundManager.Instance.BgmVolume * 100).ToString("N0");
                EffectVolume_Text.text = (SoundManager.Instance.EffectVolume * 100).ToString("N0");
                break;
        }
    }

    public void ClickVolumeBtn(int index)
    {
        switch(index)
        {
            //BGM���� ���̳ʽ� 
            case 0:
                BgmSlider.value = BgmSlider.value - 0.1f < 0 ? BgmSlider.value = 0 : BgmSlider.value-0.01f;
                SetBgmVolume();
                break;
            //BGM���� �÷���
            case 1:
                BgmSlider.value = BgmSlider.value + 0.1f > 1f ? BgmSlider.value = 1 : BgmSlider.value+0.01f;
                SetBgmVolume();
                break;
             //����Ʈ ���� ���̳ʽ�
            case 2:
                EffectSlider.value = EffectSlider.value - 0.1f < 0 ? EffectSlider.value = 0 : EffectSlider.value-0.01f;
                SetEffectVolume();
                break;
            //����Ʈ ���� �÷���
            case 3:
                EffectSlider.value = EffectSlider.value + 0.1f > 1f ? EffectSlider.value = 1 : EffectSlider.value+0.01f;
                SetEffectVolume();
                break;
        }

    }

    // Bgm���� ����
    void SetBgmVolume()
    {
        SoundManager.Instance.BgmVolume = BgmSlider.value;
        BgmVolume_Text.text = (BgmSlider.value * 100).ToString("N0");
    }

    // ����Ʈ ���� ����
    void SetEffectVolume()
    {
        SoundManager.Instance.EffectVolume = SoundManager.Instance.EffectSource != null ? EffectSlider.value : SoundManager.Instance.EffectVolume;
        EffectVolume_Text.text = (EffectSlider.value * 100).ToString("N0");
    }

    public void TimeSet(int index)
    {
        Time.timeScale = index;

    }

}
