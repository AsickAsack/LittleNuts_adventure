using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool : MonoBehaviour
{

    public static ObjectPool Instance;

    public ObjectPool<GameObject>[] ObjectManager = new ObjectPool<GameObject>[10];
    public GameObject BulletPf;
    public GameObject SkillBulletPf;
    public GameObject CoinPf;
    public GameObject SlimePf;
    public GameObject SlimeEtcPf;
    public GameObject TurtleShellPf;
    public GameObject TurtleShellEtcPf;
    public GameObject MolePf;
    public GameObject MoleEtcPf;
    public GameObject DamageTextPf;

    public Transform[] folder;


    private void Awake()
    {

        Instance = this;

        Init(0, BulletPf);
        Init(1, SkillBulletPf);
        Init(2, CoinPf);
        Init(3, SlimePf);
        Init(4, SlimeEtcPf);
        Init(5, TurtleShellPf);
        Init(6, TurtleShellEtcPf);
        Init(7, MolePf);
        Init(8, MoleEtcPf);
        Init(9, DamageTextPf);
 
    }

    void Init(int index ,GameObject prefab)
    {
        ObjectManager[index] = new ObjectPool<GameObject>(
        //���ο� ������Ʈ�� �����Ҷ�
        createFunc: () =>
        {
            var CreateObj = Instantiate(prefab,folder[index]);
            return CreateObj;
        },
        //��������
        actionOnGet: (Obj) =>
        {
            Obj.gameObject.SetActive(true);
        },
        // �ٽ� ������ �ö�
        actionOnRelease: (Obj) =>

        {  
            if(QuestManager.Instance.IsQuest)
            {
                QuestManager.Instance.CheckQuest(Obj);
            }
            

            Obj.gameObject.SetActive(false);
        },
        //Ÿ�� ������Ʈ���� ������ �Լ�
        actionOnDestroy: (Obj) =>
        {
            Destroy(Obj.gameObject);
        }, maxSize: 5);
    }



}
