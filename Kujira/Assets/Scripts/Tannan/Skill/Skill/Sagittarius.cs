using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sagittarius", menuName = "ScriptableObjects/Sagittarius")]
public class Sagittarius : Skill
{
    [SerializeField]
    private float SagittariusBulletTime;
    [SerializeField]
    private float SagittariusLength;
    [SerializeField]
    private float SagittariusTime;

    private BulletShooter bulletShooter;
    public override void Init(SkillObjectPropaty skillObjectPropaty)
    {
        bulletShooter = skillObjectPropaty.Bullet_P.GetComponent<BulletShooter>();
        
    }
    public override void Effect()
    {
        bulletShooter.StartSagittarius(SagittariusBulletTime, SagittariusLength);
        SagittariusEffectTime();
    }
   
    private async void SagittariusEffectTime()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(SagittariusTime));

        bulletShooter.EndSagittarius();
    }
}
