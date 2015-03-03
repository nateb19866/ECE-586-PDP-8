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
          
           int IncrementedPcCounter = ++instItems.pcCounter;

           // Increment memory & mask for 12 bits
           int FinalValue = instItems.MemoryValueOctal;
           FinalValue = ++FinalValue;
           FinalValue = FinalValue & 0xfff;

           //Skip next instruction if memory is 0
           if (FinalValue == 0)
           {
               IncrementedPcCounter = ++IncrementedPcCounter;
           }

           //Mask to get only 12 bits PcCounter
           IncrementedPcCounter = IncrementedPcCounter & 0xFFF;


           return new InstructionResult()
           {
               accumulatorOctal = instItems.accumulatorOctal,
               LinkBit = instItems.LinkBit,
               MemoryAddress = instItems.MemoryAddress,
               MemoryValueOctal = FinalValue,
               InstructionRegister = instItems.InstructionRegister,
               SetMemValue = true,
               pcCounter = IncrementedPcCounter,
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
