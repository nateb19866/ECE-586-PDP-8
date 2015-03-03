using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECE486_PDP_8_Emulator.Classes;

namespace ECE486_PDP_8_Emulator.Instructions
{
   public class JmsInstruction:IInstruction
    {
       private int ClockCycles = 2;
       private Constants.OpCode InstructionType = Constants.OpCode.JMS;

       
       public InstructionResult ExecuteInstruction(InstructionItems instItems)
        {
            // Call Function to get EA and AC
            ///MemArray[EA] = MemArray[PC];
            // MemArray[PC] = EA+1;
           
            int TestWord2Bytes = instItems.pcCounter;
            int pCupdate = (instItems.MemoryAddress + 1) & 0xFFF;


            return new InstructionResult()
            {

                accumulatorOctal = instItems.accumulatorOctal,
                LinkBit = instItems.LinkBit,
                MemoryAddress = instItems.MemoryAddress,
                pcCounter = pCupdate,
                InstructionRegister = instItems.InstructionRegister,
                MemoryValueOctal = TestWord2Bytes,
                SetMemValue = true,
                BranchTaken = true,
                BranchType = Constants.BranchType.Subroutine
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
