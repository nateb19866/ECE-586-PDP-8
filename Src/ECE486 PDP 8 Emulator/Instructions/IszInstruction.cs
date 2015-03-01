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
            
            //MemArray[AC] = MemArray[AC] + MemArray[EA];
            //if(MemArray[EA] ==0)
            //MemArray[PC] = MemArray[PC] +1;
           int FinalValue = 0;
           int IncrementedPcCounter = instItems.pcCounter;

           if (instItems.MemoryValueOctal == Convert.ToInt32(7777.ToString(), 8))
           {
               FinalValue = 0;
               instItems.pcCounter++;
           }

           else
               FinalValue = ++instItems.MemoryValueOctal;


           return new InstructionResult()
           {
               accumulatorOctal = instItems.accumulatorOctal,
               LinkBit = instItems.LinkBit,
               MemoryAddress = instItems.MemoryAddress,
               MemoryValueOctal = FinalValue,
               InstructionRegister = instItems.InstructionRegister,
               SetMemValue = true,
               pcCounter = ++IncrementedPcCounter,
               BranchTaken = FinalValue == 0,
               BranchType = Constants.BranchType.Conditional

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
