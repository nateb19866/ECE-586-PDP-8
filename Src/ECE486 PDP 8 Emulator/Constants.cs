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
            //MRI = Xnnn
            AND =0,
            TAD,
            ISZ,
            DCA,
            JMS,
            JMP,
            IOT,
            OPR,
        }
        public enum Microcode
        {
            /*Microinstructions 1 Opcode 7
            |1|1|1|0|CLA|CLL|CMA|CML|RAR|RAL|0/1|IAC| */
            NOP = 7000,
            M1_CLA = 7200,
            CLL = 7100,
            CMA = 7040,
            CML = 7020,
            IAC = 7001,
            RAR = 7010,
            RTR = 7012,
            RAL = 7004,
            RTL = 7006,
            /* Microinstructions 2 Opcode 7
            |1|1|1|1|CLA|SMA|SZA|SNL|0/1|OSR|HLT|0| */
            SMA = 7500,
            SZA = 7440,
            SNL = 7420,
            SPA = 7510,
            SNA = 7450,
            SZL = 7430,
            SKP = 7410,
            M2_CLA = 7600,
            OSR = 7404,
            HLT = 7402,
            /*Microinstructions 3 Opcode 7
             |1|1|1|1|CLA|MQA| |MQL| | | |1| */
            M3_CLA = 7601,
            MQL = 7421,
            MQA = 7501,
            SWP = 7521,
            CAM = 7621,
        }
        public enum IOCode
        {
            // Keyboard 
            KCF = 6030,
            KSF = 6031,
            KCC = 6032,
            KRS = 6034,
            KRB = 6036,

            // Printer
            TFL = 6040,
            TSF = 6041,
            TCF = 6042,
            TPC = 6044,
            TLS = 6046,

            // Interrupt System
            SKON = 6000,
            ION = 6001,
            IOF = 6002,
        }
        
       public enum OpType
        {
            DataRead = 0,
            DataWrite,
            InstructionFetch
        }

    }
}
