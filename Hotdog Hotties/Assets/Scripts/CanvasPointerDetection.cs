using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CanvasPointerDetection : MonoBehaviour
{
    public GameObject crosshair;
    PointerEventData m_PointerEventData;
    GraphicRaycaster m_Raycaster;
    EventSystem m_EventSystem;
    List<RaycastResult> results;
    private GameObject resultButton;
    public bool engaged = true;

    void Start()
    {
        results = new List<RaycastResult>();
        m_Raycaster = GetComponent<GraphicRaycaster>();
        m_EventSystem = GetComponent<EventSystem>();
    }

    void Update()
    {
        DetectingRaycasst();
    }

    private void DetectingRaycasst()
    {
        m_PointerEventData = new PointerEventData(m_EventSystem);
        m_PointerEventData.position = crosshair.transform.position;
        m_Raycaster.Raycast(m_PointerEventData, results);

        if (Input.GetButtonDown("A button") && resultButton != null && engaged)
        {
            resultButton.gameObject.GetComponent<Button>().OnPointerClick(m_PointerEventData);
        }
        //print(results[results.Count - 1].gameObject.name);

        try {
            if (results[results.Count - 1].gameObject.CompareTag("Button"))
            {
                resultButton = results[results.Count - 1].gameObject;
                resultButton.gameObject.GetComponent<Button>().Select();
            }
            else
            {
                resultButton = null;
            }
        }
        catch(Exception e)
        {

        }
    }
}
