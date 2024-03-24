using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Libra", menuName = "ScriptableObjects/Libra")]
public class Libra : Skill
{
    [SerializeField]
    private int LibraTime;
    [SerializeField]
    private int LibraDecreaseTime;

    private GameObject timeManager;
    private GameObject manager;

    public override void Init(SkillObjectPropaty skillObjectPropaty)
    {
        manager = skillObjectPropaty.Manager_P;
        timeManager = skillObjectPropaty.TimeManager_P;
    }

    public override void Effect()
    {
        manager.GetComponent<ScoreManeger>().StartLibra(LibraTime);
        timeManager.GetComponent<TimeManager>().LibraTime(LibraDecreaseTime);
    }
}
