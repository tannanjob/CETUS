using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;

    private float time = 0;
    private float deadTime = 30;
    private GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time++;
        if(time > deadTime * 60) 
        {
            Destroy(bullet);
        }
    }

    private void OnParticleTrigger()
    {
        Destroy(bullet);
        target.GetComponent<MeteoMove>().HitBeam();
    }

    //どのコライダーと当たり判定をとるか設定する
    public void SetCollider(GameObject obj)
    {
        this.GetComponent<ParticleSystem>().trigger.SetCollider(0, obj.GetComponent<Collider>());
        target = obj;
    }

}
