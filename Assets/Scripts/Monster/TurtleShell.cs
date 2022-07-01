using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleShell : Monster
{


    //State�� �ٲ� ���� �� �Լ�
    public override void ChangeState(State s)
    {
        if (mystate == s) return;

        mystate = s;

        switch (s)
        {
            case State.Create:
                Create();
                break;
            case State.Common:
               // SetPatrolDir();
                break;
            case State.Battle:
                myStat.Speed = Mon.Speed;
                break;
            case State.Death:
                Death();
                break;
        }

    }

    

    //State�� Battle�϶�
    public override void Battle()
    {
        //�÷��̾ �����ϸ� ����ٴ�
        if (Detect.Enemy.Count != 0)
        {
            Dir = new Vector3(Detect.Enemy[0].transform.position.x, 0.0f ,Detect.Enemy[0].transform.position.z) 
                - this.transform.position;

            if(Vector3.Distance(Detect.Enemy[0].transform.position,this.transform.position) < myStat.AttackRange)
            {
                myAnim.SetBool("IsWalk", false);
                myAnim.SetBool("IsBattle",true);

                if (!myAnim.GetBool("IsAttack"))
                { 
                    int attackType = Random.Range(0, 2);

                    switch (attackType)
                    {
                        case 0:
                            myAnim.SetTrigger("Attack01");
                            break;
                        case 1:
                            myAnim.SetTrigger("Attack02");
                            break;
                    }
                }
            }
            else
            {
                
                myAnim.SetBool("IsBattle", false);
                myAnim.SetBool("IsWalk", true);

                this.transform.position += Dir.normalized * Time.deltaTime * myStat.Speed;
            }
            
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(Dir), Time.deltaTime * 15.0f);
        }
        //�÷��̾ ������ ����� ���� ���¸� Common���� ��ȭ��Ŵ 
        else
        {
            ChangeState(State.Common);
        }
    }

    //State�� Common�϶�
    public override void Move()
    {
        myAnim.SetBool("IsWalk", true);
        //��Ʈ�� Ÿ���� 10�ʰ� �Ѿ�� ������ �ٲ�
        PatrolTime += Time.deltaTime;
        if (PatrolTime > 5.0f)
        {
            SetPatrolDir();
        }

        //���� ���Ⱚ�� ���Ͱ� 0�� �ƴϸ� ȸ����Ŵ
        if (Quaternion.LookRotation(Dir).eulerAngles != Vector3.zero)
        {
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(Dir), Time.deltaTime * 15.0f);
        }

        //������ġ�� ��� �̵���
        this.transform.position += Dir.normalized * Time.deltaTime * (myStat.Speed*0.25f);

        //�÷��̾ �����Ǹ� Battle ���·� �ٲ�
        if (Detect.Enemy.Count != 0)
        {
            ChangeState(State.Battle);
        }
    }

  

    //������ �Ծ�����
    public override void OnDamage(float Damage)
    {
        float CirDamage = Damage + myStat.DEF;
        myStat.HP -= CirDamage;
        GameObject DmObject = setDamage(CirDamage);
        Destroy(DmObject, 0.5f);
        PatrolTime = 0.0f;
        StartCoroutine(HitColor(mat));
        myAnim.SetTrigger("GetHit");
        Dir = new Vector3(Player.transform.position.x, 0.0f, Player.transform.position.z) - this.transform.position;
       
        if(mystate == State.Common)
            myStat.Speed = 6f;
    }

    
    public override void OnAttack() 
    {
        Collider[] coll;
        coll = Physics.OverlapSphere(this.transform.position, 1f, 1<<LayerMask.NameToLayer("Player"));
        foreach(Collider col in coll)
        {
            col.GetComponent<BattleSystem>()?.OnDamage(myStat.ATK);
            col.GetComponentInParent<Rigidbody>().AddForce(this.transform.forward * 200.0f);
        }
       
    }
}
