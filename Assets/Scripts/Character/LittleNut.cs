using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleNut : Character
{
    public JoySticMove myJoystic;
    public GameObject myCharacter;
    public float MoveSpeed = 30.0f;
    public float limit = 0.0f;
    float test;

    private void Update()
    {
        

        if (myJoystic.isDrag)
        {
            myAnim.SetBool("IsRun", true);
            //myRigid.MovePosition(myJoystic.MovePos);

        }
        else
            myAnim.SetBool("IsRun", false);

        Quaternion v3Rotation = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0);

        Vector3 dir = v3Rotation * new Vector3(myJoystic.JoysticDir.x, 0, myJoystic.JoysticDir.y);

        this.transform.position += dir * Time.deltaTime * MoveSpeed;


        if (!(myJoystic.JoysticDir.x == 0 && myJoystic.JoysticDir.y == 0) && myJoystic.JoysticDir != Vector3.zero)
        {
            Quaternion mylook = Quaternion.LookRotation(new Vector3(dir.x, this.transform.position.y, dir.z));
            //myRigid.MoveRotation(mylook);
            myCharacter.transform.rotation = Quaternion.Slerp(myCharacter.transform.rotation, Quaternion.Euler(0, mylook.eulerAngles.y, 0), Time.deltaTime*15.0f);
        }

        

        if(Input.GetKeyDown(KeyCode.Space))
        {
            test = this.transform.position.y;
        }


        
     
      

    }

}
