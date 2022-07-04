using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstMapCollider : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        GameData.Instance.EventString.Enqueue("문독전사에게 퀘스트를\n 받아야 합니다!");
    }
}
