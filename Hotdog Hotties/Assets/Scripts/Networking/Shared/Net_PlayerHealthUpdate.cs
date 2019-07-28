[System.Serializable]
public class Net_PlayerHealthUpdate : NetMsg
{
    public int newHealth { get; set; }
    public bool isPlayerOne { get; set; }

    public Net_PlayerHealthUpdate()
    {
        OperationCode = NetOperationCode.PlayerHealthUpdate;
    }
}
