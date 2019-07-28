using System.Collections.Generic;
using UnityEngine;

public class Bullet
{
    public string bulletName { get; }
    public float bulletDamage { get; set; } = 10.0f;
    public float bulletSpeed { get; set; } = 20.0f;
    public float attackSpeed { get; set; } = 2.0f;
    public float duration { get; set; } = 4.0f;
    public Sprite sprite { get; }
    private static int idTotal = 0;
    public int bulletId;
    public List<Sprite> extraSprites; 
    
    public Bullet(string bulletName, float bulletDamage, float bulletSpeed, float attackSpeed, Sprite image)
    {
        this.bulletSpeed = bulletSpeed;
        this.bulletDamage = bulletDamage;
        this.bulletName = bulletName;
        this.attackSpeed = attackSpeed;
        sprite = image;
        bulletId = idTotal;
        idTotal++;
        extraSprites = new List<Sprite>();
    }
}
