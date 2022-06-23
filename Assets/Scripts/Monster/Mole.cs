using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : Monster
{

    [SerializeField]private GameObject BombEffect;


    private void Update()
    {
        StateProcess();
    }


    public override void Battle()
    {
        
        if(Detect.Enemy.Count != 0)
        {
            Vector3 dir = Detect.Enemy[0].transform.position - this.transform.position;
            this.transform.Translate(dir.normalized * Time.deltaTime * Mon.Speed);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 15.0f);
        }
        else
        {
            ChangeState(State.Common);
        }    
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(mystate == State.Battle && collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            BombEffect.SetActive(true);
            //Destroy(this.gameObject);
        }
    }


    public override void Death()
    {
        //
    }

    public override void Move()
    {
        if (Detect.Enemy.Count != 0)
        {
            ChangeState(State.Battle);
        }
    }

    public override void OnAttack(float Damage)
    {
        throw new System.NotImplementedException();
    }

    public override void OnDamage(float Damage)
    {
        StartCoroutine(HitColor(mat));
    }
}
