using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryBGMPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SoundPlayer.SP.BGMset.SetBGM(SceneController.SceneType.toStory);
    }


}
