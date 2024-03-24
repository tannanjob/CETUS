using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{

    public static SettingManager SettingManagerInstance;
    [SerializeField]
    private GameObject settingPanel; //設定画面のプレハブ取得用変数

    public static GameObject settingInstance; //設定画面のシングルトン確認用変数
    [SerializeField]
    public bool IsActiveSettingPanel = false;    //設定画面がアクティブかどうか

    private Camera mainCam; //メインカメラ取得用
    private FollowPlayer followPlayer;  //感度適用関数取得用

    [SerializeField]
    private InputAction action;

    [SerializeField]
    private InputAction pressStartButton;

    private Slider FirstPosSlider;
    private void Awake()
    {

        //最初の１度のみ設定画面を生成し、非アクティブ化
        if(settingInstance == null)
        {
            SettingManagerInstance = this;
            settingInstance = Instantiate(settingPanel, this.gameObject.transform);
            FirstPosSlider = settingInstance.transform.Find("Sens/SensSetting").gameObject.GetComponent<Slider>();
            settingInstance.SetActive(false);
            IsActiveSettingPanel = false;
        }

        
    }

    private void Start()
    {
        pressStartButton.Enable();
        pressStartButton.performed += Pause;
    }

    private void Update()
    {
        //ゲームシーンの時にメインカメラの取得
        if (mainCam != Camera.main && SceneController.sceneController.GameSceneName == SceneManager.GetActiveScene().name)
        {
            mainCam = Camera.main;
            followPlayer = mainCam.GetComponent<FollowPlayer>();
        }

        //Escを押した時、設定画面を表示/非表示
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(!IsActiveSettingPanel) EnableSettingPanel();
            else if(IsActiveSettingPanel) DisableSettingPanel();
        }

        if (IsActiveSettingPanel)
        {
            if (EventSystem.current.currentSelectedGameObject.tag != "SettingUI")
            {
                EventSystem.current.SetSelectedGameObject(FirstPosSlider.gameObject);
            }
        }
    }

    //Startボタン押した時pause画面トグル
    public void Pause(InputAction.CallbackContext context)
    {
        if (!IsActiveSettingPanel) EnableSettingPanel();
    }

    /// <summary>
    /// Bボタン押したら設定画面を非表示に
    /// </summary>
    /// <param name="context"></param>
    private void DisablePanel(InputAction.CallbackContext context)
    {
        DisableSettingPanel();
    }

    /// <summary>
    /// 設定画面呼び出し用関数
    /// </summary>
    public void EnableSettingPanel()
    {
        action.performed += DisablePanel;
        settingInstance.SetActive(true);
        IsActiveSettingPanel = true;
        action?.Enable();
        EventSystem.current.SetSelectedGameObject(FirstPosSlider.gameObject);
        SoundPlayer.SP.SM.Play("AnyButtonClick");


    }

    /// <summary>
    /// 設定画面非表示用関数
    /// </summary>
    public void DisableSettingPanel()
    {
        action.performed -= DisablePanel;
        action?.Disable();
        settingInstance.SetActive(false);
        IsActiveSettingPanel = false;
        EventSystem.current.SetSelectedGameObject(null);
        //感度の適用
        if (followPlayer != null) followPlayer.Resume();
        SoundPlayer.SP.SM.Play("AnyButtonClick");
    }

}
