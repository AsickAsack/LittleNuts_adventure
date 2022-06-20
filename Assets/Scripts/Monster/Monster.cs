using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    Material mat;
    private void Awake()
    {
        mat = this.GetComponent<MeshRenderer>().material;
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.layer == LayerMask.NameToLayer("Bullet"))
    //    {
    //        StartCoroutine(HitColor(mat));
    //    }
    //}


    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.gameObject.layer == LayerMask.NameToLayer("Bullet"))
    //    {
    //        StartCoroutine(HitColor(mat));
    //    }
    //}

    public void HitColor()
    {
        StartCoroutine(HitColor(mat));
    }


    IEnumerator HitColor(Material mat)
    {
        mat.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        mat.color = Color.white;

    }


}
