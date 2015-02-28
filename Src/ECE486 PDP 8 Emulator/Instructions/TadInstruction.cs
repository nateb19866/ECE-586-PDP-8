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

           int FinalAccumulator = Convert.ToInt32(instItems.MemoryValueOctal.ToString(),8) + Convert.ToInt32(instItems.accumulatorOctal.ToString(),8);
           bool FinalLinkBit = instItems.LinkBit;
            //Check to see if they're both positive, and if so, check for carry-out condition and mask the extra bit out
            if(instItems.MemoryValueOctal <= 3777 && instItems.accumulatorOctal <= 3777 && FinalAccumulator > 3777)
            {
                FinalLinkBit = !FinalLinkBit;

                FinalAccumulator = FinalAccumulator & 0x000007FF;
            }
                //if they're both negative, check to see if there is an overflow, and if so, complement the link bit and mask out the carryout bit
            else if(instItems.MemoryValueOctal > 3777 && instItems.accumulatorOctal > 3777 && FinalAccumulator >7777)
            {
                FinalLinkBit = !FinalLinkBit;
                FinalAccumulator = FinalAccumulator & 0x00000FFF;
            }

            return new InstructionResult()
            {
                accumulatorOctal = Utils.DecimalToOctal(FinalAccumulator),
                LinkBit = FinalLinkBit,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                MicroCodes = instItems.MicroCodes,
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
