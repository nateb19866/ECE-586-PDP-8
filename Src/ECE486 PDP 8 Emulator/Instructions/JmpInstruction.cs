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
            /* JMP: Place Eff Addr to PC */
            instItems.pcCounter = instItems.MemoryAddress & 0xFFF;

            // Mask PC to ensure no overflow.
            instItems.pcCounter = instItems.pcCounter & 0xFFF;

            return new InstructionResult()
            {
                accumulatorOctal = instItems.accumulatorOctal,
                LinkBit = instItems.LinkBit,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                InstructionRegister = instItems.InstructionRegister,
                pcCounter = instItems.pcCounter,
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
