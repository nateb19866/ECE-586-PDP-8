using ECE486_PDP_8_Emulator.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECE486_PDP_8_Emulator.Instructions
{
    public class AndInstruction:IInstruction    
    {
       
        private int ClockCycles = 2;
        private Constants.OpCode InstructionType = Constants.OpCode.AND;
       
        
        public InstructionResult ExecuteInstruction(InstructionItems instItems)
        {

            //Converting to byte array to make things easier.
            int TestWord1Bytes = instItems.accumulatorOctal;
            int TestWord2Bytes = instItems.MemoryValueOctal;

            // AND AC and Value.
            int FinalWord = (TestWord1Bytes & TestWord2Bytes) & 0xFFF;

            // Mask AC to ensure no overflow.
            instItems.accumulatorOctal = FinalWord;

            // Increment PC to next instruction and mask to ensure no overflow.
            int IncrementedPcCounter = (++instItems.pcCounter);

            // Mask PC to ensure no overflow.
            instItems.pcCounter = IncrementedPcCounter & 0xFFF;

            return new InstructionResult()
            {
                // Only important part
                accumulatorOctal = instItems.accumulatorOctal,
                // Rest is just copying from inputs
                LinkBit = instItems.LinkBit,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                InstructionRegister = instItems.InstructionRegister,
                BranchTaken = false,
                pcCounter = instItems.pcCounter,
                SetMemValue = false,
                OsrSwitchBits =instItems.OsrSwitchBits
            };


        }

        public int clockCycles
        {
            get
            {
                return ClockCycles;
            }
          
        }

        public Constants.OpCode instructionType
        {
            get { return InstructionType; }
        }
    }
}
