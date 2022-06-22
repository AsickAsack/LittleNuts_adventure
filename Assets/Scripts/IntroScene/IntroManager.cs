using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : MonoBehaviour
{
    [Header("[메인 메뉴 버튼 관련]")]
   


    public GameObject IntroCanvas;
    

    void Start()
    {
        //인트로 패널 애니메이션 재생시간 대기 코루틴
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
