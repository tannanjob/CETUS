using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class OtherSceneButton : MonoBehaviour
{
    [SerializeField]
    private InputAction pressButton;

    private Button button;


    private void Start()
    {
        //button = this.transform.Find("Test").GetComponent<Button>();

        pressButton.Enable();
        pressButton.performed += pressButtonAction;
    }

    private void pressButtonAction(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            button.GetComponent<SceneChangeButton>().ButtonClick();
        }
    }

    private void OnDestroy()
    {
        pressButton.Disable();
        pressButton.performed -= pressButtonAction;
    }



}
