using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTriggerScript : MonoBehaviour
{
    public GameObject barrier;
    private float y = 0;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Van"))
        {
            y += 0.5f;
            barrier.transform.position = new Vector3(barrier.transform.position.x, barrier.transform.position.y - (y*Time.deltaTime*0.2f), barrier.transform.position.z);
            if (barrier.transform.position.y <= -1)
                Destroy(barrier);
        }
    }
}
