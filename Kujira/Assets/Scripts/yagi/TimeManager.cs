using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class TimeManager : MonoBehaviour
{
    [SerializeField] float maxTime = 300;
    float time = 300;
    bool finish = false;
    [SerializeField] TextMeshProUGUI text;

    float debugTime;
    [SerializeField] float timeShakeInterval = 1f;
    float timeShake = 0.5f;

    //ゲームクリア時のテロップを出す用
    [SerializeField] GameObject ResultCanvas;

    [SerializeField] GameObject playerCollider;
    [SerializeField] MiniMapManager mmm;


    // Start is called before the first frame update
    void Start()
    {
        time = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(time > 0){
            time -= Time.deltaTime;
            text.text = time.ToString("N0");

            if(time <= 11){
                timeShake += Time.deltaTime;
                if(timeShake >= timeShakeInterval){
                    text.gameObject.transform.DOPunchScale(text.gameObject.transform.localScale * 1.1f, 0.5f, 2);
                    timeShake = 0;
                }
            }

            if(Input.GetKey(KeyCode.T)){
                debugTime += Time.deltaTime;
                if(debugTime > 5){
                    time = 1;
                    Finish();
                }
            }
        }
        else if(finish == false){
            finish = true;
            Finish();
        }
    }

    void Finish(){
        SoundPlayer.SP.SM.BGMStop();
        //SceneController.sceneController.ChangeScene(SceneController.SceneType.toResult);
        ResultCanvas.GetComponent<GameResult>().StartClear();
        mmm.isGame = false;
        playerCollider.SetActive(true);
    }
    //天秤座の時間減少
    public void LibraTime(int t)
    {
        time -= t;
    }
}
