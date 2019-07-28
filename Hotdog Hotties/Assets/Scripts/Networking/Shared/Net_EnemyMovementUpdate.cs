[System.Serializable]
public class Net_EnemyMovementUpdate : NetMsg
{
    public float x { get; set; }
    public float y { get; set; }

    public int EnemyID { get; set; }

    public Net_EnemyMovementUpdate()
    {
        OperationCode = NetOperationCode.EnemyMovementUpdate;
    }
}
