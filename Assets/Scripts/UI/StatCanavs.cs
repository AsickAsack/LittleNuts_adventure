using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatCanavs : MonoBehaviour
{
    public Text ATK_tx;
    public Text DEF_tx;
    public Text SPEED_tx;
    public Text AllStat_tx;


    //스텟올리는 버튼  클릭
    public void StatUp(int index)
    {
        if(GameData.Instance.playerdata.StatPoint > 0)
        {
            switch(index)
            {
                case 0:
                    GameData.Instance.playerdata.ATK_Point += 1;
                    break;
                case 1:
                    GameData.Instance.playerdata.DEF_Point += 1;
                    break;
                case 2:
                    GameData.Instance.playerdata.Speed_Point += 1;
                    break;
            }
            GameData.Instance.playerdata.StatPoint -= 1;
            initStat();
        }
        else
        {
            //스텟포인트 없다고 띄우기
            Debug.Log("스텟포인트 없음");
        }

    }

    //스텟 초기화버튼
    public void ResetStat()
    { 
        //팝업창 띄우고 확인 누르면
        if(GameData.Instance.playerdata.Level > 1)
        { 
            GameData.Instance.playerdata.ATK_Point = 0;
            GameData.Instance.playerdata.DEF_Point = 0;
            GameData.Instance.playerdata.Speed_Point = 0;
            GameData.Instance.playerdata.StatPoint = (GameData.Instance.playerdata.Level * 3) -3;
            initStat();
        }
        
    }
    
    //스텟 텍스트 새로고침
    public void initStat()
    {
        ATK_tx.text = GameData.Instance.playerdata.ATK.ToString();
        DEF_tx.text = GameData.Instance.playerdata.DEF.ToString();
        SPEED_tx.text = GameData.Instance.playerdata.StatSpeed.ToString();
        AllStat_tx.text = GameData.Instance.playerdata.StatPoint.ToString();

    }


}
