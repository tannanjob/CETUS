using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Leo", menuName = "ScriptableObjects/Leo")]
public class Leo : Skill
{
    [SerializeField]
    private float LeoDuration = 10;
    [SerializeField]
    private float LeoMag = 2f;
    [SerializeField]
    private float Offset = 1.25f;
    [SerializeField]
    private float DStart = 0.5f;
    [SerializeField]
    private float DEnd = 0.2f;

    private GameObject player;
    private FollowPlayer followPlayer;
    private PlayerController playerController;
    private Vector3 startScale;
    public async override void Effect()
    {
        //カメラ引く
        followPlayer.offsetMag *= Offset;
        //巨大化
        player.transform.DOScale(startScale * LeoMag, DStart).SetEase(Ease.OutBack);

        playerController.SkillLeo();
        await UniTask.Delay(TimeSpan.FromSeconds(LeoDuration));
        playerController.SkillLeo();

        //もとのサイズへ
        player.transform.DOScale(startScale, DEnd);
        //カメラ近づく
        followPlayer.offsetMag /= Offset;
    }

    public override void Init(SkillObjectPropaty skillObjectPropaty)
    {
        player = skillObjectPropaty.Whale_P;
        playerController = player.GetComponent<PlayerController>();
        followPlayer = playerController.FollowPlayer_P;
        startScale = playerController.StartScale_P;
    }
}


