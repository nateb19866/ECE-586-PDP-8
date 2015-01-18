using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECE486_PDP_8_Emulator
{
    public class MemArray
    {
        private int[] MemoryArray;
        public int MemReads = 0;
        public int MemWrites = 0;
        private string TraceFilePath;

        public MemArray( int[] memoryArray, string traceFilePath )
        {
            MemoryArray = memoryArray;
            this.TraceFilePath = traceFilePath;
        }

       public int GetValue(int address)
        {
           return 0;
        }

      public void SetValue(int address, int value)
        {

        }
    }
}
