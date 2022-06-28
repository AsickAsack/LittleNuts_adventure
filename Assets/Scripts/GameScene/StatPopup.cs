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
        HP.value = GameData.Instance.playerdata.CurHP / GameData.Instance.playerdata.MaxHP;
        MP.value = GameData.Instance.playerdata.CurMP / GameData.Instance.playerdata.MaxMP;
        SP.value = GameData.Instance.playerdata.CurSP / GameData.Instance.playerdata.MaxSP;
        EXP.fillAmount = GameData.Instance.playerdata.CurEXP / GameData.Instance.playerdata.MaxEXP;
    }
}
