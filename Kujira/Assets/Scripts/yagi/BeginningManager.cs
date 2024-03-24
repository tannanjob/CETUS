using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class BeginningManager : MonoBehaviour
{
    [SerializeField] GameObject[] UI;
    [SerializeField] Vector3[] UIStartPos;
    [SerializeField] PlayerController pc;
    [SerializeField] TimeManager tm;
    [SerializeField] GameObject im;
    [SerializeField] TextMeshProUGUI startSign;

    IEnumerator Start()
    {
        yield return null;
        pc.enabled = false;
        tm.enabled = false;
        im.SetActive(false);
        for (int i = 0; i < UI.Length; i++)
        {
            UIStartPos[i] = UI[i].transform.localPosition;
            if(i == 0)UI[i].transform.localPosition = UIStartPos[i] + new Vector3(-0.6f, 0, 0);
            else if (i != 0) UI[i].transform.localPosition = UIStartPos[i] + new Vector3(1.5f, 0, 0);
        }
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < UI.Length;i++){
            SoundPlayer.SP.SM.Play("UIMove");
            UI[i].transform.DOLocalMove(UIStartPos[i], 0.5f).SetEase(Ease.OutQuint);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(1f);
        startSign.transform.localScale = new Vector3(0, 0.01f, 1);
        startSign.transform.DOScale(new Vector3(0.01f, 0.01f, 1), 0.2f);
        pc.enabled = true;
        tm.enabled = true;
        im.SetActive(true);
        SoundPlayer.SP.BGMset.SetBGM(SceneController.SceneType.toGame);
        yield return new WaitForSeconds(1f);
        startSign.transform.DOScale(new Vector3(0, 0, 1), 0.5f).SetEase(Ease.InBack);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
