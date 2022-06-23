using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDetecting : MonoBehaviour
{
    public List<GameObject> Enemy;
    public LayerMask EnemyMask;
    public SphereCollider myColl;




    public void ChangeRange(float x)
    {
        myColl.radius = x;
    }



    private void OnTriggerEnter(Collider other)
    {
        
        if (EnemyMask == 1<<other.gameObject.layer)
        {
            Enemy.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (EnemyMask == 1 << other.gameObject.layer)
        {
            if (Enemy.Find(x => x.gameObject == other.gameObject))
            {
                Enemy.Remove(other.gameObject);
            }
        }
    }
}

