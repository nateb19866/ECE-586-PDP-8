using ECE486_PDP_8_Emulator.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECE486_PDP_8_Emulator.Instructions
{
    public class M2_ClaInstruction : IInstruction
    {

        private int ClockCycles = 1;
        private Constants.Microcode InstructionType = Constants.Microcode.M2_CLA;


        public InstructionResult ExecuteInstruction(InstructionItems instItems)
        {
            // Clear AC of uInstruction-2
            return new InstructionResult()
            {
                //Only important part
                accumulatorOctal = Utils.DecimalToOctal(0),
                //Rest is just copying from inputs
                LinkBit = instItems.LinkBit,
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
