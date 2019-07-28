using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    private float timer = 0;
    private float destroyDuration = 5.0f;
    private float bulletSpeed = 10.0f;

    private Rigidbody rb;
    private CapsuleCollider col;

    void Start()
    {
        timer = Time.time;
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        if (Time.time >= timer + destroyDuration)
        {
            Destroy(gameObject);
        }

        rb.MovePosition(transform.position + (transform.forward * bulletSpeed * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("Player"))
        {
            other.GetComponent<PlayerHealthScript>().HurtPlayer(other.gameObject);
            Destroy(gameObject);
        } else
        {
            List<string> ignoreList = new List<string>();
            ignoreList.Add("Bullet");
            ignoreList.Add("Turret");
            ignoreList.Add("Enemy");
            ignoreList.Add("Terrain");
            ignoreList.Add("Boundary Death Colliders");
            ignoreList.Add("Boss");
            if(!ignoreList.Contains(other.tag))
            {
                Destroy(gameObject);
            }
        }
    }
}
