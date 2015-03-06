using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ECE486_PDP_8_Emulator.Classes;
using ECE486_PDP_8_Emulator;
using ECE486_PDP_8_Emulator.Instructions;

namespace ECE486_PDP_8_Emulator_Tests.InstructionTests
{
    [TestClass]
    public class SzlTests
    {
        [TestMethod]
        public void TestM2SZLArgumentPassthrough()
        {

            InstructionItems TestItems = new InstructionItems()
            //Skips only if Link Bit is Zero.
            {
                accumulatorOctal = 0000,
                LinkBit = false,    //Skips a Instruction
                MemoryAddress = 0,
                MemoryValueOctal = Convert.ToInt32(7777.ToString(), 8),
                pcCounter = 1300,
                InstructionRegister = Convert.ToInt32(7430.ToString(), 8)


            };


            InstructionResult ExpectedItems = new InstructionResult()
            {
                accumulatorOctal = 0000,
                LinkBit = false,
                MemoryAddress = 0,
                MemoryValueOctal = Convert.ToInt32(7777.ToString(), 8),
                pcCounter = 1302,
                InstructionRegister = Convert.ToInt32(7430.ToString(), 8),
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
        public void TestM2SZLOperation()
        {
            //Link Bit - 1 (No skip), PCout - PC + 1


            InstructionItems TestItems = new InstructionItems()
            {
                accumulatorOctal = 0000,
                LinkBit = true,
                MemoryAddress = 0,

                MemoryValueOctal = 0000,
                pcCounter = 1300,
                InstructionRegister = Convert.ToInt32(7430.ToString(), 8)
            };


            InstructionResult ExpectedItems = new InstructionResult()
            {
                accumulatorOctal = 0000,
                LinkBit = true,
                MemoryAddress = 0,
                MemoryValueOctal = 0000,
                pcCounter = 1301,
                InstructionRegister = Convert.ToInt32(7430.ToString(), 8),
                SetMemValue = false
            };

            IInstruction TestOprInstruction = new OprInstruction();

            InstructionResult ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);

            Assert.AreEqual(0, ActualResult.accumulatorOctal);

            /* Test cases for PC skipping next instruction if Link is false */

            // Test Link true and PC does not skip
            TestItems.LinkBit = true;
            TestItems.pcCounter = 0;

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(1, ActualResult.pcCounter);


            //Test Link false, PC skips next instruction
            TestItems.LinkBit = false;
            TestItems.pcCounter = 0;

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(2, ActualResult.pcCounter);

            /* 
             * Below Tests ensure that PC only skips when Link is false
             * Testing for PC similar to previous test cases. Alternating
             * whether PC increments correctly, given various octals.
             * */
            //Test Link true, PC does not skip
            TestItems.LinkBit = true;
            TestItems.pcCounter = Convert.ToInt32(7770.ToString(), 8);


            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(7771.ToString(), 8), ActualResult.pcCounter);

            //Test lowest octal value
            TestItems.LinkBit = false;
            TestItems.pcCounter = Convert.ToInt32(7770.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(7772.ToString(), 8), ActualResult.pcCounter);

            //Test 1st octal value
            TestItems.LinkBit = true;
            TestItems.pcCounter = Convert.ToInt32(4001.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(4002.ToString(), 8), ActualResult.pcCounter);

            //test 2nd octal value
            TestItems.LinkBit = false;
            TestItems.pcCounter = Convert.ToInt32(4002.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(4004.ToString(), 8), ActualResult.pcCounter);

            //test 3rd octal value
            TestItems.LinkBit = false;
            TestItems.pcCounter = Convert.ToInt32(240.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(242.ToString(), 8), ActualResult.pcCounter);

            //test 4th octal value
            TestItems.LinkBit = false;
            TestItems.pcCounter = Convert.ToInt32(2525.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(2527.ToString(), 8), ActualResult.pcCounter);

            //test 5th octal value
            TestItems.LinkBit = true;
            TestItems.pcCounter = Convert.ToInt32(7777.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(0.ToString(), 8), ActualResult.pcCounter);

            //test 6th octal value
            TestItems.LinkBit = false;
            TestItems.pcCounter = Convert.ToInt32(7777.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(1.ToString(), 8), ActualResult.pcCounter);


        }

        [TestMethod]
        public void TestM2SZLCombination()
        {
            //Test for AND Sub group Skips only if all condition from SMA SZA SNL are True.
            //Test for combination of SPA and SZL - 111 101 011 000
            //AC - positive (Skip), Link - 1 (No skip), PCout - PC + 1 (Only SPA is True)

            InstructionItems TestItems = new InstructionItems()
            {
                accumulatorOctal = 0x200,
                LinkBit = true,
                MemoryAddress = 0,

                MemoryValueOctal = 0000,
                pcCounter = 1400,
                InstructionRegister = Convert.ToInt32(7530.ToString(), 8)
            };


            InstructionResult ExpectedItems = new InstructionResult()
            {
                accumulatorOctal = 0x200,
                LinkBit = true,
                MemoryAddress = 0,
                MemoryValueOctal = 0000,
                pcCounter = 1401,
                InstructionRegister = Convert.ToInt32(7530.ToString(), 8),
                SetMemValue = false
            };

            IInstruction TestOprInstruction = new OprInstruction();

            InstructionResult ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);

            Assert.AreEqual(ExpectedItems.accumulatorOctal, ActualResult.accumulatorOctal);
            Assert.AreEqual(ExpectedItems.LinkBit, ActualResult.LinkBit);
            Assert.AreEqual(ExpectedItems.MemoryAddress, ActualResult.MemoryAddress);
            Assert.AreEqual(ExpectedItems.MemoryValueOctal, ActualResult.MemoryValueOctal);
            Assert.AreEqual(ExpectedItems.pcCounter, ActualResult.pcCounter);

            //AC - Positive (Skip) Link bit - 0 (Skip), PCout - PC + 2, (Both conditions are True)
            TestItems.accumulatorOctal = Convert.ToInt32(0.ToString(), 8);
            TestItems.LinkBit = false;
            TestItems.pcCounter = Convert.ToInt32(7777.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(1.ToString(), 8), ActualResult.pcCounter);

            //AC - Negative (No skip) Link bit - 1 (No skip), PCout - PC + 1 (Both are False)
            TestItems.accumulatorOctal = Convert.ToInt32(0.ToString(), 8);
            TestItems.LinkBit = true;
            TestItems.pcCounter = Convert.ToInt32(0.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(1.ToString(), 8), ActualResult.pcCounter);
        }

        [TestMethod]
        public void TestM2ANDCombination()
        {
            //Test for AND Sub group Skips only if all condition (SMA SZA SNL) is True.
            //Test for combination of SMA, SZA and SNL - 111 101 111 000
            //AC - Positive (Skip), AC - Non Zero (Skip) Link Bit - 0 (Skip), PCout - PC + 2 (All 3 are True)


            InstructionItems TestItems = new InstructionItems()
            {
                accumulatorOctal = 0x100,   //0001 0000 0000 - Positive non zero hex number
                LinkBit = false,
                MemoryAddress = 0,

                MemoryValueOctal = 0000,
                pcCounter = 1400,
                InstructionRegister = Convert.ToInt32(7570.ToString(), 8)
            };


            InstructionResult ExpectedItems = new InstructionResult()
            {
                accumulatorOctal = 0x100,
                LinkBit = false,
                MemoryAddress = 0,
                MemoryValueOctal = 0000,
                pcCounter = 1402,
                InstructionRegister = Convert.ToInt32(7570.ToString(), 8),
                SetMemValue = false
            };

            IInstruction TestOprInstruction = new OprInstruction();

            InstructionResult ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);

            Assert.AreEqual(ExpectedItems.accumulatorOctal, ActualResult.accumulatorOctal);
            Assert.AreEqual(ExpectedItems.LinkBit, ActualResult.LinkBit);
            Assert.AreEqual(ExpectedItems.MemoryAddress, ActualResult.MemoryAddress);
            Assert.AreEqual(ExpectedItems.MemoryValueOctal, ActualResult.MemoryValueOctal);
            Assert.AreEqual(ExpectedItems.pcCounter, ActualResult.pcCounter);

            //AC - Negative Non Zero (SPA - No Skip, SNA - Skip), Link bit - 1 (No Skip), PCout - PC + 1 (Only SNA is True)

            TestItems.accumulatorOctal = Convert.ToInt32(7777.ToString(), 8);
            TestItems.LinkBit = true;
            TestItems.pcCounter = Convert.ToInt32(7777.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(0.ToString(), 8), ActualResult.pcCounter);

            //AC - Zero (SPA - Skip, SNA - Skip), Link bit - 1 (No Skip), PCout - PC + 1 (SZL is False)

            TestItems.accumulatorOctal = Convert.ToInt32(0.ToString(), 8);
            TestItems.LinkBit = true;
            TestItems.pcCounter = Convert.ToInt32(7777.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(0.ToString(), 8), ActualResult.pcCounter);
        }
    }
}
