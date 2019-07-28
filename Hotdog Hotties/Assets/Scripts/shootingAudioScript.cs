using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootingAudioScript : MonoBehaviour
{
    public AudioClip ShootingClip;

    public AudioSource musicSource; 

    // Start is called before the first frame update
    void Start()
    {
        musicSource.clip = ShootingClip;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0)){
            musicSource.Play();
        }
    }
}
