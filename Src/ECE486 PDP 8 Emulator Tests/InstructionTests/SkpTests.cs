using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ECE486_PDP_8_Emulator.Classes;
using ECE486_PDP_8_Emulator;
using ECE486_PDP_8_Emulator.Instructions;

namespace ECE486_PDP_8_Emulator_Tests.InstructionTests
{
    [TestClass]
    public class M2SKPTests
    {
        [TestMethod]
        public void TestM2SKPArgumentPassthrough()

        {

            InstructionItems TestItems = new InstructionItems()

            {
                accumulatorOctal = 0000,
                LinkBit = true,
                MemoryAddress = 0,
                MemoryValueOctal = Convert.ToInt32(7777.ToString(), 8),
                pcCounter = 1200,
                InstructionRegister = Convert.ToInt32(7410.ToString(), 8)


            };


            InstructionResult ExpectedItems = new InstructionResult()
            {
                accumulatorOctal = 0000,
                LinkBit = true,
                MemoryAddress = 0,
                MemoryValueOctal = Convert.ToInt32(7777.ToString(), 8),
                pcCounter = 1202,
                InstructionRegister = Convert.ToInt32(7410.ToString(), 8),
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
            Assert.AreEqual(true, ActualResult.BranchTaken);
            Assert.AreEqual(Constants.BranchType.Unconditional, ActualResult.BranchType);
           
        }


        [TestMethod]
        public void TestM2SKPOperation()
        {
            //Need to initialize the instruction items for the first time


            InstructionItems TestItems = new InstructionItems()
            {
                accumulatorOctal = 0000,
                LinkBit = true,
                MemoryAddress = 0,

                MemoryValueOctal = 0000,
                pcCounter = 1202,
                InstructionRegister = Convert.ToInt32(7410.ToString(), 8)
            };


            InstructionResult ExpectedItems = new InstructionResult()
            {
                accumulatorOctal = 0000,
                LinkBit = true,
                MemoryAddress = 0,
                MemoryValueOctal = 0000,
                pcCounter = 1204,
                InstructionRegister = Convert.ToInt32(7410.ToString(), 8),
                SetMemValue = false
            };

            IInstruction TestOprInstruction = new OprInstruction();

            InstructionResult ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);

            Assert.AreEqual(0, ActualResult.accumulatorOctal);
            Assert.AreEqual(Constants.BranchType.Unconditional, ActualResult.BranchType);

            /* Test cases ensure PC always skip */

           // Test for PC starting 0
            TestItems.pcCounter = Convert.ToInt32(0.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(2, ActualResult.pcCounter);
            Assert.AreEqual(Constants.BranchType.Unconditional, ActualResult.BranchType);

            //Test for PC with end bits 1s
            TestItems.pcCounter = Convert.ToInt32(4001.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(4003.ToString(), 8), ActualResult.pcCounter);
            Assert.AreEqual(Constants.BranchType.Unconditional, ActualResult.BranchType);

            //Test for PC with last octal 0
            TestItems.pcCounter = Convert.ToInt32(7770.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(7772.ToString(), 8), ActualResult.pcCounter);
            Assert.AreEqual(Constants.BranchType.Unconditional, ActualResult.BranchType);

            //Test for PC skip correctly from 7777
            TestItems.pcCounter = Convert.ToInt32(7777.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(1.ToString(), 8), ActualResult.pcCounter);
            Assert.AreEqual(Constants.BranchType.Unconditional, ActualResult.BranchType);
        }
       

        [TestMethod]
        public void TestM2SKPCombination()
        {
            //Test for combination of SKP and HLT - 111 100 001 010
            //Always servers skip first and then Halts.
            //Halt priority 3 and skip priority 1

            InstructionItems TestItems = new InstructionItems()
            {
                accumulatorOctal = 1000,
                LinkBit = true,
                MemoryAddress = 0,

                MemoryValueOctal = 0000,
                pcCounter = 1400,
                InstructionRegister = Convert.ToInt32(7412.ToString(), 8)
            };


            InstructionResult ExpectedItems = new InstructionResult()
            {
                accumulatorOctal = 1000,
                LinkBit = true,
                MemoryAddress = 0,
                MemoryValueOctal = 0000,
                pcCounter = 1402,
                InstructionRegister = Convert.ToInt32(7412.ToString(), 8),
                SetMemValue = false
            };

            IInstruction TestOprInstruction = new OprInstruction();

            InstructionResult ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);

            Assert.AreEqual(ExpectedItems.accumulatorOctal, ActualResult.accumulatorOctal);
            Assert.AreEqual(ExpectedItems.LinkBit, ActualResult.LinkBit);
            Assert.AreEqual(ExpectedItems.MemoryAddress, ActualResult.MemoryAddress);
            Assert.AreEqual(ExpectedItems.MemoryValueOctal, ActualResult.MemoryValueOctal);
            Assert.AreEqual(ExpectedItems.pcCounter, ActualResult.pcCounter);
            Assert.AreEqual(true, ActualResult.BranchTaken);
            Assert.AreEqual(Constants.BranchType.Unconditional, ActualResult.BranchType);
            Assert.AreEqual(true, ActualResult.IsHalted);

        }
    }
}
