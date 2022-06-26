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
        if(GameData.Instance.StatPoint > 0)
        {
            switch(index)
            {
                case 0:
                    GameData.Instance.ATK += 1;
                    break;
                case 1:
                    GameData.Instance.DEF += 1;
                    break;
                case 2:
                    GameData.Instance.MoveSpeed += 1;
                    break;
            }
            GameData.Instance.StatPoint -= 1;
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
        if(GameData.Instance.Level > 1)
        { 
            GameData.Instance.ATK_Point = 0;
            GameData.Instance.DEF_Point = 0;
            GameData.Instance.Speed_Point = 0;
            GameData.Instance.StatPoint = (GameData.Instance.Level * 3) -3;
            initStat();
        }
        
    }
    
    //���� �ؽ�Ʈ ���ΰ�ħ
    public void initStat()
    {
        ATK_tx.text = GameData.Instance.ATK_Point.ToString();
        DEF_tx.text = GameData.Instance.DEF_Point.ToString();
        SPEED_tx.text = GameData.Instance.Speed_Point.ToString();
        AllStat_tx.text = GameData.Instance.StatPoint.ToString();

    }


}
