using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frenchFryController : MonoBehaviour
{
    public Bullet bulletType;

    private List<GameObject> fries;
    [SerializeField] private float range = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(new Vector3(90.0f, 0.0f, 0.0f));
        fries = new List<GameObject>();

        GameObject fryTemplate = new GameObject();
        SpriteRenderer spr = fryTemplate.AddComponent<SpriteRenderer>();
        spr.sprite = bulletType.sprite;
        spr.sortingOrder = 1;
        CapsuleCollider col = fryTemplate.AddComponent<CapsuleCollider>();
        col.isTrigger = true;
        Rigidbody rb = fryTemplate.AddComponent<Rigidbody>();
        rb.mass = 0.01f;
        rb.useGravity = false;
        rb.freezeRotation = true;
        fryTemplate.transform.position = transform.position;
        fryTemplate.transform.rotation = transform.rotation;
        fryTemplate.transform.localScale = new Vector2(0.4f, 0.4f);

        for (int i = -40; i <= 40; i+=20) {
            GameObject temp = Instantiate(fryTemplate);
            simpleBulletHit sbh = temp.AddComponent<simpleBulletHit>();
            sbh.bulletType = bulletType;
            sbh.ffc = this;
            temp.transform.position = transform.position;
            temp.transform.rotation = transform.rotation;
            temp.transform.Rotate(new Vector3(0.0f, 0.0f, i));
            temp.tag = "Bullet";
            fries.Add(temp);
        }
        Destroy(fryTemplate);
    }

    // Update is called once per frame
    void Update()
    {
        if (fries.Count == 0)
        {
            Destroy(gameObject);
        }

        if (Vector3.Distance(fries[0].transform.position, transform.position) >= range) {
            foreach (GameObject fry in fries) {
                bulletManager.GenerateHitEffect(gameObject);
                Destroy(fry);
            }
            Destroy(gameObject);
        }

        foreach (GameObject fry in fries) {
            fry.GetComponent<Rigidbody>().MovePosition(fry.transform.position + (fry.transform.up * bulletType.bulletSpeed * Time.deltaTime));
        }
    }

    public void DeleteFry(GameObject fry)
    {
        fries.Remove(fry);
    }
}
