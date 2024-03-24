using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class SkillPopUp : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI skillName;
    [SerializeField] TextMeshProUGUI skillCaption;
    Vector3 startPos;
    Coroutine popCoroutine;
    Vector3 hidePos;
    public int order = 0;

    void Start()
    {
        startPos = transform.localPosition;
        transform.localPosition = startPos + new Vector3(-1f, 0, 0);
    }

    void Update()
    {

    }

    public void Pop(int kind, string name, string caption)
    {
        order = 0;
        StartCoroutine(PopCoroutine(kind));
        skillName.text = name;
        skillCaption.text = caption;
    }

    public IEnumerator PopCoroutine(int kind)
    {
        transform.localScale = new Vector3(1, 1, 1);
        transform.localPosition = startPos;
        transform.DOLocalMove(startPos + new Vector3(0.3f, 0, 0), 0.5f).SetEase(Ease.OutCubic);
        yield return null;
    }

    public void Ascend()
    {
        order += 1;
        if (order < 3) transform.DOLocalMoveY(transform.localPosition.y + 0.2f, 0.5f);
        else transform.DOScale(new Vector3(0, 0, 0), 0.5f);
    }

    public IEnumerator PopDown(){
        transform.DOScale(new Vector3(0, 0, 0), 0.5f).SetEase(Ease.InBack);
        yield return new WaitForSeconds(0.5f);
        
        transform.localPosition = startPos;
        gameObject.SetActive(false);
    }
}
