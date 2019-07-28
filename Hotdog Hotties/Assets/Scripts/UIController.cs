using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Button JoinButton;
    [SerializeField] private InputField inputField;

    public GameManager gameManager;

    void Start()
    {
        
    }
    
    void Update()
    {
        JoinButton.interactable = !inputField.text.Equals("");
    }

    public void OnJoinClick() {
        //gameManager.AddClientScript(inputField.text);
    }

    public void OnHostClick() {
        //gameManager.AddServerScript();
    }
}
