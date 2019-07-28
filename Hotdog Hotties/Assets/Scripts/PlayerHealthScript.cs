using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthScript : MonoBehaviour
{
    public Slider healthBar;
    public int startHealth;
    private int healthPoints;
    public Text healthText;
    private float timer = 1, healthSize;
    private bool damage = true;
    private bool loseLife1 = true, loseLife2 = true;
    private LifeSystem lifeSystem;
    public AudioSource hit, explode;

    void Start()
    {
        startHealth = 10;
        healthPoints = startHealth;
        lifeSystem = FindObjectOfType<LifeSystem>();
    }

    void Update()
    {
        if (healthPoints <= 0 && ((gameObject.Equals(GameManager.playerOne) && loseLife1) || (gameObject.Equals(GameManager.playerTwo) && loseLife2)))
        {
            Instantiate(Resources.Load("vfx_Hit_Radioactive"), transform.position, transform.rotation, null);
            explode.Play();
            lifeSystem.LoseLife(this.gameObject);
            if (gameObject.tag.Contains("PlayerOne"))
                loseLife1 = false;
            if (gameObject.tag.Contains("PlayerTwo"))
                loseLife2 = false;
        }

        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        if (timer < 0)
        {
            damage = true;
        }
    }

    public void UpdatePlayerHealth(int newHealth) {
        healthPoints = newHealth;
        healthBar.value = healthPoints;
    }

    public void IncreaseHealth(GameObject player)
    {
        healthBar.maxValue += 2;
        healthBar.value = healthBar.maxValue;
        if (player.CompareTag("PlayerOne"))
        {
            healthPoints = startHealth + GameManager.player1stats.healthBuffCount;
            healthText.text = healthPoints + "/" + (startHealth + GameManager.player1stats.healthBuffCount);
        }
        if (player.CompareTag("PlayerTwo"))
        {
            healthPoints = startHealth + GameManager.player2stats.healthBuffCount;
            healthText.text = healthPoints + "/" + (startHealth + GameManager.player2stats.healthBuffCount);
        }
    }

    public void FullHealth(GameObject player)
    {
        healthBar.value = 10;
        if (player.CompareTag("PlayerOne"))
        {
            healthPoints = startHealth + GameManager.player1stats.healthBuffCount;
            healthText.text = healthPoints + "/" + (startHealth + GameManager.player1stats.healthBuffCount);
            loseLife1 = true;
        }
        if (player.CompareTag("PlayerTwo"))
        {
            healthPoints = startHealth + GameManager.player2stats.healthBuffCount;
            healthText.text = healthPoints + "/" + (startHealth + GameManager.player2stats.healthBuffCount);
            loseLife2 = true;
        }
    }

    public void HurtPlayer(GameObject player)
    {
        if (player.CompareTag("PlayerOne"))
        {
            healthPoints--;
            healthText.text = healthPoints + "/" + (startHealth + GameManager.player1stats.healthBuffCount);
            healthBar.value = healthPoints;
        }
        if (player.CompareTag("PlayerTwo"))
        {
            healthPoints--;
            healthText.text = healthPoints + "/" + (startHealth + GameManager.player2stats.healthBuffCount);
            healthBar.value = healthPoints;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Enemy") && damage)
        {
            healthPoints--;
            if (healthPoints > 0)
                hit.Play();
            timer = 1;
            healthBar.value = healthPoints;
            if (gameObject.CompareTag("PlayerOne"))
                healthText.text = healthPoints + "/" + (startHealth + GameManager.player1stats.healthBuffCount);
            if (gameObject.CompareTag("PlayerTwo"))
                healthText.text = healthPoints + "/" + (startHealth + GameManager.player2stats.healthBuffCount);
            damage = false;
        }
    }
}
