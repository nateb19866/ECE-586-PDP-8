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
            ///MemArray[AC] = MemArray[AC] + MemArray[EA];
            //Complement Link if carry out

           int FinalAccumulator = instItems.MemoryValueOctal + instItems.accumulatorOctal;
           bool FinalLinkBit = instItems.LinkBit;

           //Check to see if they're both positive, and if so, check for carry-out condition and mask the extra bit out
           /*
            * 3777 = 011 111 111 111  - In hex - 0111 1111 1111 = 0x7FF
            * 
            * */

           if (instItems.MemoryValueOctal <= 0x7FF && instItems.accumulatorOctal <= 0x7FF && FinalAccumulator > 0x7FF)
           {
               FinalAccumulator = FinalAccumulator & 0x000007FF;
           }

                //if they're both negative, check to see if there is an overflow, and if so, complement the link bit and mask out the carryout bit
           else if (instItems.MemoryValueOctal > 0x7FF && instItems.accumulatorOctal > 0x7FF && FinalAccumulator > 0x7FF)
           {
               FinalLinkBit = !FinalLinkBit;
               FinalAccumulator = FinalAccumulator & 0x00000FFF;
           }


        /* Original */
           // //Check to see if they're both positive, and if so, check for carry-out condition and mask the extra bit out
           // /*
           //  * 3777 = 011 111 111 111  - In hex - 0111 1111 1111 = 0x3FF
           //  * 
           //  * */

           //if (instItems.MemoryValueOctal <= 0x3FF && instItems.accumulatorOctal <= 0x3FF && FinalAccumulator > 0x3FF)
           // {
           //     FinalLinkBit = !FinalLinkBit;

           //     FinalAccumulator = FinalAccumulator & 0x000007FF;
           // }
             
           //     //if they're both negative, check to see if there is an overflow, and if so, complement the link bit and mask out the carryout bit
           //else if (instItems.MemoryValueOctal > 0x3FF && instItems.accumulatorOctal > 0x3FF && FinalAccumulator > 0xFFF)
           // {
           //     FinalLinkBit = !FinalLinkBit;
           //     FinalAccumulator = FinalAccumulator & 0x00000FFF;
           // }

           /* Original */

            return new InstructionResult()
            {
                accumulatorOctal = FinalAccumulator,
                LinkBit = FinalLinkBit,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                InstructionRegister = instItems.InstructionRegister,
                SetMemValue = false,
                pcCounter = ++instItems.pcCounter,
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
