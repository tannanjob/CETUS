using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.InputSystem;

public class AchievPanelPageButton : MonoBehaviour, ISelectHandler
{
    //[SerializeField]
    //private TextMeshProUGUI text;

    [SerializeField]
    private Color color;

    [SerializeField]
    private GameObject enablePanel;

    private AchievmentPanelManager apm;

    private InputAction pressButton;





    private void Awake()
    {
        if (apm == null)
        {
            apm = this.transform.root.gameObject.GetComponent<AchievmentPanelManager>();
        }
        if(pressButton == null)
        {
            pressButton = apm.pressButton;
        }
        
    }

    private void OnEnable()
    {
        
    }
    private void Update()
    {
        //ColorChange();
    }

    private void OnDisable()
    {
        pressButton.performed -= OnClick;
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (EventSystem.current.currentSelectedGameObject == this.gameObject && context.performed)
        {
            pressButton.performed -= OnClick;
            enablePanel.SetActive(true);
            this.transform.parent.gameObject.SetActive(false);
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        pressButton.performed += OnClick;
        SoundPlayer.SP.SM.Play("ButtonSelect");
    }

}
