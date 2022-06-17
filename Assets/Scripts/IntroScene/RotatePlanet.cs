using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlanet : MonoBehaviour
{
    int a = 0;
    float Speed = 0;

    private void Start()
    {
        //시작 할 때 회전 방향과 스피드에 대한 랜덤값을 받음.
        a = Random.Range(0, 2);
        Speed = Random.Range(70.0f, 100.0f);
    }

    void Update()
    {
        //위의 난수에 맞춰 회전하게함.
        switch(a)
        {
            case 0:
                this.transform.Rotate(Vector3.up * Time.deltaTime * Speed);
                break;
            case 1:
                this.transform.Rotate(-Vector3.up * Time.deltaTime * Speed);
                break;
        }
            
        
    }
}
