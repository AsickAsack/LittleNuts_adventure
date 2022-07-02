using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBullet : MonoBehaviour
{

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
        Destroy(this.gameObject);
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
            other.GetComponent<BattleSystem>()?.OnDamage(GameData.Instance.playerdata.ATK + Random.Range(-2,3));
            Destroy(this.gameObject);
            Debug.Log("ºÎ¼­Áü");
            
        }
        else if (other.gameObject.layer != LayerMask.NameToLayer("PlayerDetect"))
        {
           
            Destroy(gameObject);
            Debug.Log("ºÎ¼­Áü2");
        }
    }
}
