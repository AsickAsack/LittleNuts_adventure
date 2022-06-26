using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEvents : MonoBehaviour
{
    [Header("총 발사 관련")]
    public GameObject bullet;
    public GameObject Skillbullet;
    public Transform myMuzzle;
    public Transform Waist;
    public AutoDetecting Detect;
    public Transform myChar;

    private Quaternion LookMonsterRot;
    private float OriginRotX = 0.0f;
    private Coroutine myCo = null;
    private Animator myAnim = null;
    private GameObject NearMonster = null;
    private bool IsWaist = false;

    private void Awake()
    {
        myAnim = this.GetComponent<Animator>();
    }

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


    public void Shot()
    {    
        GameObject obj = Instantiate(bullet, myMuzzle.position, Quaternion.Euler(new Vector3(LookMonsterRot.eulerAngles.x+97f, LookMonsterRot.eulerAngles.y, LookMonsterRot.eulerAngles.z)));
    }

        


    public void TrrigerSkillShot()
    {
        if(!myAnim.GetBool("IsSkillShot") && GameData.Instance.CurMP >= 30.0f)
        {
            if(Detect.Enemy.Count != 0)
            { 
                GameData.Instance.CurMP -= 30.0f;
                myAnim.SetTrigger("SkillShot");
                CheckMonster();
            }
            else
            {
                GameData.Instance.CurMP -= 30.0f;
                myAnim.SetTrigger("SkillShot");

            }
        }
        else
        {
            //안되는 bmg
        }
        
    }



    public void SkillShot()
    {
        GameObject obj = Instantiate(Skillbullet, myMuzzle.position, Quaternion.Euler(new Vector3(LookMonsterRot.eulerAngles.x + 97f, LookMonsterRot.eulerAngles.y, LookMonsterRot.eulerAngles.z)));
        obj.GetComponent<SkillBullet>().Shot();
        this.GetComponentInParent<Rigidbody>().AddForce(-this.transform.forward * 250.0f);
    }

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

            
            
            LookMonsterRot = Quaternion.LookRotation(NearMonster.transform.position - this.transform.position);

            
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
            myChar.GetComponent<Rigidbody>().MovePosition(myChar.transform.position + this.transform.forward * 5.0f * Time.deltaTime);
            yield return null;
        }
        while (myAnim.GetBool("IsRoll"));
                 
    }




}
