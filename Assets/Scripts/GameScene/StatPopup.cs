using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatPopup : MonoBehaviour
{
    public Slider HP;
    public Slider MP;
    public Slider SP;
    public Image EXP;

    void Update()
    {
        HP.value = PlayerStat.Instance.CurHP / PlayerStat.Instance.MaxHP;
        MP.value = PlayerStat.Instance.CurMP / PlayerStat.Instance.MaxMP;
        SP.value = PlayerStat.Instance.CurSP / PlayerStat.Instance.MaxSP;
        EXP.fillAmount = PlayerStat.Instance.CurEXP / PlayerStat.Instance.MaxEXP;
    }
}
