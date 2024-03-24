using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SkillManeger : MonoBehaviour
{
    [SerializeField]
    private SkillObjectPropaty skillObjectPropaty;
    [SerializeField] private List<Skill> skills; 

    [Header("システムの設定項目")]
    [SerializeField]
    private GameObject Earth;
    [SerializeField]
    private GameObject WhaleObj;
    [SerializeField]
    private GameObject Meteo;
    [SerializeField]
    private GameObject BarrierObj;
    [SerializeField]
    private GameObject meteoChild;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private GameObject PopUp;
    [SerializeField]
    private GameObject WaveObj;
    [SerializeField]
    private GameObject WaterObj;
    [SerializeField]
    private GameObject Talet;
    [SerializeField]
    private GameObject TimeManager;
    [SerializeField]
    private GameObject Manager;

    private Action[] skillEffect = new Action[27];

    // Start is called before the first frame update
    void Start()
    {
        //スキルの初期化
        for(int i = 0; i < skills.Count; i++)
        {
            skills[i].Init(skillObjectPropaty);
        }

        skillEffect[0] = skills[4].Invocate;
        skillEffect[1] = skills[6].Invocate;
        skillEffect[2] = skills[6].Invocate;
        skillEffect[3] = skills[1].Invocate;
        skillEffect[4] = skills[1].Invocate;
        skillEffect[5] = skills[12].Invocate;
        skillEffect[6] = skills[10].Invocate;
        skillEffect[7] = skills[12].Invocate;
        skillEffect[8] = skills[10].Invocate;
        skillEffect[9] = skills[5].Invocate;
        skillEffect[10] = skills[5].Invocate;
        skillEffect[11] = skills[12].Invocate;
        skillEffect[12] = skills[3].Invocate;
        skillEffect[13] = skills[9].Invocate;
        skillEffect[14] = skills[3].Invocate;
        skillEffect[15] = skills[12].Invocate;
        skillEffect[16] = skills[8].Invocate;
        skillEffect[17] = skills[18].Invocate;
        skillEffect[18] = skills[0].Invocate;
        skillEffect[19] = skills[12].Invocate;
        skillEffect[20] = skills[0].Invocate;
        skillEffect[21] = skills[12].Invocate;
        skillEffect[22] = skills[11].Invocate;
        skillEffect[23] = skills[11].Invocate;
        skillEffect[24] = skills[7].Invocate;
        skillEffect[25] = skills[7].Invocate;
        skillEffect[26] = skills[2].Invocate;
    }

    private void Update()
    {
        //デバック用
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            skills[0].Invocate();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            skills[1].Invocate();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            skills[2].Invocate();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            skills[3].Invocate();
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            skills[4].Invocate();
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            skills[5].Invocate();
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            skills[6].Invocate();
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            skills[7].Invocate();
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            skills[8].Invocate();
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            skills[9].Invocate();
        }
        if (Input.GetKeyDown(KeyCode.Minus))
        {
            skills[10].Invocate();
        }
        if (Input.GetKeyDown(KeyCode.Backslash))
        {
            skills[11].Invocate();
        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            skills[12].Invocate();
        }
    }


    //与えられた星座の破片(string[])の組み合わせをスキルの組み合わせと比較
    //組み合わせが一致するスキルを発動
    //赤 = 0, 緑 = 1, 青 = 2
    public void SkillInvocate(int[] starFragment)
    {
        int skillNum = starFragment[0] * 9 + starFragment[1] * 3 + starFragment[2];
        //例外処理:27以上の入出を受け付けない
        if (skillNum > 26)
        {
            skillNum = 26;
        }
        //PopUp.GetComponent<PopUpManager>().PopUp(skillNum);

        skillEffect[skillNum]();
    }
}

[System.Serializable]
public class SkillObjectPropaty
{
    [SerializeField]
    private GameObject WhaleObj;
    public GameObject Whale_P { get { return WhaleObj; } }
    [SerializeField]
    private GameObject Meteo;
    public GameObject Meteo_P { get { return Meteo; } }
    [SerializeField]
    private GameObject BarrierObj;
    public GameObject Barrier_P { get { return BarrierObj; } }
    [SerializeField]
    private GameObject meteoChild;
    public GameObject MeteoChild { get {  return MeteoChild; } }
    [SerializeField]
    private GameObject bullet;
    public GameObject Bullet_P { get { return bullet; } }
    [SerializeField]
    private GameObject WaveObj;
    public GameObject Wave_P { get { return WaveObj; } }
    [SerializeField]
    private GameObject WaterObj;
    public GameObject Water_P { get { return WaterObj; } }
    [SerializeField]
    private GameObject TimeManager;
    public GameObject TimeManager_P { get { return TimeManager; } }
    [SerializeField]
    private GameObject Manager;
    public GameObject Manager_P { get { return Manager; } }
}
