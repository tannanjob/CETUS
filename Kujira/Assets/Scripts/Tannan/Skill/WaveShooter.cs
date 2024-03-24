using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveShooter : MonoBehaviour
{
    [SerializeField]
    private GameObject Wave;
    [SerializeField]
    private GameObject MeteoParent;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartScorpio()
    {
        StartCoroutine(Scorpio());
    }


    private IEnumerator Scorpio()
    {
        int meteoNum = MeteoParent.transform.childCount;
        GameObject wave = Instantiate(Wave, this.transform.position, this.transform.rotation);
        List<GameObject> target = new List<GameObject>(); 

        for (int i = 0; i < meteoNum; i++)
        {
            if (MeteoParent.transform.GetChild(i).gameObject.activeSelf)
            {
                target.Add(MeteoParent.transform.GetChild(i).gameObject);
                wave.transform.GetChild(2).GetComponent<Wave>().SetCollider(MeteoParent.transform.GetChild(i).gameObject, i);
            }
        }
        wave.transform.GetChild(2).GetComponent<Wave>().SetWave(target);
        yield return null;
    } 
}
