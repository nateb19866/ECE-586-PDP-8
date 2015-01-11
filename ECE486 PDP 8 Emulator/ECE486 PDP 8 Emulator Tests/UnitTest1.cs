using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ECE486_PDP_8_Emulator;

namespace ECE486_PDP_8_Emulator_Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            InstructionFactory test1 = new InstructionFactory();

            int result = test1.ExampleFunction(1, 2);

            Assert.AreEqual(result,3);
        }
    }
}
