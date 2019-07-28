[System.Serializable]
public class Net_VanMovementUpdate : NetMsg
{
    public float vectorx;
    public float vectory;

    public float posx;
    public float posy;

    public float rotx;
    public float roty;
    public float rotz;
    public float rotw;

    public Net_VanMovementUpdate()
    {
        OperationCode = NetOperationCode.VanMovementUpdate;
    }
}
