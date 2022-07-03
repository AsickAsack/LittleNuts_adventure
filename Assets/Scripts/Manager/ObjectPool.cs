using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{

    public static ObjectPool Instance;

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

    public Dictionary<int, Queue<GameObject>> ObjectManager = new Dictionary<int, Queue<GameObject>>();

    public Queue<GameObject> Bullet = new Queue<GameObject>();
    public Queue<GameObject> SkillBullet = new Queue<GameObject>();
    public Queue<GameObject> Coin = new Queue<GameObject>();
    public Queue<GameObject> Slime = new Queue<GameObject>();
    public Queue<GameObject> SlimeEtc = new Queue<GameObject>();
    public Queue<GameObject> TurtleShell = new Queue<GameObject>();
    public Queue<GameObject> TurtleShellEtc = new Queue<GameObject>();
    public Queue<GameObject> Mole = new Queue<GameObject>();
    public Queue<GameObject> MoleEtc = new Queue<GameObject>();
    public Queue<GameObject> DamageText = new Queue<GameObject>();


    private void Awake()
    {
        Instance = this;
        OBJInitiallize();
    }

    private void OBJInitiallize()
    {
        for(int i=0;i<5;i++)
        {
            Bullet.Enqueue(CreateInstance(BulletPf));
          //  DamageText.Enqueue(CreateInstance(DamageTextPf));
        }
        ObjectManager.Add(1, Bullet);
        /*
        for (int i = 0; i < 5; i++)
        {
            SkillBullet.Enqueue(CreateInstance(SkillBulletPf));
            Coin.Enqueue(CreateInstance(CoinPf));
            Slime.Enqueue(CreateInstance(SlimePf));
            SlimeEtc.Enqueue(CreateInstance(SlimeEtcPf));
            TurtleShell.Enqueue(CreateInstance(TurtleShellPf));
            TurtleShellEtc.Enqueue(CreateInstance(TurtleShellEtcPf));
            Mole.Enqueue(CreateInstance(MolePf));
            MoleEtc.Enqueue(CreateInstance(MoleEtcPf));

        }

       
        /*
        ObjectManager.Add("SkillBullet", SkillBullet);
        ObjectManager.Add("Coin", Coin);
        ObjectManager.Add("Slime", Slime);
        ObjectManager.Add("SlimeEtc", SlimeEtc);
        ObjectManager.Add("TurtleShell", TurtleShell);
        ObjectManager.Add("TurtleShellEtc", TurtleShellEtc);
        ObjectManager.Add("Mole", Mole);
        ObjectManager.Add("MoleEtc", MoleEtc);
        ObjectManager.Add("DamageText", DamageText);
        */
    }

    private GameObject CreateInstance(GameObject Prefab)
    {
        GameObject obj = Instantiate(Prefab, this.transform);
        obj.SetActive(false);

        return obj;
    }

    public static GameObject GetObject(int key,GameObject Prefab = null)
    {
        if (Instance.ObjectManager[key] != null)
        {
            if (Instance.ObjectManager[key].Count > 0)
            {
                GameObject obj = Instance.ObjectManager[key].Dequeue();
                obj.gameObject.SetActive(true);
                obj.transform.SetParent(null);

                return obj;

            }
            else
            {
                GameObject obj = Instance.CreateInstance(Prefab);
                obj.transform.SetParent(null);
                obj.gameObject.SetActive(true);

                return obj;

            }
        }
        else
            return null;
    }

    public Transform bulletTr;

    public static void retrunObejct(GameObject obj,int key)
    {
        

        if (Instance.ObjectManager[key] != null)
        {
            obj.SetActive(false);
            obj.transform.SetParent(Instance.transform);
            Instance.ObjectManager[key].Enqueue(obj);

        }

        
    }




}
