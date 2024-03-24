using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    [System.Serializable]
    public class SoundData
    {
        public string name;
        public AudioClip audioClip;
        public float playedTime;    //前回再生した時間
    }
    private AudioSource[] audioSourceList = new AudioSource[30]; //AudioSourceを同時に鳴らしたい音の数だけ用意
    [HideInInspector]public AudioSource BGMsource;

    [SerializeField] private SoundData[] soundDatas;    //音声データの取得
    
    private Dictionary<string, SoundData> soundDictionary = new Dictionary<string, SoundData>(); //音声データに名前を登録
    
    [SerializeField] private float playableDistance = 0.2f;     //一度再生してから次再生出来るまでの間隔(秒)


    private void Awake()
    {
        //auidioSourceList配列の数だけAudioSourceを自分自身に生成して配列に格納
        for (var i = 0; i < audioSourceList.Length; ++i)
        {
            audioSourceList[i] = gameObject.AddComponent<AudioSource>();
        }

        //soundDictionaryにセット
        foreach (var soundData in soundDatas)
        {
            soundDictionary.Add(soundData.name, soundData);
        }
    }

    private void Update()
    {
        if(BGMsource != null && BGMsource.volume != SoundSetting.BGMVolume)
        {
            BGMsource.volume = SoundSetting.BGMVolume;
        }
    }

    //未使用のAudioSourceの取得 全て使用中の場合はnullを返却
    private AudioSource GetUnusedAudioSource()
    {
        for (var i = 0; i < audioSourceList.Length; ++i)
        {
            if (audioSourceList[i].isPlaying == false) return audioSourceList[i];
        }

        return null; 
    }

    /// <summary>
    /// 音声データの再生
    /// </summary>
    /// <param name="clip">再生したい音声データの名前</param>
    public void Play(AudioClip clip)
    {
        var audioSource = GetUnusedAudioSource();
        if (audioSource == null) return; //再生不可
        audioSource.clip = clip;
        if(audioSource.loop == true)audioSource.loop = false;
        audioSource.volume = SoundSetting.SEVolume;
        audioSource.Play();
    }

    /// <summary>
    /// 音声データのループ再生
    /// </summary>
    /// <param name="clip">再生したい音声データの名前</param>
    public void BGMPlay(AudioClip clip)
    {
        var audioSource = GetUnusedAudioSource();
        if (audioSource == null) return; //再生不可
        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.volume = SoundSetting.BGMVolume;
        BGMsource = audioSource;
        audioSource.Play();
    }

    /// <summary>
    /// BGMの変更
    /// </summary>
    /// <param name="clip">再生したい音声データの名前</param>
    public void BGMChange(AudioClip clip)
    {
        if(BGMsource == null) return;
        BGMsource.clip = clip;
        if(BGMsource.loop == false)BGMsource.loop = true;
        BGMsource.volume = SoundSetting.BGMVolume;
        BGMsource.Play();

    }

    public void BGMStop()
    {
        if (BGMsource == null) return;
        BGMsource.Stop();
    }

    /// <summary>
    /// 登録された名前で音声データの再生
    /// </summary>
    /// <param name="name">音声データに登録した名前</param>
    public void Play(string name)
    {
        if (soundDictionary.TryGetValue(name, out var soundData)) //管理用Dictionary から、別名で探索
        {
            if (Time.realtimeSinceStartup - soundData.playedTime < playableDistance) return;    //まだ再生するには早い
            soundData.playedTime = Time.realtimeSinceStartup;//次回用に今回の再生時間の保持
            Play(soundData.audioClip); //見つかったら、再生
        }
        else
        {
            Debug.LogWarning($"その別名は登録されていません:{name}");
        }
    }

    /// <summary>
    /// 登録された名前で音声データのループ再生
    /// </summary>
    /// <param name="name">音声データに登録した名前</param>
    public void LoopPlay(string name)
    {
        if (soundDictionary.TryGetValue(name, out var soundData)) //管理用Dictionary から、別名で探索
        {
            if (Time.realtimeSinceStartup - soundData.playedTime < playableDistance) return;    //まだ再生するには早い
            soundData.playedTime = Time.realtimeSinceStartup;//次回用に今回の再生時間の保持    
            BGMPlay(soundData.audioClip); //見つかったら、再生
        }
        else
        {
            Debug.LogWarning($"その別名は登録されていません:{name}");
        }
    }
    /// <summary>
    /// 登録された名前でBGM変更
    /// </summary>
    /// <param name="name">音声データに登録した名前</param>

    public void BGMChange(string name)
    {
        if (soundDictionary.TryGetValue(name, out var soundData)) //管理用Dictionary から、別名で探索
        {
            BGMChange(soundData.audioClip);//見つかったら、再生
        }
        else
        {
            Debug.LogWarning($"その別名は登録されていません:{name}");
        }
    }
}
