using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bulletManager : MonoBehaviour
{
    public static List<Bullet> bulletTypes;

    [SerializeField] private Sprite basicHotdog;
    [SerializeField] private Sprite hamburger;
    [SerializeField] private Sprite hamburgerRadius;
    [SerializeField] private Sprite frenchFry;
    [SerializeField] private Sprite sauceShot;
    [SerializeField] private Sprite chorizoShot;
    [SerializeField] private Sprite baguette;
    [SerializeField] private Sprite sausageSpear;

    public static float bulletDamage1, bulletDamage2;
    private static GameObject shooterPlayer;
    public static List<string> BulletHitIgnoreTags;
    public static GameObject hitEffect;

    void Start()
    {
        //if(SceneManager.sceneCount == 0)
            //hitEffect = GameObject.FindGameObjectWithTag("HitPrefab");
        if (bulletTypes == null)
        {
            bulletTypes = new List<Bullet>();
            bulletTypes.Add(new Bullet("Basic Hotdog", 20.0f, 20.0f, 2.0f, basicHotdog));
            bulletTypes.Add(new Bullet("Hamburger", 0.0f, 2.0f, 0.1f, hamburger));
            bulletTypes[bulletTypes.Count - 1].extraSprites.Add(hamburgerRadius);
            bulletTypes.Add(new Bullet("French Fry Shell", 20.0f, 40.0f, 0.3f, frenchFry));
            bulletTypes.Add(new Bullet("Sauce Sniper Shot", 100.0f, 100.0f, 0.25f, sauceShot));
            bulletTypes.Add(new Bullet("Uzi Chorizo Shot", 5.0f, 30.0f, 0.1f,chorizoShot));
            bulletTypes.Add(new Bullet("Freshly Toasted Baguette", 20.0f, 0.25f, 2.0f, baguette));
            bulletTypes.Add(new Bullet("Sausage Harpoon Spear", 20.0f, 40.0f, 0.3f, sausageSpear));
        }

        BulletHitIgnoreTags = new List<string>();
        BulletHitIgnoreTags.Add("Player");
        BulletHitIgnoreTags.Add("Bullet");
        BulletHitIgnoreTags.Add("VanNoHits");
    }

    public static Bullet GetBulletByName(string name) {
        foreach (Bullet bullet in bulletTypes) {
            if (bullet.bulletName.Equals(name)) {
                return bullet;
            }
        }
        return null;
    }

    public static Bullet GetBulletByID(int id) {
        foreach (Bullet bullet in bulletTypes)
        {
            if (bullet.bulletId == id)
            {

                return bullet;
            }
        }
        return null;
    }

    public static void GenerateBullet(Bullet bulletType, Vector3 position, Quaternion rotation, GameObject shooter) {
        shooterPlayer = shooter;
        GameObject bulletObject = new GameObject(bulletType.bulletName);
        bulletObject.tag = "Bullet";

        bulletObject.transform.position = position;
        bulletObject.transform.rotation = rotation;

        SpriteRenderer spr = bulletObject.AddComponent<SpriteRenderer>();
        spr.sprite = bulletType.sprite;
        spr.sortingOrder = 1;
        spr.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;

        CapsuleCollider col = bulletObject.AddComponent<CapsuleCollider>();
        col.isTrigger = true;

        Rigidbody rb = bulletObject.AddComponent<Rigidbody>();
        rb.mass = 0.01f;
        rb.freezeRotation = true;
        rb.useGravity = false;
        
        if (bulletType.bulletId == 0) {//Hotdog Pistol
            bulletController bc = bulletObject.AddComponent<bulletController>();
            bulletObject.transform.localScale = new Vector2(0.2f, 0.2f);
            bc.bullet = bulletType;
            bc.firedBy = shooter;
        }

        if (bulletType.bulletId == 1) {//Hamburger Launcher
            hamburgerController hc = bulletObject.AddComponent<hamburgerController>();
            hc.bulletType = bulletType;
            hc.transform.position = position;
            hc.firedBy = shooter;
            hc.directionOfTravel = rotation;
            bulletObject.transform.rotation = new Quaternion();
            bulletObject.transform.localScale = new Vector2(0.2f, 0.2f);
            //bulletObject.tag = "Untagged";
            GameObject radiusRange = new GameObject();
            SpriteRenderer radSprite = radiusRange.AddComponent<SpriteRenderer>();
            radSprite.sprite = bulletType.extraSprites[0];
            radiusRange.transform.parent = bulletObject.transform;
            radSprite.enabled = false;
            radSprite.sortingOrder = 1;
            radiusRange.transform.localPosition = new Vector2(1.0f, 0.0f);
            radiusRange.transform.localScale = new Vector2(2.0f, 2.0f);
        }

        if (bulletType.bulletId == 2) {//French Fry Shotgun
            Destroy(spr);
            Destroy(rb);
            Destroy(col);
            frenchFryController ffc = bulletObject.AddComponent<frenchFryController>();
            ffc.bulletType = bulletType;
            bulletObject.tag = "Untagged";
        }

        if (bulletType.bulletId == 3) {//Sauce Sniper Rifle
            sauceSniperController ssc = bulletObject.AddComponent<sauceSniperController>();
            ssc.bullet = bulletType;
            ssc.firedBy = shooter;
            ssc.transform.position = ssc.transform.position + ssc.transform.up * 0.35f;
        }

        if (bulletType.bulletId == 4) {//Chorizo Uzi
            Destroy(spr);
            Destroy(rb);
            Destroy(col);
            UziChorizoController ucc = bulletObject.AddComponent<UziChorizoController>();
            bulletObject.transform.localScale = new Vector2(0.15f, 0.15f);
            ucc.bullet = bulletType;
            ucc.firedBy = shooter;
            ucc.transform.parent = shooter.transform;
        }
        if(bulletType.bulletId == 5) {//Freshly Toasted Baguette
            Destroy(rb);
            BaguetteController bc = bulletObject.AddComponent<BaguetteController>();
            bulletObject.transform.localScale = new Vector2(0.1f, 0.1f);
            bc.bullet = bulletType;
            spr.sprite = bulletType.sprite;
            bc.firedBy = shooter;
            bulletObject.transform.parent = shooter.transform;
            //bc.transform.position = new Vector3(shooter.transform.position.x + shooter.transform.forward.x * 2.0f, position.y, shooter.transform.position.z + shooter.transform.forward.z * 2.0f);
            bulletObject.transform.position += bulletObject.transform.forward * 0.8f;
        }
        if(bulletType.bulletId == 6) {//Sausage Harpoon Launcher
            SausageHarpoonController shc = bulletObject.AddComponent<SausageHarpoonController>();
            shc.bullet = bulletType;
            shc.firedBy = shooter;
            shc.transform.position = shc.transform.position + shc.transform.up;
            bulletObject.transform.localScale = new Vector2(0.15f, 0.15f);
            col.radius = 3.0f;
        }
    }

    public static bool HitTrigger(Bullet bullet, Collider col) {
        foreach(string tag in BulletHitIgnoreTags)
        {
            if(col.gameObject.tag.Contains(tag))
            {
                return false;
            }
        }

        bool hit = false;
        if (col.gameObject.tag.Contains("Enemy") || col.gameObject.tag.Contains("Destructable") || col.gameObject.tag.Contains("Turret") || col.gameObject.tag.Contains("Boss"))
        {
            //Will do something interesting with these bulletDamage values later (damage pop up number or something)
            bulletDamage1 = bullet.bulletDamage + (GameManager.player1stats.damageBuffCount * (bullet.bulletDamage / 2));
            bulletDamage2 = bullet.bulletDamage + (GameManager.player2stats.damageBuffCount * (bullet.bulletDamage / 2));
            if (shooterPlayer.CompareTag("PlayerOne"))
            {
                
                col.GetComponent<EnemyHealthScript>().HurtEnemy(bulletDamage1);
            }
            else
            {
                col.GetComponent<EnemyHealthScript>().HurtEnemy(bulletDamage2);
            }
            hit = true;
        }

        if (hit) {
            if(bullet.bulletId == 5 || bullet.bulletId == 6)
            {
                return false;
            } else
            {
                return true;
            }
        } else
        {
            return true;
        }
    }

    public static void GenerateHitEffect(GameObject bullet)
    {
        Instantiate(Resources.Load("vfx_Hit_PinkGum"), bullet.transform.position, bullet.transform.rotation, null);
    }
}
