using Cysharp.Threading.Tasks.Triggers;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ResultClearManager : MonoBehaviour
{
    private enum ClearState
    {
        scaleX,
        up,
        end
    }

    [SerializeField] private InputAction next;
    [SerializeField] private bool Skip = false;
    [SerializeField] private bool isClear = false;

    [SerializeField] private GameObject BackGround;
    [SerializeField] private GameObject ClearImage;
    [SerializeField] private TextMeshProUGUI HpPoint;
    [SerializeField] private TextMeshProUGUI ScorePoint;
    [SerializeField] private float ClearScaleSpeed = 0.03f;
    [SerializeField] private float ResultScaleSpeed = 0.03f;
    [SerializeField] private float ClearHight = 255f;
    [SerializeField] private float ClearMoveSpeed = 1.2f;


    private Vector3 clearSclaeSpeed;
    private Vector3 resultSclaeSpeed;
    private float clearWidth = 19.2394f;
    private ClearState clearState = ClearState.scaleX;
    private Vector3 clearMoveSpeed;
    private bool close = false;
    private Vector3 ClearAfterSkipPos = new Vector3(0, 256, 0);
    private Vector3 ClearAfterSkipScale = new Vector3(20, 1.4642f, 1);
    private Vector3 BackAfterSkipPos = new Vector3(0, -165, 0);
    private Vector3 BackAfterSkipScale = new Vector3(1, 1.05f, 1);

    //クリア画面拡大のフラグ
    private bool clear = true;
    //リザルト画面拡大のフラグ
    private bool result = true;
    // Start is called before the first frame update
    void Start()
    {
        next.Enable();
        next.performed += NextScene;

        clearMoveSpeed = new Vector3(0, ClearMoveSpeed, 0);
        clearSclaeSpeed = new Vector3(ClearScaleSpeed, 0, 0);
        resultSclaeSpeed = new Vector3(0, ResultScaleSpeed, 0);
        HpPoint.text = EarthManager.EarthHp.ToString();
        if(EarthManager.EarthHp < 0)
        {
            HpPoint.text = 0.ToString();
        }
        ScorePoint.text = ScoreManeger.Instance.GetScore().ToString();
        if (isClear)
        {
            SoundPlayer.SP.SM.Play("CleatBGM");
        }
        else
        {
            SoundPlayer.SP.SM.Play("DefeatBGM");
        }

        if (Skip)
        {
            ClearImage.transform.localScale = ClearAfterSkipScale;
            ClearImage.transform.localPosition = ClearAfterSkipPos;
            BackGround.transform.localScale = BackAfterSkipScale;
            BackGround.transform.localPosition = BackAfterSkipPos;
        }
        else
        {
            StartCoroutine(DelayCoroutine(0.5f));
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!clear)
        {

            switch (clearState)
            {
                case ClearState.scaleX:
                    ClearImage.transform.localScale += clearSclaeSpeed;
                    if (ClearImage.transform.localScale.x >= clearWidth)
                    {
                        clearState = ClearState.up;
                    }
                    break;

                case ClearState.up:
                    ClearImage.transform.localPosition += clearMoveSpeed;
                    if (ClearImage.transform.localPosition.y >= ClearHight)
                    {
                        clearState = ClearState.end;
                    }
                    break;

                case ClearState.end:
                    clear = true;
                    StartCoroutine(DelayCoroutine(0.1f));                  
                    break;


            }
        }
        if (!result)
        {
            BackGround.transform.localScale += resultSclaeSpeed;
            if (BackGround.transform.localScale.y >= 1f)
            {
                result = true;
            }
        }
        if(close)
        {
            ClearImage.transform.localScale -= clearSclaeSpeed;
            if (ClearImage.transform.localScale.x <= 0)
            {
                ClearImage.transform.localScale = new Vector3(0, ClearImage.transform.localScale.y, ClearImage.transform.localScale.z);
            }
            BackGround.transform.localScale -= resultSclaeSpeed;
            if (BackGround.transform.localScale.y <= 0)
            {
                BackGround.transform.localScale = new Vector3(ClearImage.transform.localScale.x, 0, ClearImage.transform.localScale.z);
            }
            if(ClearImage.transform.localScale.x == 0 && BackGround.transform.localScale.y == 0)
            {
                SoundPlayer.SP.SM.BGMStop();
                SceneController.sceneController.ChangeScene(SceneController.SceneType.toEnding);
            }
        }
    }

    
    private IEnumerator DelayCoroutine(float t)
    {
        yield return new WaitForSeconds(t);
        if (clear == true && clearState != ClearState.end)
        {
            clear = false;
        }
        if (clearState == ClearState.end)
        {
            result = false;
        }
    }
    
    private void NextScene(InputAction.CallbackContext context)
    {
        close = true;
    }
}
