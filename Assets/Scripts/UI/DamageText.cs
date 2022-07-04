using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour
{



    // Update is called once per frame
    private void Update()
    {
        transform.rotation = Camera.main.transform.rotation;

    }

    public void BackPool()
    {
        ObjectPool.Instance.ObjectManager[9].Release(this.gameObject);
    }
}
