using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Aquarius", menuName = "ScriptableObjects/Aquarius")]
public class Aquarius : Skill
{

    private GameObject water;
    public override void Init(SkillObjectPropaty skillObjectPropaty)
    {
        water = skillObjectPropaty.Water_P;
    }


    public override void Effect()
    {
        water.GetComponent<WaterShooter>().StartAquarius();
    }
}
