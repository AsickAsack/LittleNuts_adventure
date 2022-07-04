using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    Rigidbody myrigid = null;

    private void Awake()
    {
        myrigid = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Destroy(gameObject, 30.0f);
    }


    void Update()
    {
        if(myrigid.velocity.y ==0.0f )
        {
            myrigid.velocity = Vector3.zero;
            
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            int money = Random.Range(50, 101);
            GameData.Instance.playerdata.money += money;
            collision.transform.GetComponent<AudioSource>().PlayOneShot(SoundManager.Instance.myEffectClip[1]);
            GameData.Instance.EventString.Enqueue("µ·À» "+money+"¿ø È¹µæ Çß½À´Ï´Ù!");
            ObjectPool.Instance.ObjectManager[2].Release(this.gameObject);
        }
    }


}
