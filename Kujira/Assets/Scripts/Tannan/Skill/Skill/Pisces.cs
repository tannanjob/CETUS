using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pisces", menuName = "ScriptableObjects/Pisces")]
public class Pisces : Skill
{
    [SerializeField]
    private float PiscesDuration = 10;
    [SerializeField]
    private float piscesSpeedMag = 1.5f;
    public float PiscesSpeedMag_P { get { return piscesSpeedMag; } }
    [SerializeField]
    private float OffsetMag = 1.25f;

    private GameObject player;
    private PlayerController playerController;
    private FollowPlayer followPlayer;
    public async override void Effect()
    {
        //カメラ引く
        followPlayer.offsetMag *= OffsetMag;
        playerController.SkillPisces();
        await UniTask.Delay(TimeSpan.FromSeconds(PiscesDuration));
        //カメラ近づく
        followPlayer.offsetMag /= OffsetMag;
        playerController.SkillPisces();
    }

    public override void Init(SkillObjectPropaty skillObjectPropaty)
    {
        player = skillObjectPropaty.Whale_P;
        playerController = player.GetComponent<PlayerController>();
        followPlayer = playerController.FollowPlayer_P;
    }
}

