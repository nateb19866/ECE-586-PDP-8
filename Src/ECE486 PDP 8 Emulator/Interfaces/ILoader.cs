using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECE486_PDP_8_Emulator.Interfaces
{
    public static interface ILoader
    {
        MemArray LoadFile(string path);
    }
}
