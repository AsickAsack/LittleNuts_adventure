using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSound : MonoBehaviour
{

    
    private AudioSource mySource = null;
    public AudioClip[] myClip = null;

    private void Awake()
    {
        //�г��� ����ִ� ����� �ҽ��� ������
        mySource = this.GetComponent<AudioSource>();
    }

    public void SquidClick()
    {
        //��¡� Ŀ����
        mySource.PlayOneShot(myClip[0]);
    }

    public void Typing()
    {
        //���ڰ� �ϳ��� ���ö�
        mySource.PlayOneShot(myClip[1]);
    }



}
