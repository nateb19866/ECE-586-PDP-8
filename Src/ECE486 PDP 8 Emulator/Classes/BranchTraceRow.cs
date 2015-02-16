using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECE486_PDP_8_Emulator.Classes
{
   public class BranchTraceRow
    {
       public int ProgramCounter;
       public Constants.OpCode BranchType;
       public int MemoryAddress;
       public bool branchTaken;
    }
}
