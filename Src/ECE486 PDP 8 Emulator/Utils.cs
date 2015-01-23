using ECE486_PDP_8_Emulator.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECE486_PDP_8_Emulator
{
    public static class Utils
    {
        public static int DecodeDataAddress(int octalAddress)
        {
            throw new NotImplementedException();
        }

        public static Operation DecodeOperationAddress(int address, MemArray memoryArray, int curPage)
        {



            Operation FinalOperation = new Operation();

            FinalOperation.FinalMemAddress = 0;
            
            throw new NotImplementedException();
        }

        public static int GetPage(int address)
        {
            throw new NotImplementedException();
        }
    }
}
