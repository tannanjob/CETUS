using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stardust : MonoBehaviour
{
    public int Kind { get; private set; }
    [SerializeField] Material[] red;
    [SerializeField] Material[] green;
    [SerializeField] Material[] blue;
    

    public void Generated(int value)
    {
        print(value);
        Kind = value;

        //Kindによって色を変える
        switch(Kind){
            case 0:
                GetComponent<Renderer>().materials = red;
                break;
            case 1:
                GetComponent<Renderer>().materials = green;
                break;
            case 2:
                GetComponent<Renderer>().materials = blue;
                break;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
