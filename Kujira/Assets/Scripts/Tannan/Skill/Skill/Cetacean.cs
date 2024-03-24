using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Cetacean", menuName = "ScriptableObjects/Cetacean")]
public class Cetacean : Skill
{
    [SerializeField]
    private float Duration = 0.1f;
    [SerializeField]
    private float WaitBreak = 0.02f;
    [SerializeField]
    private float ShakeDuration = 0.2f;

    private float cetaceanLevel = 0;
    public float CetaceanLevel_P { get { return cetaceanLevel; } }
    private GameObject player;
    private PlayerController playerController;
    private FollowPlayer followPlayer;
    private GameObject meteoriteEffect;
    private List<GameObject> meteoriteEffectList;
    private GameObject effectParent;
    public async override void Effect()
    {
        //isCetacean = true;
        cetaceanLevel += 1;
        //カメラ揺らす
        Coroutine shake = followPlayer.StartCoroutine(followPlayer.Shake(ShakeDuration));
        //レベルに応じ壊す数増えてく
        for (int i = 0; i < cetaceanLevel; i++)
        {
            var target = FetchRandomObjectWithTag("Meteorite");
            if (target)
            {
                var ef = GetFreeObject(meteoriteEffectList, meteoriteEffect);
                ef.SetActive(true);
                ef.transform.position = target.transform.position;
                target.SetActive(false);
            }
            else
            {
                break;
            }
            await UniTask.Delay(TimeSpan.FromSeconds(WaitBreak));
        }
        await UniTask.Delay(TimeSpan.FromSeconds(Duration));
    }

    public override void Init(SkillObjectPropaty skillObjectPropaty)
    {
        cetaceanLevel = 0;
        player = skillObjectPropaty.Whale_P;
        playerController = player.GetComponent<PlayerController>();
        followPlayer = playerController.FollowPlayer_P;
        meteoriteEffect = player.GetComponent<PlayerController>().MeteoriteEffect_P;
        meteoriteEffectList = player.GetComponent<PlayerController>().MeteoriteEffectList_P;
        effectParent = player.GetComponent<PlayerController>().EffectParent_P;
    }

    //tagNameタグがついたオブジェクトをランダムに返す
    GameObject FetchRandomObjectWithTag(string tagName)
    {
        var searchTargets = GameObject.FindGameObjectsWithTag(tagName);
        if (searchTargets.Length == 0) return null;
        return searchTargets[UnityEngine.Random.Range(0, searchTargets.Length)];
    }

    private GameObject GetFreeObject(List<GameObject> list, GameObject prefab)
    {
        // リストの中から探す
        foreach (var obj in list)
        {
            if (!obj.activeInHierarchy)
            { return obj; }
        }

        var newObj = Instantiate(prefab, player.transform.position, player.transform.rotation, effectParent.transform);
        list.Add(newObj);
        return newObj;
    }

}
