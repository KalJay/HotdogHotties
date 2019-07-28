using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameManager gameManager;
    private Camera MainCamera;

    public GameObject crosshair;
    public float movementSpeed = 10.0f;
    public bool isPlayerOne = true;

    public LayerMask layerMask;

    public GameManager.ControlType controlScheme;

    public enum Direction { Left, Right, Up, Down}
    public const float Deadzone = 0.5f;

    private Rigidbody rb;
    private BoxCollider2D col;

    private Vector2 previousPos;
    private Quaternion previousRot;

    private float RollStartTimer = -600.0f;
    private float RollCooldown = 5.0f;
    private float RollDuration = 0.3f;
    private float RollSpeed = 0.2f;
    private Vector3 RollDirection;
    private bool isRolling = false;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<BoxCollider2D>();
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        isPlayerOne = GameManager.playerOne.Equals(gameObject);

        UpdateControlScheme();
        
        previousPos = transform.position;
        previousRot = transform.rotation;

    }
    
    void Update()
    {
        {//Movement Logic
            Vector3 movement = GameManager.GetMovementVectorForControlScheme(controlScheme, movementSpeed);
            rb.velocity = new Vector3();
            rb.angularVelocity = new Vector3();

            if(controlScheme == GameManager.ControlType.Keyboard)
            {
                MouseRotatePlayer();
            } else
            {
                CrosshairRotatePlayer();
                //Quaternion rotation = GameManager.GetRotationForController(controlScheme, transform.rotation);
                //transform.rotation = rotation;
            }

            if(isRolling) //Actually doing the roll movement
            {
                movement += RollDirection*RollSpeed;
            }

            rb.MovePosition(new Vector3(transform.position.x + movement.x, transform.position.y, transform.position.z + movement.z));
        }

        { //Roll Logic
            if(isRolling)
            {
                if((Time.time - RollStartTimer) > RollDuration)
                {
                    isRolling = false;
                }
            }

            if(isRollOffCooldown() &&  GameManager.GetBoolForInstruction(GameManager.Instruction.Ability, controlScheme))
            {
                RollStartTimer = Time.time;
                RollDirection = transform.forward;
                isRolling = true;
            }
        }
    }

    private void MouseRotatePlayer()
    {
        Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray);

        foreach (RaycastHit hit in hits)
        {
            if(hit.collider.gameObject.CompareTag("Terrain"))
            {
                transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
            }
        }
    }

    private void CrosshairRotatePlayer()
    {
        Ray ray = MainCamera.ScreenPointToRay(crosshair.transform.position);
        RaycastHit[] hits = Physics.RaycastAll(ray);

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.gameObject.CompareTag("Terrain"))
            {
                transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
            }
        }
    }

    public void IncreaseMovementSpeed(int playerNum)
    {
        if(playerNum == 1)
            movementSpeed += GameManager.player1stats.speedBuffCount + 1;
        if (playerNum == 2)
            movementSpeed += GameManager.player2stats.speedBuffCount + 1;
    }

    private bool isRollOffCooldown()
    {
        return Time.time > RollStartTimer + RollCooldown;
    }

    public float GetRollCooldownAsPercentage()
    {
        if(isRollOffCooldown())
        {
            return 0.0f;
        } else
        {
            return (Time.time - RollStartTimer) / RollCooldown;
        }
    }

    public void UpdateControlScheme(GameManager.ControlType scheme)
    {
        if(isPlayerOne)
        {
            GameManager.CurrentPlayerOneController = scheme;
        } else
        {
            GameManager.CurrentPlayerTwoController = scheme;
        }
        controlScheme = scheme;
    }

    public void UpdateControlScheme()
    {
        if(isPlayerOne)
        {
            UpdateControlScheme(GameManager.DefaultPlayerOneController);
        } else
        {
            UpdateControlScheme(GameManager.DefaultPlayerTwoController);
        }
    }
}
