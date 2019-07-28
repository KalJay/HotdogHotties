using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnPointFollowers : MonoBehaviour
{
    public float timeBetweenSpawns = .5f;

    float nextTimeToSpawn = 0f;
    //float countDownTimer = 3f;
    public GameObject follower;


    public Transform[] spawnPoints;
    private static Dictionary<int, EnemyFollow> EnemyDict;

    private void Update()
    {
        /* CountDownTimer
        if (countDownTimer <= 0f{
            SpawnCar();
            countDownTimer = 3f;
        }
        else
        {
            countDownTimer -= Time.deltaTime;
        } */

        if (nextTimeToSpawn <= Time.time)
        {
            SpawnFollower();
            nextTimeToSpawn = Time.time + timeBetweenSpawns;
        }
    }

    void SpawnFollower()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];
        GameObject temp = Instantiate(follower, spawnPoint.position, spawnPoint.rotation);
        EnemyFollow ef = temp.GetComponent<EnemyFollow>();
        EnemyDict.Add(ef.enemyId, ef);
    }

    public void SpawnFollowerFromSpawnPoint(int index)
    {
        Transform spawnPoint = spawnPoints[index];

        GameObject temp = Instantiate(follower, spawnPoint.position, spawnPoint.rotation);
        EnemyFollow ef = temp.GetComponent<EnemyFollow>();
        EnemyDict.Add(ef.enemyId, ef);
    }

    public EnemyFollow GetEnemyFromID(int ID)
    {
        return EnemyDict[ID];
    }
}
