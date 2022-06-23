using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct MonsStat
{
    public float MaxHP;
    public float HP;
    public float Speed;
    public float ATK;
    public float DEF;
    public float AttackRange;
    public float Drop_Rate;
    public float EXP;

    public void init(float MaxHP, float HP, float Speed, float ATK, float DEF, float AttackRange, float Drop_Rate, float EXP)
    {
        this.MaxHP = MaxHP;
        this.HP = HP;
        this.Speed = Speed;
        this.ATK = ATK;
        this.DEF = DEF;
        this.AttackRange = AttackRange;
        this.Drop_Rate = Drop_Rate;
        this.EXP = EXP;

    }
}



//몬스터별로 상속받을 부모 클래스
public abstract class Monster : MonoBehaviour, BattleSystem
{
    //스크립터블 오브젝트
    [SerializeField] protected MonsterStat Mon = null;
    [SerializeField] protected AutoDetecting Detect = null;
    protected Material mat;
    private GameObject Player;
    


    public abstract void Move();
    public abstract void Battle();
    public abstract void Death();
    public abstract void OnAttack(float damage);
    public abstract void OnDamage(float damage);

    public enum State
    {
       None, Create, Common , Battle , Death
    }

    public State mystate = State.None;
    public MonsStat myStat = new MonsStat();

    private void Awake()
    {
        Player = GameObject.Find("LittleNut");
    }

    private void Start()
    {
        myStat.init(Mon.MaxHP, Mon.HP, Mon.Speed, Mon.ATK, Mon.DEF, Mon.AttackRange, Mon.Drop_Rate,Mon.EXP);
        ChangeState(State.Create);
    }

    private void Update()
    {
        StateProcess();
        if(myStat.HP < 0.0f)
        {
            ChangeState(State.Death);
        }    
    }

    public void ChangeState(State s)
    {
        if (mystate == s) return;

        mystate = s;

        switch(s)
        {
            case State.Create:
                mat = this.GetComponentInChildren<SkinnedMeshRenderer>().material;
                SoundManager.Instance.AddEffectSource(this.GetComponent<AudioSource>());
                ChangeState(State.Common);
                break;
            case State.Common:
                
                break;
            case State.Battle:
                
                break;
            case State.Death:

                SoundManager.Instance.DeleteEffectSource(this.GetComponent<AudioSource>());
                Player.GetComponentInChildren<AutoDetecting>().RemoveEnemy(this.gameObject);
                GameData.Instance.CurHP += myStat.EXP;
                Death();
                break;
        }
    }

    protected void StateProcess()
    {
        switch (mystate)
        {
            case State.Create:
                break;
            case State.Common:
                Move();
                         
                break;
            case State.Battle:
                Battle();
                if (Detect.Enemy.Count == 0)
                {
                    ChangeState(State.Common);
                }
                break;
            case State.Death:
                
                break;
        }
    }




    protected IEnumerator HitColor(Material mat)
    {
        mat.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        mat.color = Color.white;

    }

   


}
