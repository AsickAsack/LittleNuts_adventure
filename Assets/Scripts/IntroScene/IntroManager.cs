using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : MonoBehaviour
{
    [Header("[���� �޴� ��ư ����]")]
   


    public GameObject IntroCanvas;
    

    void Start()
    {
        //��Ʈ�� �г� �ִϸ��̼� ����ð� ��� �ڷ�ƾ
        StartCoroutine(Delay(3.2f));
    }

    IEnumerator Delay(float time)
    {
        yield return new WaitForSeconds(time);
        IntroCanvas.SetActive(false);        
    }

    public void test()
    {
        Debug.Log(SoundManager.Instance.EffectSource.Count);
    }


}
