using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECE486_PDP_8_Emulator
{
    interface IInstruction
    {
        
        int ExecuteInstruction(int memArray);
        int MemArray { get; set; }
        int clockCycle { get; set; }
    }
}
