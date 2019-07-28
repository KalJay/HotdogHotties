using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public Image DefaultCooldown1;
    public Image WeaponCooldown1;
    public Image AbilityCooldown1;

    public Image DefaultCooldown2;
    public Image WeaponCooldown2;
    public Image AbilityCooldown2;

    public Sprite[] WeaponIcons;

    public PlayerWeaponController playerWeaponController1;
    public PlayerWeaponController playerWeaponController2;
    public PlayerController pc1;
    public PlayerController pc2;

    void Start()
    {
        playerWeaponController1 = GameObject.FindGameObjectWithTag("PlayerOne").GetComponent<PlayerWeaponController>();
        playerWeaponController2 = GameObject.FindGameObjectWithTag("PlayerTwo").GetComponent<PlayerWeaponController>();
        pc1 = GameObject.FindGameObjectWithTag("PlayerOne").GetComponent<PlayerController>();
        pc2 = GameObject.FindGameObjectWithTag("PlayerTwo").GetComponent<PlayerController>();
        WeaponCooldown1.transform.parent.GetComponent<Image>().sprite = WeaponIcons[GameManager.SpecialWeaponToID(playerWeaponController1.weapon)];
        WeaponCooldown2.transform.parent.GetComponent<Image>().sprite = WeaponIcons[GameManager.SpecialWeaponToID(playerWeaponController2.weapon)];
    }


    
    void Update()
    {
        DefaultCooldown1.fillAmount = playerWeaponController1.GetCooldownRemainingAsPercentage(bulletManager.GetBulletByName("Basic Hotdog"));
        WeaponCooldown1.fillAmount = playerWeaponController1.GetCooldownRemainingAsPercentage(bulletManager.GetBulletByID(GameManager.SpecialWeaponToID(playerWeaponController1.weapon)));
        AbilityCooldown1.fillAmount = pc1.GetRollCooldownAsPercentage();

        DefaultCooldown2.fillAmount = playerWeaponController2.GetCooldownRemainingAsPercentage(bulletManager.GetBulletByName("Basic Hotdog"));
        WeaponCooldown2.fillAmount = playerWeaponController2.GetCooldownRemainingAsPercentage(bulletManager.GetBulletByID(GameManager.SpecialWeaponToID(playerWeaponController2.weapon)));
        AbilityCooldown2.fillAmount = pc2.GetRollCooldownAsPercentage();
    }
}
