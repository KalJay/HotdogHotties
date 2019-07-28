using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DrivingScript : MonoBehaviour
{
    public GameObject indicatorText1, indicatorText2;
    private bool allowed1, allowed2, driving, startDriving, endStartDrivingBool;
    private GameObject player1, player2;
    private GameObject van, playerInVan;
    public float acceleration = 2, steering = 1;
    private Rigidbody rbd;
    private Vector2 curspeed;
    private bool vanFull, boss = false;
    private float petrol;
    public float startingPetrol;
    public Slider fuelGuage;
    private float time = 0.25f;

    private Vector2 PrevPos;
    private bool isMoving = false;

    private Animator anim;
    private AudioSource audioSource; 

    void Start()
    {
        van = this.transform.parent.gameObject;
        PrevPos = van.transform.position;
        petrol = startingPetrol / 2;
        fuelGuage.maxValue = startingPetrol;
        fuelGuage.value = petrol;
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (petrol > startingPetrol && !boss)
            petrol = startingPetrol;
        if (petrol < 0)
            petrol = 0;
        if (petrol > 0)
            fuelGuage.transform.GetChild(0).GetComponent<Image>().color = Color.white;
        if (petrol == 0)
            fuelGuage.transform.GetChild(0).GetComponent<Image>().color = new Color(0.4f, 0.4f, 0.4f);

        //CHEAT
        if (Input.GetKeyDown(KeyCode.F5))
            AddFuel();

        if (isMoving && time > 0)
        {
            time -= Time.deltaTime;
        }
        if (isMoving && time < 0)
        {
            petrol--;
            fuelGuage.value = petrol;
            time = 0.5f;
        }

        if (Input.GetButtonDown("Submit") && allowed2 && (!vanFull || GetPlayer().Equals(GameManager.playerTwo)))
        {
            startDriving = !startDriving;
            endStartDrivingBool = false;
            indicatorText2.gameObject.GetComponent<TextMeshPro>().enabled = false;
            SetPlayer(GameManager.playerTwo);
        }
        if (Input.GetKeyDown(KeyCode.F) && allowed1 && (!vanFull || GetPlayer().Equals(GameManager.playerOne)))
        {
            startDriving = !startDriving;
            endStartDrivingBool = false;
            indicatorText1.gameObject.GetComponent<TextMeshPro>().enabled = false;
            SetPlayer(GameManager.playerOne);
        }

        {
            if (startDriving && !endStartDrivingBool)
            {
                driving = true;
                DriveStart(GetPlayer());
                endStartDrivingBool = true;
            }
            if (!startDriving && !endStartDrivingBool)
            {
                driving = false;
                DriveEnd(GetPlayer());
                endStartDrivingBool = true;
            }
        }

        if (driving && petrol > 0)
        {
            Vector3 movement = GameManager.GetMovementVectorForControlScheme(playerInVan.GetComponent<PlayerController>().controlScheme, 1.0f / Time.deltaTime);
            if(movement.Equals(Vector3.zero))
            {
                isMoving = false;
            } else
            {
                isMoving = true;
            }
            van.transform.Rotate(Vector3.up * movement.x * steering); //was h
            van.transform.position += transform.up * movement.z * acceleration* Time.deltaTime; //was v
        }
    }

    public float GetPetrolAmount()
    {
        return petrol;
    }

    public void InifiniteFuel()
    {
        boss = true;
        petrol = 3000;
        fuelGuage.value = petrol;
    }

    public void AddFuel()
    {
        petrol += 5;
        fuelGuage.value = petrol;
    }

    void SetPlayer(GameObject player)
    {
        playerInVan = player;
    }
    GameObject GetPlayer()
    {
        return playerInVan;
    }

    void DriveStart(GameObject player)
    {
        vanFull = true;
        player.transform.parent = van.transform;
        player.SetActive(false);
    }

    void DriveEnd(GameObject player)
    {
        if (player != null)
        {
            player.transform.parent = null;
            player.SetActive(true);
        }
        isMoving = false;
        vanFull = false;
        allowed1 = false;
        allowed2 = false;
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.tag.Contains("PlayerOne") && !vanFull)
        {
            allowed1 = true;
            indicatorText1.gameObject.GetComponent<TextMeshPro>().enabled = true;
            indicatorText1.GetComponent<TextMeshPro>().text = "ENTER";
        }
        if (collision.tag.Contains("PlayerOne") && vanFull)
            indicatorText1.gameObject.GetComponent<TextMeshPro>().enabled = false;

        if (collision.tag.Contains("PlayerTwo") && !vanFull)
        {
            allowed2 = true;
            indicatorText2.gameObject.GetComponent<TextMeshPro>().enabled = true;
            indicatorText2.GetComponent<TextMeshPro>().text = "ENTER";
        }
        if (collision.tag.Contains("PlayerTwo") && vanFull)
            indicatorText2.gameObject.GetComponent<TextMeshPro>().enabled = false;
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.tag.Contains("PlayerOne"))
        {
            indicatorText1.gameObject.GetComponent<TextMeshPro>().enabled = false;
            allowed1 = false;
        }
        if (collision.tag.Contains("PlayerTwo"))
        {
            indicatorText2.gameObject.GetComponent<TextMeshPro>().enabled = false;
            allowed2 = false;
        }
    }
    
    /*public void UpdateVanMovement(Vector2 force, Vector2 position, Quaternion rotation)
    {
        transform.parent.position = position;
        transform.parent.rotation = rotation;
        rbd.AddForce(force);
    }

    public void UpdatePlayerDrive()
    {
        if(occupiedby == null)
        {
            occupiedby = GameManager.otherPlayer;
            DriveStart(GameManager.otherPlayer);
            //indicatorText.SetActive(false);
        } else
        {
            occupiedby = null;
            DriveEnd(GameManager.otherPlayer);
        }
    }*/
}
