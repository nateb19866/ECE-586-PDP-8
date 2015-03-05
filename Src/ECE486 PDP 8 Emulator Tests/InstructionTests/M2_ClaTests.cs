using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ECE486_PDP_8_Emulator.Classes;
using ECE486_PDP_8_Emulator;
using ECE486_PDP_8_Emulator.Instructions;

namespace ECE486_PDP_8_Emulator_Tests.InstructionTests
{
    [TestClass]
    public class M2_ClaTests
    {
        [TestMethod]
        public void TestM2CLArgumentPassthrough()
        {

            InstructionItems TestItems = new InstructionItems()

            {
                accumulatorOctal = 0000,
                LinkBit = true,
                MemoryAddress = 0,
                MemoryValueOctal = Convert.ToInt32(7777.ToString(), 8),
                pcCounter = 1400,
                InstructionRegister = Convert.ToInt32(7600.ToString(), 8)
            };


            InstructionResult ExpectedItems = new InstructionResult()
            {
                accumulatorOctal = 0000,
                LinkBit = true,
                MemoryAddress = 0,
                MemoryValueOctal = Convert.ToInt32(7777.ToString(), 8),
                pcCounter = 1401,
                InstructionRegister = Convert.ToInt32(7600.ToString(), 8),
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
        public void TestM2CLAOperation()
        {
            //Need to initialize the instruction items for the first time


            InstructionItems TestItems = new InstructionItems()
            {
                accumulatorOctal = 0000,
                LinkBit = true,
                MemoryAddress = 0,

                MemoryValueOctal = 0000,
                pcCounter = 5649,
                InstructionRegister = Convert.ToInt32(7600.ToString(), 8)
            };


            InstructionResult ExpectedItems = new InstructionResult()
            {
                accumulatorOctal = 0000,
                LinkBit = true,
                MemoryAddress = 0,
                MemoryValueOctal = 0000,
                pcCounter = 5650,
                InstructionRegister = Convert.ToInt32(7600.ToString(), 8),
                SetMemValue = false
            };

            IInstruction TestOprInstruction = new OprInstruction();

            InstructionResult ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);

            Assert.AreEqual(0, ActualResult.accumulatorOctal);

            //Always clears Accumulator

            TestItems.accumulatorOctal = 1;
            TestItems.pcCounter = Convert.ToInt32(0000.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(0, ActualResult.accumulatorOctal);
            Assert.AreEqual(Convert.ToInt32(1.ToString(), 8), ActualResult.pcCounter);


            // Test AC clears and PC increments

            TestItems.accumulatorOctal = Convert.ToInt32(240.ToString(), 8);
            TestItems.pcCounter = Convert.ToInt32(0000.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(0, ActualResult.accumulatorOctal);
            Assert.AreEqual(Convert.ToInt32(1.ToString(), 8), ActualResult.pcCounter);

            //Test AC clears given 3 octals = 1 and PC increments

            TestItems.accumulatorOctal = Convert.ToInt32(7770.ToString(), 8);
            TestItems.pcCounter = Convert.ToInt32(0000.ToString(), 8);
            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);

            Assert.AreEqual(0, ActualResult.accumulatorOctal);
            Assert.AreEqual(Convert.ToInt32(1.ToString(), 8), ActualResult.pcCounter);

            //Test AC clears givevn all 4 octals 1 and PC increments

            TestItems.accumulatorOctal = Convert.ToInt32(7777.ToString(), 8);
            TestItems.pcCounter = Convert.ToInt32(0000.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(0, ActualResult.accumulatorOctal);
            Assert.AreEqual(Convert.ToInt32(1.ToString(), 8), ActualResult.pcCounter);
        }
    }
}
