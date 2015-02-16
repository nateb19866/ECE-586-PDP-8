using ECE486_PDP_8_Emulator.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECE486_PDP_8_Emulator.Instructions
{
    public class CmlInstruction : IInstruction
    {

        private int ClockCycles = 1;
        private Constants.Microcode InstructionType = Constants.Microcode.CML;


        public InstructionResult ExecuteInstruction(InstructionItems instItems)
        {
            // complement every bit of AC
            return new InstructionResult()
            {
           
                accumulatorOctal = instItems.accumulatorOctal,
                LinkBit = !(instItems.LinkBit),
                // If used in conjunction with CLL, this sets the link bit to one. 
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
