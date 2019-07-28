using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour
{
    private Transform target;
    private Animator animator;

    [SerializeField] private GameObject fireball;
    private float timer;
    private float attackTimer;
    private float attackSpeed = 1.0f;
    [SerializeField] private float retargettingDelay = 10.0f;

    
    void Start()
    {
        animator = GetComponent<Animator>();
        timer = Time.time;
        attackTimer = Time.time;
        FindNewTarget();
    }
    
    void Update()
    {
        if(Time.time >= timer + retargettingDelay)
        {
            FindNewTarget();
            timer = Time.time;
        }

        if (Time.time >= attackTimer + attackSpeed)
        {
            AttackTarget();
            animator.SetTrigger("Skill1");
            attackTimer = Time.time;
        }

    }

    private void AttackTarget()
    {
        FaceTarget();
        CreateBullet(-30f);
        CreateBullet(0f);
        CreateBullet(30f);
    }

    private void CreateBullet(float degree)
    {
        GameObject fireball1 = Instantiate(fireball);
        fireball1.tag = "Bullet";
        fireball1.transform.position = new Vector3(transform.position.x, 0.832f, transform.position.z);
        fireball1.transform.rotation = transform.rotation;
        fireball1.transform.Rotate(new Vector3(0.0f, degree, 0.0f));
        fireball1.AddComponent<BossBullet>();
        Rigidbody rb = fireball1.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.mass = 0.01f;
        rb.freezeRotation = true;
        CapsuleCollider col = fireball1.AddComponent<CapsuleCollider>();
        col.isTrigger = true;
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

        if (playerOneDist <= playerTwoDist)
        {
            target = GameManager.playerOne.transform;
        }
        else
        {
            target = GameManager.playerTwo.transform;
        }
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = lookRotation;
    }
}
