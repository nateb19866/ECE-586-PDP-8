using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECE486_PDP_8_Emulator.Classes
{
    public class InstructionResult:InstructionItems
    {
       public bool SetMemValue;
       public bool NextInstructionIsIndirect;
    }
}
