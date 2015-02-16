using ECE486_PDP_8_Emulator.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECE486_PDP_8_Emulator.Instructions
{
    public class CmaInstruction : IInstruction
    {

        private int ClockCycles = 1;
        private Constants.Microcode InstructionType = Constants.Microcode.CMA;


        public InstructionResult ExecuteInstruction(InstructionItems instItems)
        {
            // complement every bit of AC
            return new InstructionResult()
            {
                // complement every bit of AC
                accumulatorOctal = ~(instItems.accumulatorOctal),
                //If used in conjunction with CLA, set all 12 bits of AC to 1 ( = 7777 )

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
