using ECE486_PDP_8_Emulator.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECE486_PDP_8_Emulator.Instructions
{
    public class MqaInstruction : IInstruction
    {

        private int ClockCycles = 1;
        private Constants.Microcode InstructionType = Constants.Microcode.MQA;


        public InstructionResult ExecuteInstruction(InstructionItems instItems)
        {
            // Need to implement MQA ors the contents of the MQ register into AC

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
