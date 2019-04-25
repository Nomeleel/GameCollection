using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace JungleWarClient
{
    public class TCPClient
    {
        public static void Main(string[] args)
        {
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientSocket.Connect(new IPEndPoint(IPAddress.Parse("192.168.1.10"), 12345));

            int pageSize = 4 * 1024;
            byte[] msgFromServer = new byte[pageSize];
            int length = clientSocket.Receive(msgFromServer);
            string msgStr = Encoding.UTF8.GetString(msgFromServer, 0, length);

            Console.WriteLine(msgStr);
            
            //while (true)
            //{
            //    string data = Console.ReadLine();
            //    if (data == "exit")
            //    {
            //        clientSocket.Close();
            //        return;
            //    }
            //    clientSocket.Send(Encoding.UTF8.GetBytes(data));
            //}
        }
    }
}
