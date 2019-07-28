[System.Serializable]
public class Net_BulletShotUpdate : NetMsg
{
    public int bulletId { get; set; }

    public float xpos { get; set; }
    public float ypos { get; set; }

    public float xdest { get; set; }
    public float ydest { get; set; }
    
    public bool isPlayerShot { get; set; }

    public Net_BulletShotUpdate()
    {
        OperationCode = NetOperationCode.BulletShotUpdate;
    }
}
