using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECE486_PDP_8_Emulator.Classes;

namespace ECE486_PDP_8_Emulator.Instructions
{
   public class JmsInstruction:IInstruction
    {
       private int ClockCycles = 2;
       private Constants.OpCode InstructionType = Constants.OpCode.JMS;
 

       public InstructionResult ExecuteInstruction(InstructionItems instItems)
        {
            // Call Function to get EA and AC
            ///MemArray[EA] = MemArray[PC];
            // MemArray[PC] = MemArray[EA]+1;
            return new InstructionResult()
            {

                accumulatorOctal = instItems.accumulatorOctal,
                LinkBit = instItems.LinkBit,
                MemoryAddress = Convert.ToInt32(instItems.MemoryValueOctal.ToString(), 8),
                pcCounter = Convert.ToInt32(instItems.MemoryValueOctal.ToString(), 8) + 1,
                MicroCodes = instItems.MicroCodes,
                MemoryValueOctal = instItems.pcCounter,
                SetMemValue = true,
                BranchTaken = true,
                BranchType = Constants.BranchType.Subroutine
            };


        }

        public int clockCycles
        {
            get { return ClockCycles; }
        }


        public Constants.OpCode instructionType
        {
            get { return InstructionType; }
        }
    }
}
