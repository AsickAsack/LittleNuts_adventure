using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject obj = ObjectPool.Instance.ObjectManager[7].Get();
            obj.transform.position = this.transform.position;
            obj.transform.rotation = Quaternion.identity;
        }
    }

    
}
