using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlanet : MonoBehaviour
{
    int a = 0;
    float Speed = 0;

    private void Start()
    {
        //���� �� �� ȸ�� ����� ���ǵ忡 ���� �������� ����.
        a = Random.Range(0, 2);
        Speed = Random.Range(70.0f, 100.0f);
    }

    void Update()
    {
        //���� ������ ���� ȸ���ϰ���.
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
