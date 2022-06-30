using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class DropItem : MonoBehaviour
{
    public int UID;
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
            UID = Random.Range(int.MinValue, int.MaxValue);
            GameData.Instance.playerdata.myItems.Add(new Item(this.UID, this.itemdata));
            Destroy(gameObject);
        }
    }
}
