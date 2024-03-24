using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoManager : MonoBehaviour
{
    public FollowPlayer fp;

    public static MeteoManager instance;
   
    public GameObject Meteorite;
    public GameObject Meteorite_L;
    public GameObject MeteoriteParent;
    [SerializeField] MiniMapManager mapManager;

    [Tooltip("生成半径")][SerializeField] float radius;
    [Tooltip("ばらつき")][SerializeField] float scatterRadius;


    //隕石の生成位置の座標
    int MeteoX, MeteoY, MeteoZ;
    //隕石初期座標を記憶
    private Dictionary<GameObject, Vector3> initialPositions = new Dictionary<GameObject, Vector3>();
    [Tooltip("何秒ごとに隕石を生成するか")]
    public float timeOut;
    private float timeElapsed;
    [Tooltip("隕石の攻撃力")]
    public int MeteoAtk;
    
    [Tooltip("隕石の生成範囲の最大半径")]
    [SerializeField] int METEO_RANGE_MAX = 80;
    [Tooltip("隕石の生成範囲の最小半径")]
    [SerializeField] int METEO_RANGE_MIN = 45;
    [Tooltip("隕石の生成範囲の上半分の高さ")]
    [SerializeField] int METEO_RANGE_HEIGHT = 40;

    
    //以下追加しました　(6/4 yagi)
    [SerializeField] GameObject effectParent;
    [SerializeField] GameObject stardustParent;


    //List<GameObject> MeteoList = new List<GameObject>();

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }





    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Wave");
    }

    // Update is called once per frame
    /*
    void Update()
    {
        // timeOut秒ごとに隕石生成
        timeElapsed += Time.deltaTime;

        if (timeElapsed >= timeOut)
        {
            MakeMeteorite();

            timeElapsed = 0.0f;
        }
    }
    */

    //リストから探す方式からparentオブジェクトの子から探す方法に変えました (6/4 yagi)
    public GameObject GetFreeObject(GameObject parent,GameObject prefab)
    {
        // parentオブジェクトの子の中から探す
        foreach (Transform child in parent.transform)
        {
            if (!child.gameObject.activeInHierarchy)
            { return child.gameObject; }
        }

        var newPrefab = Instantiate(prefab, transform.position, transform.rotation,parent.transform);
        return newPrefab;
    }

    //6/18　yagi 一定時間毎に数個まとめて生成されるように変更しました
    IEnumerator Wave()
    {
        while (Time.timeScale <= 0) yield return null;
        yield return new WaitForSeconds(1);

        while (true)
        {
            //極座標 0<=angle<2π
            float angle = Random.Range(0, 2 * Mathf.PI);
            float height = METEO_RANGE_HEIGHT;
            //地球からradius離れた点をbasicPosとする 極座標radius,angleをxz平面に変換
            Vector3 basicPos = new Vector3(radius * Mathf.Cos(angle), Random.Range(-height, height), radius * Mathf.Sin(angle));
            //一度に生成される隕石の数
            int r = Random.Range(4, 7);
            //r個生成
            for (int i = 0; i < r; i++)
            {
                //basicPosからちょっとずれた地点に生成
                MakeMeteorite(basicPos + new Vector3(Random.Range(-scatterRadius, scatterRadius), Random.Range(-scatterRadius, scatterRadius), Random.Range(-scatterRadius, scatterRadius)));

                //ちょっと時間差つける
                while (Time.timeScale <= 0) yield return null;
                yield return new WaitForSeconds(0.1f);
            }

            if (timeOut > 1f) timeOut *= 0.98f;

            while (Time.timeScale <= 0) yield return null;
            yield return new WaitForSeconds(timeOut);
        }
    }

    private void MakeMeteorite(Vector3 makePos)
    {
        GameObject ef = GetFreeObject(MeteoriteParent, Meteorite);
        ef.SetActive(true);
        ef.transform.position = makePos;
        ef.transform.eulerAngles = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
        //prefabにparentオブジェクトを渡す
        ef.GetComponent<MeteoMove>().Generated(effectParent, stardustParent, GetComponent<MeteoManager>());
        mapManager.Plus(ef);

    }

    public void Minus(GameObject meteo)
    {
        mapManager.Minus(meteo);
    }


    // 隕石生成
    /*
    private void MakeMeteorite()
    {
        int rand;
        MeteoX = Random.Range(METEO_RANGE_MIN, METEO_RANGE_MAX);
        MeteoY = Random.Range(METEO_RANGE_MIN, METEO_RANGE_HEIGHT);
        MeteoZ = Random.Range(METEO_RANGE_MIN, METEO_RANGE_MAX);
        // この乱数でどの座標を負にするか決める
        rand = Random.Range(0, 7);

        switch (rand)
        {
            case 0:
                MeteoX = MeteoX * -1;
                break;
            case 1:
                MeteoY = MeteoY * -1;
                break;
            case 2:
                MeteoZ = MeteoZ * -1;
                break;
            case 3:
                MeteoX = MeteoX * -1;
                MeteoY = MeteoY * -1;
                break;
            case 4:
                MeteoX = MeteoX * -1;
                MeteoZ = MeteoZ * -1;
                break;
            case 5:
                MeteoY = MeteoY * -1;
                MeteoZ = MeteoZ * -1;
                break;
            default:
                MeteoX = MeteoX * -1;
                MeteoY = MeteoY * -1;
                MeteoZ = MeteoZ * -1;
                break;

        }
        Debug.Log(rand + " " + MeteoX + " " + MeteoY + " " + MeteoZ);
        //AsteroidにMeteorite,Meteorite_S,Meteorite_Lのうちのどれかを入れる
        GameObject Asteroid = ChoiceMeteo(rand);
        
        GameObject ef = GetFreeObject(MeteoriteParent, Asteroid);
        ef.SetActive(true); 
        ef.transform.position = new Vector3(MeteoX, MeteoY, MeteoZ);
        //prefabにparentオブジェクトを渡す
        ef.GetComponent<MeteoMove>().Generated(effectParent,stardustParent,GetComponent<MeteoManager>());
       

    }
    */

    private GameObject ChoiceMeteo(int r)
    {
        GameObject gb;
        switch (r)
        {
            case 0:
            case 1:
            case 2:
            case 3:
            case 4:
                gb = Meteorite;
                MeteoAtk = 50;
                break;
                  
            case 5:
            default:
                gb = Meteorite_L;
                MeteoAtk = 200;
                break;

        }
        return gb;
    }
   
}
