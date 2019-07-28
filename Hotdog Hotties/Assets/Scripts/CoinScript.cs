using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinScript : MonoBehaviour
{
    private levelManager gameLevelManager;
    private Text coinCountText1, coinCountText2;
    private AudioSource audioSrc;

    void Awake()
    {
        coinCountText1 = GameObject.FindGameObjectWithTag("Coin1").GetComponent<Text>();
        coinCountText2 = GameObject.FindGameObjectWithTag("Coin2").GetComponent<Text>();
    }

    void Start(){
        audioSrc = GameObject.FindGameObjectWithTag("AudioObjects").transform.GetChild(0).GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
     {

         if (collision.gameObject.tag.Contains("PlayerOne"))
         {
            GameManager.player1stats.coinCount++;
            coinCountText1.text = "$" + GameManager.player1stats.coinCount;
            audioSrc.Play();
            Destroy(gameObject);
            
         }
         if (collision.gameObject.tag.Contains("PlayerTwo"))
         {
            GameManager.player2stats.coinCount++;
            coinCountText2.text = "$" + GameManager.player2stats.coinCount;
            audioSrc.Play();
            Destroy(gameObject);
           
         }
        
     }
}
