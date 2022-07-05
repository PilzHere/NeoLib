namespace NeoLib.component.logic;

public class NetworkIdComponent : Component
{
    public new const int Bits = 16;

    private int _netId;
    
    public NetworkIdComponent(int netId)
    {
        base.Bits = Bits;

        _netId = netId;
    }

    public int NetId
    {
        get => _netId;
        set => _netId = value;
    }
}