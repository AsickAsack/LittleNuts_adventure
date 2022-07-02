using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class DropItem : MonoBehaviour
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
            AddInventory();
            //나중에는 오브젝트 풀링으로 바꾸기
            Destroy(gameObject);
        }
    }

    
    public void AddInventory()
    {
        GameData.Instance.EventString.Enqueue(itemdata.ItemName + " 획득!");

        if (this.itemdata.myType == ItemType.Weapon || this.itemdata.myType == ItemType.Armor || this.itemdata.myType == ItemType.Shoes)
        {
            int UID = Random.Range(int.MinValue, int.MaxValue);
            GameData.Instance.playerdata.myItems.Add(new Item(UID, this.itemdata));
        }
        else
        {
            for(int i= 0; i< GameData.Instance.playerdata.myItems.Count; i++)
            {
                if (GameData.Instance.playerdata.myItems[i].itemdata == itemdata)
                {
                    GameData.Instance.playerdata.myItems[i].itemCount++;
                    return;
                }
            }
            GameData.Instance.playerdata.myItems.Add(new Item(this.itemdata));
        } 
    }
}
