using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapManager : MonoBehaviour
{
    public bool isGame = true;
    public static float mapScale = 300;
    [SerializeField] GameObject meteoParent;
    List<GameObject> meteoList = new List<GameObject>();
    List<GameObject> meteoIconList = new List<GameObject>();
    [SerializeField] GameObject iconPrefab;
    Dictionary<GameObject, GameObject> meteoDict = new Dictionary<GameObject, GameObject>();

    void Start()
    {

    }


    void Update()
    {
        foreach (Transform meteoChild in meteoParent.transform)
        {
            bool inList = false;
            for (int i = 0; i < meteoList.Count;i++){
                if(meteoList[i] == meteoChild.gameObject){
                    inList = true;
                    break;
                }
            }
            if (meteoChild.gameObject.activeSelf)
            {
                meteoDict[meteoChild.gameObject].transform.localPosition = new Vector3(meteoChild.transform.position.x, meteoChild.transform.position.z, 0) / mapScale;
            }
            else if (inList)
            {
                Minus(meteoChild.gameObject);
            }
        }

    }

    public void Plus(GameObject meteo)
    {
        meteoList.Add(meteo);
        meteoDict[meteo] = GetFreeObject(meteoIconList, iconPrefab);
        meteoDict[meteo].SetActive(true);
    }

    public void Minus(GameObject meteo)
    {
        if(isGame)ScoreManeger.Instance.MeteoScore();
        meteoDict[meteo].SetActive(false);
        meteoList.Remove(meteo);
        SoundPlayer.SP.SM.Play("MetorClash");
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

        var newObj = Instantiate(prefab, transform.position, transform.rotation, transform);
        list.Add(newObj);
        return newObj;
    }
}
