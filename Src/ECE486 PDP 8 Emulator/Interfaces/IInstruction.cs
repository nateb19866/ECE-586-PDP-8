using ECE486_PDP_8_Emulator.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECE486_PDP_8_Emulator
{
    public interface IInstruction
    {
        int value;
        InstructionResult ExecuteInstruction(InstructionItems instItems);
        int clockCycles { get; }
        Constants.OpCode instructionType { get; }
     
    }
}
