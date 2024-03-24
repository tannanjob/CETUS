using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HPManager : MonoBehaviour
{
    
    [SerializeField] FollowPlayer fp;
    [SerializeField] Slider slider;
    [SerializeField] Slider lateSlider;
    bool finished = false;
 
     [SerializeField] GameObject deco;
    float preHp;
    float sliderValue;

    int maxHp;
    float hp;

    [SerializeField] EarthManager earthManager;
     
     // Start is called before the first frame update
     void Start()
     {
        
        maxHp = 500;
        hp = EarthManager.EarthHp;
        preHp = maxHp;
        sliderValue = maxHp;
        slider.value = sliderValue / maxHp;
        lateSlider.value = sliderValue / maxHp;
     }


    // Update is called once per frame
    void Update()
    {
        hp = EarthManager.EarthHp;
        deco.transform.Rotate(0,0,(maxHp - hp) * 0.002f + 0.1f);
        if(maxHp!= 0 && hp >= 0 && preHp != hp){
            slider.value = hp / maxHp;
            fp.StartCoroutine(fp.Shake(0.2f));
            StartCoroutine(Rewrite(preHp - hp));
            preHp = hp;
        }

        else if(!finished){
            finished = true;
        }
    }

    public IEnumerator Rewrite(float damage){
        float dec = 1;
        for(int i = 0;i < Mathf.Abs(damage);i++){
            if(damage>0)sliderValue -= 1;
            if(damage<0)sliderValue += 1;

            lateSlider.value = sliderValue / maxHp;

            //バーが変化する速度減衰
            dec += 0.1f;
            for(int j = 0;j < dec;j++){
                yield return null;
            }
        }
    }

}
