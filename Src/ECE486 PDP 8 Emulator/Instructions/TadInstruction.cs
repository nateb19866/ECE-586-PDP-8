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
            // Call Function to get EA and AC
            ///MemArray[AC] = MemArray[AC] + MemArray[EA];
            //? Complement Link if carry out?
            throw new NotImplementedException();
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
