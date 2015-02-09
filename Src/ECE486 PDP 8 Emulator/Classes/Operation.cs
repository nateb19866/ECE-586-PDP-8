﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECE486_PDP_8_Emulator.Classes
{
    public class Operation
    {
       public IInstruction Instruction;
        public int FinalMemAddress;
        public int ExtraClockCyles;
        public bool IsIndirect;
    }
}
