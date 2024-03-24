using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class ChooseStardust : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI itemName;
    public GameObject stardustParent;
    bool inside = false;
    [SerializeField] GameObject player;
    [SerializeField] PlayerController pc;
    [SerializeField] Image buttonImage;
    public GameObject holdStardust;
    public int holdStardustKind;
    [Tooltip("隕石取得範囲")][SerializeField] float range;
    bool isNear = false;

    void Start()
    {
        itemName.text = "";
        buttonImage.DOFade(0, 0.1f);
    }

    
    void Update()
    {
        var obj = FetchNearObject();
        if(obj != null){
            holdStardust = obj;
            holdStardustKind = obj.GetComponent<Stardust>().Kind;
            CanChoose(holdStardustKind);
            if(!isNear){
                isNear = true;
                itemName.DOFade(1, 0.5f);
                buttonImage.DOFade(1, 0.5f);
            }
        }
        else{
            if (isNear)
            {
                isNear = false;
                itemName.DOFade(0, 0.5f);
                buttonImage.DOFade(0, 0.5f);
                holdStardust = null;
                holdStardustKind = -1;
            }
        }
    }

    GameObject FetchNearObject()
    {
        float minimumDistance = range;
        GameObject target = null;
        foreach (Transform stardust in stardustParent.transform)
        {
            if(!stardust.gameObject.activeInHierarchy)continue;
            var targetDistance = Vector3.Distance(player.transform.position, stardust.position);
            if (targetDistance != 0 && targetDistance <= minimumDistance){
                minimumDistance = targetDistance;
                target = stardust.gameObject;
            }
        }

        return target;
    }

    public void CanChoose(int kind){
        switch(kind){
            case 0:
                itemName.text = "あかいかけら";
                break;
            case 1:
                itemName.text = "みどりのかけら";
                break;
            case 2:
                itemName.text = "あおいかけら";
                break;
        }
    }
}
