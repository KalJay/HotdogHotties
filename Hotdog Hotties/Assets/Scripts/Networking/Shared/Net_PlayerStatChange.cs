[System.Serializable]
public class Net_PlayerStatChange : NetMsg
{
    public int coinCount { get; set; }
    public int damageBuffCount { get; set; }
    public int rangeBuffCount { get; set; }
    public int healthBuffCount { get; set; }
    public int speedBuffCount { get; set; }
    public int defaultBuffCount { get; set; }
    public int specialBuffCount { get; set; }
    public int abilityBuffCount { get; set; }

    public Net_PlayerStatChange()
    {
        OperationCode = NetOperationCode.PlayerStatChange;
    }
}
