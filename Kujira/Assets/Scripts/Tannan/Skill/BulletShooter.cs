using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    [SerializeField]
    private GameObject Bullet;
    [SerializeField]
    private GameObject MeteoParent;

    //弾の発射間隔
    private float shootTime;
    private bool canShoot;
    //スキルの有効範囲
    private float distance;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartSagittarius(float t, float l)
    {
        distance = l;
        shootTime = t;
        canShoot = true;
        StartCoroutine(Sagittarius());
    }

    public void EndSagittarius()
    {
        canShoot = false;
    }

    private IEnumerator Sagittarius()
    {
        if (canShoot)
        {
            int meteoNum = MeteoParent.transform.childCount;

            for (int i = 0; i < meteoNum; i++)
            {
                if(MeteoParent.transform.GetChild(i).gameObject.activeSelf)
                {
                    float dis = Vector3.Distance(MeteoParent.transform.GetChild(i).position, transform.position);
                    if(dis < distance)
                    {
                        ShootBullet(MeteoParent.transform.GetChild(i).gameObject);
                    }                    
                }               
            }
            yield return new WaitForSeconds(shootTime);
            StartCoroutine(Sagittarius());
        }
    }

    private void ShootBullet(GameObject meteo)
    {
        GameObject bullet = Instantiate(Bullet, this.transform.position, this.transform.rotation);
        SoundPlayer.SP.SM.Play("Bullet");
        bullet.transform.LookAt(meteo.transform);
        bullet.transform.GetChild(0).GetChild(3).GetComponent<Bullet>().SetCollider(meteo);        
    }
}
