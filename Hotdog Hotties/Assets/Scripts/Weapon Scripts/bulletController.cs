using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletController : MonoBehaviour
{
    public GameObject firedBy;
    public Bullet bullet;

    private Rigidbody rb;
    private CapsuleCollider col;
    private float timer;
    public float destructionDelay = 5.0f;

    void Start()
    {
        timer = Time.time;
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        transform.Rotate(new Vector3(90.0f, 0.0f, 0.0f));
    }

    void Update()
    {
        rb.MovePosition(transform.position + (transform.up * bullet.bulletSpeed * Time.deltaTime));
        if (Time.time > timer + destructionDelay) {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter(Collider col) {
        if (bulletManager.HitTrigger(bullet, col))
        {
            bulletManager.GenerateHitEffect(gameObject);
            Destroy(gameObject);
        }
    }
}
