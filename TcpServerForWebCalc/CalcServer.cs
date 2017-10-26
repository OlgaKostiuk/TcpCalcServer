using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TcpServerForWebCalc
{
    class CalcServer
    {
        TcpListener server;
        public void Start()
        {
            server.Start();
            Console.WriteLine("Waiting for a client...");
            while (true)
            {
                try
                {
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Client was connected. Waititng for query...");
                    NetworkStream stream = client.GetStream();
                    BinaryReader reader = new BinaryReader(stream);
                    BinaryWriter writer = new BinaryWriter(stream);


                    string data = reader.ReadString();
                    Console.WriteLine($"Client: {data}");
                    Dictionary<string, string> param = ParseHttpQuery(data);
                    int res = Calculate(param["num1"], param["num2"], param["opr"]);

                    string response = "HTTP/1.1 200 OK\n" +
                                        "Access-Control-Allow-Origin: * \n" +
                                        "Connection: close\n" +
                                        "Content-Type: text/plain\n" +
                                        "Content-Length: " + res.ToString().Length + "\n\n" + res;

                    writer.Write(response);
                    Console.WriteLine($"Server: {res}");

                }
                catch
                {
                    Console.WriteLine("Client was disconnected.");
                }
            }
        }



        public CalcServer(string ip, int port)
        {
            server = new TcpListener(IPAddress.Parse(ip), port);
        }

        private int Calculate(string a, string b, string op)
        {
            int res = 0;
            int n1 = Convert.ToInt32(a);
            int n2 = Convert.ToInt32(b);
            switch (op)
            {
                case "-": res = n1 - n2; break;
                case "p": res = n1 + n2; break;
                case "*": res = n1 * n2; break;
                case "/": res = n1 / n2; break;
            }
            return res;
        }

        private Dictionary<string, string> ParseHttpQuery(string request)
        {
            Dictionary<string, string> header = new Dictionary<string, string>();

            string data = request.Split('?')[1].Split(' ')[0];
            string[] param = data.Split(new char[] { '=', '&' });
            for (int i = 0; i < param.Length; i += 2)
            {
                header.Add(param[i], param[i + 1]);
            }
            return header;
        }
    }
}
