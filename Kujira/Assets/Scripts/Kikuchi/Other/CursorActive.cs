using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CursorActive : MonoBehaviour
{
    [SerializeField]
    private EventSystem eventSystem;

    private GameObject selectedObj;

    [SerializeField]
    private GameObject titleFirstButton;

    [SerializeField]
    private float buttonScaleSize = 1.25f;

    SettingManager settingManager;

    private void Start()
    {
        if(settingManager == null)
        {
            settingManager = SettingManager.settingInstance.GetComponent<SettingManager>();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (SettingManager.SettingManagerInstance.IsActiveSettingPanel == true) return;
            if (eventSystem.currentSelectedGameObject != null)
            {
            if (selectedObj == null)
            {
                selectedObj = eventSystem.currentSelectedGameObject.gameObject;
                selectedObj.transform.Find("SelectCursor").gameObject.SetActive(true);
                selectedObj.transform.localScale = new Vector3(buttonScaleSize, buttonScaleSize, buttonScaleSize);
                SoundPlayer.SP.SM.Play("ButtonSelect");
            }
            else if (eventSystem.currentSelectedGameObject.gameObject != selectedObj)
            {
                SelectedButtonCursorActive();
            }
            }
            else
            {
                EventSystem.current.SetSelectedGameObject(titleFirstButton);
            }

            
    }


    /// <summary>
    /// 選択中のボタンのカーソルを表示するようにするやつ
    /// </summary>
    private void SelectedButtonCursorActive()
    {
        //タグ比較して合っている時
        if (eventSystem.currentSelectedGameObject.gameObject.tag == "ButtonUI")
        {
            //直前に選ばれてたボタンのカーソル非表示にして
            selectedObj.transform.Find("SelectCursor").gameObject.SetActive(false);
            //縮小して
            selectedObj.transform.localScale = new Vector3(1f, 1f, 1f);
            //今のボタンのgameObjectを格納して
            selectedObj = eventSystem.currentSelectedGameObject.gameObject;
            //今のボタンのカーソルを表示して
            selectedObj.transform.Find("SelectCursor").gameObject.SetActive(true);
            //拡大
            selectedObj.transform.localScale = new Vector3(buttonScaleSize, buttonScaleSize, buttonScaleSize);
            //SE再生
            SoundPlayer.SP.SM.Play("ButtonSelect");

        }
        else
        {
            //タグが合ってないときは何もしない
            return;
        }
    }
}
