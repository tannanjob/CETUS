using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AchievPanelFirstSelectObj : MonoBehaviour
{
    [SerializeField]
    private GameObject expoObj;
    private void OnEnable()
    {
        if(EventSystem.current.currentSelectedGameObject != this.gameObject)
        {
            EventSystem.current.SetSelectedGameObject(this.gameObject);
        }
    }
}
