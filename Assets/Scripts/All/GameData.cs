using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{

    #region ΩÃ±€≈Ê
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


    #region «√∑π¿ÃæÓ Ω∫≈›

    private int _Level = 1;
    public int Level
    {
        get => _Level;
        set
        {
            _Level = value;
            StatPoint += 3;
            SkillPoint += 3;
            //∏∆Ω∫ HP,MP,SPæ˜
        }

    }
    public int StatPoint = 0;
    public int SkillPoint = 0;
    private int _ATK = 10;
    public int ATK
    {
        get
        {
            _ATK = _ATK + ATK_Point;
            return _ATK;
        }

        set => _ATK = value;
    }

    private int _DEF = 10;
    public int DEF
    {
        get
        {
            _DEF = _DEF + DEF_Point;
            return _DEF;
        }

        set => _DEF = value;
    }
    private int _MoveSpeed = 30;
    public int MoveSpeed
    {
        get
        {
            _MoveSpeed = _MoveSpeed + Speed_Point;
            return _MoveSpeed;
        }

        set => _MoveSpeed = value;
    }
    public int ATK_Point = 0;
    public int DEF_Point = 0;
    public int Speed_Point = 0;
 

    private float _CurHP=200;
    public float CurHP
    {
        get => _CurHP;
        set => _CurHP = value;
    }
    private float _MaxHP=200;
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
    private float _CurSP=100;
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
                // √÷¥Î EXPæÓ∂ª∞‘«“¡ˆ _MaxEXP = 
            }    
        }
    }
    private float _MaxEXP = 100;
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
