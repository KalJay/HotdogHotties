[System.Serializable]
public class Net_EnemySpawned : NetMsg
{
    public int SpawnPointIndex { get; set; }

    public Net_EnemySpawned()
    {
        OperationCode = NetOperationCode.EnemySpawned;
    }
}
