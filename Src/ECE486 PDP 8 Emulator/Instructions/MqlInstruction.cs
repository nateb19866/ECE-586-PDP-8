using ECE486_PDP_8_Emulator.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECE486_PDP_8_Emulator.Instructions
{
    public class MqlInstruction : IInstruction
    {

        private int ClockCycles = 1;
        private Constants.Microcode InstructionType = Constants.Microcode.MQL;


        public InstructionResult ExecuteInstruction(InstructionItems instItems)
        {
            // Need to implement MQL loads the contents of the MQ register from 
            // AC and then clears AC. 

            return new InstructionResult()
            {
                accumulatorOctal = instItems.accumulatorOctal,
                LinkBit = instItems.LinkBit,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                MicroCodes = instItems.MicroCodes,
                BranchTaken = false,
                pcCounter = instItems.pcCounter++,
                SetMemValue = false
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
