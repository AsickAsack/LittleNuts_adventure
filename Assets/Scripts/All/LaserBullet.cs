using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBullet : MonoBehaviour
{

    private void Update()
    {
        this.transform.Translate(Vector3.up * Time.deltaTime * 20.0f, Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            other.GetComponent<Monster>()?.HitColor();
            Destroy(this.gameObject);
            Debug.Log("ºÎ¼­Áü");
            
        }
    }
}
