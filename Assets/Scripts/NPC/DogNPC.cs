using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DogNPC : MonoBehaviour
{

    public GameObject AllBtn;
    public GameObject NewCamera;
    public GameObject NPC;
    private Quaternion myRot;
    Coroutine EndCo = null;


    private void Start()
    {
        myRot = NPC.transform.rotation;
        SoundManager.Instance.AddEffectSource(GetComponentInParent<AudioSource>());
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {

            if (EndCo != null)
                StopCoroutine(EndCo);
            AllBtn.gameObject.SetActive(true);
            
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {

            NPC.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(other.transform.position - NPC.transform.position), Time.deltaTime * 15.0f);

        }
    }



    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if(EndCo == null)
            EndCo = StartCoroutine(OriginRot());
            else
            {
                StopCoroutine(EndCo);
                EndCo = StartCoroutine(OriginRot());
            }
            AllBtn.gameObject.SetActive(false);
          

        }
    }

    IEnumerator OriginRot()
    {
        float CheckTime = 0.0f;
        while(CheckTime < 1.0f)
        {
            CheckTime += Time.deltaTime;
            NPC.transform.rotation = Quaternion.Slerp(NPC.transform.rotation, myRot, Time.deltaTime * 15.0f);
            

            yield return null;
        }
        
    }

    public void ClickBtn()
    {
        
        NewCamera.SetActive(true);
        AllBtn.gameObject.SetActive(false);
       

    }



  
}
