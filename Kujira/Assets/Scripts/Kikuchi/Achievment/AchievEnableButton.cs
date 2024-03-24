using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievEnableButton : MonoBehaviour
{
    [SerializeField]
    private Canvas achievCanv;
    public static bool isFirtstActive;

    public void AchievPanelEnable()
    {
        achievCanv.gameObject.GetComponent<AchievmentPanelManager>().AchievPanelEnable();
        isFirtstActive = true;
    }
}
