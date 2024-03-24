using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] GameObject player;
    GameObject target;
    float length;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(target){
            length = (target.transform.position - player.transform.position).magnitude;
            transform.position = (target.transform.position + player.transform.position)/2;
            transform.LookAt(target.transform);
        }
    }

    public void SetTarget(GameObject t){
        target = t;
    }


}
