using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    public GameObject bulletPrefab;
    private GameObject shootPoint;
    public float shootIntervalTime;
    public float shootSpeed;
    public float shootSurvival;
    private float time;

    void Start()
    {
        time = shootIntervalTime;
        shootPoint = transform.GetChild(0).gameObject;
    }

    void Update()
    {
        time -= Time.deltaTime;
        if(time <= 0)
        {
            GameObject bullet1 = Instantiate(bulletPrefab, shootPoint.transform.position, shootPoint.transform.rotation, transform);
            GameObject bullet2 = Instantiate(bulletPrefab, shootPoint.transform.position, shootPoint.transform.rotation, transform);
            GameObject bullet3 = Instantiate(bulletPrefab, shootPoint.transform.position, shootPoint.transform.rotation, transform);
            GameObject bullet4 = Instantiate(bulletPrefab, shootPoint.transform.position, shootPoint.transform.rotation, transform);
            bullet1.GetComponent<Rigidbody>().AddForce(new Vector3(shootSpeed, 0, 0));
            bullet2.GetComponent<Rigidbody>().AddForce(new Vector3(-shootSpeed, 0, 0));
            bullet3.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, shootSpeed));
            bullet4.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, -shootSpeed));
            bullet1.transform.rotation = Quaternion.Euler(0, 90, 0);
            bullet2.transform.rotation = Quaternion.Euler(0, 270, 0);
            bullet3.transform.rotation = Quaternion.Euler(0, 0, 0);
            bullet4.transform.rotation = Quaternion.Euler(0, 180, 0);
            time = shootIntervalTime;
        }

    }
}
