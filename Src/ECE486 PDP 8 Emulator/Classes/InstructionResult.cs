using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECE486_PDP_8_Emulator.Classes
{
    public class InstructionResult:InstructionItems
    {
       public  int NextInstructionAddress;
       public bool SetMemValue;
       public bool NextInstructionIsIndirect;
    }
}
