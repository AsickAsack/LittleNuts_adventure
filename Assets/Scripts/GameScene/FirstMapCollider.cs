using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstMapCollider : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        GameData.Instance.EventString.Enqueue("�������翡�� ����Ʈ��\n �޾ƾ� �մϴ�!");
    }
}
