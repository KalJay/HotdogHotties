using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UziChorizoController : MonoBehaviour
{
    public GameObject firedBy;
    public Bullet bullet;

    public Vector2 ShootingOffset;
    private int Side = 1;
    private float UziAttackSpeed = 8.0f; //bullets per second

    private int shotsFired = 0;

    private PlayerWeaponController pwc;
    private float timer;
    public float destructionDelay = 5.0f;

    void Start()
    {
        timer = Time.time;
        pwc = transform.parent.GetComponent<PlayerWeaponController>();
        pwc.isUziFiring = true;
        ShootingOffset = transform.localPosition;
        gameObject.tag = "Untagged";
        gameObject.name = "Uzi Chorizo Controller";
    }

    void Update()
    {
        if((Time.time - timer) > (shotsFired/UziAttackSpeed))
        {
            shotsFired++;
            CreateNewUziShot();
        }
        if (Time.time > timer + destructionDelay)
        {
            pwc.isUziFiring = false;
            Destroy(gameObject);
        }
    }

    private void CreateNewUziShot()
    {
        GameObject UziShot = new GameObject(bullet.bulletName);
        UziShot.transform.localPosition = transform.parent.TransformPoint(new Vector2(ShootingOffset.x * Side, ShootingOffset.y));
        Side *= -1;
        UziShot.transform.localScale = new Vector2(0.05f, 0.05f);

        UziShot.transform.rotation = transform.parent.rotation;

        SpriteRenderer spr = UziShot.AddComponent<SpriteRenderer>();
        spr.sprite = bullet.sprite;
        spr.sortingOrder = 1;
        spr.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;

        CapsuleCollider col = UziShot.AddComponent<CapsuleCollider>();
        col.isTrigger = true;

        Rigidbody rb = UziShot.AddComponent<Rigidbody>();
        rb.mass = 0.01f;
        rb.useGravity = false;
        rb.freezeRotation = true;

        bulletController bc = UziShot.AddComponent<bulletController>();
        bc.bullet = bullet;
        bc.firedBy = transform.parent.gameObject;
    }
}
