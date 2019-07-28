using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : MonoBehaviour
{
    private float time = 0;

    void Update()
    {
        time += Time.deltaTime;
        if(time >= (transform.parent.GetComponent<TurretScript>().shootSurvival))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("Player"))
        {
            other.GetComponent<PlayerHealthScript>().HurtPlayer(other.gameObject);
            Destroy(gameObject);
        }
        if(!other.tag.Contains("Turret"))
            Destroy(gameObject);
    }
}
