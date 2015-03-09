using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using ECE486_PDP_8_Emulator.Classes;
using ECE486_PDP_8_Emulator;
using ECE486_PDP_8_Emulator.Instructions;

namespace ECE486_PDP_8_Emulator_Tests.InstructionTests
{
    [TestClass]
    public class OsrTests
    {

        [TestMethod]
        public void TestM2OsrArgumentPassthrough()
        {

            InstructionItems TestItems = new InstructionItems()

            {
                accumulatorOctal = 0000,
                LinkBit = true,
                MemoryAddress = 0,
                MemoryValueOctal = 7777,
                pcCounter = 1800,
                InstructionRegister = Convert.ToInt32(7404.ToString(), 8)


            };


            InstructionResult ExpectedItems = new InstructionResult()
            {
                accumulatorOctal = 0000,
                LinkBit = true,
                MemoryAddress = 0,
                MemoryValueOctal = 7777,
                pcCounter = 1801,
                InstructionRegister = Convert.ToInt32(7404.ToString(), 8),
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
            Assert.AreEqual(false, ActualResult.BranchTaken);
            Assert.AreEqual(null, ActualResult.BranchType);
        }


        [TestMethod]
        public void TestM2OsrOperation()
        {
            //Need to initialize the instruction items for the first time


            InstructionItems TestItems = new InstructionItems()
            {
                accumulatorOctal = 0000,
                LinkBit = true,
                MemoryAddress = 0,

                MemoryValueOctal = 0000,
                pcCounter = 5649,
                InstructionRegister = Convert.ToInt32(7404.ToString(), 8)
            };


            InstructionResult ExpectedItems = new InstructionResult()
            {
                accumulatorOctal = 0000,
                LinkBit = true,
                MemoryAddress = 0,
                MemoryValueOctal = 0000,
                pcCounter = 5650,
                InstructionRegister = Convert.ToInt32(7404.ToString(), 8),
                SetMemValue = false
            };

            IInstruction TestOprInstruction = new OprInstruction();

            InstructionResult ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);

            Assert.AreEqual(0, ActualResult.accumulatorOctal);
            Assert.AreEqual(false, ActualResult.BranchTaken);
            Assert.AreEqual(null, ActualResult.BranchType);

            /* Test cases place octals into PC and observe PC incremented by 1 */

            //Test 0
            TestItems.pcCounter = Convert.ToInt32(0.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(1.ToString(), 8), ActualResult.pcCounter);
            Assert.AreEqual(false, ActualResult.BranchTaken);
            Assert.AreEqual(null, ActualResult.BranchType);

            //Test 1
            TestItems.pcCounter = Convert.ToInt32(1.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(2.ToString(), 8), ActualResult.pcCounter);
            Assert.AreEqual(false, ActualResult.BranchTaken);
            Assert.AreEqual(null, ActualResult.BranchType);


            //Test all 1's, should loop back to 0
            TestItems.pcCounter = Convert.ToInt32(7777.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(0.ToString(), 8), ActualResult.pcCounter);
            Assert.AreEqual(false, ActualResult.BranchTaken);
            Assert.AreEqual(null, ActualResult.BranchType);


            //Test alternating pattern of 1s and 0s - Start with 1
            TestItems.pcCounter = Convert.ToInt32(5252.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(5253.ToString(), 8), ActualResult.pcCounter);
            Assert.AreEqual(false, ActualResult.BranchTaken);
            Assert.AreEqual(null, ActualResult.BranchType);


            //Test alternating pattern of 1s and 0s - Start with 0
            TestItems.pcCounter = Convert.ToInt32(2525.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(2526.ToString(), 8), ActualResult.pcCounter);
            Assert.AreEqual(false, ActualResult.BranchTaken);
            Assert.AreEqual(null, ActualResult.BranchType);


            //Test 0 for first and last octals, ensure not losing octals on ends
            TestItems.pcCounter = Convert.ToInt32(0770.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(0771.ToString(), 8), ActualResult.pcCounter);
            Assert.AreEqual(false, ActualResult.BranchTaken);
            Assert.AreEqual(null, ActualResult.BranchType);


            //Test end 1's
            TestItems.pcCounter = Convert.ToInt32(4001.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(4002.ToString(), 8), ActualResult.pcCounter);
            Assert.AreEqual(false, ActualResult.BranchTaken);
            Assert.AreEqual(null, ActualResult.BranchType);


            //Test last octal 0
            TestItems.pcCounter = Convert.ToInt32(70.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(71.ToString(), 8), ActualResult.pcCounter);
            Assert.AreEqual(false, ActualResult.BranchTaken);
            Assert.AreEqual(null, ActualResult.BranchType);

            //Test last 2 octals 0
            TestItems.pcCounter = Convert.ToInt32(700.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(701.ToString(), 8), ActualResult.pcCounter);
            Assert.AreEqual(false, ActualResult.BranchTaken);
            Assert.AreEqual(null, ActualResult.BranchType);

            //Test last octal 1
            TestItems.pcCounter = Convert.ToInt32(771.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(772.ToString(), 8), ActualResult.pcCounter);
            Assert.AreEqual(false, ActualResult.BranchTaken);
            Assert.AreEqual(null, ActualResult.BranchType);

            //Test last 3 octals 7, end case, 1777
            TestItems.pcCounter = Convert.ToInt32(777.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(1000.ToString(), 8), ActualResult.pcCounter);
            Assert.AreEqual(false, ActualResult.BranchTaken);
            Assert.AreEqual(null, ActualResult.BranchType);

        }

        [TestMethod]
        public void TestM2OSRCombination()
        {
            //Test for combination of OSR and HLT - 111 100 000 110
            //ORs Switch register with AC
            // HLT = true
          
           

            InstructionItems TestItems = new InstructionItems()
            {
                accumulatorOctal = 1000,
                LinkBit = true,
                MemoryAddress = 0,

                MemoryValueOctal = 0000,
                pcCounter = 1400,
                InstructionRegister = Convert.ToInt32(7406.ToString(), 8)
            };


            InstructionResult ExpectedItems = new InstructionResult()
            {
                accumulatorOctal = 1000,
                LinkBit = true,
                MemoryAddress = 0,
                MemoryValueOctal = 0000,
                pcCounter = 1401,
                InstructionRegister = Convert.ToInt32(7406.ToString(), 8),
                SetMemValue = false
            };

            IInstruction TestOprInstruction = new OprInstruction();

            InstructionResult ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);

            Assert.AreEqual(ExpectedItems.accumulatorOctal, ActualResult.accumulatorOctal);
            Assert.AreEqual(ExpectedItems.LinkBit, ActualResult.LinkBit);
            Assert.AreEqual(ExpectedItems.MemoryAddress, ActualResult.MemoryAddress);
            Assert.AreEqual(ExpectedItems.MemoryValueOctal, ActualResult.MemoryValueOctal);
            Assert.AreEqual(ExpectedItems.pcCounter, ActualResult.pcCounter);
            Assert.AreEqual(false, ActualResult.BranchTaken);
            Assert.AreEqual(null, ActualResult.BranchType);
            Assert.AreEqual(true, ActualResult.IsHalted);
        }
    }
}

