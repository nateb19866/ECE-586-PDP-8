using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ECE486_PDP_8_Emulator;
using ECE486_PDP_8_Emulator.Classes;
using ECE486_PDP_8_Emulator.Interfaces;
using ECE486_PDP_8_Emulator_Tests.Properties;

namespace ECE486_PDP_8_Emulator_Tests
{
    [TestClass]
    public class LoaderUnitTests
    {
        [TestMethod]
        public void TestObjLoading()
        {

            ILoader FileLoader = new ObjLoader();
            LoaderResult TestMemArray = FileLoader.LoadFile(Resources.TestFilePath + "/add01.obj");

            //Instruction section - 200 octal = 0x80 Hex
            Assert.AreEqual(Convert.ToInt32(7300.ToString(),8),TestMemArray.FinishedArray.GetValue(0x80,false,false));
            Assert.AreEqual(Convert.ToInt32(1250.ToString(), 8), TestMemArray.FinishedArray.GetValue(0x81, false, false));
            Assert.AreEqual(Convert.ToInt32(1251.ToString(), 8), TestMemArray.FinishedArray.GetValue(0x82, false, false));
            Assert.AreEqual(Convert.ToInt32(3252.ToString(), 8), TestMemArray.FinishedArray.GetValue(0x83, false, false));
            Assert.AreEqual(Convert.ToInt32(7402.ToString(), 8), TestMemArray.FinishedArray.GetValue(0x84, false, false));
            Assert.AreEqual(Convert.ToInt32(5200.ToString(), 8), TestMemArray.FinishedArray.GetValue(0x85, false, false));

            //Data section - 0250 octal (000 010 101 000) = 0x0A8(0000 1010 1000) hex
            Assert.AreEqual(Convert.ToInt32(2.ToString(), 8), TestMemArray.FinishedArray.GetValue(0xA8, false, false));
            Assert.AreEqual(Convert.ToInt32(3.ToString(), 8), TestMemArray.FinishedArray.GetValue(0xA9, false, false));
            Assert.AreEqual(Convert.ToInt32(0.ToString(), 8), TestMemArray.FinishedArray.GetValue(0xAA, false, false));

        }
    }
}
