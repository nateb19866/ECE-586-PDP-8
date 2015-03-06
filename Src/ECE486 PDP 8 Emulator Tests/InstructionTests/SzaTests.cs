using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ECE486_PDP_8_Emulator.Classes;
using ECE486_PDP_8_Emulator.Instructions;
using ECE486_PDP_8_Emulator;

namespace ECE486_PDP_8_Emulator_Tests.InstructionTests
{
    [TestClass]
    public class SzaTests
    {
        [TestMethod]
        public void TestM2SZAArgumentPassthrough()
        {
            InstructionItems TestItems = new InstructionItems()
            //Skips only if AC is zero.
            {
                accumulatorOctal = 0x100,   //0001 0000 0000
                LinkBit = true,
                MemoryAddress = 0,
                MemoryValueOctal = Convert.ToInt32(7777.ToString(), 8),
                pcCounter = 1400,           // No skip
                InstructionRegister = Convert.ToInt32(7440.ToString(), 8)


            };


            InstructionResult ExpectedItems = new InstructionResult()
            {
                accumulatorOctal = 0x100,
                LinkBit = true,
                MemoryAddress = 0,
                MemoryValueOctal = Convert.ToInt32(7777.ToString(), 8),
                pcCounter = 1401,
                InstructionRegister = Convert.ToInt32(7440.ToString(), 8),
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
        public void TestM2SZAOperation()
        {
            //AC - 0000 PCout - PC + 2.


            InstructionItems TestItems = new InstructionItems()
            {
                accumulatorOctal = 0000,
                LinkBit = true,
                MemoryAddress = 0,

                MemoryValueOctal = 0000,
                pcCounter = 1400,
                InstructionRegister = Convert.ToInt32(7440.ToString(), 8)
            };


            InstructionResult ExpectedItems = new InstructionResult()
            {
                accumulatorOctal = 0000,
                LinkBit = true,
                MemoryAddress = 0,
                MemoryValueOctal = 0000,
                pcCounter = 1402,
                InstructionRegister = Convert.ToInt32(7440.ToString(), 8),
                SetMemValue = false
            };

            IInstruction TestOprInstruction = new OprInstruction();

            InstructionResult ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);

            Assert.AreEqual(0, ActualResult.accumulatorOctal);

            // NOTE: PC always increments to next instruction ( +1 ), it will only skip ( +2 ) if conditions are met

            //AC - 0, PC + 2
            TestItems.accumulatorOctal = Convert.ToInt32(0.ToString(), 8);
            TestItems.pcCounter = Convert.ToInt32(0.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(2.ToString(), 8), ActualResult.pcCounter);

            //AC - 1, PC + 1
            TestItems.accumulatorOctal = Convert.ToInt32(1.ToString(), 8);
            TestItems.pcCounter = Convert.ToInt32(0.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(1.ToString(), 8), ActualResult.pcCounter);


            //AC - 10, PC + 1
            TestItems.accumulatorOctal = Convert.ToInt32(10.ToString(), 8);
            TestItems.pcCounter = Convert.ToInt32(0.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(1.ToString(), 8), ActualResult.pcCounter);


            //AC is non zero PC + 1
            TestItems.accumulatorOctal = Convert.ToInt32(7777.ToString(), 8);
            TestItems.pcCounter = Convert.ToInt32(7770.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(7771.ToString(), 8), ActualResult.pcCounter);

            //Test AC not 0, recognize first octal 0s
            TestItems.accumulatorOctal = Convert.ToInt32(0777.ToString(), 8);
            TestItems.pcCounter = Convert.ToInt32(7770.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(7771.ToString(), 8), ActualResult.pcCounter);

            //Test AC 0, PC skips next instruction
            TestItems.accumulatorOctal = Convert.ToInt32(0.ToString(), 8);
            TestItems.pcCounter = Convert.ToInt32(7771.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(7773.ToString(), 8), ActualResult.pcCounter);

            //test AC alternating 1's with starting 0, PC increments by 1 not skips
            TestItems.accumulatorOctal = Convert.ToInt32(2525.ToString(), 8);
            TestItems.pcCounter = Convert.ToInt32(240.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(241.ToString(), 8), ActualResult.pcCounter);

            //AC - zero, PC reached 7777 so it again initializes to 0 and gets incremented by 1
            TestItems.accumulatorOctal = Convert.ToInt32(0.ToString(), 8);
            TestItems.pcCounter = Convert.ToInt32(7777.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(1.ToString(), 8), ActualResult.pcCounter);

            //AC non zero, PC reaches 7777 so it again initializes to 0
            TestItems.accumulatorOctal = Convert.ToInt32(1.ToString(), 8);
            TestItems.pcCounter = Convert.ToInt32(7777.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(0.ToString(), 8), ActualResult.pcCounter);

            //AC non zero, PC reached 7777 so it again initializes to 0
            TestItems.accumulatorOctal = Convert.ToInt32(7777.ToString(), 8);
            TestItems.pcCounter = Convert.ToInt32(7777.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(0.ToString(), 8), ActualResult.pcCounter);

        }


        [TestMethod]
        public void TestM2SZACombination()
        {
            //Test for OR Sub group Skips if at least one condition from SMA SZA SNL is met.
            //Test for combination of SZA and SNL - 111 100 110 000
            //AC - Zero (skip), Link Bit - 0 (no skip), PCout - PC + 2 (SZA is True)


            InstructionItems TestItems = new InstructionItems()
            {
                accumulatorOctal = 0000,
                LinkBit = false,
                MemoryAddress = 0,

                MemoryValueOctal = 0000,
                pcCounter = 1400,
                InstructionRegister = Convert.ToInt32(7460.ToString(), 8)
            };


            InstructionResult ExpectedItems = new InstructionResult()
            {
                accumulatorOctal = 0000,
                LinkBit = false,
                MemoryAddress = 0,
                MemoryValueOctal = 0000,
                pcCounter = 1402,
                InstructionRegister = Convert.ToInt32(7460.ToString(), 8),
                SetMemValue = false
            };

            IInstruction TestOprInstruction = new OprInstruction();

            InstructionResult ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);

            Assert.AreEqual(ExpectedItems.accumulatorOctal, ActualResult.accumulatorOctal);
            Assert.AreEqual(ExpectedItems.LinkBit, ActualResult.LinkBit);
            Assert.AreEqual(ExpectedItems.MemoryAddress, ActualResult.MemoryAddress);
            Assert.AreEqual(ExpectedItems.MemoryValueOctal, ActualResult.MemoryValueOctal);
            Assert.AreEqual(ExpectedItems.pcCounter, ActualResult.pcCounter);

            //AC - Non Zero (No skip), Link bit - 0 (No Skip), PCout - PC + 1 (Both are False)
            TestItems.accumulatorOctal = Convert.ToInt32(10.ToString(), 8);
            TestItems.LinkBit = false;
            TestItems.pcCounter = Convert.ToInt32(0.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(1.ToString(), 8), ActualResult.pcCounter);

            //AC - Zero (Skip), Link bit - 1 (Skip), PCout - PC + 2 (Both are True)
            TestItems.accumulatorOctal = Convert.ToInt32(0.ToString(), 8);
            TestItems.LinkBit = true;
            TestItems.pcCounter = Convert.ToInt32(0.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(2.ToString(), 8), ActualResult.pcCounter);
        }

        [TestMethod]
        public void TestM2ORCombination()
        {
            //Test for OR Sub group Skips if at least one condition from SMA SZA SNL is True.
            //Test for combination of SMA, SZA and SNL - 111 101 110 000
            //AC - Positive (No skip), AC - Non Zero (No Skip) Link Bit - 0 (No Skip), PCout - PC + 1 (All 3 are False)


            InstructionItems TestItems = new InstructionItems()
            {
                accumulatorOctal = 0x100,   //0001 0000 0000 - Positive non zero hex number
                LinkBit = false,
                MemoryAddress = 0,

                MemoryValueOctal = 0000,
                pcCounter = 1400,
                InstructionRegister = Convert.ToInt32(7560.ToString(), 8)
            };


            InstructionResult ExpectedItems = new InstructionResult()
            {
                accumulatorOctal = 0x100,
                LinkBit = false,
                MemoryAddress = 0,
                MemoryValueOctal = 0000,
                pcCounter = 1401,
                InstructionRegister = Convert.ToInt32(7560.ToString(), 8),
                SetMemValue = false
            };

            IInstruction TestOprInstruction = new OprInstruction();

            InstructionResult ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);

            Assert.AreEqual(ExpectedItems.accumulatorOctal, ActualResult.accumulatorOctal);
            Assert.AreEqual(ExpectedItems.LinkBit, ActualResult.LinkBit);
            Assert.AreEqual(ExpectedItems.MemoryAddress, ActualResult.MemoryAddress);
            Assert.AreEqual(ExpectedItems.MemoryValueOctal, ActualResult.MemoryValueOctal);
            Assert.AreEqual(ExpectedItems.pcCounter, ActualResult.pcCounter);

            //AC - Negative Non Zero (SMA - Skip, SZA - No skip), Link bit - 0 (No Skip), PCout - PC + 1 (SMA is True)
           
            TestItems.accumulatorOctal = Convert.ToInt32(7777.ToString(), 8);
            TestItems.LinkBit = false;
            TestItems.pcCounter = Convert.ToInt32(7777.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(1.ToString(), 8), ActualResult.pcCounter);
        }
    }
}
