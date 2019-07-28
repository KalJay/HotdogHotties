using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class PauseMenu : MonoBehaviour
{
	public string SampleScene;

	public GameObject pauseMenu;

	public static bool gamePaused = false; 

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Joystick1Button7) || Input.GetKeyDown(KeyCode.Joystick2Button7)) //p key to pause game
        {
        	if(gamePaused){
        		Resume();
        	}else{
        		gamePaused = true;
        		pauseMenu.SetActive(true);
				Time.timeScale = 0f;//freeze action in game
        	}
        }
    }

    public void Resume()
    {
		pauseMenu.SetActive(false);
		Time.timeScale = 1f;
        gamePaused = false;
    }

    void Paused(){
		pauseMenu.SetActive(true);
		Time.timeScale = 0f;
        gamePaused = true;
    }

    public void MenuLoad()
    {
    	SceneManager.LoadScene("MainMenu");
        Debug.Log("load the menu");
    	//Application.LoadLevel("LevelPicker"); 
    }

    public void QuitGame(){
        Debug.Log("game has quit");
        Application.Quit();
    }

}
