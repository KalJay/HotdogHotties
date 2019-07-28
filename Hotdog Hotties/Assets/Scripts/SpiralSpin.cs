using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralSpin : MonoBehaviour
{
    public float spinSpeed;
    void Start()
    {
        
    }

    void Update()
    {
        float i = 0;
        i += Time.deltaTime * spinSpeed;
        transform.Rotate(new Vector3(0, 0, i));
    }
}
