using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour //메인으로 쓸 캐릭터의 부모 클래스
{

    //애니메이터 프로퍼티를 사용해 언제든 애니메이터에 접근 할 수 있다.
    private Animator _myAnim;

    public Animator myAnim
    {
        get
        {
            _myAnim = this.GetComponentInChildren<Animator>();
            return _myAnim;
        }
    }

    //리지드바드 프로퍼티를 사용해 언제든 리지드바디에 접근 할 수 있다.
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
