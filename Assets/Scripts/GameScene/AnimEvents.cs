using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AnimEvents : MonoBehaviour
{
    [Header("총 발사 관련")]
    public GameObject bullet;
    public GameObject Skillbullet;
    public Transform myMuzzle;
    public Transform BulletRot;
    public Transform Waist;
    public AutoDetecting Detect;
    public Transform myChar;



    private Quaternion LookMonsterRot;
    private Vector3 NearMonsterDir = Vector3.zero;
    private float OriginRotX = 0.0f;
    private Coroutine myCo = null;
    private Animator myAnim = null;
    private GameObject NearMonster = null;
    private bool IsWaist = false;

    private void Awake()
    {
        myAnim = this.GetComponent<Animator>();

    }

    //기본 공격 트리거
    public void TrrigerShot()
    {
       
        
        if (Detect.Enemy.Count != 0)
        {
            myAnim.SetTrigger("Shot");
            CheckMonster();
        }
        else
            myAnim.SetTrigger("Shot");
    }

    //총알 생성
    public void Shot()
    {
        if (Detect.Enemy.Count == 0)
        {
            GameObject obj = ObjectPool.Instance.GetBullet();
            obj.transform.position = myMuzzle.position;
             obj.transform.rotation = BulletRot.rotation;
            obj.SetActive(true);
            obj.GetComponent<LaserBullet>().ShotBullet(Vector3.forward,Space.Self);
        }
        else
        {
            GameObject obj = ObjectPool.Instance.GetBullet();
            obj.transform.position = myMuzzle.position;
            obj.transform.rotation = BulletRot.rotation;
            obj.SetActive(true);
            obj.GetComponent<LaserBullet>().ShotBullet(NearMonsterDir.normalized, Space.World);
        }
        this.GetComponentInParent<AudioSource>().PlayOneShot(SoundManager.Instance.myEffectClip[0]);

    }

    IEnumerator Delay()
    {

        yield return new WaitForSeconds(0.1f);
        Debug.Log("1초 기다림");
    }


    //초록색 총알 트리거, mp검사함
    public void TrrigerSkillShot()
    {
        if(!myAnim.GetBool("IsSkillShot") && GameData.Instance.playerdata.CurMP >= 30.0f)
        {
            if(Detect.Enemy.Count != 0)
            { 
                GameData.Instance.playerdata.CurMP -= 30.0f;
                myAnim.SetTrigger("SkillShot");
                CheckMonster();
            }
            else
            {
                GameData.Instance.playerdata.CurMP -= 30.0f;
                myAnim.SetTrigger("SkillShot");

            }
        }
        else
        {
            //안되는 bmg 넣기
        }
        
    }

    //스킬 총알 생성 / 캐릭터 넉백
    public void SkillShot()
    {
        if (Detect.Enemy.Count == 0)
        {

            GameObject obj = ObjectPool.Instance.ObjectManager[1].Get();
            obj.transform.position = myMuzzle.position;
            obj.transform.rotation = BulletRot.rotation;
            obj.GetComponent<SkillBullet>().ShotBullet(Vector3.forward, Space.Self);
            
        }
        else
        {

            GameObject obj = ObjectPool.Instance.ObjectManager[1].Get();
            obj.transform.position = myMuzzle.position;
            obj.transform.rotation = LookMonsterRot;
            obj.GetComponent<SkillBullet>().ShotBullet(NearMonsterDir.normalized, Space.World);
        }
        this.GetComponentInParent<Rigidbody>().AddForce(-this.transform.forward * 150.0f);
    }

    //총 쏘고 나서 허리를 되돌림
    public void ShotEnd()
    {
        IsWaist = false;
        StartCoroutine(ChangeRotation(Waist.transform,OriginRotX,Waist.transform.rotation));
    }

    private void LateUpdate()
    {
        if(IsWaist)
        {
            Waist.rotation = Quaternion.Euler(LookMonsterRot.eulerAngles.x+20.0f, Waist.eulerAngles.y, Waist.eulerAngles.z);
            //Waist.rotation = Quaternion.Slerp(Waist.rotation, Quaternion.Euler(WaistRot.eulerAngles.x, Waist.eulerAngles.y, Waist.eulerAngles.z), 0.25f);

        }
    }


    //가장 가까이에 있는 몬스터를 탐색함
    public void CheckMonster()
    {
       
            NearMonster = Detect.Enemy[0];
            float minDist = Vector3.Distance(NearMonster.transform.position, this.transform.position);

            for (int i = 0; i < Detect.Enemy.Count;i++)
            {
                if (minDist > Vector3.Distance(Detect.Enemy[i].transform.position, this.transform.position))
                {
                    minDist = Vector3.Distance(Detect.Enemy[i].transform.position, this.transform.position);
                    NearMonster = Detect.Enemy[i];

                    if (minDist <= 2.5f)
                        break;
                }
            }

            
            
        
        NearMonsterDir = NearMonster.transform.position - this.transform.position;
        LookMonsterRot = Quaternion.LookRotation(NearMonsterDir);
        //if(myChar.position.y+0.6f <= NearMonster.transform.position.y || myChar.position.y - 0.6f >= NearMonster.transform.position.y)

        IsWaist = true;
           OriginRotX = Waist.eulerAngles.x;
        

        if (myCo == null)
            { 
                myCo = StartCoroutine(ChangeRotation(this.transform, 0.0f, LookMonsterRot));
            }
            else
            { 
                StopCoroutine(myCo);
                myCo = StartCoroutine(ChangeRotation(this.transform, 0.0f, LookMonsterRot));
            }
         
    }

    //적 방향으로 방향 돌리기
    IEnumerator ChangeRotation(Transform Tr, float TargetRotX, Quaternion TargetRot)
    {
        float shottime = 0.0f;
        while (shottime < 0.2f)
        {
            shottime += Time.deltaTime;
            Tr.rotation = Quaternion.Slerp(Tr.rotation,Quaternion.Euler(TargetRotX, TargetRot.eulerAngles.y,TargetRot.eulerAngles.z), Time.deltaTime * 15.0f);

            yield return null;
        }
    }


    //구르기
    public void Roll()
    {
        if(!myAnim.GetBool("IsSkillShot") && !myAnim.GetBool("IsFly") && !myAnim.GetBool("IsRoll"))
        { 
            myAnim.SetTrigger("Roll");
            StartCoroutine(Rolling());
        }
    }
   
    IEnumerator Rolling()
    {
        do 
        {
            myChar.GetComponent<Rigidbody>().MovePosition(myChar.transform.position + this.transform.forward * 1.0f * Time.deltaTime);
            yield return null;
        }
        while (myAnim.GetBool("IsRoll"));
                 
    }




}
