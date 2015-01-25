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

        private void AppendToTraceFile(int address, OpType operationType)
        {
            throw new NotImplementedException();
        }
       public int GetValue(int address, bool isInstruction)
        {
            
           AppendToTraceFile(address, isInstruction?OpType.InstructionFetch:OpType.DataRead);
            return MemoryArray[address];
        }

      public void SetValue(int address, int value)
        {
            AppendToTraceFile(address, OpType.DataWrite);
            MemoryArray[address] = value;
        }

        enum OpType {
            DataRead = 0,
            DataWrite,
            InstructionFetch
        }
    }
}
