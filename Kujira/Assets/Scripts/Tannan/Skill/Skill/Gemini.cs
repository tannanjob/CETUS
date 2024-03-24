using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gemi", menuName = "ScriptableObjects/Gemi")]
public class Gemini : Skill
{
    [SerializeField]
    private GameObject talet;
    [SerializeField]
    float GeminiTime;
    [SerializeField]
    float GeminiSpeed;

    private GameObject meteo;
    private GameObject whale;
    public override void Init(SkillObjectPropaty skillObjectPropaty)
    {
        meteo = skillObjectPropaty.Meteo_P;
        whale = skillObjectPropaty.Whale_P;
    }
    public override void Effect()
    {
        GameObject t = Instantiate(talet);
        t.GetComponent<TaletShooter>().SetTalet(GeminiTime, GeminiSpeed, whale, meteo);
    }

}
