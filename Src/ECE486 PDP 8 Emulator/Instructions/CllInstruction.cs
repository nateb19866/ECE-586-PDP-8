using ECE486_PDP_8_Emulator.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECE486_PDP_8_Emulator.Instructions
{
    public class CllInstruction : IInstruction
    {

        private int ClockCycles = 1;
        private Constants.Microcode InstructionType = Constants.Microcode.CLL;


        public InstructionResult ExecuteInstruction(InstructionItems instItems)
        {
            // Clear Link of uInstruction-1
            return new InstructionResult()
            {
                accumulatorOctal = instItems.accumulatorOctal,
                //Rest is just copying from inputs
                LinkBit = false,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                MicroCodes = instItems.MicroCodes,
                BranchTaken = false,    //?
                pcCounter = instItems.pcCounter++,
                SetMemValue = false     //?
            };


        }


        public int clockCycles
        {
            get
            {
                return ClockCycles;
            }

        }


        public Constants.Microcode instructionType
        {
            get { return InstructionType; }
        }
    }
}
