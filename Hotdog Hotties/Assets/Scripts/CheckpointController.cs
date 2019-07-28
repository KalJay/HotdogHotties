using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    public GameManager.Level CheckPointAtEndOfLevel = GameManager.Level.None;
    public bool EndOfGame;
    private bool changeLevel, startMusic;
    private AudioSource camAudio;
    public AudioClip levelMusic;
    public GameObject[] turrets;

    private void Start()
    {
        TurretManagement();
        camAudio = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (changeLevel)
        {
            camAudio.volume -= Time.deltaTime;
            if(camAudio.volume <= 0)
            {
                startMusic = true;
                changeLevel = false;
            }
        }
        if (startMusic)
        {
            camAudio.clip = levelMusic;
            camAudio.Play();
            camAudio.volume += Time.deltaTime;
            if (camAudio.volume >= 0.36f)
            {
                startMusic = false;
            }
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag.Equals("CheckpointChecker"))
        {
            if(col.transform.parent.position.z > transform.position.z && GameManager.currentLevel == CheckPointAtEndOfLevel)
            {
                if(EndOfGame)
                {
                    Debug.Log("Victory!");
                } else
                {
                    changeLevel = true;
                    GameManager.GoToLevel(CheckPointAtEndOfLevel + 1);
                    TurretManagement();
                }
            }
        }
    }

    private void TurretManagement()
    {
        switch (GameManager.currentLevel)
        {
            case GameManager.Level.Level1:
                foreach (GameObject turret in turrets)
                    turret.SetActive(false);
                turrets[0].SetActive(true);
                break;
            case GameManager.Level.Level2:
                foreach (GameObject turret in turrets)
                    turret.SetActive(false);
                turrets[1].SetActive(true);
                break;
            case GameManager.Level.Level3:
                foreach (GameObject turret in turrets)
                    turret.SetActive(false);
                turrets[2].SetActive(true);
                break;
            case GameManager.Level.Level4:
                foreach (GameObject turret in turrets)
                    turret.SetActive(false);
                turrets[3].SetActive(true);
                break;
            case GameManager.Level.Level5:
                foreach (GameObject turret in turrets)
                    turret.SetActive(false);
                break;
        }
    }
}
