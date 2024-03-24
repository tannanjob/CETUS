using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterShooter : MonoBehaviour
{
    [SerializeField]
    private GameObject Water;
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

    public void StartAquarius()
    {
        StartCoroutine(Aquarius());
    }


    private IEnumerator Aquarius()
    {
        int meteoNum = MeteoParent.transform.childCount;
        GameObject water = Instantiate(Water, this.transform.position, this.transform.rotation);
        List<GameObject> target = new List<GameObject>();

        for (int i = 0; i < meteoNum; i++)
        {
            if (MeteoParent.transform.GetChild(i).gameObject.activeSelf)
            {
                target.Add(MeteoParent.transform.GetChild(i).gameObject);
                water.transform.GetComponent<Water>().SetCollider(MeteoParent.transform.GetChild(i).gameObject, i);
            }
        }
        water.transform.GetComponent<Water>().SetWater(target);
        yield return null;
    }
}
