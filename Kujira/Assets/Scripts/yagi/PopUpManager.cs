using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpManager : MonoBehaviour
{
    [SerializeField]
    private Cetacean cetacean;
    [SerializeField] GameObject windowPrefab;
    List<GameObject> windows = new List<GameObject>();
    GameObject window;
    [SerializeField] List<string> skillNameList = new List<string>();
    [SerializeField] List<string> skillCaptionList = new List<string>();
    [SerializeField] SkillActivator sa;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PopUp(int skillKind,float duration = 2){
        //くじら座に変更
        
    }

    public void Pop(int skillKind){
        if (skillNameList[skillKind] == "くじら座")
        {
            //skillCaptionList[skillKind] = "隕石" + (int)(sa.cetaceanLevel + 1) + "個破壊";
            skillCaptionList[skillKind] = "隕石" + (int)(cetacean.CetaceanLevel_P + 1) + "個破壊";
        }

        /*
        foreach(Transform c in transform){
            if(c.gameObject.activeInHierarchy){
                c.gameObject.GetComponent<SkillPopUp>().Ascend();
            }
        }
        */
        
        GameObject prefab = GetFreeObject(windows,window);
        prefab.SetActive(true);
        prefab.GetComponent<SkillPopUp>().Pop(skillKind,skillNameList[skillKind],skillCaptionList[skillKind]);
    }

    public void PopDown(){
        foreach(Transform c in transform){
            if(c.gameObject.activeInHierarchy){
                SkillPopUp spu = c.gameObject.GetComponent<SkillPopUp>();
                spu.GetComponent<SkillPopUp>().StartCoroutine(spu.PopDown());
            }
        }
    }

    //list内のfalse状態のオブジェクトを返す　なかったら生成
    GameObject GetFreeObject(List<GameObject> list, GameObject prefab)
    {
        // リストの中から探す
        foreach (var obj in list)
        {
            if (!obj.activeInHierarchy)
            { return obj; }
        }

        var newObj = Instantiate(windowPrefab, transform.localPosition, transform.rotation, gameObject.transform);
        list.Add(newObj);
        return newObj;
    }
}
