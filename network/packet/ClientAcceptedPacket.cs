using LiteNetLib;
using LiteNetLib.Utils;

namespace NeoLib.network.packet;

public class ClientAcceptedPacket : INetSerializable
{
    private int _clientId;
    //private Dictionary<int, NetPeer> _connectedClients = new Dictionary<int, NetPeer>();

    public int ClientId
    {
        get => _clientId;
        set => _clientId = value;
    }

    /*
    public Dictionary<int, NetPeer> ConnectedClients
    {
        get => _connectedClients;
        set => _connectedClients = value ?? throw new ArgumentNullException(nameof(value));
    }
    */

    public void Serialize(NetDataWriter writer)
    {
        writer.Reset();
        writer.Put(_clientId);
        
        //writer.Put(_connectedClients);
    }

    public void Deserialize(NetDataReader reader)
    {
        _clientId = reader.GetInt();
    }
}