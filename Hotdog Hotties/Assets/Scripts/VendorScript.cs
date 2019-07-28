using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VendorScript : MonoBehaviour
{
    public TextMeshPro indicatorText1, indicatorText2;
    public GameObject vendorMenu, vendorMenu2;
    public Text coinCount, coinCount2, coinCountHUD1, coinCountHUD2;
    public Slider dmgSlider, healthSlider, speedSlider, defaultSlider, specialSlider, abilitySlider;
    public Slider dmgSlider2, healthSlider2, speedSlider2, defaultSlider2, specialSlider2, abilitySlider2;
    public Button[] upgradeBtn, upgradeBtn2;
    private bool allowed1, allowed2, vendorOpen, vendorOpen2, vanFull;
    private GameObject playerInVan;
    public int cost;
    private bool a, b, setFirstEventObject = true;

    void Awake()
    {
        vendorMenu.SetActive(false);
        vendorOpen = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Submit") && allowed2 && (!vanFull || playerInVan.Equals(GameManager.playerTwo)))
        {       
            vendorOpen2 = !vendorOpen2;
        }
        if (Input.GetKeyDown(KeyCode.F) && allowed1 && (!vanFull || playerInVan.Equals(GameManager.playerOne)))
        {
            vendorOpen = !vendorOpen;
        }
        //Has controller related event system handling.
        if (vendorOpen)
        {
            VendorMenuOpen(1);
            //if (Mathf.Abs(Input.GetAxis("HorizontalKey")) > 0 || (Mathf.Abs(Input.GetAxis("VerticalKey")) > 0))
            //    upgradeBtn[0].gameObject.GetComponent<Image>().color = Color.white;
        }
        if (!vendorOpen && a)
        {
            VendorMenuClose(1);
            //upgradeBtn[0].gameObject.GetComponent<Image>().color = new Color(0.98f, 1, 0.6f);
            //setFirstEventObject = true;
            a = false;
        }

        if (vendorOpen2)
        {
            VendorMenuOpen(2);
            //if (Mathf.Abs(Input.GetAxis("Controller1LeftHorizontal")) > 0 || (Mathf.Abs(Input.GetAxis("Controller1LeftVertical")) > 0))
            //    upgradeBtn2[0].gameObject.GetComponent<Image>().color = Color.white;
        }
        if (!vendorOpen2 && b)
        {
            VendorMenuClose(2);
            //upgradeBtn2[0].gameObject.GetComponent<Image>().color = new Color(0.98f, 1, 0.6f);
            //setFirstEventObject = true;
            b = false;
        }

        if (!GameManager.playerOne.activeSelf)
        {
            indicatorText1.enabled = false;
            allowed1 = false;
            vendorOpen = false;
        }
        if (!GameManager.playerTwo.activeSelf)
        {
            indicatorText2.enabled = false;
            allowed2 = false;
            vendorOpen2 = false;
        }
    }

    void VendorMenuOpen(int player)
    {
        vanFull = true;
        if (player == 1)
        {
            a = true;
            playerInVan = GameManager.playerOne;
            vendorMenu.SetActive(true);
            /*if (setFirstEventObject)
            {
                FindObjectOfType<StandaloneInputModule>().horizontalAxis = "HorizontalKey";
                FindObjectOfType<StandaloneInputModule>().verticalAxis = "VerticalKey";
                FindObjectOfType<StandaloneInputModule>().submitButton = "Submit";
                EventSystem.current.SetSelectedGameObject(upgradeBtn2[0].gameObject);
                setFirstEventObject = false;
            }*/
            indicatorText1.enabled = false;
            GameManager.playerOne.GetComponent<PlayerController>().enabled = false;
            GameManager.playerOne.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            coinCount.text = "$" + GameManager.player1stats.coinCount;
            CheckWallet(1);
        }
        if (player == 2)
        {
            b = true;
            playerInVan = GameManager.playerTwo;
            vendorMenu2.SetActive(true);
            /*if (setFirstEventObject)
            {
                FindObjectOfType<StandaloneInputModule>().horizontalAxis = "Controller1LeftHorizontal";
                FindObjectOfType<StandaloneInputModule>().verticalAxis = "Controller1LeftVertical";
                FindObjectOfType<StandaloneInputModule>().submitButton = "A button";
                EventSystem.current.SetSelectedGameObject(upgradeBtn[0].gameObject);
                setFirstEventObject = false;
            }*/
            indicatorText2.enabled = false;
            GameManager.playerTwo.GetComponent<PlayerController>().enabled = false;
            GameManager.playerTwo.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            coinCount2.text = "$" + GameManager.player2stats.coinCount;
            CheckWallet(2);
        }
    }
    void VendorMenuClose(int player)
    {
        if (player == 1)
        {
            vendorMenu.SetActive(false);
            GameManager.playerOne.GetComponent<PlayerController>().enabled = true;
            GameManager.playerOne.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
        }
        if (player == 2)
        {
            vendorMenu2.SetActive(false);
            GameManager.playerTwo.GetComponent<PlayerController>().enabled = true;
            GameManager.playerTwo.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
        }
        vanFull = false;
        playerInVan = null;
    }

    void CheckWallet(int i)
    {
        if (i == 1)
        {
            foreach (Button btn in upgradeBtn2)
            {
                if (GameManager.player1stats.coinCount >= cost)
                {
                    btn.interactable = true;
                }
                if (GameManager.player1stats.coinCount < cost)
                {
                    btn.interactable = false;
                }
            }
        }
        if (i == 2)
        {
            foreach (Button btn in upgradeBtn)
            {
                
                if (GameManager.player2stats.coinCount >= cost)
                {
                    btn.interactable = true;
                }
                if (GameManager.player2stats.coinCount < cost)
                {
                    btn.interactable = false;
                }
            }
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if(collision.tag.Contains("PlayerOne") && !vanFull)
        {
            indicatorText1.enabled = true;
            indicatorText1.text = "VENDOR";
            allowed1 = true;
        }
        if (collision.tag.Contains("PlayerOne") && vanFull)
            indicatorText1.enabled = false;

        if (collision.tag.Contains("PlayerTwo") && !vanFull)
        {
            indicatorText2.enabled = true;
            indicatorText2.text = "VENDOR";
            allowed2 = true;
        }
        if (collision.tag.Contains("PlayerTwo") && vanFull)
            indicatorText2.enabled = false;
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.tag.Contains("PlayerOne"))
        {
            indicatorText1.enabled = false;
            allowed1 = false;
            vendorOpen = false;
        }
        if (collision.tag.Contains("PlayerTwo"))
        {
            indicatorText2.enabled = false;
            allowed2 = false;
            vendorOpen2 = false;
        }
    }

    void SpendMoney(int i)
    {
        if(i == 1)
        {
            GameManager.player1stats.coinCount -= cost;
            coinCount.text = "$" + GameManager.player1stats.coinCount;
            //CheckWallet(1);
        }
        if (i == 2)
        {
            GameManager.player2stats.coinCount -= cost;
            coinCount.text = "$" + GameManager.player2stats.coinCount;
            //CheckWallet(2);
        }  
    }

    public void PurchaseDamage()
    {
        if (GameManager.player1stats.damageBuffCount < 3)
        {
            GameManager.player1stats.damageBuffCount++;
            dmgSlider2.value = GameManager.player1stats.damageBuffCount;
            SpendMoney(1);
        }   
    }
    public void PurchaseDamage2()
    {
        if (GameManager.player2stats.damageBuffCount < 3)
        {
            GameManager.player2stats.damageBuffCount++;
            dmgSlider.value = GameManager.player2stats.damageBuffCount;
            SpendMoney(2);
        }
    }

    public void PurchaseHealth()
    {
        if (GameManager.player1stats.healthBuffCount < 3)
        {
            GameManager.player1stats.healthBuffCount++;
            GameManager.playerOne.GetComponent<PlayerHealthScript>().IncreaseHealth(GameManager.playerOne);
            healthSlider2.value = GameManager.player1stats.healthBuffCount;
            SpendMoney(1);
        }
    }
    public void PurchaseHealth2()
    {
        if (GameManager.player2stats.healthBuffCount < 3)
        {
            GameManager.player2stats.healthBuffCount++;
            GameManager.playerTwo.GetComponent<PlayerHealthScript>().IncreaseHealth(GameManager.playerTwo);
            healthSlider.value = GameManager.player2stats.healthBuffCount;
            SpendMoney(2);
        }
    }
    public void PurchaseSpeed()
    {
        if (GameManager.player1stats.speedBuffCount < 3)
        {
            GameManager.player1stats.speedBuffCount++;
            GameManager.playerOne.GetComponent<PlayerController>().IncreaseMovementSpeed(1);
            speedSlider2.value = GameManager.player1stats.speedBuffCount;
            SpendMoney(1);
        }
    }
    public void PurchaseSpeed2()
    {
        if (GameManager.player2stats.speedBuffCount < 3)
        {
            GameManager.player2stats.speedBuffCount++;
            GameManager.playerTwo.GetComponent<PlayerController>().IncreaseMovementSpeed(2);
            speedSlider.value = GameManager.player2stats.speedBuffCount;
            SpendMoney(2);
        }
    }
    public void PurchaseDefaultCooldown()
    {
        if (GameManager.player1stats.defaultBuffCount < 3)
        {
            GameManager.player1stats.defaultBuffCount++;
            defaultSlider2.value = GameManager.player1stats.defaultBuffCount;
            SpendMoney(1);
        }
    }
    public void PurchaseDefaultCooldown2()
    {
        if (GameManager.player2stats.defaultBuffCount < 3)
        {
            GameManager.player2stats.defaultBuffCount++;
            defaultSlider.value = GameManager.player2stats.defaultBuffCount;
            SpendMoney(2);
        }
    }
    public void PurchaseSpecialCooldown()
    {
        if (GameManager.player1stats.specialBuffCount < 3)
        {
            GameManager.player1stats.specialBuffCount++;
            specialSlider2.value = GameManager.player1stats.specialBuffCount;
            SpendMoney(1);
        }
    }
    public void PurchaseSpecialCooldown2()
    {
        if (GameManager.player2stats.specialBuffCount < 3)
        {
            GameManager.player2stats.specialBuffCount++;
            specialSlider.value = GameManager.player2stats.specialBuffCount;
            SpendMoney(2);
        }
    }
    public void PurchaseAbilityCooldown()
    {
        if (GameManager.player1stats.abilityBuffCount < 3)
        {
            GameManager.player1stats.abilityBuffCount++;
            abilitySlider2.value = GameManager.player1stats.abilityBuffCount;
            SpendMoney(1);
        }
    }
    public void PurchaseAbilityCooldown2()
    {
        if (GameManager.player2stats.abilityBuffCount < 3)
        {
            GameManager.player2stats.abilityBuffCount++;
            abilitySlider.value = GameManager.player2stats.abilityBuffCount;
            SpendMoney(2);
        }
    }
}
