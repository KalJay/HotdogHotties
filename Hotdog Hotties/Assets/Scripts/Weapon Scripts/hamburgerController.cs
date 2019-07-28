using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hamburgerController : MonoBehaviour
{
    public Bullet bulletType;
    public Quaternion directionOfTravel;
    public GameObject firedBy;
    
    private float landedTimer;
    [SerializeField] private float duration = 5.0f;
    [SerializeField] private float radius = 15.0f;
    private bool landed = false;
    private SpriteRenderer radSprite;
    private Rigidbody rb;
    
    
    void Start()
    {
        radSprite = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        transform.Rotate(new Vector3(90.0f, 0.0f, 0.0f));
        rb = GetComponent<Rigidbody>();
        rb.AddForce(directionOfTravel * transform.up * 3.0f);
    }
    
    void Update()
    {
        if(landed) { 
            radSprite.enabled = true;
            radSprite.transform.Rotate(new Vector3(0.0f, 0.0f, 5.0f * Time.deltaTime));
            ToggleLure(true);

            if (Time.time > landedTimer + duration) {
                ToggleLure(false);
                Destroy(gameObject);
            }
        } else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.1f * Time.deltaTime, transform.position.z);
        }
    }

    private void ToggleLure(bool enable) {
        
        foreach (Collider col in Physics.OverlapSphere(transform.position, radius))
        {
            if (col.gameObject.tag.Contains("Enemy"))
            {
                EnemyController ec = col.GetComponent<EnemyController>();
                EnemyControllerP2 ec2 = col.GetComponent<EnemyControllerP2>();

                if(ec != null)
                {
                    if(enable)
                    {
                        ec.UpdateTarget(transform);
                    } else
                    {
                        ec.FindNewTarget();
                    }
                    
                } else
                {
                    if(ec2 != null)
                    {
                        if(enable)
                        {
                            ec2.UpdateTarget(transform);
                        } else
                        {
                            ec2.FindNewTarget();
                        }
                        
                    }
                }
            }
        }
    }

    public void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag.Contains("Player"))
        {
            return;
        }
        landed = true;
        landedTimer = Time.time;
        rb.velocity = new Vector3();
    }
}
