using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : Monster
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
                //SetPatrolDir();
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
        if(Detect.Enemy.Count != 0)
        {
            Dir = new Vector3(Detect.Enemy[0].transform.position.x, 0.0f, Detect.Enemy[0].transform.position.z)
                - this.transform.position;
            this.transform.position+= Dir.normalized * Time.deltaTime * myStat.Speed;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(Dir), Time.deltaTime * 15.0f);
        }
        //�÷��̾ ������ ����� ���� ���¸� Common���� ��ȭ��Ŵ 
        else
        {
            ChangeState(State.Common);
        }    
    }

    //��Ʋ���¿��� �÷��̾�� �ε����� ������ �������� �� 
    private void OnCollisionEnter(Collision collision)
    {
        if(mystate == State.Battle && collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Instantiate(BombEffect, this.transform.position, Quaternion.identity);
            collision.gameObject.GetComponentInChildren<BattleSystem>()?.OnDamage(myStat.ATK);
            ChangeState(State.Death);
            Destroy(this.gameObject);
            
        }
    }

    //State�� Common�϶�
    public override void Move()
    {
        //��Ʈ�� Ÿ���� 10�ʰ� �Ѿ�� ������ �ٲ�
        PatrolTime += Time.deltaTime;
        if(PatrolTime > 5.0f)
        {
            SetPatrolDir();
        }
        
        //���� ���Ⱚ�� ���Ͱ� 0�� �ƴϸ� ȸ����Ŵ
        if(Quaternion.LookRotation(Dir).eulerAngles != Vector3.zero)
        { 
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(Dir), Time.deltaTime * 15.0f);
        }

        //������ġ�� ��� �̵���
        this.transform.position += Dir.normalized * Time.deltaTime * (myStat.Speed *0.2f);
        
        //�÷��̾ �����Ǹ� Battle ���·� �ٲ�
        if (Detect.Enemy.Count != 0)
        {
            ChangeState(State.Battle);
        }
    }

    //�׾����� 
    public override void Death()
    {
        Instantiate(BombEffect, this.transform.position, Quaternion.identity);
        SoundManager.Instance.DeleteEffectSource(this.GetComponent<AudioSource>());

        if (Player.GetComponentInChildren<AutoDetecting>().Enemy.Find(x => x.gameObject == this.gameObject))
            Player.GetComponentInChildren<AutoDetecting>().Enemy.Remove(this.gameObject);

        GameData.Instance.playerdata.CurEXP += myStat.EXP;
        Destroy(this.gameObject);

    }

    //������ �Ծ�����
    public override void OnDamage(float Damage)
    {
        PatrolTime = 0.0f;
        StartCoroutine(HitColor(mat));
        Dir = new Vector3(Player.transform.position.x, 0.0f, Player.transform.position.z) - this.transform.position;
        myStat.HP -= Damage + myStat.DEF;
        if (mystate == State.Common)
            myStat.Speed = 10f;
    }

    //�δ����� OnAttack �ʿ����
    public override void OnAttack() { }
}
