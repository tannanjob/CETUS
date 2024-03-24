using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaletShooter : MonoBehaviour
{
    [SerializeField]
    private GameObject Bullet;

    private GameObject Player;
    private GameObject MeteoParent;

    private int time;
    private float deadTime;
    private float shootTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time++;
        if(time >= deadTime * 60)
        {
            Destroy(this.gameObject);
        }
    }

    public void SetTalet(float dt, float st, GameObject player, GameObject meteoParent)
    {
        Player = player;
        MeteoParent = meteoParent;

        time = 0;
        this.gameObject.SetActive(true);
        deadTime = dt;
        shootTime = st;
        this.gameObject.transform.position = Player.transform.position;
        StartCoroutine(Gemini());       
    }

    private IEnumerator Gemini()
    {
        int meteoNum = MeteoParent.transform.childCount;

        for (int i = 0; i < meteoNum; i++)
        {
            if (MeteoParent.transform.GetChild(i).gameObject.activeSelf)
            {
                ShootBullet(MeteoParent.transform.GetChild(i).gameObject);
                SoundPlayer.SP.SM.Play("Bullet");
                break;
            }
        }

        yield return new WaitForSeconds(shootTime);
        if(this.gameObject.activeSelf)
        {
            StartCoroutine(Gemini());
        }      
    }

    private void ShootBullet(GameObject meteo)
    {
        GameObject bullet = Instantiate(Bullet, this.transform.position, this.transform.rotation);
        this.gameObject.transform.LookAt(meteo.transform);
        bullet.transform.LookAt(meteo.transform);
        bullet.transform.GetChild(0).GetChild(3).GetComponent<Bullet>().SetCollider(meteo);
    }
}
