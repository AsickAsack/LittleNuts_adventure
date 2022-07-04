using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class LaserBullet : MonoBehaviour
{

    public IObjectPool<GameObject> poolToreturn;

    public void ShotBullet(Vector3 Target,Space Where)
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
        ObjectPool.Instance.ObjectManager[0].Release(this.gameObject);

    }


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
}
