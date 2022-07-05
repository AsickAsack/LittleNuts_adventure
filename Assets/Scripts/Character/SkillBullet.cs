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

            if (Physics.Raycast(this.transform.position, this.transform.forward, out RaycastHit hit, Time.deltaTime * 20.0f, 1 << LayerMask.NameToLayer("Monster")))
            {
                if (hit.transform.GetComponent<BattleSystem>() != null)
                {
                    Collider[] colls;
                    colls = Physics.OverlapSphere(this.transform.position, 1f, 1 << LayerMask.NameToLayer("Monster"));
                    foreach (Collider coll in colls)
                    {
                        coll.GetComponent<BattleSystem>()?.OnDamage(GameData.Instance.playerdata.ATK);
                        coll.GetComponent<Rigidbody>().AddExplosionForce(300, this.transform.position, 20f);
                    }
                    ObjectPool.Instance.ObjectManager[1].Release(this.gameObject);
                }
            }

            yield return null;
        }
       ObjectPool.Instance.ObjectManager[1].Release(this.gameObject);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
         
         
        }
        else if (other.gameObject.layer != LayerMask.NameToLayer("PlayerDetect"))
        { 
            ObjectPool.Instance.ObjectManager[1].Release(this.gameObject);
        }
    }
}
