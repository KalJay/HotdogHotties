using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IndicatorTextPrompt : MonoBehaviour
{
    TextMeshPro text;
    void Start()
    {
        text = GetComponent<TextMeshPro>();
    }

    void Update()
    {
        if (text.enabled)
            transform.GetChild(0).gameObject.SetActive(true);
        else
            transform.GetChild(0).gameObject.SetActive(false);
    }
}
