using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LifeSystem : MonoBehaviour
{
    public int startingLives = 5;
    public GameObject resultsScreen;
    public Text livesText1, livesText2;
    private float respawnTime1, respawnTime2;
    private bool respawn1, respawn2, done1, done2;
    public GameObject respawnText1, respawnText2, respawnPoint1, respawnPoint2;
    private int deadPlayers = 0;
    public GameObject gameOverScreen;
    public AudioSource respawnSound;

    void Start()
    {
        respawnTime1 = 5;
        respawnText1.gameObject.SetActive(false);
        respawnText2.gameObject.SetActive(false);
        respawnTime2 = respawnTime1;
        GameManager.player1stats.lives = startingLives;
        GameManager.player2stats.lives = startingLives;
        livesText1.text = "x" + GameManager.player1stats.lives;
        livesText2.text = "x" + GameManager.player2stats.lives;
    }

    void Update()
    {
        if(deadPlayers == 2)
        {
            gameOverScreen.SetActive(true);
        }
        if (respawn1)
        {
            done1 = true;
            respawnTime1 -= Time.deltaTime;
            respawnText1.gameObject.SetActive(true);
            respawnText1.transform.GetChild(1).GetComponent<Text>().text = "" + (int)respawnTime1;
            GameManager.playerOne.SetActive(false);
            if (respawnTime1 <= 0)
                respawn1 = false;
        }
        if (!respawn1 && done1)
        {
            respawnSound.Play();
            GameManager.playerOne.SetActive(true);
            respawnText1.gameObject.SetActive(false);
            GameManager.playerOne.GetComponent<PlayerHealthScript>().FullHealth(GameManager.playerOne);
            respawnTime1 = 5;
            GameManager.playerOne.transform.position = respawnPoint1.transform.position;
            Instantiate(Resources.Load("vfx_Hit_Comet_Blue"), GameManager.playerOne.transform.position, GameManager.playerOne.transform.rotation, null);
            done1 = false;
        }

        if (respawn2)
        {
            done2 = true;
            respawnTime2 -= Time.deltaTime;
            respawnText2.gameObject.SetActive(true);
            respawnText2.transform.GetChild(1).GetComponent<Text>().text = "" + (int)respawnTime2;
            GameManager.playerTwo.SetActive(false);
            if (respawnTime2 <= 0)
                respawn2 = false;
        }
        if (!respawn2 && done2)
        {
            respawnSound.Play();
            GameManager.playerTwo.SetActive(true);
            respawnText2.gameObject.SetActive(false);
            GameManager.playerTwo.GetComponent<PlayerHealthScript>().FullHealth(GameManager.playerTwo);
            respawnTime2 = 5;
            GameManager.playerTwo.transform.position = respawnPoint2.transform.position;
            Instantiate(Resources.Load("vfx_Hit_Comet_Blue"), GameManager.playerTwo.transform.position, GameManager.playerTwo.transform.rotation, null);
            done2 = false;
        }
    }

    public void GainLife(GameObject player)
    {
        if (player.CompareTag("PlayerOne"))
            GameManager.player1stats.lives++;
        if (player.CompareTag("PlayerTwo"))
            GameManager.player2stats.lives++;
    }

    public void LoseLife(GameObject player)
    {
        if (player.CompareTag("PlayerOne"))
        {
            GameManager.player1stats.lives--;
            livesText1.text = "x" + GameManager.player1stats.lives;
            if (GameManager.player1stats.lives > 0)
            {
                UpdateEnemyTarget(GameManager.playerOne.transform, GameManager.playerTwo.transform);
                respawn1 = true;
            }
            else
            {
                GameManager.playerOne.SetActive(false);
                deadPlayers++;
            }
        }

        if (player.CompareTag("PlayerTwo"))
        {
            GameManager.player2stats.lives--;
            livesText2.text = "x" + GameManager.player2stats.lives;
            if(GameManager.player2stats.lives > 0)
            {
                UpdateEnemyTarget( GameManager.playerTwo.transform, GameManager.playerOne.transform);
                respawn2 = true;
            }
            else
            {
                GameManager.playerTwo.SetActive(false);
                deadPlayers++;
            }
        }
    }

    private void UpdateEnemyTarget(Transform fromPlayer, Transform toPlayer)
    {
        if(deadPlayers != 2)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach(GameObject enemy in enemies)
            {
                EnemyController ec = enemy.GetComponent<EnemyController>();
                EnemyControllerP2 ec2 = enemy.GetComponent<EnemyControllerP2>();
                if(ec != null && ec.CompareTarget(fromPlayer))
                {
                    ec.UpdateTarget(toPlayer);
                } else
                {
                    if(ec2 != null && ec2.CompareTarget(fromPlayer))
                    {
                        ec2.UpdateTarget(toPlayer);
                    }
                }
            }
        }
    }

    public void TryAgain()
    {
        SceneManager.LoadScene(1);
    }
    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
}
