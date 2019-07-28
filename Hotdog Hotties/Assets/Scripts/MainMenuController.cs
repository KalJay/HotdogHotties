using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private int view = 0;

    public GameManager gameManager;

    [SerializeField] private Button PlayButton;
    [SerializeField] private Button BackButton;
    [SerializeField] private Button QuitButton;

    [SerializeField] private Button WeaponButton1;
    [SerializeField] private Button WeaponButton2;
    [SerializeField] private Button WeaponButton3;
    [SerializeField] private Button WeaponButton4;
    [SerializeField] private Button WeaponButton5;
    [SerializeField] private Button WeaponButton6;
    
    [SerializeField] private Text WeaponText;

    [SerializeField] private GameObject WeaponsSelectionPanel;
    [SerializeField] private GameObject blackScreen;
    private bool load;
    public CanvasPointerDetection detection;
    public GameObject blocker;

    void Start()
    {
        load = false;
        WeaponButton1.transform.GetChild(0).GetComponent<Text>().text = "Hamburger Gun";
        WeaponButton2.transform.GetChild(0).GetComponent<Text>().text = "French Fry Shotgun";
        WeaponButton3.transform.GetChild(0).GetComponent<Text>().text = "Sauce Sniper Rifle";
        WeaponButton4.transform.GetChild(0).GetComponent<Text>().text = "Chorizo Uzis";
        WeaponButton5.transform.GetChild(0).GetComponent<Text>().text = "Freshly Toasted Baguette";
        WeaponButton6.transform.GetChild(0).GetComponent<Text>().text = "Corn Dog Harpoon Launcher";

        UpdateView();   
    }

    private void Update()
    {
        if (load)
        {
            blackScreen.SetActive(true);
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().volume -= Time.deltaTime * 2;
        }
    }

    private void UpdateView() {
        PlayButton.gameObject.SetActive(false);
        BackButton.gameObject.SetActive(false);
        QuitButton.gameObject.SetActive(false);

        WeaponButton1.gameObject.SetActive(false);
        WeaponButton2.gameObject.SetActive(false);
        WeaponButton3.gameObject.SetActive(false);
        WeaponButton4.gameObject.SetActive(false);
        WeaponButton5.gameObject.SetActive(false);
        WeaponButton6.gameObject.SetActive(false);
        
        WeaponText.gameObject.SetActive(false);

        WeaponsSelectionPanel.SetActive(false);

        switch(view)
        {
            case 0:
                PlayButton.gameObject.SetActive(true);
                QuitButton.gameObject.SetActive(true);
                break;
            case 1:
                detection.enabled = false;
                blocker.SetActive(false);
                BackButton.gameObject.SetActive(true);
                WeaponButton1.gameObject.SetActive(true);
                WeaponButton2.gameObject.SetActive(true);
                WeaponButton3.gameObject.SetActive(true);
                WeaponButton4.gameObject.SetActive(true);
                WeaponButton5.gameObject.SetActive(true);
                WeaponButton6.gameObject.SetActive(true);
                WeaponText.gameObject.SetActive(true);
                WeaponText.text = "Player One: Select your weapon";
                WeaponsSelectionPanel.SetActive(true);
                break;
            case 2:
                detection.enabled = true;
                blocker.SetActive(true);
                BackButton.gameObject.SetActive(true);
                WeaponButton1.gameObject.SetActive(true);
                WeaponButton2.gameObject.SetActive(true);
                WeaponButton3.gameObject.SetActive(true);
                WeaponButton4.gameObject.SetActive(true);
                WeaponButton5.gameObject.SetActive(true);
                WeaponButton6.gameObject.SetActive(true);
                WeaponText.gameObject.SetActive(true);
                WeaponText.text = "Player Two: Select your weapon";
                WeaponsSelectionPanel.SetActive(true);
                break;
        }
    }

    public void PlayClick()
    {
        view = 1;
        UpdateView();
    }

    public void BackClick()
    {
        switch(view)
        {
            case 1:
                view = 0;
                break;
            case 2:
                view = 1;
                break;
        }
        UpdateView();
    }

    public void QuitClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }

    public void WeaponClick(int weaponID) {
        if (view == 2)
        {
            GameManager.SetPlayerWeapon(weaponID - 1, false);
            StartCoroutine(LoadScene());
            
        }
        if (view == 1)
        {
            view = 2;
            GameManager.SetPlayerWeapon(weaponID - 1, true);
            UpdateView();
        }
        
    }

    IEnumerator LoadScene()
    {
        load = true;
        yield return new WaitForSeconds(2);
        GameManager.LoadGameScene();
    }
}
