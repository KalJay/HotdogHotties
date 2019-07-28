using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float speed;
    private Transform target;
    private static int enemyidTotal = 0;
    public int enemyId;

    private Vector2 prevPos;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("PlayerOne").GetComponent <Transform>();
        enemyId = enemyidTotal;
        enemyidTotal++;
        prevPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        //if(!prevPos.Equals(transform.position))
        //{
        //    //GameManager.Send(GameManager.CreateEnemyMovementUpdate(transform.position));
        //    prevPos = transform.position;
        //}
    }

    public void MoveTo(Vector2 position)
    {
        transform.position = position;
    }
}
