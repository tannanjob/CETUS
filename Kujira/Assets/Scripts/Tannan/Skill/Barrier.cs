using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    private int barrierHp = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(barrierHp == 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Meteorite")
        {
            barrierHp--;
            other.gameObject.SetActive(false);
            SoundPlayer.SP.SM.Play("Cancer");
        }
    }

    //バリア初期化用
    //これが呼び出されるとバリアが張られる
    public void SetBarrier(int num)
    {
        barrierHp = num;
        this.gameObject.SetActive(true);
    }
}
