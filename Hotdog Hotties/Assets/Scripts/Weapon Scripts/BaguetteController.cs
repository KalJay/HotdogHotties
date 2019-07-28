using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaguetteController : MonoBehaviour
{
    public Bullet bullet;
    public GameObject firedBy;

    private float timer;
    private float currentAngle;
    
    private CapsuleCollider col;

    private float totalAngle = 120.0f;

    void Start()
    {
        timer = Time.time;
        col = GetComponent<CapsuleCollider>();
        transform.RotateAround(firedBy.transform.position, firedBy.transform.up, -totalAngle / 2.0f);
        transform.Rotate(new Vector3(90.0f, 0.0f, 0.0f));
    }
    
    void Update()
    {
        if(Time.time > timer + bullet.bulletSpeed)
        {
            Destroy(gameObject);
        }
        
        transform.RotateAround(firedBy.transform.position, firedBy.transform.up, Time.deltaTime * totalAngle * Mathf.Pow(bullet.bulletSpeed, -1));
    }

    private void OnTriggerEnter(Collider other)
    {
        bulletManager.HitTrigger(bullet, other);
    }
}
