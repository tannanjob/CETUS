using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    //シーン切り替えようの関数
    public void LoadScene(string sceneName)
    {
        SceneController.SceneType type = 0;
        switch (sceneName)
        {
            case "title": type = SceneController.SceneType.toTitle; break;
            case "story": type = SceneController.SceneType.toStory; break;
            case "rule": type = SceneController.SceneType.toRule; break;
            case "game": type = SceneController.SceneType.toGame; break;
            case "result": type = SceneController.SceneType.toResult; break;
            case "gameover": type = SceneController.SceneType.toGameOver; break;
            case "Ending": type = SceneController.SceneType.toEnding; break;
        }

        SceneController.sceneController.ChangeScene(type);
    }
}
