using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[CreateAssetMenu(fileName = "Canser", menuName = "ScriptableObjects/Canser")]
public class Canser : Skill
{
    [SerializeField]
    private int BarrierHp;

    private GameObject barrier;
    [SerializeField]
    public override void Init(SkillObjectPropaty skillObjectPropaty)
    {
        barrier = skillObjectPropaty.Barrier_P;
    }
    public override void Effect()
    {
        barrier.GetComponent<Barrier>().SetBarrier(BarrierHp);
    }
}
