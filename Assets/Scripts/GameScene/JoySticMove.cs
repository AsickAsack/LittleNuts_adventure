using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoySticMove : MonoBehaviour, IPointerDownHandler , IPointerUpHandler, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    [Header("조이스틱")]
    public RectTransform JoysticBackGround;
    public RectTransform Joystic;

    [Header("캐릭터 이동")]
    public GameObject myChar;
    public bool isDrag = false;

    Vector2 startpos = Vector2.zero; //드래그가 시작됐을때의 벡터 값
    public Vector3 JoysticDir = Vector3.zero; //조이패드가 움직인 방향값을 넣을 벡터

    //드래그 시작할때 isDrag값을 켜줌
    public void OnBeginDrag(PointerEventData eventData) => isDrag = true;
   
    public void OnDrag(PointerEventData eventData)
    {

        //드래그하는 곳으로 조이스틱을 이동시켜주고 배경 밖으로 빠져나오지 않게 막음
        Joystic.position = eventData.position;

        Vector2 JoysticPos = Joystic.anchoredPosition;
        JoysticPos.x = Mathf.Clamp(JoysticPos.x, -50.0f, 50.0f);
        JoysticPos.y = Mathf.Clamp(JoysticPos.y, -50.0f, 50.0f);
        Joystic.anchoredPosition = JoysticPos;

        //현재 지점에서 시작 지점을 뺀 방향값
        JoysticDir = (eventData.position - startpos).normalized;
    }

    
    public void OnEndDrag(PointerEventData eventData)
    {
        // 드래그가 끝나면 조이스틱을 원 지점으로 되돌림
        Joystic.anchoredPosition = Vector2.zero;

        //조이스틱 방향값도 초기화 시켜줌
        JoysticDir = Vector3.zero;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //클릭했을때 조이스틱을 켜주고 클릭 지점으로 이동시킴
        JoysticBackGround.gameObject.SetActive(true);
        JoysticBackGround.position = eventData.position;

        //조이스틱 시작지점을 저장
        startpos = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //클릭을 떼면 조이스틱을 사라지게함
        JoysticBackGround.gameObject.SetActive(false);
        isDrag = false;
    }


}

   

