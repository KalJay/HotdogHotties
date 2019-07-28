[System.Serializable]
public class Net_RotationUpdate : NetMsg
{
    public float x { get; set; }
    public float y { get; set; }
    public float z { get; set; }
    public float w { get; set; }

    public Net_RotationUpdate()
    {
        OperationCode = NetOperationCode.RotationUpdate;
    }
}
