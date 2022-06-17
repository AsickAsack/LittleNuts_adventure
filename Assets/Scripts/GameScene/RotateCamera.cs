using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotateCamera : MonoBehaviour , IDragHandler
{
    [SerializeField]
    private Transform myPivot; //카메라를 돌릴 기준점의 트랜스폼

    [SerializeField]
    private Vector2 VerticalRotRange; // pivot x축의 최소값, 최대값

    public float CameraRotSpeed = 90.0f; //카메라를 회전 시킬 스피드 값
    public float CameraSmoothRot = 90.0f; //보간 값

    public void OnDrag(PointerEventData eventData)
    {
        //IDragHandler의 델타 값을 받아서 pivot을 회전 시킨다.
        Vector3 myCamerRot = new Vector3(myPivot.localRotation.eulerAngles.x, myPivot.localRotation.eulerAngles.y,0.0f);
        myCamerRot.x += -eventData.delta.y * Time.deltaTime * CameraRotSpeed;
        myCamerRot.y += eventData.delta.x * Time.deltaTime * CameraRotSpeed;
        myCamerRot.z = 0.0f;

        //x값이 0 이하로 떨어지면 360이 되어 회전값이 이상해지므로 180이 넘어가면 360을 빼서 맞춰준다.
        if (myCamerRot.x > 180.0f) myCamerRot.x -= 360.0f;
        
        myCamerRot.x = Mathf.Clamp(myCamerRot.x, VerticalRotRange.x, VerticalRotRange.y);
        myCamerRot.z = Mathf.Clamp(myCamerRot.z,0.0f,0.0f);

        myPivot.localRotation = Quaternion.Slerp(myPivot.localRotation, Quaternion.Euler(myCamerRot), Time.deltaTime * CameraSmoothRot);

    }

}
