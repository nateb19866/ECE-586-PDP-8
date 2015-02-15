using ECE486_PDP_8_Emulator.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECE486_PDP_8_Emulator.Instructions
{
    public class AndInstruction:IInstruction    
    {
       
        private int ClockCycles = 2;
        private Constants.OpCode InstructionType = Constants.OpCode.AND;
       
        
        public InstructionResult ExecuteInstruction(InstructionItems instItems)
        {
            // Call Function to get EA and AC
            ///MemArray[AC] = MemArray[AC] & MemArray[EA];
            throw new NotImplementedException();

        }

       

        public int clockCycles
        {
            get
            {
                return ClockCycles;
            }
          
        }


        public Constants.OpCode instructionType
        {
            get { return InstructionType; }
        }
    }
}
