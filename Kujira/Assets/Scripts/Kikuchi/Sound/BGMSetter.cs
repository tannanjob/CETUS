using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMSetter : MonoBehaviour
{

    #region BGM名
    [SerializeField]
    [Header("タイトル画面BGM名")]
    private string startBGMName;

    [SerializeField]
    [Header("ストーリー、チュートリアル画面BGM名")]
    private string storyBGMName;

    [SerializeField]
    [Header("ゲーム画面BGM名")]
    private string gameBGMName;

    [SerializeField]
    [Header("クリア画面BGM名")]
    private string clearBGMName;

    [SerializeField]
    [Header("敗北画面BGM名")]
    private string loseBGMName;

    #endregion

    private void Start()
    {
        SetBGM(SceneController.SceneType.toTitle);
    }

    public void SetBGM(SceneController.SceneType sceneType)
    {
        switch (sceneType)
        {
            case SceneController.SceneType.toTitle:
                {
                    if(SoundPlayer.SP.SM.BGMsource == null)SoundPlayer.SP.SM.LoopPlay(startBGMName);
                    else SoundPlayer.SP.SM.BGMChange(startBGMName);
                }
                break;
            case SceneController.SceneType.toStory:
                {
                    SoundPlayer.SP.SM.BGMChange(storyBGMName);
                }
                break;
            case SceneController.SceneType.toGame:
                {
                    SoundPlayer.SP.SM.BGMChange(gameBGMName);
                }
                break;
            case SceneController.SceneType.toResult:
                {
                    SoundPlayer.SP.SM.BGMChange(clearBGMName);
                }
                break;
            case SceneController.SceneType.toGameOver:
                {
                    SoundPlayer.SP.SM.BGMChange(loseBGMName);
                }
                break;
        }
    }

}
