using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class AchievementChecker : MonoBehaviour
{

    public static AchievementChecker AcievInctance;

    #region 条件数設定用変数
    [SerializeField]
    [Header("実績No1～12(〇〇座の加護を１ゲーム中にn回使用)の条件数")]
    private int signSkillCastCondition = 5;

    [SerializeField]
    [Header("実績No13(くじら座の加護を１ゲーム中にn回使用)の条件数")]
    private int cetusSkillCastCondition = 10;

    [SerializeField]
    [Header("実績No14(能力の使用回数が合計でn回以上)の条件数")]
    private int anySkillCastCondition = 20;

    [SerializeField]
    [Header("実績No15(１ゲーム中の移動距離がnM)の条件数")]
    private int moveDistanceCondition = 20;
    [SerializeField]
    [Header("くじらの１秒間で進める距離M")]
    private int moveParSeconds = 10;

    [SerializeField]
    [Header("実績No16(ダッシュの使用回数がn回以上)の条件数")]
    private int dashCastCondition = 50;

    [SerializeField]
    [Header("実績No18(隕石衝突回数がn回以上)の条件数")]
    private int metorCollisionCondition = 10;

    [SerializeField]
    [Header("実績No20(隕石破壊回数がn回以上)の条件数")]
    private int metorDestroyCondition = 10;

    [SerializeField]
    [Header("実績No20(隕石破壊回数がn回以上)の条件数")]
    private int metorEatCondition = 10;

    

    #endregion

    /// <summary>
    /// スキル系実績判定関数の引数用
    /// </summary>
    public enum AchievType
    {
        Aries, // 牡羊座
        Taurus, //牡牛
        Gemini, //双子
        Cancer, //蟹座
        Leo, //獅子座
        Virgo, //乙女座
        Libra, //天秤座
        Scorpio, //蠍座
        Sagittarius, //射手座
        Capricorn, //山羊座
        Aquarius, //水瓶座
        Pisces, //魚座
        Cetus, //くじら座
    }

    /// <summary>
    /// 隕石系実績判定関数の引数用
    /// </summary>
    public enum MetorAchievType
    {
        Collision,
        Destroy,
        Eat,
    }

    #region アチーブメント用int

    //スキル発動回数格納用
    private int ariesCount = 0;
    private int taurusCount = 0;
    private int geminiCount = 0;
    private int cancerCount = 0;
    private int leoCount = 0;
    private int virgoCount = 0;
    private int libraCount = 0;
    private int scorpioCount = 0;
    private int sagittariusCount = 0;
    private int capricornCount = 0;
    private int aquariusCount = 0;
    private int piscesCount = 0;
    private int cetusCount = 0;
    private int anySkillCastCount = 0;

    private int dashCount = 0;　//ダッシュ発動回数格納用
    private float moveDistance = 0; //移動距離格納用

    private int metorCollisionCount = 0; //隕石衝突回数格納変数
    private int metorDestroyCount = 0; //隕石破壊回数格納変数
    private int metorEatCount = 0; //隕石を食べた回数格納変数

    private int earthMaxHP = 0; // 地球の最大HP

    #endregion

    #region アチーブメント達成フラグ

    //Skill類
     [HideInInspector] public  bool isAriesAchievEnabled = false;
     [HideInInspector] public  bool isTaurusAchievEnabled = false;
     [HideInInspector] public  bool isGeminiAchievEnabled = false;
     [HideInInspector] public  bool isCancerAchievEnabled = false;
     [HideInInspector] public  bool isLeoAchievEnabled = false;
     [HideInInspector] public  bool isVirgoAchievEnabled = false;
     [HideInInspector] public  bool isLibraAchievEnabled = false;
     [HideInInspector] public  bool isScorpioAchievEnabled = false;
     [HideInInspector] public  bool isSagittariusAchievEnabled = false;
     [HideInInspector] public  bool isCapricornAchievEnabled = false;
     [HideInInspector] public  bool isAquariusAchievEnabled = false;
     [HideInInspector] public  bool isPiscesAchievEnabled = false;
     [HideInInspector] public  bool isCetusAchievEnabled = false;
     [HideInInspector] public  bool isAnySkillCastAchievEnabled = false;
    //移動距離
     [HideInInspector] public  bool isMoveDistanceAvievEnabled = false;
    //Dash類
     [HideInInspector] public  bool isDashUseAvievEnabled = false;
     [HideInInspector] public  bool isDashNotUseAvievEnabled = false;
    //Metor類
     [HideInInspector] public  bool isMetorHitAvievEnabled = false;
     [HideInInspector] public  bool isNotMetorHitAvievEnabled = false;
     [HideInInspector] public  bool isMetorDestroyAvievEnabled = false;
     [HideInInspector] public  bool isMetorEatAvievEnabled = false;
    //地球関連
     [HideInInspector] public  bool isEarthHPMaxAvievEnabled = false;
     [HideInInspector] public  bool isEarthHPLowAvievEnabled = false;

    //上記の実績が全て獲得されたときの実績
     [HideInInspector] public  bool isAllAvievEnabled = false;

    #endregion

    #region ゲーム起動時獲得済みアチーブメントフラグ

    //Skill類
    private bool isAcquiredAriesAchievEnabled = false;
    private bool isAcquiredTaurusAchievEnabled = false;
    private bool isAcquiredGeminiAchievEnabled = false;
    private bool isAcquiredCancerAchievEnabled = false;
    private bool isAcquiredLeoAchievEnabled = false;
    private bool isAcquiredVirgoAchievEnabled = false;
    private bool isAcquiredLibraAchievEnabled = false;
    private bool isAcquiredScorpioAchievEnabled = false;
    private bool isAcquiredSagittariusAchievEnabled = false;
    private bool isAcquiredCapricornAchievEnabled = false;
    private bool isAcquiredAquariusAchievEnabled = false;
    private bool isAcquiredPisAcquiredcesAchievEnabled = false;
    private bool isAcquiredCetusAchievEnabled = false;
    private bool isAcquiredAnySkillCastAchievEnabled = false;
    //移動距離
    private bool isAcquiredMoveDisAcquiredtanceAvievEnabled = false;
    //Dash類
    private bool isAcquiredDashUseAvievEnabled = false;
    private bool isAcquiredDashNotUseAvievEnabled = false;
    //Metor類
    private bool isAcquiredMetorHitAvievEnabled = false;
    private bool isAcquiredNotMetorHitAvievEnabled = false;
    private bool isAcquiredMetorDestroyAvievEnabled = false;
    private bool isAcquiredMetorEatAvievEnabled = false;
    //地球関連
    private bool isAcquiredEarthHPMaxAvievEnabled = false;
    private bool isAcquiredEarthHPLowAvievEnabled = false;

    //上記の実績が全て獲得されたときの実績
    private bool isAcquiredAllAvievEnabled = false;
    #endregion


    #region アチーブメントアイコン画像
    //CHANGE:仕様変更で実績のリザルト表示方法が名前→画像になったため
    //変数名そのまま型だけ変更
    [SerializeField]
    [Header("牡羊座実績名")]
    private Sprite AriesAchievName = null;

    [SerializeField]
    [Header("牡牛座実績名")]
    private Sprite TaurusAchievName = null;

    [SerializeField]
    [Header("双子座実績名")]
    private Sprite GeminiAchievName = null;

    [SerializeField]
    [Header("蟹座実績名")]
    private Sprite cancerAchievName = null;

    [SerializeField]
    [Header("獅子座実績名")]
    private Sprite leoAchievName = null;

    [SerializeField]
    [Header("乙女座実績名")]
    private Sprite virgoAchievName = null;

    [SerializeField]
    [Header("天秤座実績名")]
    private Sprite libraAchievName = null;

    [SerializeField]
    [Header("蠍座実績名")]
    private Sprite scorpAchievName = null;

    [SerializeField]
    [Header("射手座実績名")]
    private Sprite sagitterAchievName = null;

    [SerializeField]
    [Header("山羊座実績名")]
    private Sprite capriconAchievName = null;

    [SerializeField]
    [Header("水瓶座実績名")]
    private Sprite aquariusAchievName = null;

    [SerializeField]
    [Header("魚座実績名")]
    private Sprite piscesAchievName = null;

    [SerializeField]
    [Header("くじら座実績名")]
    private Sprite cetusAchievName = null;

    [SerializeField]
    [Header("スキル一定以上使用実績名")]
    private Sprite anySkillAchievName = null;

    [SerializeField]
    [Header("移動距離実績名")]
    private Sprite moveDistanceAchievName = null;

    [SerializeField]
    [Header("ダッシュ使用回数実績名")]
    private Sprite dashUseAchievName = null;

    [SerializeField]
    [Header("ダッシュ不使用実績名")]
    private Sprite dashNouUseAchievName = null;

    [SerializeField]
    [Header("隕石衝突回数実績名")]
    private Sprite metorHitAchievName = null;

    [SerializeField]
    [Header("隕石非衝突実績名")]
    private Sprite notMetorHitAchievName = null;

    [SerializeField]
    [Header("隕石破壊実績名")]
    private Sprite metorDestroyAchievName = null;

    [SerializeField]
    [Header("隕石捕食回数実績")]
    private Sprite metorEatAchievName = null;

    [SerializeField]
    [Header("地球の残り体力最大実績名")]
    private Sprite earthHpMaxAchievName = null;

    [SerializeField]
    [Header("地球の残り体力最低実績名")]
    private Sprite earthHpLowAchievName = null;

    [SerializeField]
    [Header("全実績全解除実績名")]
    private Sprite allAchievGetAchievName = null;
    #endregion

    //新規獲得実績名格納用配列
    public Sprite[] newGetAchievName = new Sprite[0];

    private void Awake()
    {
        if(AcievInctance == null)
        {
            AcievInctance = this;
        }
    }



    private void Update()
    {
        if(!isAllAvievEnabled)AllAchievEnableCheck();
    }
    /// <summary>
    /// ゲーム終了時実績判定用関数　
    /// </summary>
    public void GameEndAchievChecker()
    {
        //ゲーム終了時にカウントが０なら実績解除
        if (0 == dashCount) isDashNotUseAvievEnabled = true;    //ダッシュの使用回数
        if (0 == metorCollisionCount) isNotMetorHitAvievEnabled = true; //隕石衝突回数
        if (earthMaxHP == EarthManager.EarthHp) isEarthHPMaxAvievEnabled = true; //地球のHPがmaxか
        if (earthMaxHP / 10 >= EarthManager.EarthHp) isEarthHPMaxAvievEnabled = true; //地球のHPが10%以下か

    }

    /// <summary>
    /// ゲーム開始時初期化用
    /// </summary>
    public void AvievIntInit()
    {
        ariesCount = 0;
        taurusCount = 0;
        geminiCount = 0;
        cancerCount = 0;
        leoCount = 0;
        virgoCount = 0;
        libraCount = 0;
        scorpioCount = 0;
        sagittariusCount = 0;
        capricornCount = 0;
        aquariusCount = 0;
        piscesCount = 0;
        cetusCount = 0;
        anySkillCastCount = 0;

        dashCount = 0;
        moveDistance = 0;
        metorCollisionCount = 0;
        metorDestroyCount = 0;
        metorEatCount = 0;
    }

    /// <summary>
    /// 発動回数系実績判定用関数
    /// </summary>
    /// <param name="signType">スキル名</param>
    public void SkillShotCount(AchievType signType)
    {
        switch (signType)
        {
            case AchievType.Aries:      SkillCountAchievChecker(ref ariesCount, ref isAriesAchievEnabled, signSkillCastCondition);              break;
            case AchievType.Taurus:     SkillCountAchievChecker(ref taurusCount, ref isTaurusAchievEnabled, signSkillCastCondition);            break;
            case AchievType.Gemini:     SkillCountAchievChecker(ref geminiCount, ref isGeminiAchievEnabled, signSkillCastCondition);            break;
            case AchievType.Cancer:     SkillCountAchievChecker(ref cancerCount, ref isCancerAchievEnabled, signSkillCastCondition);            break;
            case AchievType.Leo:        SkillCountAchievChecker(ref leoCount, ref isLeoAchievEnabled, signSkillCastCondition);                  break;
            case AchievType.Virgo:      SkillCountAchievChecker(ref virgoCount, ref isVirgoAchievEnabled, signSkillCastCondition);              break;
            case AchievType.Libra:      SkillCountAchievChecker(ref libraCount, ref isLibraAchievEnabled, signSkillCastCondition);              break;
            case AchievType.Scorpio:    SkillCountAchievChecker(ref scorpioCount, ref isScorpioAchievEnabled, signSkillCastCondition);          break;
            case AchievType.Sagittarius:SkillCountAchievChecker(ref sagittariusCount, ref isSagittariusAchievEnabled, signSkillCastCondition);  break;
            case AchievType.Capricorn:  SkillCountAchievChecker(ref capricornCount, ref isCapricornAchievEnabled, signSkillCastCondition);      break;
            case AchievType.Aquarius:   SkillCountAchievChecker(ref aquariusCount, ref isAquariusAchievEnabled, signSkillCastCondition);        break;
            case AchievType.Pisces:     SkillCountAchievChecker(ref piscesCount, ref isPiscesAchievEnabled, signSkillCastCondition);            break;
            case AchievType.Cetus:      SkillCountAchievChecker(ref cetusCount, ref isCetusAchievEnabled, cetusSkillCastCondition);             break;
            default: break;
        }
    }

    /// <summary>
    /// ダッシュ発動回数実績判定用関数
    /// </summary>
    public void DashCastCount()
    {
        DashCountAchievChecker(ref isDashUseAvievEnabled, dashCastCondition);
    }
    /// <summary>
    /// 移動距離実績判定用関数
    /// </summary>
    public void MoveDistanceCheck()
    {
        MoveDistanceChecker(ref isMoveDistanceAvievEnabled, moveDistanceCondition);
    }

    /// <summary>
    /// 隕石系実績判定用関数
    /// </summary>
    /// <param name="metorAchiev"></param>
    public void MetorAchievCount(MetorAchievType metorAchiev)
    {
        switch(metorAchiev)
        {
            case MetorAchievType.Collision: MetorCollisionAchievChecker(ref isMetorHitAvievEnabled, metorCollisionCondition); break;
            case MetorAchievType.Destroy:   MetorDestroyAchievChecker(ref isMetorDestroyAvievEnabled, metorDestroyCondition); break;
            case MetorAchievType.Eat:       MetorEatAchievChecker(ref isMetorEatAvievEnabled, metorEatCondition);             break;
        }
    }


    #region スキル回数系アチーブメント



    /// <summary>
    /// スキルの発動回数確認関数
    /// </summary>
    /// <param name="skillCount">いまスキルが何回打たれたかを格納</param>
    /// <param name="isActiveAchiev">スキルに対応した称号獲得条件を満たしたかどうか確認用フラグ</param>
    /// <param name="Condition">スキルに対応した称号獲得条件値</param>
    private void SkillCountAchievChecker(ref int skillCount, ref bool isActiveAchiev, int Condition)
    {
        skillCount++;
        anySkillCastCount++;
        if (skillCount >= Condition && !isActiveAchiev)
        {
            isActiveAchiev = true;
            ScoreManeger.Instance.AchieveScore();
        }
        if (anySkillCastCount >= anySkillCastCondition && !isAnySkillCastAchievEnabled)
        {
            isAnySkillCastAchievEnabled = true;
            ScoreManeger.Instance.AchieveScore();
        }

    }

    #endregion

    #region クジラ関係アチーブメント

    //ダッシュ

    /// <summary>
    /// ダッシュ発動回数確認関数
    /// </summary>
    /// <param name="isActiveAchiev"></param>
    /// <param name="Condition"></param>
    private void DashCountAchievChecker(ref bool isActiveAchiev, int Condition)
    {
        dashCount++;
        if (dashCount >= Condition && !isActiveAchiev)
        {
            isActiveAchiev = true;
            ScoreManeger.Instance.AchieveScore();
        }
    }

    //移動距離

    private void MoveDistanceChecker(ref bool isActiveAchiev, int Condition)
    {
        moveDistance += Time.deltaTime * moveParSeconds;
        if (moveDistance >= Condition && !isActiveAchiev)
        {
            isActiveAchiev = true;
            ScoreManeger.Instance.AchieveScore();
        }
    }

    //隕石



    /// <summary>
    /// 隕石衝突回数確認関数
    /// </summary>
    /// <param name="isActiveAchiev"></param>
    /// <param name="Condition"></param>
    private void MetorCollisionAchievChecker(ref bool isActiveAchiev, int Condition)
    {
        metorCollisionCount++;
        if (metorCollisionCount >= Condition && !isActiveAchiev)
        {
            isActiveAchiev = true;
            ScoreManeger.Instance.AchieveScore();
        }

        }

    /// <summary>
    /// 隕石破壊回数確認関数
    /// </summary>
    /// <param name="isActiveAchiev"></param>
    /// <param name="Condition"></param>
    private void MetorDestroyAchievChecker(ref bool isActiveAchiev, int Condition)
    {
        metorDestroyCount++;
        if (metorDestroyCount >= Condition && !isActiveAchiev)
        {
            isActiveAchiev = true;
            ScoreManeger.Instance.AchieveScore();
        }

    }

    /// <summary>
    /// 隕石を食べた回数確認関数
    /// </summary>
    /// <param name="isActiveAchiev"></param>
    /// <param name="Condition"></param>
    private void MetorEatAchievChecker(ref bool isActiveAchiev, int Condition)
    {
        metorEatCount++;
        if (metorEatCount >= Condition && !isActiveAchiev)
        {
            isActiveAchiev = true;
            ScoreManeger.Instance.AchieveScore();
        }
    }

    #endregion

    /// <summary>
    /// 全実績解除実績判定用
    /// </summary>
    private void AllAchievEnableCheck()
    {
        if( isActiveAndEnabled &&
            isTaurusAchievEnabled &&
            isGeminiAchievEnabled &&
            isCancerAchievEnabled &&
            isCancerAchievEnabled &&
            isVirgoAchievEnabled &&
            isLibraAchievEnabled &&
            isScorpioAchievEnabled &&
            isSagittariusAchievEnabled &&
            isCapricornAchievEnabled &&
            isAquariusAchievEnabled &&
            isPiscesAchievEnabled &&
            isCetusAchievEnabled &&
            isAnySkillCastAchievEnabled &&
            isMoveDistanceAvievEnabled &&
            isDashUseAvievEnabled &&
            isDashNotUseAvievEnabled &&
            isMetorHitAvievEnabled &&
            isNotMetorHitAvievEnabled &&
            isMetorDestroyAvievEnabled &&
            isMetorEatAvievEnabled &&
            isEarthHPMaxAvievEnabled &&
            isEarthHPLowAvievEnabled
            )
        {
            isAllAvievEnabled = true;
        }
    }

    /// <summary>
    /// ゲーム起動時に獲得済みアチーブメントの取得
    /// </summary>
    private void StartCheckAchiev()
    {
        // TODO
    }

    /// <summary>
    /// 新規獲得アチーブメントのチェック処理
    /// </summary>
    private void AchievaAquiredCheck(ref bool acquired, bool released, Sprite achievName)
    {
        if(acquired == false && released == true)
        {
            Array.Resize(ref newGetAchievName, newGetAchievName.Length + 1);
            newGetAchievName[newGetAchievName.Length - 1] = achievName;
            acquired = true;
        }
    }

    /// <summary>
    /// 新規獲得実績の確認用関数
    /// </summary>
    /// <returns></returns>
    public Sprite[] NewGetAchievCheck()
    {
        AchievaAquiredCheck(ref isAcquiredAriesAchievEnabled, isAriesAchievEnabled, AriesAchievName);
        AchievaAquiredCheck(ref isAcquiredTaurusAchievEnabled, isTaurusAchievEnabled, TaurusAchievName);
        AchievaAquiredCheck(ref isAcquiredGeminiAchievEnabled , isGeminiAchievEnabled, GeminiAchievName);
        AchievaAquiredCheck(ref isAcquiredCancerAchievEnabled, isCancerAchievEnabled, cancerAchievName);
        AchievaAquiredCheck(ref isAcquiredLeoAchievEnabled, isLeoAchievEnabled, leoAchievName);
        AchievaAquiredCheck(ref isAcquiredVirgoAchievEnabled, isVirgoAchievEnabled, virgoAchievName);
        AchievaAquiredCheck(ref isAcquiredLibraAchievEnabled, isLibraAchievEnabled, libraAchievName);
        AchievaAquiredCheck(ref isAcquiredScorpioAchievEnabled, isScorpioAchievEnabled, scorpAchievName);
        AchievaAquiredCheck(ref isAcquiredSagittariusAchievEnabled, isSagittariusAchievEnabled, sagitterAchievName);
        AchievaAquiredCheck(ref isAcquiredCapricornAchievEnabled, isCapricornAchievEnabled, capriconAchievName);
        AchievaAquiredCheck(ref isAcquiredAquariusAchievEnabled, isAquariusAchievEnabled, aquariusAchievName);
        AchievaAquiredCheck(ref isAcquiredPisAcquiredcesAchievEnabled, isPiscesAchievEnabled, piscesAchievName);
        AchievaAquiredCheck(ref isAcquiredCetusAchievEnabled, isCetusAchievEnabled, cetusAchievName);

        AchievaAquiredCheck(ref isAcquiredAnySkillCastAchievEnabled, isAnySkillCastAchievEnabled, anySkillAchievName);

        AchievaAquiredCheck(ref isAcquiredMoveDisAcquiredtanceAvievEnabled, isMoveDistanceAvievEnabled, moveDistanceAchievName);

        AchievaAquiredCheck(ref isAcquiredDashUseAvievEnabled, isDashUseAvievEnabled, dashUseAchievName);
        AchievaAquiredCheck(ref isAcquiredDashNotUseAvievEnabled, isDashNotUseAvievEnabled, dashNouUseAchievName);

        AchievaAquiredCheck(ref isAcquiredMetorHitAvievEnabled, isMetorHitAvievEnabled, metorHitAchievName);
        AchievaAquiredCheck(ref isAcquiredNotMetorHitAvievEnabled, isNotMetorHitAvievEnabled, notMetorHitAchievName);
        AchievaAquiredCheck(ref isAcquiredMetorDestroyAvievEnabled, isMetorDestroyAvievEnabled, metorDestroyAchievName);
        AchievaAquiredCheck(ref isAcquiredMetorEatAvievEnabled, isMetorEatAvievEnabled, metorEatAchievName);

        AchievaAquiredCheck(ref isAcquiredEarthHPMaxAvievEnabled, isEarthHPMaxAvievEnabled, earthHpMaxAchievName);
        AchievaAquiredCheck(ref isAcquiredEarthHPLowAvievEnabled, isEarthHPLowAvievEnabled, earthHpLowAchievName);

        AchievaAquiredCheck(ref isAcquiredAllAvievEnabled, isAllAvievEnabled, allAchievGetAchievName);

        return newGetAchievName;
    }




}
