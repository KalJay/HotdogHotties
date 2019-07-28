using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CrosshairAimingScript : MonoBehaviour
{
    private RectTransform t;
    public float sensitivity;

    void Start()
    {
        t = GetComponent<RectTransform>();
    }

    void Update()
    {
        float Vertical = Input.GetAxisRaw("Controller1RightHorizontal");
        float Horizontal = Input.GetAxisRaw("Controller1RightVertical");

        t.position += new Vector3(Horizontal, Vertical, 0) * sensitivity;

        if (t.position.x < 0)
            t.position = new Vector3(0, t.position.y, 0);
        if (t.position.x > Screen.width)
            t.position = new Vector3(Screen.width, t.position.y, 0);
        if (t.position.y < 0)
            t.position = new Vector3(t.position.x, 0, 0);
        if (t.position.y > Screen.height)
            t.position = new Vector3(t.position.x, Screen.height, 0);
    }
}
