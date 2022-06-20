using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEvents : MonoBehaviour
{
    public GameObject bullet;
    public GameObject Skillbullet;
    public Transform myMuzzle;
    public Transform Pivot;
    public AutoDetecting Detect;


    public void Shot()
    {
        GameObject obj = Instantiate(bullet, myMuzzle.position, Quaternion.Euler(new Vector3(this.transform.rotation.eulerAngles.x+90.0f, this.transform.rotation.eulerAngles.y, this.transform.rotation.eulerAngles.z)));
    }

    public void ShotEnd()
    {
        StartCoroutine(ChangeRotation(Quaternion.Euler(0, this.transform.rotation.eulerAngles.y, this.transform.rotation.eulerAngles.z)));
    }

    public void CheckMonster()
    {
        if(Detect.Monster.Count != 0)
        {
            GameObject NearMonster = Detect.Monster[0];
            float minDist = Vector3.Distance(NearMonster.transform.position, this.transform.position);

            for (int i = 1; i < Detect.Monster.Count;i++)
            {
                if(minDist > Vector3.Distance(Detect.Monster[i].transform.position, this.transform.position))
                {
                    minDist = Vector3.Distance(Detect.Monster[i].transform.position, this.transform.position);
                    NearMonster = Detect.Monster[i];
                }
            }

            StartCoroutine(ChangeRotation(Quaternion.LookRotation(NearMonster.transform.position - this.transform.position)));
        } 
    }

    IEnumerator ChangeRotation(Quaternion Rot)
    {
        float shottime = 0.0f;
        while (shottime<0.2f)
        {
            shottime += Time.deltaTime;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Rot, Time.deltaTime * 15.0f);
            
            yield return null;
        }
        
    }

    public void SkillShot()
    {
        PlayerStat.Instance.CurMP -= 30.0f;
        GameObject obj = Instantiate(Skillbullet, myMuzzle.position, Quaternion.Euler(new Vector3(this.transform.rotation.eulerAngles.x + 90.0f, this.transform.rotation.eulerAngles.y, this.transform.rotation.eulerAngles.z)));
    }
}
