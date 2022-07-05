using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using LiteNetLib;
using LiteNetLib.Utils;
using NeoLib.entity;
using NeoLib.network.packet;

namespace NeoLib.network.listener;

public class ServerListener : INetEventListener
{
    private readonly NetPacketProcessor _netPacketProcessor = new NetPacketProcessor();

    private NetManager _server;
    
    private int maxConnections = 10;

    private Dictionary<int, NetPeer> _connectedClients;

    public Dictionary<int, NetPeer> ConnectedClients => _connectedClients;

    // TODO: CLIENT JOINED PACKAGE!?!

    public NetManager Server
    {
        get => _server;
        set => _server = value ?? throw new ArgumentNullException(nameof(value));
    }

    public ServerListener()
    {
        _connectedClients = new Dictionary<int, NetPeer>(maxConnections);
        
        _netPacketProcessor.SubscribeReusable<PlayerJoinedPacket, NetPeer>(PlayerJoinedPacketReceived);
        _netPacketProcessor.SubscribeReusable<MessagePacket, NetPeer>(OnMessagePacketReceived);
    }

    private void PlayerJoinedPacketReceived(PlayerJoinedPacket packet, NetPeer peer)
    {
    }

    private void OnMessagePacketReceived(MessagePacket packet, NetPeer peer)
    {
        Console.WriteLine("[SERVER] received {0} from client {1}:\n{2}", packet.GetType().Name, peer.EndPoint,
            packet.Message);
    }

    public void OnPeerConnected(NetPeer peer)
    {
        Console.WriteLine("[SERVER] Client {0} has joined the server.", peer.EndPoint);

        var writer = new NetDataWriter();

        _connectedClients.Add(peer.Id, peer);
        
        var cap = new ClientAcceptedPacket();
        cap.ClientId = peer.Id;
        cap.Serialize(writer);

        foreach (var item in _connectedClients)
        {
            
        }
        
        _netPacketProcessor.Send(peer, cap, DeliveryMethod.ReliableOrdered);

        Console.WriteLine("[SERVER] Client {0} has ID: " + peer.Id, peer.EndPoint);
        
        // ! now send me player data!
    }

    public void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo)
    {
        if (_connectedClients.ContainsKey(peer.Id))
            _connectedClients.Remove(peer.Id);
        
        Console.WriteLine("[SERVER] Client {0} disconnected.", peer.EndPoint);
    }

    public void OnNetworkError(IPEndPoint endPoint, SocketError socketError)
    {
        Console.WriteLine("[SERVER] Network error! {0} Error: " + socketError, endPoint);
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
        Console.WriteLine("[SERVER] Client {0} has latency: " + latency, peer.EndPoint);
    }

    public void OnConnectionRequest(ConnectionRequest request)
    {
        Console.WriteLine("[SERVER] Client {0} is requesting connection...", request.RemoteEndPoint);

        if (_server.ConnectedPeersCount < maxConnections)
        {
            var peer = request.AcceptIfKey("SomeConnectionKey");
            if (peer != null)
            {
                Console.WriteLine("[SERVER] Client {0} accepted.", request.RemoteEndPoint);

                return;
            }

            const string reason1 = "Wrong password.";
            request.Reject(NetDataWriter.FromString(reason1));
            Console.WriteLine("[SERVER] Client {0} rejected. Reason: {1}", request.RemoteEndPoint, reason1);

            return;
        }

        const string reason2 = "Server is full.";
        request.Reject(NetDataWriter.FromString(reason2));
        Console.WriteLine("[SERVER] Client {0} rejected. Reason: {1}", request.RemoteEndPoint, reason2);
    }
}