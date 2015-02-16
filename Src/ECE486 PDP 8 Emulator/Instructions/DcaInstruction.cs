using ECE486_PDP_8_Emulator.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECE486_PDP_8_Emulator.Instructions
{
   public class DcaInstruction:IInstruction
    {
       private int ClockCycles = 2;
       private Constants.OpCode InstructionType = Constants.OpCode.DCA;
 
       
       public InstructionResult ExecuteInstruction(InstructionItems instItems)
        {
            // Call Function to get EA and AC
            //MemArray[EA] = MemArray[AC];
            //MemArray[AC] = 0;
            return new InstructionResult()
            {
                accumulatorOctal = 0,
                LinkBit = instItems.LinkBit,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.accumulatorOctal,
                pcCounter = instItems.pcCounter++,
                MicroCodes = instItems.MicroCodes,
                SetMemValue = true,
                BranchTaken = false

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
