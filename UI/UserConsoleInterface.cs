using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserTcpClient.UI
{
    class UserConsoleInterface
    {
        bool isValidInput;
        int retVal;
        public int UIManu()
        {
            isValidInput = false;
            retVal = -1;
            while (!isValidInput)
            {
                Console.WriteLine("press:");
                Console.WriteLine("1 - register");
                Console.WriteLine("2 - send Message");
                string val = Console.ReadLine();
                retVal = CheckInput(val);
            }
            return retVal;
        }
        private int CheckInput(string val)
        {
            int x = 0;
            isValidInput = Int32.TryParse(val, out x);
            if (isValidInput)
                return x;
            else
                return 0;
        }
    }
}
