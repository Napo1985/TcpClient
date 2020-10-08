using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserTcpClient
{
    class Program
    {
        static void Main(string[] args)
        {
            ClientManager client = new ClientManager("127.0.0.1", 5000);
            client.StartClient();
        }
    }
}
