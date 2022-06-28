using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleShell : Monster
{


    //State가 바뀔때 실행 할 함수
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

    

    //State가 Battle일때
    public override void Battle()
    {
        //플레이어를 감지하면 따라다님
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
        //플레이어가 범위를 벗어나면 현재 상태를 Common으로 변화시킴 
        else
        {
            ChangeState(State.Common);
        }
    }

    //State가 Common일때
    public override void Move()
    {
        myAnim.SetBool("IsWalk", true);
        //패트롤 타임이 10초가 넘어가면 방향을 바꿈
        PatrolTime += Time.deltaTime;
        if (PatrolTime > 5.0f)
        {
            SetPatrolDir();
        }

        //보는 방향값의 벡터가 0이 아니면 회전시킴
        if (Quaternion.LookRotation(Dir).eulerAngles != Vector3.zero)
        {
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(Dir), Time.deltaTime * 15.0f);
        }

        //랜덤위치로 계속 이동함
        this.transform.position += Dir.normalized * Time.deltaTime * (myStat.Speed*0.25f);

        //플레이어가 감지되면 Battle 상태로 바뀜
        if (Detect.Enemy.Count != 0)
        {
            ChangeState(State.Battle);
        }
    }

    //죽었을때 
    public override void Death()
    {
        Instantiate(BombEffect, this.transform.position, Quaternion.identity);
        myAnim.SetTrigger("Die");

        SoundManager.Instance.DeleteEffectSource(this.GetComponent<AudioSource>());
        if (Player.GetComponentInChildren<AutoDetecting>().Enemy.Find(x => x.gameObject == this.gameObject))
            Player.GetComponentInChildren<AutoDetecting>().Enemy.Remove(this.gameObject);

        GameData.Instance.playerdata.CurEXP += myStat.EXP;
        Destroy(this.gameObject);

    }

    //데미지 입었을때
    public override void OnDamage(float Damage)
    {
        PatrolTime = 0.0f;
        StartCoroutine(HitColor(mat));
        myAnim.SetTrigger("GetHit");
        Dir = new Vector3(Player.transform.position.x, 0.0f, Player.transform.position.z) - this.transform.position;
        myStat.HP -= Damage - myStat.DEF;
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
