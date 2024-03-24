using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Taurus", menuName = "ScriptableObjects/Taurus")]
public class Taurus : Skill
{
    [SerializeField]
    private float taurusDuration = 10;

    private PlayerController playerController;

    public async override void Effect()
    {
        playerController.SkillTaurus();
        await UniTask.Delay(TimeSpan.FromSeconds(taurusDuration));
        playerController.SkillTaurus();
    }

    public override void Init(SkillObjectPropaty skillObjectPropaty)
    {
        playerController = skillObjectPropaty.Whale_P.GetComponent<PlayerController>();
    }
}
