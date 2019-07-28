using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthTrigger : MonoBehaviour
{
    public GameObject bossHealthSlider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Van"))
        {
            bossHealthSlider.SetActive(true);
        }
    }
}
