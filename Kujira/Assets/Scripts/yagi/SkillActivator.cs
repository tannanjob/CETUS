using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SkillActivator : MonoBehaviour
{
    [SerializeField] PopUpManager pum;
    [SerializeField] SkillManeger skillManager;
    [SerializeField] PlayerController pc;
    //[SerializeField] int skillKind = 0;

    //[Header("スキル")]
    //[Header("牡牛座")]
    //public float taurusDuration = 10;
    //[Header("獅子座")]
    //public float leoDuration = 10;
    //public float leoMag = 2f;
    //[Header("天秤座")]
    //public float libraDuration = 10;
    //[Header("山羊座")]
    //public float capricornDuration = 10;
    //[Header("蠍座")]
    //public float scorpionDuration = 10;
    //public float scorpionSpeedMag = 0.5f;
    //[Header("魚座")]
    //public float piscesDuration = 10;
    //public float piscesSpeedMag = 1.5f;
    //[Header("くじら座")]
    //public float cetaceanDuration = 10;
    //public float cetaceanSpeedMag = 1.5f;
    //public float cetaceanLevel = 0;

    //[Tooltip("スキル発動に必要な欠片数")][SerializeField] int skillThreshold = 0;

    void Start()
    {
    }


    void Update()
    {

    }

    public void Skill(){
            //SkillManagerがあれば
            if(skillManager != null){
                if(pc.stardustDigit >= 2){
                    skillManager.SkillInvocate(pc.swallowedStardust);
                    pc.ResetStardust();
                }
            }
    }
}
