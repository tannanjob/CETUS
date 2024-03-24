using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SoundSetting : MonoBehaviour
{ 
    [SerializeField]
    private Slider slider;  //音量設定用スライダー取得用変数
    [SerializeField]
    private TextMeshProUGUI tmPro;



    public enum SoundType   //スライダー判別用タグリスト
    {
        BGM,
        SE,
        SENS
    }
    [SerializeField]
    private SoundType soundType;    //スライダー判別タグ設定用変数

    //設定用変数
    public static float BGMVolume = 0.5f;
    public static float SEVolume = 0.5f;
    public static float UserSens = 1;

    private void Update()
    {
        if (soundType != SoundType.BGM)
        {
            tmPro.text = this.slider.value.ToString("P0");
        }
        else
        {
            float BGMnum = this.slider.value * 2;
            tmPro.text = BGMnum.ToString("P0");
        }
    }

    /// <summary>
    /// スライダーの値を対応した変数に代入
    /// </summary>
    public void OnChangeValue()
    {
        switch (soundType)
        {
            case SoundType.BGM:
                {
                    BGMVolume /= 2f;
                    BGMVolume = slider.value; 
                }
                break;
            case SoundType.SE:
                {
                    SoundPlayer.SP.SM.Play("AnyButtonClick");
                    SEVolume = slider.value; 
                }
                break;
            case SoundType.SENS: UserSens = slider.value;break;
        }
    }

}
