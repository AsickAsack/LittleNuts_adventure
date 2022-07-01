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


    //���ݿø��� ��ư  Ŭ��
    public void StatUp(int index)
    {
        if(GameData.Instance.playerdata.StatPoint > 0)
        {
            switch(index)
            {
                case 0:
                    GameData.Instance.playerdata.ATK += 1;
                    break;
                case 1:
                    GameData.Instance.playerdata.DEF += 1;
                    break;
                case 2:
                    GameData.Instance.playerdata.MoveSpeed += 1;
                    break;
            }
            GameData.Instance.playerdata.StatPoint -= 1;
            initStat();
        }
        else
        {
            //��������Ʈ ���ٰ� ����
            Debug.Log("��������Ʈ ����");
        }

    }

    //���� �ʱ�ȭ��ư
    public void ResetStat()
    { 
        //�˾�â ���� Ȯ�� ������
        if(GameData.Instance.playerdata.Level > 1)
        { 
            GameData.Instance.playerdata.ATK_Point = 0;
            GameData.Instance.playerdata.DEF_Point = 0;
            GameData.Instance.playerdata.Speed_Point = 0;
            GameData.Instance.playerdata.StatPoint = (GameData.Instance.playerdata.Level * 3) -3;
            initStat();
        }
        
    }
    
    //���� �ؽ�Ʈ ���ΰ�ħ
    public void initStat()
    {
        ATK_tx.text = GameData.Instance.playerdata.ATK.ToString();
        DEF_tx.text = GameData.Instance.playerdata.DEF.ToString();
        SPEED_tx.text = GameData.Instance.playerdata.MoveSpeed.ToString();
        AllStat_tx.text = GameData.Instance.playerdata.StatPoint.ToString();

    }


}
