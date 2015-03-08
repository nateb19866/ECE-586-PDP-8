using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECE486_PDP_8_Emulator.Classes;

namespace ECE486_PDP_8_Emulator.Instructions
{
   public class IszInstruction:IInstruction
    {
       private int ClockCycles = 2;
       private Constants.OpCode InstructionType = Constants.OpCode.ISZ;
 

       public InstructionResult ExecuteInstruction(InstructionItems instItems)
        {
          /* 
           * ISZ: Increment and Skip on 0
           * Mem[EA] = Mem[EA] + 1
           * If Mem[EA] = 0
           *    PC + 2 
           */


            // Increment PC as normal
            int IncrementedPcCounter = (++instItems.pcCounter);

           // Increment mem Value and mask before checking, to ensure no overflow
            instItems.MemoryValueOctal = (++instItems.MemoryValueOctal) & 0xFFF;

           //Skip next instruction if mem Value is 0
            if (instItems.MemoryValueOctal == 0)
           {
               // Increment PC to skip if Value = 0 
               IncrementedPcCounter = ++IncrementedPcCounter;
           }

           // Mask PC to ensure no overflow.
           instItems.pcCounter = IncrementedPcCounter & 0xFFF;

           return new InstructionResult()
           {
               accumulatorOctal = instItems.accumulatorOctal,
               LinkBit = instItems.LinkBit,
               MemoryAddress = instItems.MemoryAddress,
               MemoryValueOctal = instItems.MemoryValueOctal,
               InstructionRegister = instItems.InstructionRegister,
               SetMemValue = true,
               pcCounter = instItems.pcCounter,
               // Conditional Branch from skip if mem Value = 0
               BranchTaken = (instItems.MemoryValueOctal == 0),
               BranchType = Constants.BranchType.Conditional,
               OsrSwitchBits = instItems.OsrSwitchBits

           };
        }

        public int clockCycles
        {
            get { return ClockCycles; }
        }


        public Constants.OpCode instructionType
        {
            get {return InstructionType; }
        }
    }
}
