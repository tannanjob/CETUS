using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NextSceneButton : MonoBehaviour
{
    [SerializeField]
    private InputAction nextButton;

    [SerializeField]
    private SceneController.SceneType sceneType;
    private void Start()
    {
        nextButton.Enable();
        nextButton.performed += NextSceneChange;
    }

    private void NextSceneChange(InputAction.CallbackContext context)
    {
        if(!SceneController.sceneController.isNowFade && context.performed)
        {
            SceneController.sceneController.ChangeScene(sceneType);
            nextButton.performed -= NextSceneChange;
            if(sceneType == SceneController.SceneType.toGame)
            {
                SoundPlayer.SP.SM.BGMStop();
                SoundPlayer.SP.SM.Play("GameStartSE");
            }
        }
    }
}
