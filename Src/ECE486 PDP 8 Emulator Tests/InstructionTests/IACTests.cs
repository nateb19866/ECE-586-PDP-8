using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using ECE486_PDP_8_Emulator.Classes;
using ECE486_PDP_8_Emulator;
using ECE486_PDP_8_Emulator.Instructions;

namespace ECE486_PDP_8_Emulator_Tests.InstructionTests
{
    [TestClass]
    public class IACTests
    {
        [TestMethod]
        public void TestIACArgumentPassthrough()
        {
            InstructionItems TestItems = new InstructionItems()
            {
                accumulatorOctal = 0000,
                LinkBit = true,
                MemoryAddress = 0,
                MemoryValueOctal = Convert.ToInt32(7777.ToString(), 8),
                pcCounter = 1500,
                InstructionRegister = Convert.ToInt32(7001.ToString(), 8)


            };


            InstructionResult ExpectedItems = new InstructionResult()
            {
                accumulatorOctal = Convert.ToInt32(0001.ToString(), 8),
                LinkBit = true,
                MemoryAddress = 0,
                MemoryValueOctal = Convert.ToInt32(7777.ToString(), 8),
                pcCounter = 1501,
                InstructionRegister = Convert.ToInt32(7001.ToString(), 8),
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
        public void TestIACOperation()
        {

            InstructionItems TestItems = new InstructionItems()
            {
                accumulatorOctal = 0000,
                LinkBit = true,
                MemoryAddress = 0,

                MemoryValueOctal = 0000,
                pcCounter = 5649,
                InstructionRegister = Convert.ToInt32(7001.ToString(), 8)
            };


            InstructionResult ExpectedItems = new InstructionResult()
            {
                accumulatorOctal = 0001,
                LinkBit = true,
                MemoryAddress = 0,
                MemoryValueOctal = 0000,
                pcCounter = 5650,
                InstructionRegister = Convert.ToInt32(7001.ToString(), 8),
                SetMemValue = false

            };

            IInstruction TestOprInstruction = new OprInstruction();

            InstructionResult ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);

            Assert.AreEqual(Convert.ToInt32(1.ToString(), 8), ActualResult.accumulatorOctal);

            /* 
             * All Test cases test AC and Link incremented, AC and link assigned according to result. 
             * All tests also confirm PC increments by 1 
             */

            // Test AC all 1s with link true
            TestItems.accumulatorOctal = Convert.ToInt32(7777.ToString(), 8);
            TestItems.LinkBit = true; 
            TestItems.pcCounter = Convert.ToInt32(0.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(0000.ToString(), 8), ActualResult.accumulatorOctal);
            Assert.AreEqual(false, ActualResult.LinkBit);
            Assert.AreEqual(Convert.ToInt32(1.ToString(), 8), ActualResult.pcCounter);

            // Test AC all 1s with link false
            TestItems.accumulatorOctal = Convert.ToInt32(7777.ToString(), 8);
            TestItems.LinkBit = true;
            TestItems.pcCounter = Convert.ToInt32(7777.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(0000.ToString(), 8), ActualResult.accumulatorOctal);
            Assert.AreEqual(false, ActualResult.LinkBit);
            Assert.AreEqual(Convert.ToInt32(0.ToString(), 8), ActualResult.pcCounter);

            // Test AC all 0s with link true
            TestItems.accumulatorOctal = Convert.ToInt32(0000.ToString(), 8);
            TestItems.LinkBit = true;
            TestItems.pcCounter = Convert.ToInt32(7070.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(0001.ToString(), 8), ActualResult.accumulatorOctal);
            Assert.AreEqual(true, ActualResult.LinkBit);
            Assert.AreEqual(Convert.ToInt32(7071.ToString(), 8), ActualResult.pcCounter);


            // Test A all 0s with link false
            TestItems.accumulatorOctal = Convert.ToInt32(0000.ToString(), 8);
            TestItems.LinkBit = false;
            TestItems.pcCounter = Convert.ToInt32(2.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(0001.ToString(), 8), ActualResult.accumulatorOctal);
            Assert.AreEqual(false, ActualResult.LinkBit);
            Assert.AreEqual(Convert.ToInt32(3.ToString(), 8), ActualResult.pcCounter);

            // Test AC rotating 1's starting with 0 and link true
            TestItems.accumulatorOctal = Convert.ToInt32(2525.ToString(), 8);
            TestItems.LinkBit = true;
            TestItems.pcCounter = Convert.ToInt32(2525.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(2526.ToString(), 8), ActualResult.accumulatorOctal);
            Assert.AreEqual(true, ActualResult.LinkBit);
            Assert.AreEqual(Convert.ToInt32(2526.ToString(), 8), ActualResult.pcCounter);

            // Test AC rotating 1's starting with 0 and link false
            TestItems.accumulatorOctal = Convert.ToInt32(2525.ToString(), 8);
            TestItems.LinkBit = false;
            TestItems.pcCounter = Convert.ToInt32(4000.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(2526.ToString(), 8), ActualResult.accumulatorOctal);
            Assert.AreEqual(false, ActualResult.LinkBit);
            Assert.AreEqual(Convert.ToInt32(4001.ToString(), 8), ActualResult.pcCounter);

            // Test AC rotating 1's starting with 1 and link true
            TestItems.accumulatorOctal = Convert.ToInt32(5252.ToString(), 8);
            TestItems.LinkBit = true;
            TestItems.pcCounter = Convert.ToInt32(0101.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(5253.ToString(), 8), ActualResult.accumulatorOctal);
            Assert.AreEqual(true, ActualResult.LinkBit);
            Assert.AreEqual(Convert.ToInt32(0102.ToString(), 8), ActualResult.pcCounter);

            // Test AC rotating 1's starting with 1 and link false
            TestItems.accumulatorOctal = Convert.ToInt32(5252.ToString(), 8);
            TestItems.LinkBit = false;
            TestItems.pcCounter = Convert.ToInt32(1111.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(5253.ToString(), 8), ActualResult.accumulatorOctal);
            Assert.AreEqual(false, ActualResult.LinkBit);
            Assert.AreEqual(Convert.ToInt32(1112.ToString(), 8), ActualResult.pcCounter);

            // Test AC end 1's with link true
            TestItems.accumulatorOctal = Convert.ToInt32(4001.ToString(), 8);
            TestItems.LinkBit = true;
            TestItems.pcCounter = Convert.ToInt32(1010.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(4002.ToString(), 8), ActualResult.accumulatorOctal);
            Assert.AreEqual(true, ActualResult.LinkBit);
            Assert.AreEqual(Convert.ToInt32(1011.ToString(), 8), ActualResult.pcCounter);

            // Test AC end 0's with link false
            TestItems.accumulatorOctal = Convert.ToInt32(3776.ToString(), 8);
            TestItems.LinkBit = false;
            TestItems.pcCounter = Convert.ToInt32(0770.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(3777.ToString(), 8), ActualResult.accumulatorOctal);
            Assert.AreEqual(false, ActualResult.LinkBit);
            Assert.AreEqual(Convert.ToInt32(0771.ToString(), 8), ActualResult.pcCounter);

            // Test AC positive and incremented caused carry, complement link
            TestItems.accumulatorOctal = Convert.ToInt32(3777.ToString(), 8);
            TestItems.LinkBit = false;
            TestItems.pcCounter = Convert.ToInt32(0770.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(4000.ToString(), 8), ActualResult.accumulatorOctal);
            Assert.AreEqual(true, ActualResult.LinkBit);
            Assert.AreEqual(Convert.ToInt32(0771.ToString(), 8), ActualResult.pcCounter);
        }
    }
}
