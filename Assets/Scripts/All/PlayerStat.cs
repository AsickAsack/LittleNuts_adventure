using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{

    #region ΩÃ±€≈Ê
    private static PlayerStat _instance = null;

    public static PlayerStat Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PlayerStat>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = "PlayerManager";
                    _instance = obj.AddComponent<PlayerStat>();
                    DontDestroyOnLoad(obj);
                    

                }
            }
            return _instance;
        }

    }
    #endregion

    private float _CurHP=100;
    public float CurHP
    {
        get => _CurHP;
        set => _CurHP = value;
    }
    private float _MaxHP=80;
    public float MaxHP
    {
        get => _MaxHP;
        set => _MaxHP = value;
    }
    private float _CurMP=40;
    public float CurMP
    {
        get => _CurMP;
        set => _CurMP = value;
    }
    private float _MaxMP;
    public float MaxMP
    {
        get => _MaxMP;
        set => _MaxMP = value;
    }
    private float _CurSP=70;
    public float CurSP
    {
        get => _CurSP;
        set
        {
           
            _CurSP = value;
            
        }
    }
    private float _MaxSP=100;
    public float MaxSP
    {
        get => _MaxSP;
        set => _MaxSP = value;
    }
    private float _CurEXP = 50;
    public float CurEXP
    {
        get => _CurEXP;
        set => _CurEXP = value;
    }
    private float _MaxEXP=100;
    public float MaxEXP
    {
        get => _MaxEXP;
        set => _MaxEXP = value;
    }


}
