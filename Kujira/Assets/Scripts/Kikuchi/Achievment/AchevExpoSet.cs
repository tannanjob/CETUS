using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class AchevExpoSet : MonoBehaviour
{
    //実績説明類親オブジェクト
    [SerializeField]
    private GameObject expoObj;
    //実績アイコン類親オブジェクト
    [SerializeField]
    private GameObject iconObj;

    private AchievmentPanelManager apm;


    private static int lastExpoNum;

    private GameObject lastSelectObj;

    //実績説明が入ったオブジェクト類格納用
    private Dictionary<int , GameObject> expoSet = new Dictionary<int , GameObject>();

    private void Awake()
    {
        if(apm == null)
        {
            apm = this.transform.root.GetComponent<AchievmentPanelManager>();
        }
        for(int i = 0; i < expoObj.transform.childCount; i++)
        {
            expoSet.Add(i , expoObj.transform.GetChild(i).gameObject);
        }
    }

    /// <summary>
    /// 説明画面表示用関数
    /// </summary>
    /// <param name="num"></param>
    public void SetExpo(int num)
    {
        if (AchievEnableButton.isFirtstActive)
        {
            AchievEnableButton.isFirtstActive = false;
            return;
        }
        lastSelectObj = EventSystem.current.currentSelectedGameObject;
        expoSet[num].SetActive(true);
        apm.closeButton.performed -= apm.DisableAchievPanel;
        apm.closeButton.performed += ExitExpo;
        EventSystem.current.sendNavigationEvents = false;
        lastExpoNum = num;
    }

    private void ExitExpo(InputAction.CallbackContext context)
    {
        apm.closeButton.performed -= ExitExpo;
        expoSet[lastExpoNum].SetActive(false);
        EventSystem.current.sendNavigationEvents = true;
        EventSystem.current.SetSelectedGameObject(lastSelectObj);
        lastSelectObj = null;
        apm.closeButton.performed += apm.DisableAchievPanel;
    }

    private void OnDisable()
    {
        lastExpoNum = 0;
        lastSelectObj = null;
    }
}
