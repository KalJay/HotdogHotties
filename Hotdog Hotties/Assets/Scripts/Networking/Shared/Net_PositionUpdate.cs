[System.Serializable]
public class Net_PositionUpdate : NetMsg
{
    public float x { get; set; }
    public float y { get; set; }

    public Net_PositionUpdate()
    {
        OperationCode = NetOperationCode.PositionUpdate;
    }
}
