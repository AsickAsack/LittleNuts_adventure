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

    //��ũ���ͺ� �����͸� MyStat���� �ű�� �Լ�
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
        //���߿� �����ϱ�
    }
}

//���ͺ��� ��ӹ��� �θ� Ŭ����
public abstract class Monster : MonoBehaviour, BattleSystem
{
    //��ũ���ͺ� ������Ʈ
    [SerializeField] protected MonsterStat Mon = null;
    //�÷��̾� ���� ��ũ��Ʈ
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

    [SerializeField] protected GameObject Coin;
    [SerializeField] protected GameObject[] DropItem;

    //�׾����� ����Ʈ 
    [SerializeField] protected GameObject BombEffect;

    //��Ʈ�� ���� ����
    protected Vector3 Dir = Vector3.zero;
    protected float PatrolTime=0.0f;

    //�ڽ�Ŭ�������� �� �����ؾ��� �߻��Լ���
    public abstract void Move();
    public abstract void Battle();
    public abstract void Death();
    public abstract void ChangeState(State s);
    public abstract void OnAttack();
    public abstract void OnDamage(float damage);
    
    //���ѻ��±�� ������ ����
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

    //�����ϸ� Create���·� �ٲ���
    private void Start()
    {
        ChangeState(State.Create);
    }

    //���������� Create ���°� ������ ������ �Լ� 
    public void Create()
    {
        myStat.init(Mon.MaxHP, Mon.HP, Mon.Speed, Mon.ATK, Mon.DEF, Mon.AttackRange, Mon.Drop_Rate, Mon.EXP);
        SoundManager.Instance.AddEffectSource(this.GetComponent<AudioSource>());
        SetPatrolDir();
        ChangeState(State.Common);
    }


    private void Update()
    {
        //���º��� �׻� �����ų ���μ���
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


    //Common �����϶� ������ ���� ����
    protected void SetPatrolDir()
    {
        PatrolTime = 0.0f;
        Dir = new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f));
    }

    //�¾����� ���� ȿ��
    protected IEnumerator HitColor(Material mat)
    {
        mat.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        mat.color = Color.white;

    }

   


}
