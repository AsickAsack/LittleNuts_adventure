using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class LaserBullet : MonoBehaviour
{

    public void ShotBullet(Vector3 Target,Space Where)
    {
        StartCoroutine(ShotTarget(Target, Where));
    }

    IEnumerator ShotTarget(Vector3 Target, Space Where)
    {


        float BulletTime = 0.0f;
        while (BulletTime <0.5f)
        {
            BulletTime += Time.deltaTime;
            this.transform.Translate(Target * Time.deltaTime * 20.0f, Where);
            

            if(Physics.Raycast(this.transform.position,this.transform.forward,out RaycastHit hit,Time.deltaTime*20.0f,1<<LayerMask.NameToLayer("Monster")))
            {

                if (hit.transform.GetComponent<BattleSystem>() !=null)
                {
                    hit.transform.GetComponent<BattleSystem>().OnDamage(GameData.Instance.playerdata.ATK);
                    ObjectPool.Instance.ReturnBullet(this.gameObject);
                }
            }


            yield return null;
        }
        ObjectPool.Instance.ReturnBullet(this.gameObject);

    }











    /*
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
            other.GetComponent<BattleSystem>()?.OnDamage(GameData.Instance.playerdata.ATK + Random.Range(-2,3));
            ObjectPool.Instance.ObjectManager[0].Release(this.gameObject);
            

        }
        else if (other.gameObject.layer != LayerMask.NameToLayer("PlayerDetect"))
        {
            ObjectPool.Instance.ObjectManager[0].Release(this.gameObject);
          
        }
    }
    */
}
