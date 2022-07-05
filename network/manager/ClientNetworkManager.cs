using LiteNetLib;
using LiteNetLib.Utils;
using NeoLib.network.listener;
using NeoLib.network.packet;

namespace NeoLib.network.manager;

public class ClientNetworkManager
{
    private bool _isRunning = true;
    private ClientListener _listener;
    private NetManager _client;
    private const int SleepTime = 15; // 1/66~67

    public ClientNetworkManager()
    {
        _listener = new ClientListener();
        _client = new NetManager(_listener);
        _listener.Client = _client;
    }

    public void Start()
    {
        _client.Start();

        Console.WriteLine("[CLIENT] NetMan started!");
    }

    public void Connect(string adress, int port, string password)
    {
        Console.WriteLine("[CLIENT] Connecting to {0}:" + port, adress);
        
        _client.Connect(adress, port, password /* text key or NetDataWriter */);
    }

    public void Update()
    {
        while (_isRunning)
        {
            /*
            var mp = new MessagePacket();
            mp.Message = "Hello from client!";
            var writer = new NetDataWriter();
            mp.Serialize(writer);
            _listener.NetPacketProcessor.Send(_client.FirstPeer, mp, DeliveryMethod.ReliableOrdered);
            */
            
            _client.PollEvents();
            Thread.Sleep(SleepTime);
        }

        _client.Stop();
        Console.WriteLine("[CLIENT] NetMan stopped!");
    }

    public void Stop()
    {
        _isRunning = false;
    }
}