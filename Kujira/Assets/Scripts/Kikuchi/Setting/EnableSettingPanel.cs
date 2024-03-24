using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableSettingPanel : MonoBehaviour
{

    public void OnClick()
    {
        SettingManager.SettingManagerInstance.EnableSettingPanel();
    }
}
