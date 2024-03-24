using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Aries", menuName = "ScriptableObjects/Aries")]
public class Aries : Skill
{
    [SerializeField]
    private float AriesSpeed;
    [SerializeField]
    private float AriesTime;

    private GameObject Meteo;
    private float originalMeteoSpeed = 3.0f;

    public override void Init(SkillObjectPropaty skillObjectPropaty)
    {
        Meteo = skillObjectPropaty.Meteo_P;
    }

    public override void Effect()
    {      
        int meteoNum = Meteo.transform.childCount;

        for (int i = 0; i < meteoNum; i++)
        {
            MeteoMove meteoMove = Meteo.transform.GetChild(i).GetComponent<MeteoMove>();
            meteoMove.MeteoSpeed = AriesSpeed;
        }

        AriesEffectTime();
    }


    private async void AriesEffectTime()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(AriesTime));

        int meteoNum = Meteo.transform.childCount;

        for (int i = 0; i < meteoNum; i++)
        {
            MeteoMove meteoMove = Meteo.transform.GetChild(i).GetComponent<MeteoMove>();
            meteoMove.MeteoSpeed = originalMeteoSpeed;
        }
    }
}
