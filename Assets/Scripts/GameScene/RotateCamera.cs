using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotateCamera : MonoBehaviour , IDragHandler
{
    [SerializeField]
    private Transform myPivot; //ī�޶� ���� �������� Ʈ������

    [SerializeField]
    private Vector2 VerticalRotRange; // pivot x���� �ּҰ�, �ִ밪

    public float CameraRotSpeed = 90.0f; //ī�޶� ȸ�� ��ų ���ǵ� ��
    public float CameraSmoothRot = 90.0f; //���� ��

    public void OnDrag(PointerEventData eventData)
    {
        //IDragHandler�� ��Ÿ ���� �޾Ƽ� pivot�� ȸ�� ��Ų��.
        Vector3 myCamerRot = new Vector3(myPivot.localRotation.eulerAngles.x, myPivot.localRotation.eulerAngles.y,0.0f);
        myCamerRot.x += -eventData.delta.y * Time.deltaTime * CameraRotSpeed;
        myCamerRot.y += eventData.delta.x * Time.deltaTime * CameraRotSpeed;
        myCamerRot.z = 0.0f;

        //x���� 0 ���Ϸ� �������� 360�� �Ǿ� ȸ������ �̻������Ƿ� 180�� �Ѿ�� 360�� ���� �����ش�.
        if (myCamerRot.x > 180.0f) myCamerRot.x -= 360.0f;
        
        myCamerRot.x = Mathf.Clamp(myCamerRot.x, VerticalRotRange.x, VerticalRotRange.y);
        myCamerRot.z = Mathf.Clamp(myCamerRot.z,0.0f,0.0f);

        myPivot.localRotation = Quaternion.Slerp(myPivot.localRotation, Quaternion.Euler(myCamerRot), Time.deltaTime * CameraSmoothRot);

    }

}
