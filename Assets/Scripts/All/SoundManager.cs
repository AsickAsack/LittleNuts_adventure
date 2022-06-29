using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    #region �̱���

    private static SoundManager _instance = null;

    public static SoundManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SoundManager>();
                if (_instance == null)
                {
                    GameObject obj = Instantiate(Resources.Load("SoundManager")) as GameObject;
                    //obj.name = "SoundManager";
                    _instance = obj.GetComponent<SoundManager>();
                    DontDestroyOnLoad(obj);
                }
            }
            return _instance;
        }
    }

    #endregion

    #region �����Ŭ����

    public AudioClip[] myBgmClip;
    public AudioClip[] myEffectClip;

    #endregion

    private void Start()
    {
        //SelectBGM();
    }

    #region BGM ���� ���

    private AudioSource _BgmAudio = null;

    public AudioSource BgmAudio
    {
        get
        {
            //�׻� ī�޶� �޸� ����� �ҽ��� ������
            if(_BgmAudio == null)
            _BgmAudio = Camera.main.GetComponent<AudioSource>();

            return _BgmAudio;
        }
    }

    private float _BgmVolum = 0.5f;

    //BgmVolume�� �����ϸ� BGM����� �ҽ� ������ �����ǵ���
    public float BgmVolume
    {
        get => _BgmVolum;
        set
        {
            _BgmVolum = value;
            BgmAudio.volume = _BgmVolum;
        }
    }

    public void SetBGM(AudioClip clip , bool loop = false)
    {
        BgmAudio.clip = clip;
        BgmAudio.loop = loop;
        BgmAudio.Play();
    }

    public void SelectBGM()
    {
        switch(GameData.Instance.playerdata.SaveMap)
        {
            case "���̽�ķ��":
                SetBGM(myBgmClip[1], true);
                break;
            case "�δ����� ��":
                SetBGM(myBgmClip[2], true);
                break;
        }
    }

    #endregion


    #region ����Ʈ ������ҽ� ��ɵ�

    //����,ĳ����,NPC�� ����Ʈ ������ҽ��� ������ ����Ʈ
    public List<AudioSource> EffectSource = new List<AudioSource>();
        
    private float _EffectVolume = 0.5f;

    //�� ������Ƽ�� �����Ͽ� ���� �ϸ� ��� ����Ʈ����� �ҽ��� ������ ������
    public float EffectVolume
    {
        get => _EffectVolume;
        set
        {
            _EffectVolume = value;
            ChangeEffectVolume(_EffectVolume);
        }
    }
    
    //����Ʈ ����� �ҽ��� ����Ʈ�� �־��� �Լ�
    public void AddEffectSource(AudioSource myAudio)=> EffectSource.Add(myAudio);
         
    //����Ʈ ����� �ҽ��� ����Ʈ���� ������ų �Լ�
    public void DeleteEffectSource(AudioSource myAudio) => EffectSource.Remove(myAudio);

    //���� �̵��ϰų� �� �� Ŭ���� ��ų �Լ�
    public void ClearEffectSource() => EffectSource.Clear();

    //�ϰ������� ����Ʈ�� �ִ� ����Ʈ�ҽ� ������ �ٲٴ� �Լ�
    public void ChangeEffectVolume(float V)
    {
        foreach(AudioSource Audio in EffectSource)
        {
            Audio.volume = V;
        }
    }

    #endregion
}
