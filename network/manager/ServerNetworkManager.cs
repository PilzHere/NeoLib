using System.Data;
using LiteNetLib;
using LiteNetLib.Utils;
using NeoLib.network.listener;
using NeoLib.network.packet;

namespace NeoLib.network.manager;

public class ServerNetworkManager
{
    //private bool _isRunning = true;
    private ServerListener _listener;
    private NetManager _server;
    private const int SleepTime = 15; // 1/66~67

    public ServerNetworkManager()
    {
        _listener = new ServerListener();
        _server = new NetManager(_listener);
        _listener.Server = _server;
    }

    public void Start()
    {
        _server.Start(9050);

        Console.WriteLine("[SERVER] NetMan started!");
    }

    public void Update()
    {
        //while (_isRunning)
        //{
            _server.PollEvents();
            //Thread.Sleep(SleepTime);
        //}

        //_server.Stop(true);
        //Console.WriteLine("[SERVER] NetMan stopped!");
    }

    public void Stop()
    {
        //_isRunning = false;
        
        _server.Stop(true);
    }
}