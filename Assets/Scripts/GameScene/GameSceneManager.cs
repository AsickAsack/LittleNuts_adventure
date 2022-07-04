using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneManager : MonoBehaviour
{
    [Header("[옵션 관련]")]
   
    public GameObject[] SelectImage;
    public GameObject[] OptionPanel;
    public Canvas OptionCanvas;

    public Text myDiff;

    [Header("[세이브 기능]")]
    public Text[] Savedate;
    public Text[] PlayTime;
    public Text[] SaveMap;
    public Text SaveCheckText;

    [Header("[사운드 기능]")]
    public Text BgmVolume_Text;
    public Text EffectVolume_Text;
    public Slider BgmSlider;
    public Slider EffectSlider;

    int myIndex = 0;
    Coroutine MyTextco = null;
    int saveIndex = 0;

    [Header("[알림 기능]")]
    public Text[] EventText = null;
    public GameObject[] TextPanel = null;
    public AudioSource PlayerAudio = null;
    RectTransform[] EventTextRect = new RectTransform[2];
    Vector2[] EventText_OrgPos = new Vector2[2];
    public GameObject LevelUpText;

    [Header("[인벤토리 기능]")]
    public GameObject[] InvenSelectImage;

    //나중에 수정하자.
    #region 난이도 기능

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
            //왼쪽
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

            //오른쪽
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
                myDiff.text = "쉬움";
                break;
            case 1:
                myDiff.text = "보통";
                break;
            case 2:
                myDiff.text = "어려움";
                break;
        }
    }

    #endregion



    //버튼에 따라 켜지는 판넬
    public void SetOptionMenu(int index)
    {
        for (int i = 0; i < SelectImage.Length; i++)
        {
            SelectImage[i].SetActive(i == index);
            OptionPanel[i].SetActive(i == index);
        }

    }


    //세이브 텍스트 초기화
    public void SetSaveText()
    {
        for (int i = 0; i < Savedate.Length; i++)
        {
            if (GameData.Instance.savedata.SaveData_date[i + 1] != null)
            {
                Savedate[i].text = "저장 일자\n" + GameData.Instance.savedata.SaveData_date[i + 1];
                PlayTime[i].text = "총 플레이타임\n" + GameData.Instance.savedata.SaveData_PlayTime[i + 1];
                SaveMap[i].text = GameData.Instance.savedata.SaveData_Map[i + 1];
            }
            else
            {
                SaveMap[i].text = "저장 데이터 없음";
            }
        }
    }




    //세이브 확인 함수
    public void CheckSave(int index)
    {
        saveIndex = index;

        if (System.IO.File.Exists(Application.dataPath+"GameData" + index + ".json"))
        {
            SaveCheckText.text = "저장 파일이 존재합니다.\n정말 저장 하시겠습니까?";
        }
        else
        {
            SaveCheckText.text = "정말 저장 하시겠습니까?";
        }
    }


    //세이브 함수
    public void Saving()
    {
        OptionCanvas.gameObject.SetActive(false);
        GameData.Instance.Set_SaveData(saveIndex);
        StartCoroutine(DelayScreenShot());
        SetSaveText();
    }

    //스샷 찍는 함수
    //나중에 시간남으면 카메라로만 찍기
    IEnumerator DelayScreenShot()
    {
        yield return new WaitForSeconds(0.1f);
        OptionCanvas.gameObject.SetActive(true);
    }

    //Bgm사운드 변경 함수
    public void ChangeVolume(int index)
    {
        switch(index)
        {
            //Bgm 슬라이더
            case 0:
                SetBgmVolume();
                break;
            // 이펙트 슬라이더
            case 1:
                SetEffectVolume();
                break;
            //옵션창 열었을때 초기화
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
            //BGM볼륨 마이너스 
            case 0:
                BgmSlider.value = BgmSlider.value - 0.1f < 0 ? BgmSlider.value = 0 : BgmSlider.value-0.01f;
                SetBgmVolume();
                break;
            //BGM볼륨 플러스
            case 1:
                BgmSlider.value = BgmSlider.value + 0.1f > 1f ? BgmSlider.value = 1 : BgmSlider.value+0.01f;
                SetBgmVolume();
                break;
             //이펙트 볼륨 마이너스
            case 2:
                EffectSlider.value = EffectSlider.value - 0.1f < 0 ? EffectSlider.value = 0 : EffectSlider.value-0.01f;
                SetEffectVolume();
                break;
            //이펙트 볼륨 플러스
            case 3:
                EffectSlider.value = EffectSlider.value + 0.1f > 1f ? EffectSlider.value = 1 : EffectSlider.value+0.01f;
                SetEffectVolume();
                break;
        }

    }

    // Bgm볼륨 세팅
    void SetBgmVolume()
    {
        SoundManager.Instance.BgmVolume = BgmSlider.value;
        BgmVolume_Text.text = (BgmSlider.value * 100).ToString("N0");
    }

    // 이펙트 볼륨 세팅
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
