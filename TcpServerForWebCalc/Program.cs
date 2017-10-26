using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpServerForWebCalc
{
    class Program
    {
        static void Main(string[] args)
        {
            CalcServer server = new CalcServer("127.0.0.1", 9050);
            server.Start();
        }
    }
}
