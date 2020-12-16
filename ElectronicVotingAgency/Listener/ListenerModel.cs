using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using ElectronicVoting.Extensions;
using ElectronicVotingAgency.Commands;
using ElectronicVotingAgency.Server;

namespace ElectronicVotingAgency.Listener
{
    public class ListenerModel
    {
        private readonly string _host;
        private readonly int _port;
        private TcpClient _client;
        private Thread _clientReceiveThread;
        private NetworkStream _stream;
        private readonly AgencyContext _context;


        public ListenerModel(string host, int port, AgencyContext context)
        {
            _host = host;
            _port = port;
            _context = context;
        }


        public void Connect()
        {
            try
            {
                _client = new TcpClient();
                _client.Connect(_host, _port);
                _stream = _client.GetStream();

                _clientReceiveThread = new Thread(ListenForData);
                _clientReceiveThread.IsBackground = true;
                _clientReceiveThread.Start();

            }
            catch (Exception e)
            {
                Console.WriteLine("On connect exception " + e);
            }
        }

        private void ListenForData()
        {
            try
            {
                while (true)
                {
                    byte[] data = new byte[1024];
                    var builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = _stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.UTF8.GetString(data, 0, bytes));
                    } while (_stream.DataAvailable);

                    string message = builder.ToString();
                    if (message.Length <= 2)
                    {
                        throw new Exception("Short message");
                    }

                    Console.WriteLine($"msg = {message}");
                    var jsonData = fastJSON.JSON.Parse(message).ToDictionary();
                    Console.WriteLine($"Incoming command type = {jsonData["type"]}");
                    var command = _context.MainFactory.CreateInstance<ICommand>(jsonData);
                    command.Execute(_context, "validator");

                    // try
                    // {
                    //     var jsonData = fastJSON.JSON.Parse(message).ToDictionary();
                    //     Debug.Log($"Incoming command type = {jsonData["key"]}");
                    //     var command = Context.Instance.MainFactory.CreateInstance<ICommand>(jsonData);
                    //     command.Execute();
                    // }
                    // catch (Exception e)
                    // {
                    //     Debug.LogError($"Incorrect incoming command. Error = {e}");
                    // }

                }
            }
            catch (SocketException socketException)
            {
                Disconnect();
                Console.WriteLine("Socket exception: " + socketException);
            }
        }

        public void SendMessage(string message)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            _stream.Write(data, 0, data.Length);
        }
        public void SendCommand(ICommand command)
        {
            var message = fastJSON.JSON.ToJSON(command.GetInfo());
            byte[] data = Encoding.UTF8.GetBytes(message);
            _stream.Write(data, 0, data.Length);
        }


        public void Disconnect()
        {
            _client?.Close();
            _clientReceiveThread.Abort();
        }
    }
}