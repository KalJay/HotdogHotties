using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Manager : MonoBehaviour
{
    #region Singleton
    public static Player2Manager instance;

    void Awake()
    {
        instance = this;
    }
    #endregion

    public GameObject player;
}
