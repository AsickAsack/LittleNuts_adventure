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
           switch(itemdata.ItemCode)
            {
                //슬라임
                case 10:
                    ObjectPool.Instance.ObjectManager[4].Release(this.gameObject);
                    break;
                    //터틀쉘
                case 11:
                    ObjectPool.Instance.ObjectManager[6].Release(this.gameObject);
                    break;
                    //몰밋
                case 12:
                    ObjectPool.Instance.ObjectManager[8].Release(this.gameObject);
                    break;
                default:
                    Destroy(this.gameObject);
                    break;
            }
        }
    }

    
    public void AddInventory()
    {
        GameData.Instance.EventString.Enqueue(itemdata.ItemName + " 획득!");

        if (this.itemdata.myType == ItemType.Weapon || this.itemdata.myType == ItemType.Armor || this.itemdata.myType == ItemType.Shoes)
        {
            uint UID = (uint)Random.Range(uint.MinValue,uint.MaxValue);
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
