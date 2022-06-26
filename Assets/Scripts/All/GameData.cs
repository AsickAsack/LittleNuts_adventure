using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{

    #region 싱글톤
    private static GameData _instance = null;

    public static GameData Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameData>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = "GameData";
                    _instance = obj.AddComponent<GameData>();
                    DontDestroyOnLoad(obj);
                    

                }
            }
            return _instance;
        }

    }
    #endregion

    public enum Difficulty
    {
        Normal=1,Hard,Insane
    }

    public Difficulty difficulty = Difficulty.Normal;


    #region 플레이어 스텟
    public int Level = 1;
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
    private float _CurMP=100;
    public float CurMP
    {
        get => _CurMP;
        set => _CurMP = value;
    }
    private float _MaxMP=100;
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
        set
        {
            _CurEXP = value;
            if(_CurEXP > _MaxEXP)
            {
                _CurEXP = 0.0f;
                Level += 1;
                // 최대 EXP어떻게할지 _MaxEXP = 
            }    
        }
    }
    private float _MaxEXP=100;
    public float MaxEXP
    {
        get => _MaxEXP;
        set => _MaxEXP = value;
    }

    #endregion

    public bool InBaseCamp = true;
    public float BgmVolume = 0.5f;
    public float EffectVolume = 0.5f;
}
