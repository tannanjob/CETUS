using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public abstract class Skill : ScriptableObject
{
    [SerializeField]
    protected AchievementChecker.AchievType type;
    [SerializeField]
    protected string s;

    public void Invocate()
    {
        AchievementChecker.AcievInctance.SkillShotCount(type);
        if(s != "")
        {
            SoundPlayer.SP.SM.Play(s);
        }

        Effect();
    }
    public abstract void Init(SkillObjectPropaty skillObjectPropaty);
    public abstract void Effect();
}
