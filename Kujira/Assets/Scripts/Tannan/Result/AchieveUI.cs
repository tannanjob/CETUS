using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class AchieveUI : MonoBehaviour
{
    [SerializeField] private InputAction Select;

    [SerializeField] GameObject[] UI = new GameObject[6];
    [SerializeField] List<Sprite> AchieveImage = new List<Sprite>();
    Image[] UIImage = new Image[6];

    private int Elements = 3;
    private int row = 2;

    private int selectRow = 0;
    private int imageRow = 0;
    // Start is called before the first frame update
    void Start()
    {
        Select.Enable();

        for(int i = 0; i < Elements * row; i++)
        {
            UIImage[i] = UI[i].GetComponent<Image>();
        }
        AchieveImage = AchievementChecker.AcievInctance.NewGetAchievCheck().ToList<Sprite>();
        imageRow = AchieveImage.Count / 3;


        DrawUI();

        Select.performed += OnNavigate;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            selectRow--;
            if(selectRow < 0)
                selectRow = 0;
            DrawUI();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            selectRow++;
            if (selectRow >  imageRow)
                selectRow = imageRow;
            DrawUI();
        }
    }

    private void DrawUI()
    {
        for(int i = 0; i < Elements * row; i++)
        {
            if(selectRow * Elements + i < AchieveImage.Count)
            {
                UI[i].SetActive(true);
                UIImage[i].sprite = AchieveImage[selectRow * Elements + i];
            }
            else
            {
                UI[i].SetActive(false);
            }          
        }
    }

    public void OnNavigate(InputAction.CallbackContext context)
    {
        // performedコールバックだけをチェックする
        if (!context.performed) return;

        // スティックの2軸入力取得
        var inputValue = context.ReadValue<Vector2>();

        if(inputValue.y == 1)
        {
            selectRow--;
            if (selectRow < 0)
                selectRow = 0;
            DrawUI();
        }
        if (inputValue.y == -1)
        {
            selectRow++;
            if (selectRow > imageRow)
                selectRow = imageRow;
            DrawUI();
        }
    }
}
