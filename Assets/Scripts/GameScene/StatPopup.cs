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
        HP.value = GameData.Instance.CurHP / GameData.Instance.MaxHP;
        MP.value = GameData.Instance.CurMP / GameData.Instance.MaxMP;
        SP.value = GameData.Instance.CurSP / GameData.Instance.MaxSP;
        EXP.fillAmount = GameData.Instance.CurEXP / GameData.Instance.MaxEXP;
    }
}
