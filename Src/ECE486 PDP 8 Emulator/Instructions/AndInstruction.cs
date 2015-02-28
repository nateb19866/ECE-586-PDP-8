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


            int FinalWord = (TestWord1Bytes & TestWord2Bytes);

            return new InstructionResult()
            {
                //Only important part
                accumulatorOctal = FinalWord,
                //Rest is just copying from inputs
                LinkBit = instItems.LinkBit,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                MicroCodes = instItems.MicroCodes,
                BranchTaken = false,
                pcCounter = ++instItems.pcCounter,
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

        public Constants.OpCode instructionType
        {
            get { return InstructionType; }
        }
    }
}
