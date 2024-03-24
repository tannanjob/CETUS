using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class AchievmentPanelManager : MonoBehaviour
{
    [SerializeField]
    public InputAction closeButton;
    [SerializeField]
    public InputAction pressButton;

    [SerializeField]
    private GameObject firstAchievObj;
    [SerializeField]
    private GameObject collectionButtonObj;

    private AchievPanelPageButton appb;

    public void AchievPanelEnable()
    {
        this.gameObject.SetActive(true);
        closeButton.Enable();
        closeButton.performed += DisableAchievPanel;
        pressButton.Enable();

        foreach (Transform item in this.transform.Find("Expo"))
        {
            item.gameObject.SetActive(false);
        }
    }

    public void DisableAchievPanel(InputAction.CallbackContext context)
    {
        closeButton.performed -= DisableAchievPanel;
        pressButton.Disable();
        closeButton.Disable();
        this.gameObject.SetActive(false);
        AchievPanelChangeIcon.isFirstGuard = true;
        EventSystem.current.SetSelectedGameObject(collectionButtonObj);
    }
}