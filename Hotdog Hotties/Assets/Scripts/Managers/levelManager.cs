using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class levelManager : MonoBehaviour
{
    public int coins;
    public Text coinText;

    // Start is called before the first frame update
    void Start()
    {
        coinText.text = "Coins: " + coins;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addCoins(int numberOfCoins){
        coins += numberOfCoins; 
        coinText.text = "Coins: " + coins;
    }

}
