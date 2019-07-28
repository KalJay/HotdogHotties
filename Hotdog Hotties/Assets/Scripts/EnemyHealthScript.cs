using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyHealthScript : MonoBehaviour
{
    public float startingHealth;
    private float healthPoints;
    private bool done = true, bossDone = true;
    public GameObject explosionPrefab, deathPrefab;
    public GameObject petrolCan, coin;
    public GameObject emoji;
    public Slider bossHealthSlider;
    public GameObject WinScreen;
    public Sprite[] emojis;

    void Start()
    {
        healthPoints = startingHealth;
    }

    void Update()
    {
        if (healthPoints <= 0)
        {
            
            if (gameObject.CompareTag("Enemy"))
            {
                if(done)
                    GameObject.FindGameObjectWithTag("AudioObjects").transform.GetChild(2).GetComponent<AudioSource>().Play();
                StartCoroutine(EnemyDeath());
                done = false;
            }
            if (gameObject.CompareTag("Boss") && bossDone)
            {
                StartCoroutine(BossDeath());
            }
            if (gameObject.CompareTag("Destructable") || gameObject.CompareTag("Turret"))
            {
                GameObject.FindGameObjectWithTag("AudioObjects").transform.GetChild(3).GetComponent<AudioSource>().Play();
                Instantiate(explosionPrefab, transform.position, transform.rotation, null);
                GenerateReward();
                Destroy(gameObject);
            }
           
        }
    }

    public void HurtEnemy(float damage)
    {
        healthPoints -= damage;
        if (gameObject.CompareTag("Boss") && healthPoints > 0)
        {
            print(healthPoints);
            bossHealthSlider.value = healthPoints;
        }
        if (gameObject.CompareTag("Enemy") && healthPoints > 0)
        {
            StartCoroutine(HitStall());
        }
        if (gameObject.CompareTag("Destructable") || gameObject.CompareTag("Turret") && healthPoints > 0)
        {
            StartCoroutine(GetHit());
        }
    }

    void GenerateReward()
    {
        int num = Random.Range(0, 10);
        if (num <= 5)
            Instantiate(petrolCan, transform.position, transform.rotation, null);
        if (num > 5)
            Instantiate(coin, transform.position, transform.rotation, null);
    }

    IEnumerator GetHit()
    {
        GameObject.FindGameObjectWithTag("AudioObjects").transform.GetChild(2).GetComponent<AudioSource>().Play();
        transform.Rotate(0, -10, 0);
        yield return new WaitForSeconds(0.05f);
        transform.Rotate(0, 10, 0);
    }

    IEnumerator HitStall()
    {
        GameObject.FindGameObjectWithTag("AudioObjects").transform.GetChild(2).GetComponent<AudioSource>().Play();
        emoji.GetComponent<SpriteRenderer>().sprite = emojis[1];
        GetComponent<NavMeshAgent>().speed = 0;
        yield return new WaitForSeconds(0.5f);
        GetComponent<NavMeshAgent>().speed = 2;
        emoji.GetComponent<SpriteRenderer>().sprite = emojis[0];
    }

    IEnumerator EnemyDeath()
    {
        emoji.GetComponent<SpriteRenderer>().sprite = emojis[2];
        GetComponent<NavMeshAgent>().speed = 0;
        yield return new WaitForSeconds(1f);
        GameObject.FindGameObjectWithTag("AudioObjects").transform.GetChild(3).GetComponent<AudioSource>().Play();
        GenerateReward();
        Instantiate(deathPrefab, transform.position, transform.rotation, null);
        Destroy(gameObject);
    }

    IEnumerator BossDeath()
    {
        bossDone = false;
        Instantiate(deathPrefab, new Vector3(1.22f, 4.89f, 348.57f), transform.rotation, null);
        yield return new WaitForSeconds(0.5f);
        WinScreen.SetActive(true);
        Destroy(gameObject);
    }
}
