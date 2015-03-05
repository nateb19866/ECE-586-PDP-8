using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using ECE486_PDP_8_Emulator.Classes;
using ECE486_PDP_8_Emulator;
using ECE486_PDP_8_Emulator.Instructions;

namespace ECE486_PDP_8_Emulator_Tests.InstructionTests
{
    [TestClass]
    public class RTRTests
    {
        [TestMethod]
        public void TestRTRArgumentPassthrough()
        {
            InstructionItems TestItems = new InstructionItems()
            {
                accumulatorOctal = 0000,
                LinkBit = true,
                MemoryAddress = 0,
                MemoryValueOctal = Convert.ToInt32(7777.ToString(), 8),
                pcCounter = 1500,
                InstructionRegister = Convert.ToInt32(7012.ToString(), 8)
            };


            InstructionResult ExpectedItems = new InstructionResult()
            {
                accumulatorOctal = Convert.ToInt32(2000.ToString(), 8),
                LinkBit = true,
                MemoryAddress = 0,
                MemoryValueOctal = Convert.ToInt32(7777.ToString(), 8),
                pcCounter = 1503,
                InstructionRegister = Convert.ToInt32(7012.ToString(), 8),
                SetMemValue = false
            };

            IInstruction TestOprInstruction = new OprInstruction();

            InstructionResult ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);


            Assert.AreEqual(ExpectedItems.accumulatorOctal, ActualResult.accumulatorOctal);
            Assert.AreEqual(ExpectedItems.LinkBit, ActualResult.LinkBit);
            Assert.AreEqual(ExpectedItems.MemoryAddress, ActualResult.MemoryAddress);
            Assert.AreEqual(ExpectedItems.MemoryValueOctal, ActualResult.MemoryValueOctal);
            Assert.AreEqual(ExpectedItems.pcCounter, ActualResult.pcCounter);
            Assert.AreEqual(ExpectedItems.InstructionRegister, ActualResult.InstructionRegister);
            Assert.AreEqual(ExpectedItems.SetMemValue, ActualResult.SetMemValue);
        }


        [TestMethod]
        public void TestRTROperation()
        {

            InstructionItems TestItems = new InstructionItems()
            {
                accumulatorOctal = 0000,
                LinkBit = false,
                MemoryAddress = 0,

                MemoryValueOctal = 0000,
                pcCounter = 1500,
                InstructionRegister = Convert.ToInt32(7012.ToString(), 8)
            };


            InstructionResult ExpectedItems = new InstructionResult()
            {
                accumulatorOctal = 0000,
                LinkBit = false,
                MemoryAddress = 0,
                MemoryValueOctal = 0000,
                pcCounter = 1503,
                InstructionRegister = Convert.ToInt32(7012.ToString(), 8),
                SetMemValue = false

            };

            IInstruction TestOprInstruction = new OprInstruction();

            InstructionResult ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);

            Assert.AreEqual(0, ActualResult.accumulatorOctal);

            // Test linke false with 0
            TestItems.accumulatorOctal = Convert.ToInt32(0.ToString(), 8);
            TestItems.LinkBit = false;
            TestItems.pcCounter = Convert.ToInt32(0.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(0.ToString(), 8), ActualResult.accumulatorOctal);
            Assert.AreEqual(false, ActualResult.LinkBit);
            Assert.AreEqual(Convert.ToInt32(1.ToString(), 8), ActualResult.pcCounter);

            // Test link true with 0
            TestItems.accumulatorOctal = Convert.ToInt32(0.ToString(), 8);
            TestItems.LinkBit = true;
            TestItems.pcCounter = Convert.ToInt32(7777.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(2000.ToString(), 8), ActualResult.accumulatorOctal);
            Assert.AreEqual(false, ActualResult.LinkBit);
            Assert.AreEqual(Convert.ToInt32(0.ToString(), 8), ActualResult.pcCounter);

            // Test link false with all 1's
            TestItems.accumulatorOctal = Convert.ToInt32(7777.ToString(), 8);
            TestItems.LinkBit = false;
            TestItems.pcCounter = Convert.ToInt32(7070.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(5777.ToString(), 8), ActualResult.accumulatorOctal);
            Assert.AreEqual(true, ActualResult.LinkBit);
            Assert.AreEqual(Convert.ToInt32(7071.ToString(), 8), ActualResult.pcCounter);


            // Test link true with all 1's
            TestItems.accumulatorOctal = Convert.ToInt32(7777.ToString(), 8);
            TestItems.LinkBit = true;
            TestItems.pcCounter = Convert.ToInt32(2.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(7777.ToString(), 8), ActualResult.accumulatorOctal);
            Assert.AreEqual(true, ActualResult.LinkBit);
            Assert.AreEqual(Convert.ToInt32(3.ToString(), 8), ActualResult.pcCounter);


            // Test link true with rotating 1's starting with 0
            TestItems.accumulatorOctal = Convert.ToInt32(2525.ToString(), 8);
            TestItems.LinkBit = true;
            TestItems.pcCounter = Convert.ToInt32(2525.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(6525.ToString(), 8), ActualResult.accumulatorOctal);
            Assert.AreEqual(false, ActualResult.LinkBit);
            Assert.AreEqual(Convert.ToInt32(2526.ToString(), 8), ActualResult.pcCounter);


            // Test link false with rotating 1's starting with 1
            TestItems.accumulatorOctal = Convert.ToInt32(2525.ToString(), 8);
            TestItems.LinkBit = false;
            TestItems.pcCounter = Convert.ToInt32(4000.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(4525.ToString(), 8), ActualResult.accumulatorOctal);
            Assert.AreEqual(false, ActualResult.LinkBit);
            Assert.AreEqual(Convert.ToInt32(4001.ToString(), 8), ActualResult.pcCounter);


            // Test link true with end bits 1
            TestItems.accumulatorOctal = Convert.ToInt32(4001.ToString(), 8);
            TestItems.LinkBit = true;
            TestItems.pcCounter = Convert.ToInt32(0101.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(7000.ToString(), 8), ActualResult.accumulatorOctal);
            Assert.AreEqual(false, ActualResult.LinkBit);
            Assert.AreEqual(Convert.ToInt32(0102.ToString(), 8), ActualResult.pcCounter);


            // Test link false with end bits 1
            TestItems.accumulatorOctal = Convert.ToInt32(4001.ToString(), 8);
            TestItems.LinkBit = false;
            TestItems.pcCounter = Convert.ToInt32(1111.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(5000.ToString(), 8), ActualResult.accumulatorOctal);
            Assert.AreEqual(false, ActualResult.LinkBit);
            Assert.AreEqual(Convert.ToInt32(1112.ToString(), 8), ActualResult.pcCounter);


            // Test link true with middle octals 1's
            TestItems.accumulatorOctal = Convert.ToInt32(770.ToString(), 8);
            TestItems.LinkBit = true;
            TestItems.pcCounter = Convert.ToInt32(1010.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(2176.ToString(), 8), ActualResult.accumulatorOctal);
            Assert.AreEqual(false, ActualResult.LinkBit);
            Assert.AreEqual(Convert.ToInt32(1011.ToString(), 8), ActualResult.pcCounter);


            // Test link false with middle octals 1's
            TestItems.accumulatorOctal = Convert.ToInt32(770.ToString(), 8);
            TestItems.LinkBit = false;
            TestItems.pcCounter = Convert.ToInt32(1010.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(0176.ToString(), 8), ActualResult.accumulatorOctal);
            Assert.AreEqual(false, ActualResult.LinkBit);
            Assert.AreEqual(Convert.ToInt32(1011.ToString(), 8), ActualResult.pcCounter);

        }
    }
}
