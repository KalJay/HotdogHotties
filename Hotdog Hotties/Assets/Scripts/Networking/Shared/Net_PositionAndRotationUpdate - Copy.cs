[System.Serializable]
public class Net_PositionAndRotationUpdate : NetMsg
{
    public float xpos { get; set; }
    public float ypos { get; set; }

    public float xrot { get; set; }
    public float yrot { get; set; }
    public float zrot { get; set; }
    public float wrot { get; set; }

    public Net_PositionAndRotationUpdate()
    {
        OperationCode = NetOperationCode.PositionAndRotationUpdate;
    }
}
