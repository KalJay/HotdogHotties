using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultScript : MonoBehaviour
{
    public GameObject resultScreen;

    public void Lose()
    {
        Time.timeScale = 0;
        resultScreen.SetActive(true);
    }
    public void Win()
    {
        Time.timeScale = 0;
        resultScreen.SetActive(true);
        resultScreen.transform.GetChild(1).GetComponent<Text>().text = "Y'ALL WIN";
    }
}
