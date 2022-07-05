using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleNut : Character
{
    public JoySticMove myJoystic;
    public GameObject myCharacter;
    public float limit = 0.0f;
    Quaternion v3Rotation = Quaternion.identity;
    private Vector3 dir = Vector3.zero;

    private void Start()
    {
        SoundManager.Instance.AddEffectSource(this.GetComponent<AudioSource>());
    }

    private void FixedUpdate()
    {
        if (!myAnim.GetBool("IsSkillShot") && !myAnim.GetBool("IsRoll"))
        {

            v3Rotation = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0);

            dir = v3Rotation * new Vector3(myJoystic.JoysticDir.x, 0, myJoystic.JoysticDir.y);


            myRigid.MovePosition(transform.position + dir * Time.deltaTime * GameData.Instance.playerdata.MoveSpeed);
        }
    }



    private void Update()
    {
        if (!myAnim.GetBool("IsSkillShot") && !myAnim.GetBool("IsRoll"))
        {

            if (myJoystic.isDrag)
            {
                myAnim.SetBool("IsRun", true);
                //myRigid.MovePosition(myJoystic.MovePos);

            }
            else
                myAnim.SetBool("IsRun", false);


            if (!(myJoystic.JoysticDir.x == 0 && myJoystic.JoysticDir.y == 0) && myJoystic.JoysticDir != Vector3.zero && !myAnim.GetBool("IsShot"))
            {
                Quaternion mylook = Quaternion.LookRotation(new Vector3(dir.x, this.transform.position.y, dir.z));
                //myRigid.MoveRotation(mylook);
                myCharacter.transform.rotation = Quaternion.Slerp(myCharacter.transform.rotation, Quaternion.Euler(0, mylook.eulerAngles.y, 0), Time.deltaTime * 15.0f);
            }
        }
    }


    public void SetShotBool()
    {
        if(!myAnim.GetBool("IsShot"))
        myAnim.SetBool("IsShot", true);
    }
}
