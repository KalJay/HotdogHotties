public static class NetOperationCode
{
    public const int None = 0;
    public const int PositionUpdate = 1;
    public const int RotationUpdate = 2;
    public const int PositionAndRotationUpdate = 3;
    public const int BulletShotUpdate = 4;
    public const int PlayerHealthUpdate = 5;
    public const int EnemySpawned = 6;
    public const int EnemyMovementUpdate = 7;
    public const int VanMovementUpdate = 8;
    public const int PlayerDriveUpdate = 9;
    public const int PlayerStatChange = 10;
}

[System.Serializable]
public abstract class NetMsg
{
    public byte OperationCode { get; set; }

    public NetMsg()
    {
        OperationCode = NetOperationCode.None;
    }
}
