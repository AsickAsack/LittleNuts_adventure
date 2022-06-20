using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JetPack : MonoBehaviour, IPointerUpHandler,IPointerDownHandler
{
    public GameObject myChar;
    public ParticleSystem JetPackEffect; 
    float Limit = 0.0f; //������ �����Ҷ� ������ y��
    float LimitRange = 3.0f; // �ְ� ���� ���� ����
    bool ClickButton = false;


    public void OnPointerDown(PointerEventData eventData)
    {
        if (!myChar.GetComponent<LittleNut>().myAnim.GetBool("IsSkillShot"))
        {
            //��ư�� Ŭ�������� ����Ʈ�� ���ְ� �ִϸ��̼��� ����, �߷��� ��
            ClickButton = true;
            JetPackEffect.Play();
            myChar.GetComponent<LittleNut>().myAnim.SetTrigger("Fly");
            myChar.GetComponent<LittleNut>().myRigid.useGravity = false;


            //�������� �ƴϰ� ������ٵ��� ���ν�Ƽ�� 0�� �ٻ�ġ��� ���� �������� y���� �ְ� ���� ���̸� ������
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
            //��ư�� ���� ����Ʈ�� ���ְ� �߷��� ����, �׸��� ������ٵ��� ���ν�Ƽ���� 0���� �ʱ�ȭ �����־� addforce�� ������ ���� �ʰ���.
            ClickButton = false;
            JetPackEffect.Stop();
            myChar.GetComponent<LittleNut>().myRigid.useGravity = true;
            myChar.GetComponent<LittleNut>().myRigid.velocity = new Vector3(0, 0.1f, 0);
        }
    }

    
    void Update()
    {
        //������ٵ��� ���ν�Ƽ�� 0�� �ٻ�ġ��� �ִϸ��̼� �Ұ��� �ٲ���
        if (Mathf.Approximately(myChar.GetComponent<LittleNut>().myRigid.velocity.y, 0.0f) && myChar.GetComponent<LittleNut>().myAnim.GetBool("IsFly"))
        {
            myChar.GetComponent<LittleNut>().myAnim.SetBool("IsFly", false);
 
        }

        // �������� �ƴ϶�� SP�� ���������� �÷���
        if (!myChar.GetComponent<LittleNut>().myAnim.GetBool("IsFly") && PlayerStat.Instance.CurSP < 100.0f)
        { 
            PlayerStat.Instance.CurSP += Time.deltaTime * 5.0f;
        }

        //��ư�� ������ SP�� 0 �̻��̶�� ������ ������
        if (Input.GetMouseButton(0) && ClickButton && PlayerStat.Instance.CurSP > 0.0f)
        {
            myChar.GetComponent<LittleNut>().myAnim.SetBool("IsFly", true);
            myChar.GetComponent<LittleNut>().myRigid.AddForce(Vector3.up);
            PlayerStat.Instance.CurSP -= Time.deltaTime*10.0f;
            
            // ������ ���̰� �����س��� ���̺��� �ö󰡸� �ִ� ���ѳ��̷� ������
            if(myChar.transform.position.y > Limit)
            {
                myChar.transform.position = new Vector3(myChar.transform.position.x, Limit, myChar.transform.position.z);
            }

            // ������ sp�� 0�� �Ǹ� �߶���
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
