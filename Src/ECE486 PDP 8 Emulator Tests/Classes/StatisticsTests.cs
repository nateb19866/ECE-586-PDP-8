using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECE486_PDP_8_Emulator.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace ECE486_PDP_8_Emulator.Classes.Tests
{
    [TestClass()]
    public class StatisticsTests
    {
        [TestMethod()]
        public void StatisticsTest()
        {
            Statistics TestStats = new Statistics();

            Assert.AreEqual(0, TestStats.ClockCyclesExecuted);
            Assert.AreEqual(0, TestStats.InstructionsExecuted);

            Assert.AreEqual("0", TestStats.InstructionTypeExecutions["AND"]);
            Assert.AreEqual("0", TestStats.InstructionTypeExecutions["DCA"]);
            Assert.AreEqual("0", TestStats.InstructionTypeExecutions["ISZ"]);
            Assert.AreEqual("0", TestStats.InstructionTypeExecutions["IOT"]);
            Assert.AreEqual("0", TestStats.InstructionTypeExecutions["OPR"]);
            Assert.AreEqual("0", TestStats.InstructionTypeExecutions["TAD"]);
            Assert.AreEqual("0", TestStats.InstructionTypeExecutions["JMP"]);
            Assert.AreEqual("0", TestStats.InstructionTypeExecutions["JMS"]);
        }
    }
}
