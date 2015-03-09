using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ECE486_PDP_8_Emulator;
using ECE486_PDP_8_Emulator.Classes;
using ECE486_PDP_8_Emulator.Interfaces;
using ECE486_PDP_8_Emulator_Tests.Properties;
using System.IO;

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

        [TestMethod]
        public void Test2ndObjLoading()
        {

            ILoader FileLoader = new ObjLoader();
            LoaderResult TestMemArray = FileLoader.LoadFile(Resources.TestFilePath + "/intSum.obj");

            //Instruction section - 200 octal = 0x80 Hex
            Assert.AreEqual(Convert.ToInt32(7200.ToString(), 8), TestMemArray.FinishedArray.GetValue(0x80, false, false));
            Assert.AreEqual(Convert.ToInt32(3250.ToString(), 8), TestMemArray.FinishedArray.GetValue(0x81, false, false));
            Assert.AreEqual(Convert.ToInt32(1252.ToString(), 8), TestMemArray.FinishedArray.GetValue(0x82, false, false));
            Assert.AreEqual(Convert.ToInt32(3251.ToString(), 8), TestMemArray.FinishedArray.GetValue(0x83, false, false));
            Assert.AreEqual(Convert.ToInt32(1250.ToString(), 8), TestMemArray.FinishedArray.GetValue(0x84, false, false));
            Assert.AreEqual(Convert.ToInt32(1251.ToString(), 8), TestMemArray.FinishedArray.GetValue(0x85, false, false));
           
            Assert.AreEqual(Convert.ToInt32(3250.ToString(), 8), TestMemArray.FinishedArray.GetValue(0x86, false, false));
            Assert.AreEqual(Convert.ToInt32(7240.ToString(), 8), TestMemArray.FinishedArray.GetValue(0x87, false, false));
            //test for octal increment from 207 to 210
            Assert.AreEqual(Convert.ToInt32(1251.ToString(), 8), TestMemArray.FinishedArray.GetValue(0x88, false, false));
            Assert.AreEqual(Convert.ToInt32(7440.ToString(), 8), TestMemArray.FinishedArray.GetValue(0x89, false, false));
            //test for octal increment from 211 to 212
            Assert.AreEqual(Convert.ToInt32(5203.ToString(), 8), TestMemArray.FinishedArray.GetValue(0x8A, false, false));
            Assert.AreEqual(Convert.ToInt32(7402.ToString(), 8), TestMemArray.FinishedArray.GetValue(0x8B, false, false));


            //Data section - 0250 octal (000 010 101 000) = 0x0A8(0000 1010 1000) hex
            Assert.AreEqual(Convert.ToInt32(2.ToString(), 8), TestMemArray.FinishedArray.GetValue(0xA8, false, false));
            Assert.AreEqual(Convert.ToInt32(3.ToString(), 8), TestMemArray.FinishedArray.GetValue(0xA9, false, false));
            Assert.AreEqual(Convert.ToInt32(0.ToString(), 8), TestMemArray.FinishedArray.GetValue(0xAA, false, false));

        }

        [TestMethod]
        public void TestOutofOrderObjLoading()
        {

            ILoader FileLoader = new ObjLoader();
            LoaderResult TestMemArray = FileLoader.LoadFile(Resources.TestFilePath + "/OutofOrderAddressing.obj");

            //Instruction section - 200 octal = 0x80 Hex
            Assert.AreEqual(Convert.ToInt32(7200.ToString(), 8), TestMemArray.FinishedArray.GetValue(0x80, false, false));
            Assert.AreEqual(Convert.ToInt32(3250.ToString(), 8), TestMemArray.FinishedArray.GetValue(0x81, false, false));
            Assert.AreEqual(Convert.ToInt32(1252.ToString(), 8), TestMemArray.FinishedArray.GetValue(0x82, false, false));
            Assert.AreEqual(Convert.ToInt32(3251.ToString(), 8), TestMemArray.FinishedArray.GetValue(0x83, false, false));
            Assert.AreEqual(Convert.ToInt32(1250.ToString(), 8), TestMemArray.FinishedArray.GetValue(0x84, false, false));

            //250 octal = 0x80 Hex
            Assert.AreEqual(Convert.ToInt32(1251.ToString(), 8), TestMemArray.FinishedArray.GetValue(0xA8, false, false));
            Assert.AreEqual(Convert.ToInt32(3250.ToString(), 8), TestMemArray.FinishedArray.GetValue(0xA9, false, false));
            Assert.AreEqual(Convert.ToInt32(7240.ToString(), 8), TestMemArray.FinishedArray.GetValue(0xAA, false, false));
            Assert.AreEqual(Convert.ToInt32(1251.ToString(), 8), TestMemArray.FinishedArray.GetValue(0xAB, false, false));

            // 230 octal = 0x98
            Assert.AreEqual(Convert.ToInt32(7440.ToString(), 8), TestMemArray.FinishedArray.GetValue(0x98, false, false));
            Assert.AreEqual(Convert.ToInt32(5203.ToString(), 8), TestMemArray.FinishedArray.GetValue(0x99, false, false));
            Assert.AreEqual(Convert.ToInt32(7402.ToString(), 8), TestMemArray.FinishedArray.GetValue(0x9A, false, false));

        }




        [TestMethod]
        public void TestBlankStartAddress()
        {
            ILoader FileLoader = new ObjLoader();
            LoaderResult TestMemArray = FileLoader.LoadFile(Resources.TestFilePath + "/TestBlankStartMemAddr.obj");
            //Instruction section - 200 octal = 0x80 Hex
            Assert.AreEqual(Convert.ToInt32(7300.ToString(), 8), TestMemArray.FinishedArray.GetValue(0x80, false, false));
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


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestBlankFileException()
        {
            ILoader FileLoader = new ObjLoader();
            LoaderResult TestMemArray = FileLoader.LoadFile(Resources.TestFilePath + "/TestBlankFile.obj");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void TestOddRowException()
        {
            ILoader FileLoader = new ObjLoader();
            LoaderResult TestMemArray = FileLoader.LoadFile(Resources.TestFilePath + "/TestOddRowException.obj");
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void TestOutOfRangeException()
        {
            ILoader FileLoader = new ObjLoader();
            LoaderResult TestMemArray = FileLoader.LoadFile(Resources.TestFilePath + "/TestOutOfRange.obj");
        }
    }
}
