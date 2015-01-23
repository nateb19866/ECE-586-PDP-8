using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECE486_PDP_8_Emulator.Classes;

namespace ECE486_PDP_8_Emulator.Instructions
{
    public class JmpInstruction:IInstruction
    {
        private int ClockCycles = 1;
        private Constants.OpCode InstructionType = Constants.OpCode.JMP;
 

        public InstructionResult ExecuteInstruction(InstructionItems instItems)
        {
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
