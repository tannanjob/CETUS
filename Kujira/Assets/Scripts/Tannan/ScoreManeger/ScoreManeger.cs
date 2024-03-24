using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManeger : MonoBehaviour
{
    public static ScoreManeger Instance;
    
    private int score;
    private bool isDouble = false;
    private float time = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Update()
    {
        if(isDouble)
        {
            time -= Time.deltaTime;
        }
        if(time < 0)
        {
            isDouble = false;
            time = 0;
        }
    }
    public void StartLibra(float t)
    {
        time = t;
        isDouble = true;
    }

    public void MeteoScore()
    {
        score += 1;
        if (isDouble)
        {
            score += 1;
        }
    }

    public void AchieveScore()
    {
        score += 1;
        if (isDouble)
        {
            score += 1;
        }
    }

    public int GetScore()
    {
        return score;
    }
}
