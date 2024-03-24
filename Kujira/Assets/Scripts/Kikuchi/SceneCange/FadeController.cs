using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    /// <summary>
    /// フェードタイプ設定用
    /// </summary>
    public enum FadeType    
    {
        FadeIn,
        FadeOut
    }

    public bool isEndFadeOut = false;
    public bool isEndFadeIn = false;

    [SerializeField] private CanvasGroup canvasGroup;   //フェードキャンバス取得用

    [SerializeField] private float fadeTime = 1;    //フェードにかかる時間設定


    /// <summary>
    /// フェード実行処理
    /// </summary>
    /// <param name="fadeType">フェードのタイプ(enum)</param>
    public async void Fade(FadeType fadeType)
    {
        if (fadeType == FadeType.FadeIn)
        {
            await FadeIn(this.GetCancellationTokenOnDestroy());
        }
        else
        {
            await FadeOut(this.GetCancellationTokenOnDestroy());
        }
    }

    /// <summary>
    /// フェードイン処理
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    private async UniTask FadeIn(CancellationToken token)
    {
        canvasGroup.alpha = 1f;
        while (canvasGroup.alpha > 0f)
        {
            canvasGroup.alpha -= Time.deltaTime / fadeTime;
            if (0f > canvasGroup.alpha) canvasGroup.alpha = 0f;
            await UniTask.Yield(PlayerLoopTiming.Update, token);
        }
        isEndFadeIn = true;
    }
    /// <summary>
    /// フェードアウト処理
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    private async UniTask FadeOut(CancellationToken token)
    {
        canvasGroup.alpha = 0f;
        while (canvasGroup.alpha < 1f)
        {
            canvasGroup.alpha += Time.deltaTime / fadeTime;
            if (1f < canvasGroup.alpha) canvasGroup.alpha = 1f;
            await UniTask.Yield(PlayerLoopTiming.Update, token);
        }
        isEndFadeOut = true;
    }
}
