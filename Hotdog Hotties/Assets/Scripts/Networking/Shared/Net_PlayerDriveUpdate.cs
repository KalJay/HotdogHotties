[System.Serializable]
public class Net_PlayerDriveUpdate : NetMsg
{
    public Net_PlayerDriveUpdate()
    {
        OperationCode = NetOperationCode.PlayerDriveUpdate;
    }
}
