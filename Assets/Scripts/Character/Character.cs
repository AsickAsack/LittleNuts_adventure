using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour //�������� �� ĳ������ �θ� Ŭ����
{

    //�ִϸ����� ������Ƽ�� ����� ������ �ִϸ����Ϳ� ���� �� �� �ִ�.
    private Animator _myAnim;

    public Animator myAnim
    {
        get
        {
            _myAnim = this.GetComponentInChildren<Animator>();
            return _myAnim;
        }
    }

    //������ٵ� ������Ƽ�� ����� ������ ������ٵ� ���� �� �� �ִ�.
    private Rigidbody _myRigid;

    public Rigidbody myRigid
    {
        get
        {
            _myRigid = this.GetComponent<Rigidbody>();
            return _myRigid;
        }
    }
}
