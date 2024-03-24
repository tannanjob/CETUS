using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolPrefab : MonoBehaviour
{
    [SerializeField] float limit;
    float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time > limit){
            time = 0;
            this.gameObject.SetActive(false);
        }
    }
}
