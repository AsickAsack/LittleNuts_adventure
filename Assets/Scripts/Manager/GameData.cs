using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Newtonsoft.Json;
using System.IO;

[System.Serializable]
public class GameData : MonoBehaviour
{

    #region ?̱???
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

    public PlayerData playerdata = new PlayerData();
    public SaveData savedata = new SaveData();
    public Queue<string> EventString = new Queue<string>();
    public UnityAction LevelUpAction=null;

    private void Update()
    {
        playerdata.PlayTime += Time.deltaTime;
    }

    public void Save(object SaveData,string SaveFileName)
    {
        string data = JsonConvert.SerializeObject(SaveData);
        File.WriteAllText(Application.persistentDataPath + SaveFileName, data);
    }

    public void Load(string LoadFileName)
    {
        string data = File.ReadAllText(Application.persistentDataPath + LoadFileName);
        playerdata = JsonConvert.DeserializeObject<PlayerData>(data);
    }

    public void LoadIntroData()
    {
        string data = File.ReadAllText(Application.persistentDataPath + "IntroData.json");
        savedata = JsonConvert.DeserializeObject<SaveData>(data);
    }

    public void Set_SaveData(int index)
    {
        if((int)((playerdata.PlayTime / 60) / 60) == 0)
        {
            playerdata.PlayTimeTx = ((int)(playerdata.PlayTime / 60)).ToString() + "  ??";
        }
        else 
        {
            playerdata.PlayTimeTx = ((int)((playerdata.PlayTime / 60) / 60)).ToString() + " ?ð?" + ((int)(playerdata.PlayTime / 60)).ToString() + "  ??";
        }
        
        playerdata.SaveDate = System.DateTime.Now.ToString();



        savedata.SaveData_PlayTime[index] = playerdata.PlayTimeTx;
        savedata.SaveData_date[index] = playerdata.SaveDate;
        savedata.SaveData_Map[index] = playerdata.SaveMap;


        ScreenCapture.CaptureScreenshot(Application.persistentDataPath + "ScreenShot" + index + ".png");
        

        Save(savedata,"IntroData.json");
        Save(playerdata, "GameData"+index+".json");

    }

    [System.Serializable]
    public class SaveData
    {
        public string[] SaveData_date = new string[3];
        public string[] SaveData_PlayTime = new string[3];
        public string[] SaveData_Map = new string[3];
     

    }


    [System.Serializable]
    public class PlayerData
    {
        public enum Difficulty
        {
            Normal = 0, Hard, Insane
        }

        public Difficulty difficulty = Difficulty.Normal;
        public List<Item> myItems = new List<Item>();


        #region ?÷??̾? ????

        private int _Level = 1;
        public int Level
        {
            get => _Level;
            set
            {
                GameData.Instance.LevelUpAction?.Invoke();
                if(value != _Level)
                {
                    StatPoint += 3;
                    SkillPoint += 3;
                }

                _Level = value;

                GameData.Instance.EventString.Enqueue("???? " + Level + "?? ?Ǿ????ϴ?!");
                //?ƽ? HP,MP,SP??
            }

        }
        public int StatPoint = 0;
        public int SkillPoint = 1;
        private int _ATK = 10;
        public int ATK
        {
            get
            {
                
                return _ATK + ATK_Point;
            }

            set => _ATK = value;
        }

        private int _DEF = 10;
        public int DEF
        {
            get
            {
                
                return _DEF + DEF_Point;
            }

            set => _DEF = value;
        }
        private float _MoveSpeed = 3;
        public float MoveSpeed
        {
            get
            {
                
                return _MoveSpeed+(float)(Speed_Point * 0.05f);
            }

            set => _MoveSpeed = value;
        }
        //?????ӿ? ????
        public float StatSpeed
        {
            get
            {
                return _MoveSpeed + Speed_Point;
            }
            private set => _MoveSpeed = value;
        }



        public int ATK_Point = 0;
        public int DEF_Point = 0;
        public int Speed_Point = 0;


        private float _CurHP = 200;
        public float CurHP
        {
            get => _CurHP;
            set => _CurHP = value;
        }
        private float _MaxHP = 200;
        public float MaxHP
        {
            get => _MaxHP;
            set => _MaxHP = value;
        }
        private float _CurMP = 100;
        public float CurMP
        {
            get => _CurMP;
            set => _CurMP = value;
        }
        private float _MaxMP = 100;
        public float MaxMP
        {
            get => _MaxMP;
            set => _MaxMP = value;
        }
        private float _CurSP = 100;
        public float CurSP
        {
            get => _CurSP;
            set
            {

                _CurSP = value;

            }
        }
        private float _MaxSP = 100;
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
                if (_CurEXP > _MaxEXP)
                {
                    float temp = _CurEXP - _MaxEXP;
                    _CurEXP = temp;
                    Level += 1;
                    // ?ִ? EXP??????? _MaxEXP = 
                }
            }
        }
        private float _MaxEXP = 100;
        public float MaxEXP
        {
            get => _MaxEXP;
            set => _MaxEXP = value;
        }

        public int money = 0;



        #endregion

        public int StoryProcess = 1;
        public bool InBaseCamp = true;
        public float BgmVolume = 0.5f;
        public float EffectVolume = 0.5f;

        public Item CurDefenceSphere;
        public Item CurWeapon;
        public Item CurShoes;


        #region ???庯????

        public float PlayTime;
        public string PlayTimeTx;
        public string SaveDate;
        public string SaveMap = "???̽? ķ??";
        





     


        #endregion
    }


}

