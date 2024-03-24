using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Capricorn", menuName = "ScriptableObjects/Capricorn")]
public class Capricorn : Skill
{
    [SerializeField]
    private float CapricornDuration = 10;

    private GameObject player;
    private PlayerController playerController;
    private GameObject meteoriteEffect;
    private List<GameObject> meteoriteEffectList;
    private GameObject effectParent;
    public async override void Effect()
    {
        playerController.SkillCapricorn();
        await UniTask.Delay(TimeSpan.FromSeconds(CapricornDuration));
        playerController.SkillCapricorn();
    }

    public override void Init(SkillObjectPropaty skillObjectPropaty)
    {
        player = skillObjectPropaty.Whale_P;
        playerController = player.GetComponent<PlayerController>();
        meteoriteEffect = player.GetComponent<PlayerController>().MeteoriteEffect_P;
        meteoriteEffectList = player.GetComponent<PlayerController>().MeteoriteEffectList_P;
        effectParent = player.GetComponent<PlayerController>().EffectParent_P;
    }

    //最も近い位置にある隕石を壊す
    public void DestroyTheNearestMeteorite()
    {
        GameObject target = FetchNearObjectWithTag("Meteorite");
        if (target)
        {
            target.gameObject.SetActive(false);
            var ef = GetFreeObject(meteoriteEffectList, meteoriteEffect);
            ef.SetActive(true);
            ef.transform.position = target.transform.position;
        }
    }

    //limit内の範囲の最も近いtagNameタグのオブジェクトを返す　なかったらnull
    private GameObject FetchNearObjectWithTag(string tagName, float limit = 999)
    {
        Dictionary<float, GameObject> dict = new Dictionary<float, GameObject>();
        var searchTargets = GameObject.FindGameObjectsWithTag(tagName);
        if (searchTargets.Length == 0) return null;

        foreach (var searchTarget in searchTargets)
        {
            var targetDistance = Vector3.Distance(player.transform.position, searchTarget.transform.position);
            if (targetDistance != 0 && targetDistance <= limit)
            {
                dict.Add(targetDistance, searchTarget);
            }
        }

        var sortedKeys = new List<float>(dict.Keys);
        if (sortedKeys.Count == 0) return null;
        sortedKeys.Sort();
        return dict[sortedKeys[0]];
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
