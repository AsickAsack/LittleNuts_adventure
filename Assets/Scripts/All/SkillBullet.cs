using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBullet : MonoBehaviour
{
    

    private void Update()
    {
        this.transform.Translate(Vector3.up * Time.deltaTime * 20.0f, Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
         
            Collider[] colls;
            colls = Physics.OverlapSphere(this.transform.position, 1f,1<<LayerMask.NameToLayer("Monster"));
            foreach(Collider coll in colls)
            {
                coll.GetComponent<Monster>()?.HitColor();
                coll.GetComponent<Rigidbody>().AddExplosionForce(300,this.transform.position, 10f);
            }
            

           Destroy(this.gameObject);
            Debug.Log("ºÎ¼­Áü");
        }
    }
}
