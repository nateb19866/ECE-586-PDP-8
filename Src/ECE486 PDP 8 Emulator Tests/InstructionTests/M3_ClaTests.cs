using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using ECE486_PDP_8_Emulator.Classes;
using ECE486_PDP_8_Emulator;
using ECE486_PDP_8_Emulator.Instructions;

namespace ECE486_PDP_8_Emulator_Tests.InstructionTests
{
    [TestClass]
    public class M3_ClaTests
    {
        [TestMethod]
        public void TestM3CLAArgumentPassthrough()
        {
            InstructionItems TestItems = new InstructionItems()
            {
                accumulatorOctal = 0000,
                LinkBit = true,
                MemoryAddress = 0,
                MemoryValueOctal = 7777,
                pcCounter = 1,
                InstructionRegister = Convert.ToInt32(7601.ToString(), 8)


            };


            InstructionResult ExpectedItems = new InstructionResult()
            {
                accumulatorOctal = 0000,
                LinkBit = true,
                MemoryAddress = 0,
                MemoryValueOctal = 7777,
                pcCounter = 2,
                InstructionRegister = Convert.ToInt32(7601.ToString(), 8),
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
        public void TestM3_ClaOperation()
        {
            //First is test 0 anded with 0 - need to initialize the instruction items for the first time
            InstructionItems TestItems = new InstructionItems()
            {
                accumulatorOctal = 0000,
                LinkBit = true,
                MemoryAddress = 0,

                MemoryValueOctal = 0000,
                pcCounter = 5649,
                InstructionRegister = Convert.ToInt32(7601.ToString(), 8)
            };


            InstructionResult ExpectedItems = new InstructionResult()
            {
                accumulatorOctal = 0000,
                LinkBit = true,
                MemoryAddress = 0,
                MemoryValueOctal = 0000,
                pcCounter = 5650,
                InstructionRegister = Convert.ToInt32(7601.ToString(), 8),
                SetMemValue = false

            };

            IInstruction TestOprInstruction = new OprInstruction();

            InstructionResult ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);

            Assert.AreEqual(0, ActualResult.accumulatorOctal);

            /* Test clears AC and PC incremented by 1 */

            //Test clears AC and PC increments
            TestItems.pcCounter = Convert.ToInt32(0.ToString(), 8);
            //TestItems.accumulatorOctal = Convert.ToInt32(7777.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(1.ToString(), 8), ActualResult.pcCounter);
            //Assert.AreEqual(0, ActualResult.accumulatorOctal);


            //Test PC increments from 0
            TestItems.pcCounter = Convert.ToInt32(1.ToString(), 8);
            //TestItems.accumulatorOctal = Convert.ToInt32(7777.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(2.ToString(), 8), ActualResult.pcCounter);
           // Assert.AreEqual(0, ActualResult.accumulatorOctal);
            


            //Test PC increments from max, back to 0
            TestItems.pcCounter = Convert.ToInt32(7777.ToString(), 8);
           // TestItems.accumulatorOctal = Convert.ToInt32(7777.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(0.ToString(), 8), ActualResult.pcCounter);
           // Assert.AreEqual(0, ActualResult.accumulatorOctal);


            //Test PC alternating pattern of 1s and 0s - Start with 1
            TestItems.pcCounter = Convert.ToInt32(5252.ToString(), 8);
           // TestItems.accumulatorOctal = Convert.ToInt32(7777.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(5253.ToString(), 8), ActualResult.pcCounter);
            //Assert.AreEqual(0, ActualResult.accumulatorOctal);


            //Test PC alternating pattern of 1s and 0s - Start with 0
            TestItems.pcCounter = Convert.ToInt32(2525.ToString(), 8);
           // TestItems.accumulatorOctal = Convert.ToInt32(7777.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(2526.ToString(), 8), ActualResult.pcCounter);
           // Assert.AreEqual(0, ActualResult.accumulatorOctal);


            //Test PC for first and last octals, ensure not losing octals on ends
            TestItems.pcCounter = Convert.ToInt32(0770.ToString(), 8);
           // TestItems.accumulatorOctal = Convert.ToInt32(7777.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(771.ToString(), 8), ActualResult.pcCounter);
           // Assert.AreEqual(0, ActualResult.accumulatorOctal);


            //Test PC end bits 1's
            TestItems.pcCounter = Convert.ToInt32(4001.ToString(), 8);
           // TestItems.accumulatorOctal = Convert.ToInt32(7777.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(4002.ToString(), 8), ActualResult.pcCounter);
          //  Assert.AreEqual(0, ActualResult.accumulatorOctal);


            //Test PC last octals 0s
            TestItems.pcCounter = Convert.ToInt32(700.ToString(), 8);
           // TestItems.accumulatorOctal = Convert.ToInt32(7777.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(701.ToString(), 8), ActualResult.pcCounter);
           // Assert.AreEqual(0, ActualResult.accumulatorOctal);

            //Test PC last octal increments from non-zero
            TestItems.pcCounter = Convert.ToInt32(771.ToString(), 8);
           // TestItems.accumulatorOctal = Convert.ToInt32(7777.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(772.ToString(), 8), ActualResult.pcCounter);
          //  Assert.AreEqual(0, ActualResult.accumulatorOctal);

            //Test PC last 3 octals 7, results 1777
            TestItems.pcCounter = Convert.ToInt32(777.ToString(), 8);
          //  TestItems.accumulatorOctal = Convert.ToInt32(7777.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(1000.ToString(), 8), ActualResult.pcCounter);
           // Assert.AreEqual(0, ActualResult.accumulatorOctal);

        }
    }
}

