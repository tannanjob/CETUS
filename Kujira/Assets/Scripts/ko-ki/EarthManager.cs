using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthManager : MonoBehaviour
{
    public static EarthManager instance;

    public int EarthMAXHp;
    public static int EarthHp;   
    

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        EarthHp = EarthMAXHp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
