using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownText : MonoBehaviour
{
	float timeNow = 0f;
	float startingTime = 200f;
	public Text TextCountDown; //or try serializefield below
	// [SerializeField] Text TextCountDown;


	void Start(){
		timeNow = startingTime;
	}

	void Update(){
		timeNow -= 1 * Time.deltaTime;
		TextCountDown.text = timeNow.ToString("0"); //display only whole numbers
		//print(timeNow);

		if(timeNow <= 0){
			timeNow = 0;
			print("You ran out of time");
			Application.LoadLevel("SampleScene");
		}
	}

}
