using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResult : MonoBehaviour
{
    [SerializeField] GameObject Clear;
    [SerializeField] GameObject Defeat;
    [SerializeField] float ScaleWidth = 1;
    [SerializeField] float ScaleSpeed = 1;
    [SerializeField] float ChangeTime = 2;

    private bool clear = false;
    private bool defeat = false;
    private bool once = true;

    private Vector3 scale;
    // Start is called before the first frame update
    void Start()
    {
        once = true;
        scale = new Vector3(ScaleSpeed, 0, 0);
        clear = false;
        defeat = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (EarthManager.instance != null)
        {
            if (EarthManager.EarthHp <= 0)
            {
                StartDefeat();
            }
        }
        if (clear)
        {
            Clear.transform.localScale += scale;
            if(Clear.transform.localScale.x >= ScaleWidth)
            {
                clear = false;
            }
        }
        if (defeat)
        {
            Defeat.transform.localScale += scale;
            if (Defeat.transform.localScale.x >= ScaleWidth)
            {
                defeat = false;
            }
        }
    }

    public void StartClear()
    {
        if(once)
        {
            once = false;
            Clear.SetActive(true);
            clear = true;
            SoundPlayer.SP.SM.Play("ClearSE");

            StartCoroutine(Delay(SceneController.SceneType.toResult, ChangeTime));
        }      
    }
    public void StartDefeat()
    {
        if (once)
        {
            once = false;
            Defeat.SetActive(true);
            defeat = true;
            SoundPlayer.SP.SM.Play("DefeatSE");

            StartCoroutine(Delay(SceneController.SceneType.toGameOver, ChangeTime));
        }        
    }

    private IEnumerator Delay(SceneController.SceneType type, float t)
    {
        yield return new WaitForSeconds(t);
        SoundPlayer.SP.SM.BGMStop();
        SceneController.sceneController.ChangeScene(type);
    }
}
