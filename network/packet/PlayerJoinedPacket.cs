using LiteNetLib.Utils;

namespace NeoLib.network.packet;

public class PlayerJoinedPacket : INetSerializable
{
    private int playerId;
    private string playerName;
    private float playerPosX;
    private float playerPosY;
    private float playerPosZ;

    public int PlayerId
    {
        get => playerId;
        set => playerId = value;
    }

    public string PlayerName
    {
        get => playerName;
        set => playerName = value ?? throw new ArgumentNullException(nameof(value));
    }

    public float PlayerPosX
    {
        get => playerPosX;
        set => playerPosX = value;
    }

    public float PlayerPosY
    {
        get => playerPosY;
        set => playerPosY = value;
    }

    public float PlayerPosZ
    {
        get => playerPosZ;
        set => playerPosZ = value;
    }

    public void Serialize(NetDataWriter writer)
    {
        writer.Put(playerId);
        writer.Put(playerName);
        writer.Put(playerPosX);
        writer.Put(playerPosY);
        writer.Put(playerPosZ);
    }

    public void Deserialize(NetDataReader reader)
    {
        playerId = reader.GetInt();
        playerName = reader.GetString();
        playerPosX = reader.GetFloat();
        playerPosY = reader.GetFloat();
        playerPosZ = reader.GetFloat();
    }
}