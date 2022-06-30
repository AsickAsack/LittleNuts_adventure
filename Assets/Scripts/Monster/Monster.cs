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

    //스크립터블 데이터를 MyStat으로 옮기는 함수
    public void init(float MaxHP, float HP, float Speed, float ATK, float DEF, float AttackRange, float Drop_Rate, float EXP)
    {
        this.MaxHP = MaxHP * ((int)GameData.Instance.playerdata.difficulty+1);
        this.HP = HP * ((int)GameData.Instance.playerdata.difficulty+1); 
        this.Speed = Speed * ((int)GameData.Instance.playerdata.difficulty+1); 
        this.ATK = ATK * ((int)GameData.Instance.playerdata.difficulty+1); 
        this.DEF = DEF * ((int)GameData.Instance.playerdata.difficulty+1); 
        this.AttackRange = AttackRange * ((int)GameData.Instance.playerdata.difficulty+1); 
        this.Drop_Rate = Drop_Rate * ((int)GameData.Instance.playerdata.difficulty+1); 
        this.EXP = EXP * ((int)GameData.Instance.playerdata.difficulty+1); 
        //나중에 수정하기
    }
}

//몬스터별로 상속받을 부모 클래스
public abstract class Monster : MonoBehaviour, BattleSystem
{
    //스크립터블 오브젝트
    [SerializeField] protected MonsterStat Mon = null;
    //플레이어 감지 스크립트
    [SerializeField] protected AutoDetecting Detect = null;

    
    protected Material mat;
    protected GameObject Player;
    
    private Animator _myAnim;
    protected Animator myAnim
    {
        get
        {
            _myAnim = GetComponent<Animator>();
            return _myAnim;
        }
    }

    [SerializeField] protected GameObject[] DropItem;

    //죽었을때 이펙트 
    [SerializeField] protected GameObject BombEffect;

    //패트롤 관련 변수
    protected Vector3 Dir = Vector3.zero;
    protected float PatrolTime=0.0f;

    //자식클래스에서 꼭 구현해야할 추상함수들
    public abstract void Move();
    public abstract void Battle();
    public abstract void ChangeState(State s);
    public abstract void OnAttack();
    public abstract void OnDamage(float damage);
    
    //유한상태기계 열거자 설정
    public enum State
    {
       None, Create, Common , Battle , Death
    }

    public State mystate = State.None;
    public MonsStat myStat = new MonsStat();

    private void Awake()
    {
        Player = GameObject.Find("LittleNut");
        mat = this.GetComponentInChildren<SkinnedMeshRenderer>().material;
    }

    //시작하면 Create상태로 바꿔줌
    private void Start()
    {
        ChangeState(State.Create);
    }

    //정상적으로 Create 상태가 됐을때 실행할 함수 
    public void Create()
    {
        myStat.init(Mon.MaxHP, Mon.HP, Mon.Speed, Mon.ATK, Mon.DEF, Mon.AttackRange, Mon.Drop_Rate, Mon.EXP);
        SoundManager.Instance.AddEffectSource(this.GetComponent<AudioSource>());
        SetPatrolDir();
        ChangeState(State.Common);
    }


    private void Update()
    {
        //상태별로 항상 실행시킬 프로세스
        StateProcess();
       
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
                break;

            case State.Death:
                
                break;
        }

        if (myStat.HP < 0.0f)
        {
            ChangeState(State.Death);
        }
    }


    //Common 상태일때 움직일 방향 세팅
    protected void SetPatrolDir()
    {
        PatrolTime = 0.0f;
        Dir = new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f));
    }

    //맞았을때 색깔 효과
    protected IEnumerator HitColor(Material mat)
    {
        mat.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        mat.color = Color.white;

    }

    //죽었을때 
    protected void Death()
    {
        Instantiate(BombEffect, this.transform.position, Quaternion.identity);
        myAnim.SetTrigger("Die");

        SoundManager.Instance.DeleteEffectSource(this.GetComponent<AudioSource>());
        if (Player.GetComponentInChildren<AutoDetecting>().Enemy.Find(x => x.gameObject == this.gameObject))
            Player.GetComponentInChildren<AutoDetecting>().Enemy.Remove(this.gameObject);

        GameData.Instance.playerdata.CurEXP += myStat.EXP;

        GameObject DropTem = Instantiate(DropItem[0], this.transform.position, Quaternion.identity);
        GameObject DropTem2 = Instantiate(DropItem[1], this.transform.position, Quaternion.identity);

        int RandomNum = Random.Range(0, 2);
        Vector3 RandomVector = RandomNum <= 0 ? Vector3.left : Vector3.right;

        DropTem.GetComponent<Rigidbody>().AddForce(Vector3.up * 200 + RandomVector * 100);
        DropTem2.GetComponent<Rigidbody>().AddForce(Vector3.up * 200 + RandomVector * 100);
        Destroy(this.gameObject);

    }


}
