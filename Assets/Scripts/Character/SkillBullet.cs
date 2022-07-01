using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBullet : MonoBehaviour
{




    public void ShotBullet(Vector3 Target, Space Where)
    {
        StartCoroutine(ShotTarget(Target, Where));
    }

    IEnumerator ShotTarget(Vector3 Target, Space Where)
    {
        float BulletTime = 0.0f;
        while (BulletTime < 10.0f)
        {
            BulletTime += Time.deltaTime;
            this.transform.Translate(Target * Time.deltaTime * 20.0f, Where);

            yield return null;
        }
        Destroy(this.gameObject);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
         
            Collider[] colls;
            colls = Physics.OverlapSphere(this.transform.position, 1f,1<<LayerMask.NameToLayer("Monster"));
            foreach(Collider coll in colls)
            {
                coll.GetComponent<BattleSystem>()?.OnDamage(GameData.Instance.playerdata.ATK); //atk넣어야함
                coll.GetComponent<Rigidbody>().AddExplosionForce(300,this.transform.position, 10f);
            }
           Destroy(this.gameObject);
        }
        else if (other.gameObject.layer != LayerMask.NameToLayer("PlayerDetect"))
        {

            Destroy(gameObject);
        }
    }
}
