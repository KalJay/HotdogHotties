using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum SpecialWeapon { HamburgerGun, FrenchFryShotgun, SauceSniperRifle, ChorizoUzis, FreshlyToastedBaguette, SausageHarpoonLauncher }
    public enum ControlType { Keyboard, Controller1, Controller2 }
    public enum Level { None, Level1, Level2, Level3, Level4, Level5}
    public enum Instruction { BasicGun, SpecialGun, Ability, Interact}

    public static PlayerStats player1stats, player2stats;

    public static GameObject playerOne;
    public static GameObject playerTwo;
    private static SpriteRenderer otherSpriteRenderer;
    public static SpecialWeapon selectedWeapon1;
    public static SpecialWeapon selectedWeapon2 = SpecialWeapon.FreshlyToastedBaguette;
    public static ControlType DefaultPlayerOneController = ControlType.Keyboard;
    public static ControlType DefaultPlayerTwoController = ControlType.Controller1;

    public static ControlType CurrentPlayerOneController;
    public static ControlType CurrentPlayerTwoController;
    public static Level currentLevel = Level.None;

    private static spawnPointFollowers spawnPointScript;
    private static DrivingScript Van;
    private static GameObject cam;

    public static int SpecialWeaponToID(SpecialWeapon weapon) {
        return (int)weapon + 1;
    }

    void Start() {
        
        if(player1stats == null)
        {
            player1stats = new PlayerStats(0, 0, 0, 0, 0, 0, 0, 0, 0);
            player2stats = new PlayerStats(0, 0, 0, 0, 0, 0, 0, 0, 0);
        }
        if (GameObject.FindGameObjectsWithTag("GameManager").Length >= 2) {
            Destroy(gameObject);
        }
        if(SceneManager.GetActiveScene().name.Equals("SampleScene"))
        { //Editor Only Executions
            cam = GameObject.FindGameObjectWithTag("MainCamera");
            playerOne = GameObject.FindGameObjectWithTag("PlayerOne");
            playerTwo = GameObject.FindGameObjectWithTag("PlayerTwo");
            Van = GameObject.FindGameObjectWithTag("Van").GetComponentInChildren<DrivingScript>();
            GoToLevel(Level.Level1);
        }
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            SceneManager.LoadScene(0);
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            SceneManager.LoadScene(1);
        }
        if(Input.GetKeyDown(KeyCode.F10))
        {
            cam.transform.position = new Vector3(1.5f, 36.8f, 278.7f);
            GoToLevel(Level.Level5);
        }
        if (Input.GetKeyDown(KeyCode.F11))
        {
            cam.transform.position = new Vector3(-13.941f, 0.832f, 36.806f);
            GoToLevel(Level.Level2);
        }
        if (Input.GetKeyDown(KeyCode.F12))
        {
            cam.transform.position = new Vector3(-2.58f, 0.832f, 97.23f);
            GoToLevel(Level.Level3);
        }
    }

    #region Control Schemes
    public static Vector3 GetMovementVectorForControlScheme(ControlType controlScheme, float movementSpeed)
    {
        Vector3 movement = new Vector3();

        if(controlScheme == ControlType.Keyboard)
        {
            if (Input.GetKey(KeyCode.A))
            {
                movement.x -= movementSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.S))
            {
                movement.z -= movementSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.D))
            {
                movement.x += movementSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.W))
            {
                movement.z += movementSpeed * Time.deltaTime;
            }
        } else
        {
            if(controlScheme == ControlType.Controller1)
            {
                if (Input.GetAxisRaw("Controller1LeftHorizontal") > 0)
                {
                    movement.x += movementSpeed * Time.deltaTime;
                }
                if (Input.GetAxisRaw("Controller1LeftHorizontal") < 0) 
                {
                    movement.x -= movementSpeed * Time.deltaTime;
                }
                if (Input.GetAxisRaw("Controller1LeftVertical") < 0)
                {
                    movement.z += movementSpeed * Time.deltaTime;
                }
                if (Input.GetAxisRaw("Controller1LeftVertical") > 0)
                {
                    movement.z -= movementSpeed * Time.deltaTime;
                }
            } else
            {
                if (Input.GetAxisRaw("Controller2LeftHorizontal") > 0)
                {
                    movement.x += movementSpeed * Time.deltaTime;
                }
                if (Input.GetAxisRaw("Controller2LeftHorizontal") < 0)
                {
                    movement.x -= movementSpeed * Time.deltaTime;
                }
                if (Input.GetAxisRaw("Controller2LeftVertical") < 0)
                {
                    movement.z += movementSpeed * Time.deltaTime;
                }
                if (Input.GetAxisRaw("Controller2LeftVertical") > 0)
                {
                    movement.z -= movementSpeed * Time.deltaTime;
                }
            }
        }
        movement = Vector3.ClampMagnitude(movement, movementSpeed * Time.deltaTime);
        return movement;
    }
    public static Quaternion GetRotationForController(ControlType controlScheme, Quaternion prevRotation)
    {
        if(controlScheme == ControlType.Controller1)
        {
            float Horizontal = Input.GetAxisRaw("Controller1RightHorizontal");
            float Vertical = Input.GetAxisRaw("Controller1RightVertical");
            if (Mathf.Abs(Horizontal) != 0 && Mathf.Abs(Vertical) != 0)
            {
                return Quaternion.LookRotation(new Vector3(Vertical, 0, Horizontal));
            }
        }
        if(controlScheme == ControlType.Controller2)
        {
            float Horizontal = Input.GetAxisRaw("Controller2RightHorizontal");
            float Vertical = Input.GetAxisRaw("Controller2RightVertical");
            if (Mathf.Abs(Horizontal) != 0 && Mathf.Abs(Vertical) != 0)
            {
                return Quaternion.LookRotation(new Vector3(Vertical, 0, Horizontal));
            }
        }
        return prevRotation;
    }
    public static bool GetBoolForInstruction(Instruction instruction, ControlType controlScheme)
    {
        switch (instruction)
        {
            case Instruction.BasicGun:
                if(controlScheme == ControlType.Keyboard)
                {
                    return Input.GetKey(KeyCode.Mouse0);
                } else
                {
                    if(controlScheme == ControlType.Controller1)
                    {
                        return Input.GetAxisRaw("Controller1RT") != 0.0f;
                    } else
                    {
                        return Input.GetAxisRaw("Controller2RT") != 0.0f;
                    }
                }
            case Instruction.SpecialGun:
                if (controlScheme == ControlType.Keyboard)
                {
                    return Input.GetKey(KeyCode.Mouse1);
                }
                else
                {
                    if (controlScheme == ControlType.Controller1)
                    {
                        return Input.GetAxisRaw("Controller1LT") != 0.0f;
                    }
                    else
                    {
                        return Input.GetAxisRaw("Controller2LT") != 0.0f;
                    }
                }
            case Instruction.Ability:
                if (controlScheme == ControlType.Keyboard)
                {
                    return Input.GetKey(KeyCode.LeftShift);
                }
                else
                {
                    if (controlScheme == ControlType.Controller1)
                    {
                        return Input.GetKeyDown(KeyCode.Joystick1Button0);
                    }
                    else
                    {
                        return Input.GetKeyDown(KeyCode.Joystick2Button0);
                    }
                }
            case Instruction.Interact:
                if(controlScheme == ControlType.Keyboard)
                {
                    return Input.GetKey(KeyCode.F);
                } else
                {
                    if(controlScheme == ControlType.Controller1)
                    {
                        return Input.GetKeyDown(KeyCode.Joystick1Button2);
                    } else
                    {
                        return Input.GetKeyDown(KeyCode.Joystick2Button2);
                    }
                }
        }
        return false;
    }
    #endregion

    #region SceneLoading
    public static void LoadGameScene() {
        SceneManager.LoadScene(1);
        SceneManager.sceneLoaded += RegisterObjectReferences;
    }
    public static void LoadMainMenuScene() {
        SceneManager.LoadScene(0);
        //GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().Reset();
    }
    public static void RegisterObjectReferences(Scene scene, LoadSceneMode lsm)
    {
        playerOne = GameObject.FindGameObjectWithTag("PlayerOne");
        playerTwo = GameObject.FindGameObjectWithTag("PlayerTwo");

        //spawnPointScript = GameObject.FindGameObjectWithTag("Enemy Spawner").GetComponent<spawnPointFollowers>();
        Van = GameObject.FindGameObjectWithTag("Van").GetComponentInChildren<DrivingScript>();
    }
    #endregion

    public static void SetPlayerWeapon(int id, bool isPlayerOne) {
        if(isPlayerOne)
        {
            selectedWeapon1 = (SpecialWeapon) id;
        } else
        {
            selectedWeapon2 = (SpecialWeapon) id;
        }
    }

    public static void GoToLevel(Level level)
    {
        if(level > currentLevel)
        {
            Vector3 resetPos;
            switch(level)
            {
                case Level.Level2:
                    resetPos = new Vector3(-13.941f, 0.832f, 36.806f);
                    break;
                case Level.Level3:
                    resetPos = new Vector3(-2.58f, 0.832f, 97.23f);
                    break;
                case Level.Level4:
                    resetPos = new Vector3(1.11f, 0.832f, 170.63f);
                    break;
                case Level.Level1:
                    resetPos = new Vector3(15.84f, 0.832f, -4.01f);
                    break;
                case Level.Level5:
                    Van.InifiniteFuel();
                    cam.GetComponent<CameraFollow>().spaceZ = 30;
                    resetPos = new Vector3(1.15f, 0.832f, 310.59f);
                    break;
                default:
                    resetPos = new Vector3(15.84f, 0.832f, -4.01f);
                    break;
            }

            Van.transform.parent.transform.position = resetPos;
            playerOne.transform.position = resetPos + new Vector3(1.5f, 0.0f, 0.0f);
            playerTwo.transform.position = resetPos + new Vector3(-1.5f, 0.0f, 0.0f);

            playerOne.GetComponent<PlayerHealthScript>().FullHealth(playerOne);
            playerTwo.GetComponent<PlayerHealthScript>().FullHealth(playerTwo);

            currentLevel = level;
        }
    }
}
