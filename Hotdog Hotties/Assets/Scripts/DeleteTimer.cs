using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteTimer : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Delete());
    }

    IEnumerator Delete()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
