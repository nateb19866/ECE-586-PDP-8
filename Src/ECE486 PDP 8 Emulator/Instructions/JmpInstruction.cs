using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECE486_PDP_8_Emulator.Classes;

namespace ECE486_PDP_8_Emulator.Instructions
{
    public class JmpInstruction:IInstruction
    {
        private int ClockCycles = 1;
        private Constants.OpCode InstructionType = Constants.OpCode.JMP;
 

        public InstructionResult ExecuteInstruction(InstructionItems instItems)
        {
            // MemArray[PC] = MemArray[EA];
            return new InstructionResult()
            {

                accumulatorOctal = instItems.accumulatorOctal,
                LinkBit = instItems.LinkBit,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                MicroCodes = instItems.MicroCodes,
                pcCounter = Convert.ToInt32(instItems.MemoryValueOctal.ToString(), 8),
                SetMemValue = false,
                BranchTaken = true,
                BranchType = Constants.BranchType.Unconditional
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
