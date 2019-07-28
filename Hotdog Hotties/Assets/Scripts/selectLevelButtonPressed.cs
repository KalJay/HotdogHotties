using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectLevelButtonPressed : MonoBehaviour
{


	 public void OnButtonClick0(){
	 	 Application.LoadLevel("LevelPicker"); 
	 }

    public void OnButtonClick1(){
	 	 Application.LoadLevel("Level1"); 
	 }

	public void OnButtonClick2(){
	 	 Application.LoadLevel("Level2"); 
	 }

	public void OnButtonClick3(){
	 	 Application.LoadLevel("Level3"); 
	 }

	 public void OnButtonClick4(){
	 	 Application.LoadLevel("Level4"); 
	 }

	 public void OnButtonClick5(){
	 	 Application.LoadLevel("Level5"); 
	 }

	 public void OnButtonClick6(){
	 	 Application.LoadLevel("Level6"); 
	 }
}
