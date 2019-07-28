using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStarterScript : MonoBehaviour
{
    public GameObject startScreen, instructionScreen, vendorMenu;
    public GameObject black, skipText;
    public GameObject[] hudElements;
    private bool skippable = false, inst = false, fade = false;
    private GameObject cam;
    private int instructionStage;
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        
        foreach (GameObject hud in hudElements)
            hud.SetActive(false);
        Time.timeScale = 0;
        StartCoroutine(StartLevel1());
    }

    void Update()
    {
        black.GetComponent<Animator>().SetBool("Fade", fade);
        if (Input.anyKeyDown && skippable)
        {
            StopCoroutine(StartLevel1());
            cam.GetComponent<Animator>().enabled = false;
            cam.GetComponent<CameraFollow>().enabled = true;
            cam.transform.position = new Vector3(1.5f, 36.8f, -36.9f);
            startScreen.SetActive(false);
            instructionStage++;
            skippable = false;
        }

        if (Input.anyKeyDown && inst)
        {
            instructionStage++;
            inst = false;
        }

            switch (instructionStage)
        {
            case 1:
                inst = true;
                instructionScreen.SetActive(true);
                skipText.SetActive(false);
                hudElements[0].SetActive(true);
                instructionScreen.transform.GetChild(1).gameObject.SetActive(true);
                break;
            case 2:
                inst = true;
                instructionScreen.transform.GetChild(1).gameObject.SetActive(false);
                hudElements[2].SetActive(true);
                instructionScreen.transform.GetChild(2).gameObject.SetActive(true);
                break;
            case 3:
                inst = true;
                instructionScreen.transform.GetChild(2).gameObject.SetActive(false);
                instructionScreen.transform.GetChild(3).gameObject.SetActive(true);
                break;
            case 4:
                inst = true;
                instructionScreen.transform.GetChild(3).gameObject.SetActive(false);
                vendorMenu.SetActive(true);
                instructionScreen.transform.GetChild(4).gameObject.SetActive(true);
                break;
            case 5:
                vendorMenu.SetActive(false);
                foreach (GameObject hud in hudElements)
                    hud.SetActive(true);
                instructionScreen.SetActive(false);
                Time.timeScale = 1;
                break;
        }
    }

    IEnumerator StartLevel1()
    {
        black.SetActive(true);
        yield return new WaitForSecondsRealtime(1);
        cam.GetComponent<Animator>().enabled = true;
        FindObjectOfType<CameraFollow>().enabled = false;
        fade = true;
        startScreen.SetActive(true);
        yield return new WaitForSecondsRealtime(3.50f);
        skipText.SetActive(true);
        skippable = true;
        yield return new WaitForSecondsRealtime(8f);
        instructionStage++;
    }
    IEnumerator EndStartUp()
    {
        yield return new WaitForSecondsRealtime(8f);
    }
}
