using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ECE486_PDP_8_Emulator.Classes;
using ECE486_PDP_8_Emulator;
using ECE486_PDP_8_Emulator.Instructions;

namespace ECE486_PDP_8_Emulator_Tests.InstructionTests
{
    [TestClass]
    public class HltTests
    {
        [TestMethod]
        public void TestM2HLTOperation()
        {
       
            //Test for HLT - 111 100 000 010
            //PCout = PC + 1
            //HLT = true

            InstructionItems TestItems = new InstructionItems()
            {
                accumulatorOctal = 1000,
                LinkBit = true,
                MemoryAddress = 0,

                MemoryValueOctal = 0000,
                pcCounter = 1400,
                InstructionRegister = Convert.ToInt32(7402.ToString(), 8)
            };


            InstructionResult ExpectedItems = new InstructionResult()
            {
                accumulatorOctal = 1000,
                LinkBit = true,
                MemoryAddress = 0,
                MemoryValueOctal = 0000,
                pcCounter = 1401,
                InstructionRegister = Convert.ToInt32(7402.ToString(), 8),
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

        [TestMethod]
        public void TestM2HLTCombination()
        {
            //Test for combination of M2_CLA and HLT - 111 110 000 010
            //Always servers M2_CLA first and then Halts.
            //Halt priority 3 and M2_CLA priority 2

            InstructionItems TestItems = new InstructionItems()
            {
                accumulatorOctal = 1000,
                LinkBit = true,
                MemoryAddress = 0,

                MemoryValueOctal = 0000,
                pcCounter = 1400,
                InstructionRegister = Convert.ToInt32(7602.ToString(), 8)
            };


            InstructionResult ExpectedItems = new InstructionResult()
            {
                accumulatorOctal = 0000,
                LinkBit = true,
                MemoryAddress = 0,
                MemoryValueOctal = 0000,
                pcCounter = 1401,
                InstructionRegister = Convert.ToInt32(7602.ToString(), 8),
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

            // Test CLA and HLT
            // CLears AC, PCout = PC + 1
            // HLT = true

            TestItems.accumulatorOctal = Convert.ToInt32(7777.ToString(), 8);
            TestItems.pcCounter = Convert.ToInt32(7777.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(0, ActualResult.accumulatorOctal);
            Assert.AreEqual(Convert.ToInt32(0.ToString(), 8), ActualResult.pcCounter);
            Assert.AreEqual(false, ActualResult.BranchTaken);
            Assert.AreEqual(null, ActualResult.BranchType);
            Assert.AreEqual(true, ActualResult.IsHalted);

        }
    }
}
