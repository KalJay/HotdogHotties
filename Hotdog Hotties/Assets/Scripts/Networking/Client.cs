using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Networking;

public class Client : MonoBehaviour
{
    private GameManager gameManager;

    private const int MAX_USERS = 10;
    private const int PORT = 26111;
    private const int BYTE_SIZE = 1024;
    public string SERVER_IP;

    private byte reliableChannel;
    private int connectionId;
    private int hostId;
    private byte error;

    private bool isStarted = false;

    #region MonoBehaviour
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        Init();
    }
    void Update()
    {
        UpdateMessagePump();
    }
    #endregion

    public void Init()
    {
        Debug.Log(string.Format("Attempting to connect on {0} via port {1}...", SERVER_IP, PORT));
        NetworkTransport.Init();

        ConnectionConfig cc = new ConnectionConfig();
        reliableChannel = cc.AddChannel(QosType.Reliable);

        HostTopology topo = new HostTopology(cc, MAX_USERS);

        hostId = NetworkTransport.AddHost(topo, 0);

        connectionId = NetworkTransport.Connect(hostId, SERVER_IP, PORT, 0, out error);

        if (error != 0)
        {
            Debug.LogError("Failed to connect to server, error code " + error);
            Shutdown();
        }
        else
        {
            isStarted = true;
        }

    }
    public void Shutdown()
    {
        isStarted = false;
        NetworkTransport.Shutdown();
        Destroy(this);
    }

    private void UpdateMessagePump()
    {
        if (!isStarted)
        {
            return;
        }

        int recHostId;
        int connectionId;
        int channelId;

        byte[] recBuffer = new byte[BYTE_SIZE];
        int datasize;

        NetworkEventType type = NetworkTransport.Receive(out recHostId, out connectionId, out channelId, recBuffer, BYTE_SIZE, out datasize, out error);

        switch (type)
        {
            case NetworkEventType.Nothing:
                break;
            case NetworkEventType.ConnectEvent:
                Debug.Log(string.Format("Connected to server at {0}:{1}", SERVER_IP, PORT));
                GameManager.LoadGameScene();
                break;
            case NetworkEventType.DisconnectEvent:
                Debug.Log(string.Format("Disconnected from the server at {0}:{1}", SERVER_IP, PORT));
                GameManager.LoadMainMenuScene();
                break;
            case NetworkEventType.DataEvent:
                BinaryFormatter formatter = new BinaryFormatter();
                MemoryStream ms = new MemoryStream(recBuffer);
                NetMsg msg = (NetMsg)formatter.Deserialize(ms);

                OnData(connectionId, channelId, recHostId, msg);
                break;
            default:
            case NetworkEventType.BroadcastEvent:
                Debug.LogWarning("Unexpected Network Event");
                break;
        }
    }

    #region onData
    private void OnData(int connectionId, int channelId, int recHostId, NetMsg msg)
    {
        //switch (msg.OperationCode)
        //{
        //    case NetOperationCode.None:
        //        break;
        //    case NetOperationCode.PositionUpdate:
        //        gameManager.RecievePosUpdate((Net_PositionUpdate)msg);
        //        break;
        //    case NetOperationCode.RotationUpdate:
        //        gameManager.RecieveRotUpdate((Net_RotationUpdate)msg);
        //        break;
        //    case NetOperationCode.PositionAndRotationUpdate:
        //        gameManager.RecievePosAndRotUpdate((Net_PositionAndRotationUpdate)msg);
        //        break;
        //    case NetOperationCode.BulletShotUpdate:
        //        gameManager.RecieveBulletShotUpdate((Net_BulletShotUpdate)msg);
        //        break;
        //    case NetOperationCode.PlayerHealthUpdate:
        //        gameManager.RecievePlayerHealthUpdate((Net_PlayerHealthUpdate)msg);
        //        break;
        //    case NetOperationCode.EnemySpawned:
        //        gameManager.RecieveEnemySpawned((Net_EnemySpawned)msg);
        //        break;
        //    case NetOperationCode.EnemyMovementUpdate:
        //        gameManager.ReceiveEnemyMovementUpdate((Net_EnemyMovementUpdate)msg);
        //        break;
        //    case NetOperationCode.VanMovementUpdate:
        //        gameManager.RecieveVanMovementUpdate((Net_VanMovementUpdate)msg);
        //        break;
        //    case NetOperationCode.PlayerDriveUpdate:
        //        gameManager.RecievePlayerDriveUpdate((Net_PlayerDriveUpdate)msg);
        //        break;
        //    case NetOperationCode.PlayerStatChange:
        //        gameManager.ReceivePlayerStatChange((Net_PlayerStatChange)msg);
        //        break;
        //}
    }
    #endregion

    #region Send
    public void SendServer(NetMsg msg) {
        byte[] buffer = new byte[BYTE_SIZE];

        BinaryFormatter formatter = new BinaryFormatter();
        MemoryStream ms = new MemoryStream(buffer);
        formatter.Serialize(ms, msg);

        NetworkTransport.Send(hostId, connectionId, reliableChannel, buffer, BYTE_SIZE, out error);
    }
    #endregion
}
