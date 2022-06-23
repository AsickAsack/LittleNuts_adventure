using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterType
{
    Mole,a,b
}

[CreateAssetMenu(fileName = "MonsterStat", menuName = "MonsterData", order = int.MinValue)]
public class MonsterStat : ScriptableObject
{
    [SerializeField]public MonsterType MonsterType;
    [SerializeField]public float MaxHP;
    [SerializeField]public float HP;
    [SerializeField]public float EXP;
    [SerializeField]public float Speed;
    [SerializeField]public float ATK;
    [SerializeField]public float DEF;
    [SerializeField]public float AttackRange;
    [SerializeField]public float Drop_Rate;

}
