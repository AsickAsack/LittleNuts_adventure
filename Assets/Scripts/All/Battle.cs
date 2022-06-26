using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BattleSystem
{
    void OnAttack();
    void OnDamage(float Damage);
}

public class Battle : MonoBehaviour, BattleSystem
{
    Material mat = null;

    private void Awake()
    {
        mat = this.GetComponentInChildren<SkinnedMeshRenderer>().material;
    }


    public void OnAttack()
    {
        //
    }

    public void OnDamage(float Damage)
    {
        this.GetComponent<Animator>().SetTrigger("Hit");
        StartCoroutine(HitColor(mat));
        GameData.Instance.CurHP -= Damage-GameData.Instance.DEF; 
    }


    //맞았을때 색깔 효과
    protected IEnumerator HitColor(Material mat)
    {
        mat.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        mat.color = Color.white;

    }
}
