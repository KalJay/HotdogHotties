using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControllerP2 : MonoBehaviour
{
    public float lookRadius = 10f;
    private GameManager.Level spawnedLevel;

    Transform target;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        target = Player2Manager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        spawnedLevel = GameManager.currentLevel;
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnedLevel != GameManager.currentLevel)
        {
            Destroy(gameObject);
        }

        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);
            //Debug.Log("Player Detected");
            if (distance <= agent.stoppingDistance)
            {
                //attack the target
                //face the target
                FaceTarget();
            }
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    public void UpdateTarget(Transform target)
    {
        this.target = target;
    }

    public bool CompareTarget(Transform compare)
    {
        return target.transform.Equals(compare);
    }

    public void FindNewTarget()
    {
        float playerOneDist = Vector3.Distance(transform.position, GameManager.playerOne.transform.position);
        float playerTwoDist = Vector3.Distance(transform.position, GameManager.playerTwo.transform.position);

        if (playerOneDist >= playerTwoDist)
        {
            target = GameManager.playerOne.transform;
        }
        else
        {
            target = GameManager.playerTwo.transform;
        }
    }
}
