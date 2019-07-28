using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SausageHarpoonController : MonoBehaviour
{
    public Bullet bullet;
    public GameObject firedBy;

    private float timer;
    private Rigidbody rb;
    private CapsuleCollider col;

    private const float travelTime = 1.0f;
    private const float destructionDelay = 1.5f;
    private float landedTime;

    void Start()
    {
        timer = Time.time;
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        transform.Rotate(new Vector3(90.0f, 0.0f, 0.0f));
    }
    
    void Update()
    {
        

        if(Time.time < timer + travelTime)
        {
            rb.MovePosition(transform.position + (transform.up * bullet.bulletSpeed * Time.deltaTime));
            landedTime = Time.time;
        } else
        {
            col.enabled = false;
            if (Time.time >= landedTime + destructionDelay)
            {
                Destroy(gameObject);
                return;
            }
        }
    }

    public void OnTriggerEnter(Collider col)
    {
        if (bulletManager.HitTrigger(bullet, col))
        {
            bulletManager.GenerateHitEffect(gameObject);
            Destroy(gameObject);
        }
    }
}
