using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class MeteoMove : MonoBehaviour
{
    [Tooltip("隕石の目的地(地球の中心)")]
    [SerializeField] GameObject EarthPoint;
    public GameObject Stardust;
    [Tooltip("隕石の移動速度")]
    public float MeteoSpeed = 3.0f;
    
    List<GameObject> StarList = new List<GameObject>();
    //隕石初期位置
    public Vector3 StartPosition;

    MeteoManager meteomanager;
    //以下追加しました　(6/4 yagi)
    [SerializeField] GameObject meteoriteEffect;
    List<GameObject> meteoriteEffectList = new List<GameObject>();
    GameObject effectParent;
    GameObject stardustParent;
    [SerializeField] float basicScale;

    [Header("巨大化関連")]
    [Tooltip("巨大化の確率")][Range(0.0f,1.0f)][SerializeField] float bigProbability;
    [Tooltip("大きさの倍率")][SerializeField] float bigScaleMag;
    [Tooltip("速さの倍率")][SerializeField] float bigSpeedMag;
    bool isBig = false;
    Vector3 startScale;
    
    Vector3 randomRotate = new Vector3(0,0,0);

    [SerializeField] GameObject particle;

    // Start is called before the first frame update
    void Start()
    {
        StartPosition = transform.position;
        //Debug.Log(StartPosition);
    }

    // Update is called once per frame
    void Update()
    {
        particle.transform.LookAt(transform.position * 2);
        transform.Rotate(randomRotate);
        // 隕石の移動
        float speedProduct = MeteoSpeed;
        if(isBig)speedProduct *= bigSpeedMag;
        transform.position = Vector3.MoveTowards(transform.position,EarthPoint.transform.position,speedProduct* Time.deltaTime);
    }

    /*
    オブジェクトプールのリストの隕石ごとに生成されていたので、
    parentオブジェクトの子オブジェクトを使い回すように変更しました(6/4 yagi)
    */
    public void Generated(GameObject ep,GameObject sp,MeteoManager mm){
        effectParent = ep;
        stardustParent = sp;

        //MateoManager取得
        meteomanager = mm;
        randomRotate = new Vector3(Random.Range(-0.50f,0.50f),Random.Range(-0.50f,0.50f),Random.Range(-0.50f,0.50f));


        if(Random.Range(0.00f,1.00f) <= bigProbability){
            isBig = true;
            startScale = new Vector3(basicScale, basicScale, basicScale) * bigScaleMag;
            transform.localScale = new Vector3(0, 0, 0);
            transform.DOScale(startScale, 1f);
        }
        else
        {
            isBig = false;
            startScale = new Vector3(basicScale,basicScale,basicScale);
            transform.localScale = new Vector3(0, 0, 0);
            transform.DOScale(startScale, 1f);
        }
    }

    /*
    CollisionEnterをTriggerEnterに、
    プレイヤーと衝突したときプレイヤーの状態によって欠片を落とすか判別するようにしました(6/4 yagi)
    */
    private void OnTriggerEnter(Collider collision)
    {
        // 衝突処理
        if (collision.gameObject.tag == "Earth")
        {
            Debug.Log("meteoがぶつかった");
            // 隕石非表示
            gameObject.SetActive(false);
            meteomanager.Minus(gameObject);


            // HP計算
           EarthManager.EarthHp = EarthManager.EarthHp - MeteoManager.instance.MeteoAtk;


        }
        if (collision.gameObject.tag == "Player")
        {
            /*gameObject.SetActive(false);
            var ef = GetStarObject(effectParent,meteoriteEffect);
            ef.SetActive(true);
            ef.transform.position = gameObject.transform.position;
           
            //初期座標を削除
            meteomanager.RemoveInitialPosition(gameObject);
            */
            HitBeam();
            //プレイヤースクリプトが存在する場合
            if(collision.transform.parent){
                if (collision.transform.parent.GetComponent<PlayerController>())
                {
                    //欠片落としてもいい状態だったら
                    //meteomanager.Minus(gameObject);
                    MakeStarDust();
                    AchievementChecker.AcievInctance.MetorAchievCount(AchievementChecker.MetorAchievType.Destroy);
                }
            }
        }

        if (collision.gameObject.tag == "Tail")
        {
            //プレイヤースクリプトが存在する場合
            if (collision.transform.parent.GetComponent<PlayerController>())
            {
                //プレイヤーが攻撃中だったら
                if (collision.transform.parent.GetComponent<PlayerController>().isAttack)
                {
                    //meteomanager.Minus(gameObject);
                    HitBeam();

                    if (collision.transform.parent.GetComponent<PlayerController>().DropStardust())
                    {
                        MakeStarDust();
                        AchievementChecker.AcievInctance.MetorAchievCount(AchievementChecker.MetorAchievType.Destroy);
                    }
                }
            }
        }
    }

    public void HitBeam()
    {
        gameObject.SetActive(false);
        var ef = GetStarObject(effectParent, meteoriteEffect);
        ef.SetActive(true);
        ef.transform.position = gameObject.transform.position;

        
    }
    public GameObject GetStarObject(GameObject parent,GameObject prefab)
    {
        // リストの中から探す
        foreach (Transform child in parent.transform)
        {
            if (!child.gameObject.activeInHierarchy)
            { return child.gameObject; }
        }

        var newStar = Instantiate(prefab, transform.position, transform.rotation,parent.transform);
        return newStar;
    }

    //　星のかけら生成
    private void MakeStarDust()
    {
        GameObject ef = GetStarObject(stardustParent, Stardust);
        ef.SetActive(true);
        ef.transform.position = gameObject.transform.position;
        ef.GetComponent<Stardust>().Generated(Random.Range(0, 3));
    }

}
