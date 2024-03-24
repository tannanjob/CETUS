using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllGetAchiev : MonoBehaviour
{
    [SerializeField]
    [Header("全実績解除")]
    public bool allAchievActive = false;

    public static bool AllAchievGet = false;

    private void Awake()
    {
        AllAchievGet = allAchievActive;
    }
}
