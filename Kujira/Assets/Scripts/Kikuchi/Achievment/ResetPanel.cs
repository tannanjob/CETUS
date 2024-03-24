using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPanel : MonoBehaviour
{
    private void OnDisable()
    {
        foreach (Transform item in this.transform)
        {
            item.gameObject.SetActive(false);
        }
        this.transform.Find("AcievPanel1").gameObject.SetActive(true);
    }
}
