using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDetecting : MonoBehaviour
{
    public List<GameObject> Monster;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
            Monster.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
            if (Monster.Find(x => x.gameObject == other.gameObject))
            {
                Monster.Remove(other.gameObject);
            }
        }
    }
}

