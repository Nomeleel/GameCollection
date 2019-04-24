using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace JungleWarServer
{
    public class TCPServer
    {
        public const int PAGE_SIZE = 4 * 1024;
        public static byte[] dataBuffer = new byte[PAGE_SIZE];
        public static void Main(string[] args)
        {
            StartServerAsync();
            Console.ReadKey();
        }

        public static void StartServerAsync()
        {
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPAddress iPAddress = IPAddress.Parse("192.168.1.10");
            IPEndPoint iPEndPoint = new IPEndPoint(iPAddress, 12345);
            serverSocket.Bind(iPEndPoint);

            // listen client, para as 0 mean no count limit.
            serverSocket.Listen(5);
              
            serverSocket.BeginAccept(AcceptCallBack, serverSocket);
        }

        public static void AcceptCallBack(IAsyncResult ar)
        {
            Socket serverSocket = ar.AsyncState as Socket;
            Socket clientSocket = serverSocket.EndAccept(ar);

            string msgToClient = "Hello, 来了 老弟！";
            byte[] data = Encoding.UTF8.GetBytes(msgToClient);
            clientSocket.Send(data);

            clientSocket.BeginReceive(dataBuffer, 0, PAGE_SIZE, SocketFlags.None, ReceiveCallBack, clientSocket);

            serverSocket.BeginAccept(AcceptCallBack, serverSocket);
        }

        public static void ReceiveCallBack(IAsyncResult ar)
        {
            Socket clientSocket = null;
            try
            {
                clientSocket = ar.AsyncState as Socket;
                int length = clientSocket.EndReceive(ar);
                if (length == 0)
                {
                    clientSocket.Close();
                    return;
                }
                string msgFromClient = Encoding.UTF8.GetString(dataBuffer, 0, length);
                Console.WriteLine(msgFromClient);
                clientSocket.BeginReceive(dataBuffer, 0, PAGE_SIZE, SocketFlags.None, ReceiveCallBack, clientSocket);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                if (clientSocket != null)
                {
                    clientSocket.Close();
                }
            }
            finally
            {

            }
        }

        public void StartServerSync()
        {
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPAddress iPAddress = IPAddress.Parse("192.168.1.10");
            IPEndPoint iPEndPoint = new IPEndPoint(iPAddress, 12345);
            serverSocket.Bind(iPEndPoint);

            // listen client, para as 0 mean no count limit.
            serverSocket.Listen(5);

            Socket clientSocket = serverSocket.Accept();

            string msgToClient = "Hello, 来了 老弟！";
            byte[] data = Encoding.UTF8.GetBytes(msgToClient);
            clientSocket.Send(data);

            int pageSize = 4 * 1024;
            byte[] buffer = new byte[pageSize];
            int length = clientSocket.Receive(buffer);

            string msgFromClient = Encoding.UTF8.GetString(buffer, 0, length);

            Console.WriteLine(msgFromClient);
            Console.ReadKey();

            clientSocket.Close();
            serverSocket.Close();

        }

    }
}
