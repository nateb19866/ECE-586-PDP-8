using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECE486_PDP_8_Emulator
{
    public static class Constants
    {
        public enum OpCode
        {
            AND =0,
            TAD,
            ISZ,
            DCA,
            JMS,
            JMP,
            IOT,
            OPR
        }

       public enum OpType
        {
            DataRead = 0,
            DataWrite,
            InstructionFetch
        }

       public const int HLT = 7402;

    }
}
