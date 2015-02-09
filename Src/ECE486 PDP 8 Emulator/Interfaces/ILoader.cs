using ECE486_PDP_8_Emulator.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECE486_PDP_8_Emulator.Interfaces
{
    public interface ILoader
    {
        LoaderResult LoadFile(string filePath);
    }
}
