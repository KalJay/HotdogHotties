using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetrolScript : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Contains("Player") && FindObjectOfType<DrivingScript>().GetPetrolAmount() < 30)
        {
            GameObject.FindGameObjectWithTag("AudioObjects").transform.GetChild(1).GetComponent<AudioSource>().Play();
            FindObjectOfType<DrivingScript>().AddFuel();
            Destroy(gameObject);
        }
    }
}
