using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Cysharp.Threading.Tasks;
using System;

public class AchievPanelChangeIcon : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField]
    private Sprite achievIcon;  //実績アイコン用画像
    [SerializeField]
    private int achievNumber;   //仕様書にある実績No

    private AchievmentPanelManager apm;
    private AchevExpoSet aes;

    private InputAction pressButton;

    [SerializeField]
    private bool firstSelectObj = false;

    public static bool isFirstGuard = true;

    private bool isChangeIcon = false;


    private void Awake()
    {
        if (apm == null)
        {
            apm = this.transform.root.gameObject.GetComponent<AchievmentPanelManager>();
        }
        if(aes == null)
        {
            aes = this.transform.root.gameObject.GetComponent<AchevExpoSet>();
        }
        if (pressButton == null)
        {
            pressButton = apm.pressButton;
        }


    }
    private void OnEnable()
    {
        switch (achievNumber)
        {
            case 0: CheckIconChange(AchievementChecker.AcievInctance.isAriesAchievEnabled); break;
            case 1: CheckIconChange(AchievementChecker.AcievInctance.isTaurusAchievEnabled); break;
            case 2: CheckIconChange(AchievementChecker.AcievInctance.isGeminiAchievEnabled); break;
            case 3: CheckIconChange(AchievementChecker.AcievInctance.isCancerAchievEnabled); break;
            case 4: CheckIconChange(AchievementChecker.AcievInctance.isLeoAchievEnabled); break;
            case 5: CheckIconChange(AchievementChecker.AcievInctance.isVirgoAchievEnabled); break;
            case 6: CheckIconChange(AchievementChecker.AcievInctance.isLibraAchievEnabled); break;
            case 7: CheckIconChange(AchievementChecker.AcievInctance.isScorpioAchievEnabled); break;
            case 8: CheckIconChange(AchievementChecker.AcievInctance.isSagittariusAchievEnabled); break;
            case 9: CheckIconChange(AchievementChecker.AcievInctance.isCapricornAchievEnabled); break;
            case 10: CheckIconChange(AchievementChecker.AcievInctance.isAquariusAchievEnabled); break;
            case 11: CheckIconChange(AchievementChecker.AcievInctance.isPiscesAchievEnabled); break;
            case 12: CheckIconChange(AchievementChecker.AcievInctance.isCetusAchievEnabled); break;
            case 13: CheckIconChange(AchievementChecker.AcievInctance.isAnySkillCastAchievEnabled); break;
            case 14: CheckIconChange(AchievementChecker.AcievInctance.isMoveDistanceAvievEnabled); break;
            case 15: CheckIconChange(AchievementChecker.AcievInctance.isDashUseAvievEnabled); break;
            case 16: CheckIconChange(AchievementChecker.AcievInctance.isDashNotUseAvievEnabled); break;
            case 17: CheckIconChange(AchievementChecker.AcievInctance.isMetorHitAvievEnabled); break;
            case 18: CheckIconChange(AchievementChecker.AcievInctance.isNotMetorHitAvievEnabled); break;
            case 19: CheckIconChange(AchievementChecker.AcievInctance.isMetorDestroyAvievEnabled); break;
            case 20: CheckIconChange(AchievementChecker.AcievInctance.isMetorEatAvievEnabled); break;
            case 21: CheckIconChange(AchievementChecker.AcievInctance.isEarthHPMaxAvievEnabled); break;
            case 22: CheckIconChange(AchievementChecker.AcievInctance.isEarthHPLowAvievEnabled); break;
            case 23: CheckIconChange(AchievementChecker.AcievInctance.isAllAvievEnabled); break;
            default: break;
        }

        if(AllGetAchiev.AllAchievGet)CheckIconChange(true);
    }


    /// <summary>
    /// アチーブメントのアイコンを切り替える関数
    /// </summary>
    /// <param name="achiev"></param>
    private void CheckIconChange(bool achiev = true)
    {
        if(achiev)
        {
            var icon = this.gameObject.GetComponent<Image>();
            icon.sprite = achievIcon;
            isChangeIcon = true;
        }
        else
        {
            return;
        }
    }

    /// <summary>
    /// オブジェクトが選択された瞬間に1回呼び出される関数
    /// </summary>
    /// <param name="eventData"></param>
    public void OnSelect(BaseEventData eventData)
    {
        SoundPlayer.SP.SM.Play("ButtonSelect");
        pressButton.started += ActiveExpo;
    }

    /// <summary>
    /// オブジェクトが選択解除された瞬間に呼ばれる関数
    /// </summary>
    /// <param name="eventData"></param>
    /// <exception cref="System.NotImplementedException"></exception>
    public void OnDeselect(BaseEventData eventData)
    {
        pressButton.started -= ActiveExpo;
    }


    public void ActiveExpo(InputAction.CallbackContext context)
    {
        if (isChangeIcon)
        {
            if (isFirstGuard)
            {
                isFirstGuard = false;
                return;
            }
            pressButton.started -= ActiveExpo;
            SoundPlayer.SP.SM.Play("AnyButtonClick");
            aes.SetExpo(achievNumber);
        }
        else
        {
            SoundPlayer.SP.SM.Play("ErrorSE");
        }
    }




}
