using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    public GameManager.SpecialWeapon weapon;
    public List<Bullet> localBulletDictionary;
    public AudioSource shootSound;
    public AudioSource specialShootSound;
    public bool isPlayerOne = true;
    public Vector3 EndOfGun = new Vector3(0.264f, 0.836f, 0.995f);
    public GameObject muzzlePrefab;

    private PlayerController pc;
    public bool isUziFiring = false;

    private float GenericTimer = -600.0f;
    private float WeaponTimer = -600.0f;

    void Start()
    {
        pc = GetComponent<PlayerController>();
        
        if(gameObject.tag.Contains("One")) {
            weapon = GameManager.selectedWeapon1;
        } else
        {
            weapon = GameManager.selectedWeapon2;
        }

        
        localBulletDictionary = bulletManager.bulletTypes;
    }
    
    void Update()
    {
        if (GameManager.GetBoolForInstruction(GameManager.Instruction.BasicGun, pc.controlScheme) && EvaluateAttackSpeedTimer(true) && !isUziFiring)
        {
            bulletManager.GenerateBullet(GetBulletByID(0), transform.TransformPoint(EndOfGun), transform.rotation, gameObject);
            GenericTimer = Time.time;
            Instantiate(muzzlePrefab, transform.TransformPoint(EndOfGun), transform.rotation, null);
            shootSound.Play();
        }

        if (GameManager.GetBoolForInstruction(GameManager.Instruction.SpecialGun, pc.controlScheme) && EvaluateAttackSpeedTimer(false))
        {
            bulletManager.GenerateBullet(GetBulletByID(GameManager.SpecialWeaponToID(weapon)), transform.TransformPoint(EndOfGun), transform.rotation, gameObject);
            WeaponTimer = Time.time;
            specialShootSound.Play();
        }
    }

    private bool EvaluateAttackSpeedTimer(bool isGeneric)
    {
        if (isGeneric)
        {
            float time = 1.0f / bulletManager.GetBulletByID(0).attackSpeed;
            if(isPlayerOne)
                return Time.time > GenericTimer + (time - (GameManager.player1stats.defaultBuffCount * time/6));
            else
                return Time.time > GenericTimer + (time - (GameManager.player2stats.defaultBuffCount * time / 6));
        } else
        {
            float time = 1.0f / bulletManager.GetBulletByID(GameManager.SpecialWeaponToID(weapon)).attackSpeed;
            if (isPlayerOne)
                return Time.time > WeaponTimer + (time - (GameManager.player1stats.specialBuffCount * time / 4));
            else
                return Time.time > WeaponTimer + (time - (GameManager.player2stats.specialBuffCount * time / 4));
        }
    }
    

    #region Cooldowns
    public float GetCooldownRemaining(Bullet bullet)
    {
        float time = 1.0f / bullet.attackSpeed;
        if (bullet.bulletId == 0)
        {
            if (isPlayerOne)
                return ((GenericTimer + (time - (GameManager.player1stats.defaultBuffCount * time / 6)) - Time.time));
            else
                return ((GenericTimer + (time - (GameManager.player2stats.defaultBuffCount * time / 6)) - Time.time));
        }
        if (isPlayerOne)
            return (WeaponTimer + (time - (GameManager.player1stats.specialBuffCount * time / 4))) - Time.time;
        else
            return (WeaponTimer + (time - (GameManager.player2stats.specialBuffCount * time / 4))) - Time.time;
    }

    public float GetCooldownRemainingAsPercentage(Bullet bullet)
    {
        float time = 1.0f / bullet.attackSpeed;
        if (bullet.bulletId == 0) {
            if (isUziFiring) return 1.0f;
            if (isPlayerOne)
                return ((GenericTimer + (time - (GameManager.player1stats.defaultBuffCount * time / 6))) - Time.time) / time;
            else
                return ((GenericTimer + (time - (GameManager.player2stats.defaultBuffCount * time / 6))) - Time.time) / time;
        }
        if(isPlayerOne)
            return ((WeaponTimer + (time - (GameManager.player1stats.specialBuffCount * time / 4))) - Time.time)/time;
        else
            return ((WeaponTimer + (time - (GameManager.player2stats.specialBuffCount * time / 4))) - Time.time) / time;
    }
    #endregion

    #region Bullet Getters
    public Bullet GetBulletByName(string name)
    {
        foreach (Bullet bullet in localBulletDictionary)
        {
            if (bullet.bulletName.Equals(name))
            {
                return bullet;
            }
        }
        return null;
    }

    public Bullet GetBulletByID(int id)
    {
        foreach (Bullet bullet in localBulletDictionary)
        {
            if (bullet.bulletId == id)
            {

                return bullet;
            }
        }
        return null;
    }
    #endregion

    #region Bullet Modifiers
    //For names see bulletManager definitions
    public void UpdateBulletDamage(string name, float damage)
    {
        GetBulletByName(name).bulletDamage = damage;
    }
    public void UpdateBulletSpeed(string name, float speed)
    {
        GetBulletByName(name).bulletSpeed = speed;
    }
    public void UpdateBulletAttackSpeed(string name, float attackSpeed)
    {
        GetBulletByName(name).attackSpeed = attackSpeed;
    }
    public void UpdateBulletDuration(string name, float duration)
    {
        GetBulletByName(name).duration = duration;
    }
    #endregion
}
