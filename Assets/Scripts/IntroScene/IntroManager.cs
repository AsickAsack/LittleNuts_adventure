using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : MonoBehaviour
{

    public GameObject IntroCanvas;

    void Start()
    {
        StartCoroutine(Delay(3.2f));
    }

    IEnumerator Delay(float time)
    {
        yield return new WaitForSeconds(time);
        IntroCanvas.SetActive(false);

    }
}
