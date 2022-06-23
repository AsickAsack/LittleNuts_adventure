using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//몬스터별로 상속받을 부모 클래스
public abstract class Monster : MonoBehaviour, BattleSystem
{


    //스크립터블 오브젝트
    [SerializeField] protected MonsterStat Mon = null;
    [SerializeField] protected AutoDetecting Detect = null;
    protected Material mat;



    public abstract void Move();
    public abstract void Battle();
    public abstract void Death();

    public enum State
    {
       None, Create, Common , Battle , Death
    }

    public State mystate = State.None;

    private void Start()
    {
        ChangeState(State.Create);
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
                Death();
                break;
        }
    }




    protected IEnumerator HitColor(Material mat)
    {
        mat.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        mat.color = Color.white;

    }

    public abstract void OnAttack(float damage);

    public abstract void OnDamage(float damage);
}
