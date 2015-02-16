using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECE486_PDP_8_Emulator.Classes;

namespace ECE486_PDP_8_Emulator.Instructions
{
    public class IotInstruction:IInstruction
    {

        private int ClockCycles = 2;
        private Constants.OpCode InstructionType = Constants.OpCode.IOT;
 

        public InstructionResult ExecuteInstruction(InstructionItems instItems)
        {
            return new InstructionResult()
            {
                accumulatorOctal = instItems.accumulatorOctal,
                LinkBit = instItems.LinkBit,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                MicroCodes = instItems.MicroCodes,
                SetMemValue = false,
                pcCounter = instItems.pcCounter++,
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
