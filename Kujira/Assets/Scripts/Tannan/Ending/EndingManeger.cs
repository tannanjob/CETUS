using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class EndingManeger : MonoBehaviour
{
    [SerializeField] private InputAction next;

    [SerializeField] private GameObject Title;
    [SerializeField] private GameObject Game;

    private Button tittleButton;
    private Button gameButton;
    private GameObject titleCursor;
    private GameObject gameCursor;
    private int isTitle = 0;

    private Vector3 scaleUp = new Vector3(1.1f, 1.1f, 1f);
    // Start is called before the first frame update
    void Start()
    {
        next.Enable();
        next.performed += NextScene;

        tittleButton = Title.GetComponent<Button>();
        gameButton = Game.GetComponent<Button>();

        titleCursor = Title.transform.GetChild(1).gameObject;
        gameCursor = Game.transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") == 1 || Input.GetKeyDown(KeyCode.F12))
        {
            ResetButton();
            Title.transform.localScale = scaleUp;
            titleCursor.SetActive(true);
            isTitle = 1;
        }
        if (Input.GetAxis("Horizontal") == -1 || Input.GetKeyDown(KeyCode.F11))
        {
            ResetButton();
            Game.transform.localScale = scaleUp;
            gameCursor.SetActive(true);
            isTitle = -1;
        }
        if (Input.GetButtonDown("Fire3") || Input.GetKeyDown(KeyCode.F10))
        {
            if(isTitle == 1)
            {
                tittleButton.onClick.Invoke();
            }
            if (isTitle == -1)
            {
                gameButton.onClick.Invoke();
            }
        }
    }

    private void ResetButton()
    {
        Title.transform.localScale = Vector3.one;
        Game.transform.localScale = Vector3.one;
        titleCursor.SetActive(false);
        gameCursor.SetActive(false);
    }

    private void NextScene(InputAction.CallbackContext context)
    {
        if (isTitle == 1)
        {
            SoundPlayer.SP.SM.BGMStop();
            tittleButton.onClick.Invoke();
        }
        if (isTitle == -1)
        {
            SoundPlayer.SP.SM.BGMStop();
            gameButton.onClick.Invoke();
        }
    }
}
