using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Scorpio scorpio;
    [SerializeField] private Capricorn capricorn;
    [SerializeField] private Pisces pisces;


    [SerializeField] PopUpManager pum;
    [SerializeField] Animator anim;
    [SerializeField] SkillActivator sa;
    [SerializeField] GameObject playerIcon;
    Rigidbody rb;
    [SerializeField][Tooltip("スピードの基準値")] float speed;
    [Header("ダッシュ関連")]
    [SerializeField][Tooltip("ダッシュの持続時間")] float dashDuration = 1;
    [SerializeField][Tooltip("ダッシュ時のスピード倍率")] float dashSpeedMag = 2;
    [SerializeField][Tooltip("ダッシュクールタイム")] float dashCoolTime = 3;
    float speedMag = 1;

    bool isDash = false;
    bool isDashCoolTime = false;
    bool isDamage = false;
    public bool isAttack = false;
    bool isEat = false;

    [Header("被ダメージ時")]
    [SerializeField] float slowDuration = 2;
    [SerializeField] float invisibleDuration = 3;
    [SerializeField] float knockBackScale = 10;

    [SerializeField] GameObject mouth;
    public int[] swallowedStardust = new int[3];
    public int stardustDigit = 0;
    [SerializeField] Image[] stardustUI;
    [SerializeField] Sprite[] stardustSprite;

    [SerializeField] GameObject effectParent;
    public GameObject EffectParent_P { get { return effectParent; } }
    [SerializeField] GameObject meteoriteEffect;
    public GameObject MeteoriteEffect_P { get { return meteoriteEffect; } }
    List<GameObject> meteoriteEffectList = new List<GameObject>();
    public List<GameObject> MeteoriteEffectList_P { get { return meteoriteEffectList; } }

    [SerializeField] ChooseStardust cs;

    [SerializeField] GameObject mainCamera;
    FollowPlayer followPlayer;
    public FollowPlayer FollowPlayer_P { get { return followPlayer; } }

    Vector2 inputDir;
    Vector3 move;
    [SerializeField] float playerSensitivity;
    public bool FixedRotate = false;
    Tween _tween = null;

    [SerializeField] MeshRenderer bodyMesh;
    [SerializeField] TextMeshProUGUI flagText;

    bool isTaurus = false;//1
    bool isLeo = false;//4
    //bool isLibra = false;//6
    bool isScorpion = false;//7
    bool isCapricorn = false;//9
    bool isPisces = false;//11
    //bool isCetacean = false;//12

    Coroutine[] skillCoroutine = new Coroutine[13];

    //debug
    [SerializeField] GameObject leftStick;

    Vector3 startScale;
    public Vector3 StartScale_P { get { return startScale; } }
    [SerializeField] GameObject earth;
    float fixedMapScale;

    public bool DropStardust()
    {
        if (isLeo || isAttack) return true;
        else return false;
    }


    void Start()
    {
        fixedMapScale = MiniMapManager.mapScale;
        anim.SetInteger("Move", 1);
        for (int i = 0; i < swallowedStardust.Length; i++)
        {
            swallowedStardust[i] = -1;
        }
        startScale = transform.localScale;
        rb = GetComponent<Rigidbody>();
        followPlayer = mainCamera.GetComponent<FollowPlayer>();
    }


    void Update()
    {
        move = Camera.main.transform.eulerAngles + new Vector3(-inputDir.y * 90, inputDir.x * 90, 0);

        //カメラのスティック離した時
        //if (followPlayer.inputDir.magnitude > 0.05f) FixedRotate = true;
        //プレイヤー操作のスティック離した時
        //if (inputDir.magnitude > 0.05f) FixedRotate = false;

        //スティックを動かしていれば回転
        //if (!FixedRotate && _tween == null)
        //{
        _tween = transform.DORotate(move, 0.5f);
        //}

        //スピードの積
        float speedProduct = speed * Time.deltaTime * speedMag;
        //if (isPisces) speedProduct *= sa.piscesSpeedMag;
        if (isPisces) speedProduct *= pisces.PiscesSpeedMag_P;
        //if (isScorpion) speedProduct *= sa.scorpionSpeedMag;
        if (isScorpion) speedProduct *= scorpio.ScorpioSpeedMag_P;
        transform.Translate(0, 0, speedProduct);

        if (new Vector3(transform.position.x, 0, transform.position.z).magnitude > fixedMapScale * 0.2f)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z).normalized * (fixedMapScale * 0.2f)
                               + new Vector3(0, transform.position.y, 0);
        }

        if (transform.position.y > 10)
        {
            transform.position = new Vector3(transform.position.x, 10, transform.position.z);
        }
        if (transform.position.y < -10)
        {
            transform.position = new Vector3(transform.position.x, -10, transform.position.z);
        }

        //移動距離のAchievement
        AchievementChecker.AcievInctance.MoveDistanceCheck();

        //ミニマップ上のプレイヤー
        playerIcon.transform.localPosition = new Vector3(transform.position.x, transform.position.z, 0) / fixedMapScale;
        playerIcon.transform.localEulerAngles = new Vector3(0, 0, -transform.eulerAngles.y);
    }

    //フラグ表示
    void VisualizeFlag()
    {
        flagText.text = "isAttack:" + isAttack + "\nisEat:" + isEat + "\nisDash:" + isDash + "\nisDashCoolTime:" + isDashCoolTime;
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _tween = null;
            inputDir = context.ReadValue<Vector2>();
            anim.SetFloat("DirectionX", inputDir.x);
            anim.SetFloat("DirectionY", inputDir.y);
            leftStick.transform.localPosition = inputDir * 0.1f;
        }
        if (context.canceled)
        {

        }
    }

    public void ExitMove(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            inputDir = new Vector2(0.1f, 0f);
            leftStick.transform.localPosition = inputDir * 0.1f;
        }
    }

    //Lボタンが押された || Spaceキーが押された
    #region Dash
    public void Dash(InputAction.CallbackContext context)
    {
        if (context.performed && ((!isDashCoolTime) || isTaurus) && !isDash)
        {
            StartCoroutine(Dash(dashDuration));
        }
    }

    IEnumerator Dash(float duration)
    {
        SoundPlayer.SP.SM.Play("Dash");
        isAttack = true;
        anim.SetInteger("Move", 2);
        anim.Play("Screw", 1, 0);
        //achievement
        AchievementChecker.AcievInctance.DashCastCount();

        isDash = true;
        isDashCoolTime = true;
        followPlayer.offsetMag *= 1.25f;
        bodyMesh.material.color = new Color32(255, 128, 128, 255);
        speedMag *= dashSpeedMag;
        yield return new WaitForSeconds(duration);
        bodyMesh.material.color = new Color32(255, 255, 255, 255);
        speedMag /= dashSpeedMag;
        followPlayer.offsetMag /= 1.25f;
        isDash = false;


        anim.SetInteger("Move", 1);
        yield return new WaitForSeconds(dashCoolTime);
        isAttack = false;
        isDashCoolTime = false;
    }
    #endregion

    /*
    //Eastボタンが押された || 左クリック
    #region Attack
    public void Attack(InputAction.CallbackContext context){
        if(context.performed && !isAttack){
            isAttack = true;
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack(){
        bodyMesh.material.color = new Color32(128, 128, 255, 255);
        yield return new WaitForSeconds(1f);
        bodyMesh.material.color = new Color32(255, 255, 255, 255);
        isAttack = false;
    }
    #endregion
    */

    //Southボタンが押された || Eキーが押された
    #region Eat
    public void Eat(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isEat = true;

            bodyMesh.material.color = new Color32(128, 255, 128, 255);

            //飲み込んだかけら
            if (stardustDigit < 3 && cs.holdStardust != null && cs.holdStardustKind != -1)
            {
                SoundPlayer.SP.SM.Play("Dust");

                AchievementChecker.AcievInctance.MetorAchievCount(AchievementChecker.MetorAchievType.Eat);
                //stardustUI[stardustDigit].transform.localScale = new Vector3(1,1,1);
                int k = cs.holdStardustKind;
                swallowedStardust[stardustDigit] = k;
                print("stardustSprite[k]" + k);
                stardustUI[stardustDigit].sprite = stardustSprite[k];

                if (stardustDigit == 2 && swallowedStardust[0] * 9 + swallowedStardust[1] * 3 + swallowedStardust[2] >= 0)
                {
                    pum.Pop(swallowedStardust[0] * 9 + swallowedStardust[1] * 3 + swallowedStardust[2]);
                    print(swallowedStardust[0] * 9 + swallowedStardust[1] * 3 + swallowedStardust[2]);
                }
                stardustDigit += 1;

                cs.holdStardust.SetActive(false);
                cs.holdStardust = null;
                cs.holdStardustKind = -1;
                //やぎ座の処理
                //if (isCapricorn) DestroyTheNearestMeteorite();
                if (isCapricorn)
                {
                    capricorn.DestroyTheNearestMeteorite();
                }
            }
            else if (stardustDigit >= 3)
            {
                sa.Skill();
                pum.PopDown();
            }
        }
        else if (context.canceled)
        {
            isEat = false;
            bodyMesh.material.color = new Color32(255, 255, 255, 255);
        }
    }
    #endregion

    //ダメージ判定とか
    public void BodyOnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Meteorite")
        {
            /*
            c.gameObject.SetActive(false);
            var ef = GetFreeObject(meteoriteEffectList,meteoriteEffect);
            ef.SetActive(true);
            ef.transform.position = c.gameObject.transform.position;
            */

            //ダメージを受けた
            if (!isLeo && !isDamage && !isAttack)
            {
                //衝突回数の実績
                AchievementChecker.AcievInctance.MetorAchievCount(AchievementChecker.MetorAchievType.Collision);

                followPlayer.StartCoroutine(followPlayer.Shake(0.2f));
                isDamage = true;
                Vector3 dir = transform.position - c.gameObject.transform.position;
                dir.Normalize();
                StartCoroutine("Damaged", dir);
            }
            else
            {

            }
        }
        if (c.gameObject.tag == "Earth")
        {
            SoundPlayer.SP.SM.Play("ShildHit");
            followPlayer.StartCoroutine(followPlayer.Shake(0.5f));
            isDamage = true;
            Vector3 dir = transform.position - c.gameObject.transform.position;
            dir.Normalize();
            StartCoroutine("Damaged", dir);
        }

    }

    //飲み込み判定とか
    public void MouthOnTriggerEnter(Collider c)
    {

    }

    //stardust消費
    public void ResetStardust()
    {
        stardustDigit = 0;
        for (int i = 0; i < swallowedStardust.Length; i++)
        {
            stardustUI[i].sprite = stardustSprite[3];
        }
    }

    //攻撃判定とか
    public void TailOnTriggerEnter(Collider c)
    {
        if (isAttack && c.gameObject.tag == "Meteorite")
        {
            AchievementChecker.AcievInctance.MetorAchievCount(AchievementChecker.MetorAchievType.Collision);
            followPlayer.StartCoroutine(followPlayer.Shake(0.2f));
        }
    }

    IEnumerator Damaged(Vector3 dir)
    {
        //減速
        speedMag /= 3f;
        bodyMesh.material.color = new Color32(128, 128, 128, 128);

        //ノックバック
        for (float t = 0; t < slowDuration; t += Time.deltaTime)
        {
            dir *= 0.98f;
            rb.velocity = dir * knockBackScale;
            yield return null;
        }

        //速度戻す
        rb.velocity = Vector3.zero;
        speedMag *= 3;
        //無敵時間
        yield return new WaitForSeconds(invisibleDuration - slowDuration);
        bodyMesh.material.color = new Color32(255, 255, 255, 255);
        isDamage = false;
    }

    ////list内のfalse状態のオブジェクトを返す　なかったら生成
    //GameObject GetFreeObject(List<GameObject> list, GameObject prefab)
    //{
    //    // リストの中から探す
    //    foreach (var obj in list)
    //    {
    //        if (!obj.activeInHierarchy)
    //        { return obj; }
    //    }

    //    var newObj = Instantiate(prefab, transform.position, transform.rotation, effectParent.transform);
    //    list.Add(newObj);
    //    return newObj;
    //}
    //
    ////limit内の範囲の最も近いtagNameタグのオブジェクトを返す　なかったらnull
    //GameObject FetchNearObjectWithTag(string tagName, float limit = 999)
    //{
    //    Dictionary<float, GameObject> dict = new Dictionary<float, GameObject>();
    //    var searchTargets = GameObject.FindGameObjectsWithTag(tagName);
    //    if (searchTargets.Length == 0) return null;

    //    foreach (var searchTarget in searchTargets)
    //    {
    //        var targetDistance = Vector3.Distance(transform.position, searchTarget.transform.position);
    //        if (targetDistance != 0 && targetDistance <= limit)
    //        {
    //            dict.Add(targetDistance, searchTarget);
    //        }
    //    }

    //    var sortedKeys = new List<float>(dict.Keys);
    //    if (sortedKeys.Count == 0) return null;
    //    sortedKeys.Sort();
    //    return dict[sortedKeys[0]];
    //}
    //
    ////tagNameタグがついたオブジェクトをランダムに返す
    //GameObject FetchRandomObjectWithTag(string tagName)
    //{
    //    var searchTargets = GameObject.FindGameObjectsWithTag(tagName);
    //    if (searchTargets.Length == 0) return null;
    //    return searchTargets[Random.Range(0, searchTargets.Length)];
    //}

    ////最も近い位置にある隕石を壊す
    //void DestroyTheNearestMeteorite()
    //{
    //    GameObject target = FetchNearObjectWithTag("Meteorite");
    //    if (target)
    //    {
    //        target.gameObject.SetActive(false);
    //        var ef = GetFreeObject(meteoriteEffectList, meteoriteEffect);
    //        ef.SetActive(true);
    //        ef.transform.position = target.transform.position;
    //    }
    //}

    //#region Taurus
    //public IEnumerator Taurus()
    //{
    //    if (skillCoroutine[1] != null)
    //    {
    //        StopCoroutine(TaurusCoroutine());
    //    }
    //    skillCoroutine[1] = StartCoroutine(TaurusCoroutine());
    //    yield return null;
    //}

    //public IEnumerator TaurusCoroutine()//牡牛座
    //{
    //    //ダッシュクールタイムをなくす
    //    isDashCoolTime = false;
    //    isTaurus = true;
    //    yield return new WaitForSeconds(sa.taurusDuration);
    //    isTaurus = false;
    //}
    //#endregion

    //#region Leo
    //public IEnumerator Leo()
    //{
    //    if (skillCoroutine[4] != null)
    //    {
    //        StopCoroutine(LeoCoroutine());
    //    }
    //    skillCoroutine[4] = StartCoroutine(LeoCoroutine());
    //    yield return null;
    //}
    //public IEnumerator LeoCoroutine()//獅子座
    //{
    //    //カメラ引く
    //    followPlayer.offsetMag *= 1.25f;
    //    isLeo = true;
    //    //巨大化
    //    transform.DOScale(startScale * sa.leoMag, 0.5f).SetEase(Ease.OutBack);
    //    yield return new WaitForSeconds(sa.leoDuration);
    //    //もとのサイズへ
    //    transform.DOScale(startScale, 0.2f);
    //    //カメラ近づく
    //    followPlayer.offsetMag /= 1.25f;
    //    isLeo = false;
    //}
    //#endregion

    //#region Libra
    ////public IEnumerator Libra()
    ////{
    ////    if (skillCoroutine[6] != null)
    ////    {
    ////        StopCoroutine(LibraCoroutine());
    ////    }
    ////    skillCoroutine[6] = StartCoroutine(LibraCoroutine());
    ////    yield return null;
    ////}

    ////public IEnumerator LibraCoroutine()//天秤座
    ////{
    ////    if (isLibra) yield break;
    ////    isLibra = true;
    ////    Vector3 preScale = transform.localScale;
    ////    playerIcon.transform.DOScale(new Vector3(0, 0, 0), 0.2f);

    ////    transform.DOScale(Vector3.zero, 0.2f);
    ////    yield return new WaitForSeconds(0.5f);
    ////    //地球から現在地への方向ベクトル
    ////    Vector3 diff = transform.position - earth.transform.position;
    ////    //地球に関して対象の位置へ
    ////    transform.position = earth.transform.position - diff;
    ////    transform.DOScale(preScale, 0.2f);
    ////    playerIcon.transform.DOScale(new Vector3(1, 1, 1), 0.2f);
    ////    playerIcon.transform.localEulerAngles = new Vector3(0, 0, transform.eulerAngles.y);
    ////    isLibra = false;
    ////}
    //#endregion Libra

    //#region Capricorn
    //public IEnumerator Capricorn()
    //{
    //    if (skillCoroutine[9] != null)
    //    {
    //        StopCoroutine(CapricornCoroutine());
    //    }
    //    skillCoroutine[9] = StartCoroutine(CapricornCoroutine());
    //    yield return null;
    //}
    //IEnumerator CapricornCoroutine()//山羊座
    //{
    //    isCapricorn = true;
    //    yield return new WaitForSeconds(sa.capricornDuration);
    //    isCapricorn = false;
    //}
    //#endregion

    //#region Scorpion
    //public void Scorpion()
    //{
    //    if (skillCoroutine[7] != null)
    //    {
    //        StopCoroutine("ScorpionCoroutine");
    //    }
    //    skillCoroutine[7] = StartCoroutine("ScorpionCoroutine");
    //}

    //IEnumerator ScorpionCoroutine()
    //{
    //    isScorpion = true;
    //    yield return new WaitForSeconds(sa.scorpionDuration);
    //    isScorpion = false;
    //}
    //#endregion

    //#region Pisces
    //public IEnumerator Pisces()//魚座
    //{
    //    //まだ効果時間中なら
    //    if (skillCoroutine[11] != null)
    //    {
    //        StopCoroutine(PiscesCoroutine());
    //    }
    //    skillCoroutine[11] = StartCoroutine(PiscesCoroutine());
    //    yield return null;
    //}

    //public IEnumerator PiscesCoroutine()
    //{
    //    //カメラ引く
    //    followPlayer.offsetMag *= 1.25f;
    //    isPisces = true;
    //    yield return new WaitForSeconds(sa.piscesDuration);
    //    //カメラ近づく
    //    followPlayer.offsetMag /= 1.25f;
    //    isPisces = false;
    //}
    //#endregion

    //#region Cetacean
    //public IEnumerator Cetacean()//魚座
    //{
    //    //まだ効果時間中なら
    //    if (skillCoroutine[12] != null)
    //    {
    //        StopCoroutine(CetaceanCoroutine());
    //    }
    //    skillCoroutine[12] = StartCoroutine(CetaceanCoroutine());
    //    yield return null;
    //}
    //public IEnumerator CetaceanCoroutine()//くじら座
    //{
    //    //isCetacean = true;
    //    sa.cetaceanLevel += 1;
    //    //カメラ揺らす
    //    Coroutine shake = followPlayer.StartCoroutine(followPlayer.Shake(0.2f));
    //    //レベルに応じ壊す数増えてく
    //    for (int i = 0; i < sa.cetaceanLevel; i++)
    //    {
    //        var target = FetchRandomObjectWithTag("Meteorite");
    //        if (target)
    //        {
    //            var ef = GetFreeObject(meteoriteEffectList, meteoriteEffect);
    //            ef.SetActive(true);
    //            ef.transform.position = target.transform.position;
    //            target.SetActive(false);
    //        }
    //        else
    //        {
    //            break;
    //        }
    //        yield return new WaitForSeconds(0.02f);
    //    }
    //    yield return new WaitForSeconds(0.1f);
    //    //isCetacean = false;
    //}
    //#endregion

    public void SkillTaurus()
    {
        if (!isTaurus)
        {
            isDashCoolTime = false;
            isTaurus = true;
        }
        else
        {
            isTaurus = false;
        }
    }
    public void SkillLeo()
    {
        if (!isLeo)
        {           
            isLeo = true;           
        }
        else
        {            
            isLeo = false;
        }
    }
    public void SkillCapricorn()
    {
        if (!isCapricorn)
        {
            isCapricorn = true;
        }
        else
        {
            isCapricorn = false;
        }
    }
    public void SkillScorpion()
    {
        if (!isScorpion)
        {
            isScorpion = true;
        }
        else
        {
            isScorpion = false;
        }
    }
    public void SkillPisces()
    {
        if(!isPisces)
        {
            isPisces = true;
        }
        else
        {
            isPisces = false;
        }
    }
}
