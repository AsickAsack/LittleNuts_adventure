using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BattleSystem
{
    void OnAttack(float Damage);
    void OnDamage(float Damage);
}

public class Battle : MonoBehaviour, BattleSystem
{
    public void OnAttack(float Damage)
    {
        //
    }

    public void OnDamage(float Damage)
    {
        this.GetComponent<Animator>().SetTrigger("Hit");
        GameData.Instance.CurHP -= Damage; //방어력도 빼야함
    }
}
