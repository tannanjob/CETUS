using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTail : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider c) {
        if(c.gameObject.tag == "Meteorite"){
            transform.root.gameObject.GetComponent<PlayerController>().TailOnTriggerEnter(c);
        }
    }
}
