using LiteNetLib.Utils;

namespace NeoLib.network.packet;

public class MessagePacket : INetSerializable
{
    private string _message = null;

    public void Serialize(NetDataWriter writer)
    {
        writer.Put(_message);
    }

    public void Deserialize(NetDataReader reader)
    {
        _message = reader.GetString();
    }

    public string Message
    {
        get => _message;
        set => _message = value ?? throw new ArgumentNullException(nameof(value));
    }
}