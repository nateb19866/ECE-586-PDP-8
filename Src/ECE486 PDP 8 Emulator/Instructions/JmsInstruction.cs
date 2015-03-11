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
           /* Place PC to mem Value
            * Increment Eff Addr and place into PC */

            // PC to mem Value, mask to ensure no overflow
            instItems.MemoryValueOctal = (instItems.pcCounter + 1) & 0xFFF;

           // Increment Eff Addr., mask off overflow, place into PC
            instItems.pcCounter = (instItems.MemoryAddress + 1) & 0xFFF;

            return new InstructionResult()
            {
                accumulatorOctal = instItems.accumulatorOctal,
                LinkBit = instItems.LinkBit,
                MemoryAddress = instItems.MemoryAddress,
                pcCounter = instItems.pcCounter,
                InstructionRegister = instItems.InstructionRegister,
                MemoryValueOctal = instItems.MemoryValueOctal,
                SetMemValue = true,
                BranchTaken = true,
                BranchType = Constants.BranchType.Subroutine,
                OsrSwitchBits = instItems.OsrSwitchBits
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
