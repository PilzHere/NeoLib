using System.Net;
using System.Net.Sockets;
using LiteNetLib;
using LiteNetLib.Utils;
using NeoLib.network.packet;

namespace NeoLib.network.listener;

public class ClientListener : INetEventListener
{
    private readonly NetPacketProcessor _netPacketProcessor = new NetPacketProcessor();

    private NetManager _client;

    private int clientId;
    
    public ClientListener()
    {
        _netPacketProcessor.SubscribeReusable<MessagePacket, NetPeer>(OnMessagePacketReceived);
        _netPacketProcessor.SubscribeReusable<ClientAcceptedPacket, NetPeer>(OnClientAcceptedPacketReceived);
    }

    private void OnClientAcceptedPacketReceived(ClientAcceptedPacket packet, NetPeer peer)
    {
        clientId = packet.ClientId;
        
        Console.WriteLine("[CLIENT] My ID is: " + clientId);
    }

    private void OnMessagePacketReceived(MessagePacket packet, NetPeer peer)
    {
        Console.WriteLine("[CLIENT] received MessagePacket from server :\n{0}", packet.Message);
    }

    public NetManager Client
    {
        get => _client;
        set => _client = value ?? throw new ArgumentNullException(nameof(value));
    }

    public NetPacketProcessor NetPacketProcessor => _netPacketProcessor;

    public void OnPeerConnected(NetPeer peer)
    {
    }

    public void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo)
    {
    }

    public void OnNetworkError(IPEndPoint endPoint, SocketError socketError)
    {
        Console.WriteLine("[CLIENT] Network error! {0} Error: " + socketError, endPoint);
    }

    public void OnNetworkReceive(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod)
    {
        _netPacketProcessor.ReadAllPackets(reader, peer);
    }

    public void OnNetworkReceiveUnconnected(IPEndPoint remoteEndPoint, NetPacketReader reader,
        UnconnectedMessageType messageType)
    {
    }

    public void OnNetworkLatencyUpdate(NetPeer peer, int latency)
    {
    }

    public void OnConnectionRequest(ConnectionRequest request)
    {
    }
}