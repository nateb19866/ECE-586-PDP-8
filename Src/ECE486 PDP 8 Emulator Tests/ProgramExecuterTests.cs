using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECE486_PDP_8_Emulator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ECE486_PDP_8_Emulator.Classes;
using ECE486_PDP_8_Emulator_Tests.Properties;
namespace ECE486_PDP_8_Emulator.Tests
{
    [TestClass()]
    public class ProgramExecuterTests
    {
        [TestMethod()]
        public void ExecuteProgramTest()
        {
            Statistics Rslt = ProgramExecuter.ExecuteProgram(Resources.TestFilePath + "/add01.obj", Resources.TestFilePath, 0);

            Assert.AreEqual(5, Rslt.InstructionsExecuted);
            Assert.AreEqual(8, Rslt.ClockCyclesExecuted);
            
        }
    }
}
