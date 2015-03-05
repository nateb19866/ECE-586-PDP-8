using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECE486_PDP_8_Emulator.Classes;

namespace ECE486_PDP_8_Emulator.Instructions
{
    public class TadInstruction:IInstruction
    {
        private int ClockCycles = 2;
        private Constants.OpCode InstructionType = Constants.OpCode.TAD;
 

        public InstructionResult ExecuteInstruction(InstructionItems instItems)
        {
            /* 2's Complement Add
             * AC = AC + memValue
             * Complement Link if carry out
             * Carry out occurs for 2 neg's and overflow */
    
           // AC = AC + memValue
           instItems.accumulatorOctal = instItems.MemoryValueOctal + instItems.accumulatorOctal;

           //Check to see if they're both positive, and if so, check for carry-out condition and mask the extra bit out
           /*
            * 3777 = 011 111 111 111  - In hex - 0111 1111 1111 = 0x7FF
            * 
            */

           //if (instItems.MemoryValueOctal <= 0x7FF && instItems.accumulatorOctal <= 0x7FF && instItems.accumulatorOctal > 0x7FF)
           //{
           //    instItems.LinkBit= !instItems.LinkBit;
           //    instItems.accumulatorOctal = instItems.accumulatorOctal & 0x000007FF;
           //}

           if (instItems.accumulatorOctal > 0xFFF)
           {
               instItems.LinkBit= !instItems.LinkBit;
           }

           // Mask to ensure no overflow for manipulated data
           instItems.accumulatorOctal = instItems.accumulatorOctal & 0xFFF;
           instItems.pcCounter = (++instItems.pcCounter) & 0xFFF;

            return new InstructionResult()
            {
                accumulatorOctal = instItems.accumulatorOctal,
                LinkBit = instItems.LinkBit,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                InstructionRegister = instItems.InstructionRegister,
                SetMemValue = false,
                pcCounter = instItems.pcCounter,
                BranchTaken = false

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
