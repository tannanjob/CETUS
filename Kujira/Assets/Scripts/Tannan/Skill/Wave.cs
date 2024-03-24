using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Wave : MonoBehaviour
{
    [SerializeField]
    private GameObject wave;

    private List<GameObject> targets;
    private float time = 0;
    private float deadTime = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time++;
        if (time > deadTime * 60)
        {
            Destroy(wave);
        }
    }

    private void OnParticleTrigger()
    {
        if(targets.Count > 0)
        {
            targets[0].GetComponent<MeteoMove>().HitBeam();
            targets.RemoveAt(0);
        }
    }
    //初期化
    public void SetWave(List<GameObject> target)
    {
        targets = target;
        targets.Sort((a, b) => (int)(Vector3.Distance(this.transform.position, a.transform.position)) - (int)(Vector3.Distance(this.transform.position, b.transform.position)));
    }
    //どのコライダーと当たり判定をとるか設定する
    public void SetCollider(GameObject obj, int num)
    {
        this.GetComponent<ParticleSystem>().trigger.SetCollider(num, obj.GetComponent<Collider>());   
    }
}
