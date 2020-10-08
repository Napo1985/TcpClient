using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
using UserTcpClient.UI;


namespace UserTcpClient
{
    class ClientManager
    {
        #region ctor
        public ClientManager(string ip, int port)
        {
            m_ip = ip;
            m_port = port;
            m_tcpClient = new TcpClient(ip, port);
            
        }
        #endregion

        #region functions
        public void StartClient()
        {
            m_isConnected = true;
            m_stream = m_tcpClient.GetStream();
            bool registerVar = false; 
   
            while (!registerVar)
            {
                Console.WriteLine("enter Your name");
                registerVar = Register(Console.ReadLine());
            }


            Thread listener = new Thread(WaitForMsg);
            listener.Start();

            UserConsoleInterface manu = new UserConsoleInterface();
            while (m_isConnected)
            {

                // NOT NEED FOR CLIENT UI 
                //int retval = manu.UIManu();
                //switch (retval)
                //{
                //    case 1:
                //        Console.WriteLine("Case 1");
                //        break;
                //    case 2:
                //        Console.WriteLine("Case 2");
                //        break;
                //    default:
                //        Console.WriteLine("Manu item not found");
                //        break;
                //}

                string fullMsg = Console.ReadLine();
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(fullMsg);
                m_stream.Write(data, 0, data.Length);
            }
        }
        private void WaitForMsg ()
        {
            while (m_isConnected)
            {
                Int32 bytes = m_stream.Read(m_dataRecived, 0, m_dataRecived.Length);
                string responseData = System.Text.Encoding.ASCII.GetString(m_dataRecived, 0, bytes);
                Console.WriteLine("Received: {0}", responseData);
                Array.Clear(m_dataRecived, 0, m_dataRecived.Length);
            }
        }
        public bool Register(string name)
        {
            string prefix = "UR-";
            string fullMsg = prefix+name;
            Byte[] data = System.Text.Encoding.ASCII.GetBytes(fullMsg);
            m_stream.Write(data, 0, data.Length);

            // TODO out to function
            Int32 bytes = m_stream.Read(m_dataRecived, 0, m_dataRecived.Length);
            string responseData = System.Text.Encoding.ASCII.GetString(m_dataRecived, 0, bytes);
            if (responseData.Equals("UR-success"))
            {
                Console.WriteLine("Connected");
                Array.Clear(m_dataRecived, 0, m_dataRecived.Length);
                return true;
            }
            else
            {
                Console.WriteLine("Not connected");
                Array.Clear(m_dataRecived, 0, m_dataRecived.Length);
                return false;
            }

        }

        #endregion

        #region members
        string m_ip;
        int m_port;
        TcpClient m_tcpClient = null;
        Byte[] m_dataRecived = new Byte[256];
        NetworkStream m_stream = null;
      
        bool m_isConnected = false;
        #endregion
    }
}
