using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.IO;



public class IntroManager : MonoBehaviour
{
    [Header("[메인 메뉴 버튼 관련]")]
     public GameObject IntroCanvas;

    public GameObject[] LoadObject;
    public Text[] SaveData_datetx;
    public Text[] SaveData_PlayTimetx;
    public Text[] SaveData_Maptx;
    public Image[] SaveImage;
    public Image BigImage;

    void Start()
    {
        //인트로 패널 애니메이션 재생시간 대기 코루틴
        StartCoroutine(Delay(3.2f));
        GameData.Instance.LoadIntroData();

    }


    IEnumerator Delay(float time)
    {
        yield return new WaitForSeconds(time);
        IntroCanvas.SetActive(false);        
    }



    public void OpenLoad_PopUp()
    {
        for (int i = 0; i < 3; i++)
        {
            if (GameData.Instance.savedata.SaveData_Map[i] == null)
            {
                LoadObject[i].SetActive(false);
                SaveData_Maptx[i].text = "저장 데이터 없음";
            }
            else
            {
                LoadObject[i].SetActive(true);
                SaveData_datetx[i].text = "저장 일자 :  "+GameData.Instance.savedata.SaveData_date[i];
                SaveData_PlayTimetx[i].text = "총 플레이타임\n" + GameData.Instance.savedata.SaveData_PlayTime[i];
                SaveData_Maptx[i].text = GameData.Instance.savedata.SaveData_Map[i];

                byte[] byteTexture = System.IO.File.ReadAllBytes(Application.persistentDataPath + "ScreenShot" + i + ".png");
                Texture2D texture = new Texture2D(Screen.width,Screen.height);
                if (byteTexture.Length > 0)
                {
                    texture.LoadImage(byteTexture);
                }

                Sprite sptite = Sprite.Create(texture, new Rect(0, 0, texture.width,texture.height),new Vector2(0.5f, 0.5f));
                SaveImage[i].sprite = sptite;
            }
        }
    }

    public void OpenBigImage(int index)
    {
        BigImage.sprite = SaveImage[index].sprite;
        BigImage.gameObject.SetActive(true);
    }



}
