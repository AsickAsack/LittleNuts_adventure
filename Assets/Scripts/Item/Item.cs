using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Item : MonoBehaviour
{
    public ItemData itemdata;
    Rigidbody myrigid = null;

    private void Awake()
    {
        myrigid = GetComponent<Rigidbody>();
    }


    void Update()
    {
        if (myrigid.velocity.y == 0.0f)
        {
            myrigid.velocity = Vector3.zero;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.transform.GetComponent<AudioSource>().PlayOneShot(SoundManager.Instance.myEffectClip[1]);
            GameData.Instance.EventString.Enqueue(itemdata.ItemName+" È¹µæ!");
            Destroy(gameObject);
        }
    }
}
