using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    /// <summary>
    /// �t�F�[�h�^�C�v�ݒ�p
    /// </summary>
    public enum FadeType    
    {
        FadeIn,
        FadeOut
    }

    public bool isEndFadeOut = false;
    public bool isEndFadeIn = false;

    [SerializeField] private CanvasGroup canvasGroup;   //�t�F�[�h�L�����o�X�擾�p

    [SerializeField] private float fadeTime = 1;    //�t�F�[�h�ɂ����鎞�Ԑݒ�


    /// <summary>
    /// �t�F�[�h���s����
    /// </summary>
    /// <param name="fadeType">�t�F�[�h�̃^�C�v(enum)</param>
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
    /// �t�F�[�h�C������
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
    /// �t�F�[�h�A�E�g����
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
