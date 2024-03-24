using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Virgo", menuName = "ScriptableObjects/Virgo")]
public class Virgo : Skill
{
    [SerializeField]
    private int healPoint;
    public override void Init(SkillObjectPropaty skillObjectPropaty)
    {

    }
    public override void Effect()
    {
        EarthManager.EarthHp += healPoint;
    }
}
