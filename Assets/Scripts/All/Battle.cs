using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BattleSystem
{
    void OnAttack(float Damage);
    void OnDamage(float Damage);
}

public class Battle : MonoBehaviour
{
}
