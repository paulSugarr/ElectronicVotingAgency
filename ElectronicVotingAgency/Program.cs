using System;
using System.Threading;
using ElectronicVotingAgency.Commands;
using ElectronicVotingAgency.Listener;
using ElectronicVotingAgency.Server;

namespace ElectronicVotingAgency
{
    class Program
    {
        static ServerModel _server;
        static Thread _serverThread;
        
        static ListenerModel _listener;
        static void Main(string[] args)
        {
            {
                try
                {
                    _server = new ServerModel();
                    _serverThread = new Thread(_server.Listen);
                    _serverThread.Start();
                    
                    _listener = new ListenerModel("127.0.0.1", 8888, _server.Context);
                    _listener.Connect();
                    
                    var command = new SendValidatorKeyCommand();
                    _listener.SendCommand(command);
                }
                catch (Exception ex)
                {
                    _server.Disconnect();
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
