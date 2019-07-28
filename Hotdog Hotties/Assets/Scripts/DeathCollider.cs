using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCollider : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Contains("Player"))
        {
            FindObjectOfType<LifeSystem>().LoseLife(collision.gameObject);
        }
        else
            Destroy(collision.gameObject);
    }
}
