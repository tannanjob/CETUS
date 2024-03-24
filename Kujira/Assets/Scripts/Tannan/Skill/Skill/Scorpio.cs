using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scorpio", menuName = "ScriptableObjects/Scorpio")]
public class Scorpio : Skill
{
    [SerializeField]
    private int ScorpioDamagePoint;
    [SerializeField]
    private int ScorpioDuration = 10;
    [SerializeField]
    private float ScorpionSpeedMag = 0.5f;
    public float ScorpioSpeedMag_P { get { return ScorpionSpeedMag; } }

    private GameObject wave;
    private PlayerController playerController;
    public override void Init(SkillObjectPropaty skillObjectPropaty)
    {
        wave = skillObjectPropaty.Wave_P;
        playerController = skillObjectPropaty.Whale_P.GetComponent<PlayerController>();
    }

    public async override void Effect()
    {
        EarthManager.EarthHp -= ScorpioDamagePoint;
        wave.GetComponent<WaveShooter>().StartScorpio();

        playerController.SkillScorpion();
        await UniTask.Delay(TimeSpan.FromSeconds(ScorpioDuration));
        playerController.SkillScorpion();
    }
}
