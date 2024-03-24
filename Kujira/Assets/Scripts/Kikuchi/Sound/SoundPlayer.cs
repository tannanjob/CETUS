using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [HideInInspector]
    public BGMSetter BGMset;
    [HideInInspector]
    public SoundManager SM;
    [HideInInspector]
    public static SoundPlayer SP;

    private void Awake()
    {
        if(SP == null)
        {
            SP = this.gameObject.GetComponent<SoundPlayer>();
        }
        if(SM == null)
        {
            SM = this.gameObject.GetComponent<SoundManager>();
        }
        if(BGMset == null)
        {
            BGMset = this.gameObject.GetComponent<BGMSetter>();
        }
    }


}
