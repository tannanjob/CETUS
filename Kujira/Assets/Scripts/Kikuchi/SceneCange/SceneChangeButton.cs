using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangeButton : MonoBehaviour
{
    [SerializeField]
    SceneController.SceneType sceneType;
    [SerializeField]
    private bool stopBGM = false;
    [SerializeField]
    private bool playSE = true;

    private bool isClick = false;
    public void ButtonClick()
    {
        if (!isClick)
        {
            isClick = true;
            SceneController.sceneController.ChangeScene(sceneType);
            if (stopBGM)
            {
                SoundPlayer.SP.SM.BGMStop();
            }
            if (playSE)
            {
                if (sceneType == SceneController.SceneType.toGame)
                {
                    SoundPlayer.SP.SM.Play("GameStartSE");
                }
                else
                {
                    SoundPlayer.SP.SM.Play("AnyButtonClick");
                }
            }
        }
    }


}
