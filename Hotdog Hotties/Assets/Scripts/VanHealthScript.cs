using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VanHealthScript : MonoBehaviour
{
    public Slider healthBar;
    public int startingHealth;
    private int healthPoints;
    private float timer = 2, healthSize;
    private bool damage = true;

    void Start()
    {
        startingHealth = 10;
        healthPoints = startingHealth;
    }

    void Update()
    {
        //print(healthPoints);
        
        if (healthPoints <= 0)
            FindObjectOfType<ResultScript>().Lose();

        //looped timer in case enemy is constantly standing at van attacking it
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            
        }
        if (timer < 0)
            damage = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            if (damage)
            {
                healthPoints--;
                timer = 2;
                if (healthBar.value > 0)
                    healthBar.value -= 0.1f;
                damage = false;
            }
        }
    }
}
