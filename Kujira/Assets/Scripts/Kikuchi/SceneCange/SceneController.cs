using System;
using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    [SerializeField]
    private GameObject fadePrefab; //生成するフェードキャンバスを取得
    private GameObject fadeCanvas;  //生成したフェードキャンバスを取得用変数

    private FadeController fadeController; //クラス格納用変数
    public static SceneController sceneController; //シーン遷移をどこからでも呼べるように


    public bool isNowFade = false;
 

    [SerializeField]
    [Header("真っ暗のタイミングの継続時間(ミリ秒)")]
    private int fadeWaitTime = 0;

    [SerializeField]
    [Header("リザルトシーン遷移開始遅延時間(ミリ秒)")]
    private int fadeStartWaitTime = 0;

    #region シーン名取得用変数
    [Header("各シーン名入力")]
    [SerializeField] private string titleSceneName;
    [SerializeField] private string storySceneName;
    [SerializeField] private string ruleSceneName;
    [SerializeField] private string tutorialSceneName;
    [SerializeField] private string gameSceneName;
    [SerializeField] private string resultSceneName;
    [SerializeField] private string gameOverSceneName;
    [SerializeField] private string endingSceneName;
    #endregion

    //感度変更用にゲームシーンの名前を渡すやつ
    public string GameSceneName { get { return gameSceneName; } }

    /// <summary>
    /// 遷移先シーン指定用enum
    /// </summary>
    public enum SceneType
    {
        toTitle,
        toStory,
        toRule,
        toTutorial,
        toGame,
        toResult,
        toGameOver,
        toEnding,
    }



    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Start()
    {
        fadeCanvas = Instantiate(fadePrefab, gameObject.transform); //フェードキャンバスの生成
        fadeController = fadeCanvas.GetComponent<FadeController>(); //フェード処理クラスの格納
        fadeCanvas.SetActive(false);    //生成したキャンバスを非表示に
        if(sceneController == null) sceneController = this.gameObject.GetComponent<SceneController>(); //このクラスをstatic変数に格納
    }

    /// <summary>
    /// シーン遷移用関数
    /// </summary>
    /// <param name="sceneType">遷移先シーン名(enum)</param>
    public void ChangeScene(SceneType sceneType)
    {
        if (isNowFade) return;
        switch (sceneType)
        {
            case SceneType.toTitle:
                {
                    FadeToSceneChange(titleSceneName);
                    SoundPlayer.SP.BGMset.SetBGM(SceneController.SceneType.toTitle);
                }
                break;
            case SceneType.toStory:     FadeToSceneChange(storySceneName);      break;
            case SceneType.toRule:      FadeToSceneChange(ruleSceneName);       break;
            case SceneType.toTutorial:  FadeToSceneChange(tutorialSceneName);   break;
            case SceneType.toGame:
                {
                    FadeToSceneChange(gameSceneName);
                }
                break;
            case SceneType.toResult:    FadeToResultScene(resultSceneName);     break;
            case SceneType.toGameOver:  FadeToResultScene(gameOverSceneName);   break;
            case SceneType.toEnding:    FadeToSceneChange(endingSceneName);     break;
            default:break;
        }
    }


    /// <summary>
    /// フェードアウト→シーン遷移→フェードイン処理
    /// </summary>
    /// <param name="sceneName"></param>
    private async void FadeToSceneChange(string sceneName)
    {
        isNowFade = true;   //フェード中フラグオン
        fadeCanvas.SetActive(true); //フェードパネルを有効化
        fadeController.Fade(FadeController.FadeType.FadeOut);   //フェードアウト開始
        await UniTask.WaitUntil(() => fadeController.isEndFadeOut, cancellationToken: this.GetCancellationTokenOnDestroy());    //フェードアウトが完了するまで待つ
        fadeController.isEndFadeOut = false;    //フェードアウトが完了したらフラグを切る
        await UniTask.Delay(TimeSpan.FromMilliseconds(fadeWaitTime / 2));
        SceneManager.LoadScene(sceneName);  //シーン遷移
        await UniTask.Delay(TimeSpan.FromMilliseconds(fadeWaitTime / 2));
        fadeController.Fade(FadeController.FadeType.FadeIn);    //フェードイン開始
        await UniTask.WaitUntil(() => fadeController.isEndFadeIn, cancellationToken: this.GetCancellationTokenOnDestroy()); //フェードインが終わるまで待つ
        fadeController.isEndFadeIn = false; //フェードインが完了したらフラグを切る
        fadeCanvas.SetActive(false);    //フェードパネルを無効化
        isNowFade = false;  //フェード中フラグオフ
    }

    private async void FadeToResultScene(string sceneName)
    {
        isNowFade = true;   //フェード中フラグオン
        await UniTask.Delay(TimeSpan.FromMilliseconds(fadeStartWaitTime)); // 遷移待機
        fadeCanvas.SetActive(true); //フェードパネルを有効化
        fadeController.Fade(FadeController.FadeType.FadeOut);   //フェードアウト開始
        await UniTask.WaitUntil(() => fadeController.isEndFadeOut, cancellationToken: this.GetCancellationTokenOnDestroy());    //フェードアウトが完了するまで待つ
        fadeController.isEndFadeOut = false;    //フェードアウトが完了したらフラグを切る
        SceneManager.LoadScene(sceneName);  //シーン遷移
        await UniTask.Delay(TimeSpan.FromMilliseconds(fadeWaitTime / 2));
        fadeCanvas.SetActive(false);    //フェードパネルを無効化
        isNowFade = false;  //フェード中フラグオフ
    }
}
