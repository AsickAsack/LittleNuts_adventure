using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSound : MonoBehaviour
{

    
    private AudioSource mySource = null;
    public AudioClip[] myClip = null;

    private void Awake()
    {
        //패널이 들고있는 오디오 소스를 가져옴
        mySource = this.GetComponent<AudioSource>();
    }

    public void SquidClick()
    {
        //오징어가 커질때
        mySource.PlayOneShot(myClip[0]);
    }

    public void Typing()
    {
        //글자가 하나씩 나올때
        mySource.PlayOneShot(myClip[1]);
    }



}
