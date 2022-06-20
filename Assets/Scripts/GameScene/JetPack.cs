using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JetPack : MonoBehaviour, IPointerUpHandler,IPointerDownHandler
{
    public GameObject myChar;
    public ParticleSystem JetPackEffect; 
    float Limit = 0.0f; //비행을 시작할때 포지션 y값
    float LimitRange = 3.0f; // 최고 비행 높이 제한
    bool ClickButton = false;


    public void OnPointerDown(PointerEventData eventData)
    {
        if (!myChar.GetComponent<LittleNut>().myAnim.GetBool("IsSkillShot"))
        {
            //버튼을 클릭했을때 이펙트를 켜주고 애니메이션을 실행, 중력을 끔
            ClickButton = true;
            JetPackEffect.Play();
            myChar.GetComponent<LittleNut>().myAnim.SetTrigger("Fly");
            myChar.GetComponent<LittleNut>().myRigid.useGravity = false;


            //비행중이 아니고 리지드바디의 벨로시티가 0의 근사치라면 현재 포지션의 y값에 최고 제한 높이를 더해줌
            if (!myChar.GetComponent<LittleNut>().myAnim.GetBool("IsFly") && Mathf.Approximately(myChar.GetComponent<LittleNut>().myRigid.velocity.y, 0.0f))
            {
                Limit = myChar.transform.position.y + LimitRange;

            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!myChar.GetComponent<LittleNut>().myAnim.GetBool("IsSkillShot"))
        {
            //버튼을 떼면 이펙트를 꺼주고 중력을 켜줌, 그리고 리지드바디의 벨로시티값을 0으로 초기화 시켜주어 addforce의 영향을 받지 않게함.
            ClickButton = false;
            JetPackEffect.Stop();
            myChar.GetComponent<LittleNut>().myRigid.useGravity = true;
            myChar.GetComponent<LittleNut>().myRigid.velocity = new Vector3(0, 0.1f, 0);
        }
    }

    
    void Update()
    {
        //리지드바디의 벨로시티가 0의 근사치라면 애니메이션 불값을 바꿔줌
        if (Mathf.Approximately(myChar.GetComponent<LittleNut>().myRigid.velocity.y, 0.0f) && myChar.GetComponent<LittleNut>().myAnim.GetBool("IsFly"))
        {
            myChar.GetComponent<LittleNut>().myAnim.SetBool("IsFly", false);
 
        }

        // 비행중이 아니라면 SP를 지속적으로 올려줌
        if (!myChar.GetComponent<LittleNut>().myAnim.GetBool("IsFly") && PlayerStat.Instance.CurSP < 100.0f)
        { 
            PlayerStat.Instance.CurSP += Time.deltaTime * 5.0f;
        }

        //버튼을 누르고 SP가 0 이상이라면 비행을 시작함
        if (Input.GetMouseButton(0) && ClickButton && PlayerStat.Instance.CurSP > 0.0f)
        {
            myChar.GetComponent<LittleNut>().myAnim.SetBool("IsFly", true);
            myChar.GetComponent<LittleNut>().myRigid.AddForce(Vector3.up);
            PlayerStat.Instance.CurSP -= Time.deltaTime*10.0f;
            
            // 비행중 높이가 제한해놓은 높이보다 올라가면 최대 제한높이로 맞춰줌
            if(myChar.transform.position.y > Limit)
            {
                myChar.transform.position = new Vector3(myChar.transform.position.x, Limit, myChar.transform.position.z);
            }

            // 비행중 sp가 0이 되면 추락함
            if(PlayerStat.Instance.CurSP < 0.1f)
            {
                ClickButton = false;
                myChar.GetComponent<LittleNut>().myRigid.velocity = new Vector3(0,0.1f,0);
                myChar.GetComponent<LittleNut>().myRigid.useGravity = true;
                PlayerStat.Instance.CurSP = 0.0f;
                JetPackEffect.Stop();
            }
        }
    }
}
