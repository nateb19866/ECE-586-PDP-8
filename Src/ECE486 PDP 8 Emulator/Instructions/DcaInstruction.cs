using ECE486_PDP_8_Emulator.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECE486_PDP_8_Emulator.Instructions
{
   public class DcaInstruction:IInstruction
    {
       private int ClockCycles = 2;
       private Constants.OpCode InstructionType = Constants.OpCode.DCA;
 
       public InstructionResult ExecuteInstruction(InstructionItems instItems)
        {
            /* DAC: Place AC and assign to Value then Clear AC, PC + 1 */
            
           // Mask AC and assign to Value
            instItems.MemoryValueOctal = instItems.accumulatorOctal & 0xFFF;

            // Increment PC
            int IncrementedPcCounter = (++instItems.pcCounter);

            // Mask PC to ensure no overflow.
            instItems.pcCounter = IncrementedPcCounter & 0xFFF;


            return new InstructionResult()
            {
                accumulatorOctal = 0,
                LinkBit = instItems.LinkBit,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.accumulatorOctal,
                pcCounter = instItems.pcCounter,
                InstructionRegister = instItems.InstructionRegister,
                SetMemValue = true,
                BranchTaken = false,
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
