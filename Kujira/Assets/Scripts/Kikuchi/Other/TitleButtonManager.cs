using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TitleButtonManager : MonoBehaviour
{
    [SerializeField]
    private GameObject startButtonObj;
    [SerializeField]
    private GameObject achievButtonObj;
    [SerializeField]
    private GameObject settingButtonObj;
    [SerializeField]
    private GameObject endButtonObj;

    private Button startButton;
    private Button achievButton;
    private Button settingButton;
    private Button endButton;


    [SerializeField]
    private InputAction buttonPress;

    // Start is called before the first frame update
    void Start()
    {
        startButton = startButtonObj.GetComponent<Button>();
        achievButton = achievButtonObj.GetComponent<Button>();
        settingButton = settingButtonObj.GetComponent<Button>();
        endButton = endButtonObj.GetComponent<Button>();

        buttonPress.Enable();
        buttonPress.performed += nowSelectButton;
    }


    private void nowSelectButton(InputAction.CallbackContext context)
    {
        if (context.performed && !SceneController.sceneController.isNowFade)
        {
            if (EventSystem.current.currentSelectedGameObject == startButtonObj)
            {
                startButton.GetComponent<SceneChangeButton>().ButtonClick();
                SoundPlayer.SP.SM.Play("StartButtonClick");
            }
            else if (EventSystem.current.currentSelectedGameObject == achievButtonObj)
            {
                achievButton.GetComponent<AchievEnableButton>().AchievPanelEnable();
                SoundPlayer.SP.SM.Play("AnyButtonClick");
            }
            else if (EventSystem.current.currentSelectedGameObject == settingButtonObj)
            {
                settingButton.GetComponent<EnableSettingPanel>().OnClick();
            }
            else if (EventSystem.current.currentSelectedGameObject == endButtonObj)
            {
                endButton.GetComponent<ExitButton>().EndGame();
                SoundPlayer.SP.SM.Play("AnyButtonClick");
            }
        }
    }

    private void OnDestroy()
    {
        buttonPress.Disable();
        buttonPress.performed -= nowSelectButton;
    }
}
