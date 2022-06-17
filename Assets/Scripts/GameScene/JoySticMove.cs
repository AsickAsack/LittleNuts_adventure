using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoySticMove : MonoBehaviour, IPointerDownHandler , IPointerUpHandler, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    [Header("���̽�ƽ")]
    public RectTransform JoysticBackGround;
    public RectTransform Joystic;

    [Header("ĳ���� �̵�")]
    public GameObject myChar;
    public bool isDrag = false;

    Vector2 startpos = Vector2.zero; //�巡�װ� ���۵������� ���� ��
    public Vector3 JoysticDir = Vector3.zero; //�����е尡 ������ ���Ⱚ�� ���� ����

    //�巡�� �����Ҷ� isDrag���� ����
    public void OnBeginDrag(PointerEventData eventData) => isDrag = true;
   
    public void OnDrag(PointerEventData eventData)
    {

        //�巡���ϴ� ������ ���̽�ƽ�� �̵������ְ� ��� ������ ���������� �ʰ� ����
        Joystic.position = eventData.position;

        Vector2 JoysticPos = Joystic.anchoredPosition;
        JoysticPos.x = Mathf.Clamp(JoysticPos.x, -50.0f, 50.0f);
        JoysticPos.y = Mathf.Clamp(JoysticPos.y, -50.0f, 50.0f);
        Joystic.anchoredPosition = JoysticPos;

        //���� �������� ���� ������ �� ���Ⱚ
        JoysticDir = (eventData.position - startpos).normalized;
    }

    
    public void OnEndDrag(PointerEventData eventData)
    {
        // �巡�װ� ������ ���̽�ƽ�� �� �������� �ǵ���
        Joystic.anchoredPosition = Vector2.zero;

        //���̽�ƽ ���Ⱚ�� �ʱ�ȭ ������
        JoysticDir = Vector3.zero;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Ŭ�������� ���̽�ƽ�� ���ְ� Ŭ�� �������� �̵���Ŵ
        JoysticBackGround.gameObject.SetActive(true);
        JoysticBackGround.position = eventData.position;

        //���̽�ƽ ���������� ����
        startpos = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //Ŭ���� ���� ���̽�ƽ�� ���������
        JoysticBackGround.gameObject.SetActive(false);
        isDrag = false;
    }


}

   

