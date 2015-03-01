﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using ECE486_PDP_8_Emulator.Classes;
using ECE486_PDP_8_Emulator;
using ECE486_PDP_8_Emulator.Instructions;

namespace ECE486_PDP_8_Emulator_Tests.InstructionTests
{
    [TestClass]
    public class DcaTests
    {
        [TestMethod]
        public void TestDcaArgumentPassthrough()
        {
            InstructionItems TestItems = new InstructionItems()
            {
                accumulatorOctal = 0000,
                LinkBit = true,
                MemoryAddress = 0,
                MemoryValueOctal = 7777,
                pcCounter = 5649,
                InstructionRegister = 7402


            };


            InstructionResult ExpectedItems = new InstructionResult()
            {
                accumulatorOctal = 0000,
                LinkBit = true,
                MemoryAddress = 0,
                MemoryValueOctal = 7777,
                pcCounter = 5650,
                InstructionRegister = 7402,
                SetMemValue = false
            };

            IInstruction TestDcaInstruction = new DcaInstruction();

            InstructionResult ActualResult = TestDcaInstruction.ExecuteInstruction(TestItems);


            Assert.AreEqual(ExpectedItems.accumulatorOctal, ActualResult.accumulatorOctal);
            Assert.AreEqual(ExpectedItems.LinkBit, ActualResult.LinkBit);
            Assert.AreEqual(ExpectedItems.MemoryAddress, ActualResult.MemoryAddress);
            Assert.AreEqual(ExpectedItems.MemoryValueOctal, ActualResult.MemoryValueOctal);
            Assert.AreEqual(ExpectedItems.pcCounter, ActualResult.pcCounter);
            Assert.AreEqual(ExpectedItems.InstructionRegister, ActualResult.InstructionRegister);
            Assert.AreEqual(ExpectedItems.SetMemValue, ActualResult.SetMemValue);
        }


        [TestMethod]
        public void TestDcaOperation()
        {
            //First is test 0 anded with 0 - need to initialize the instruction items for the first time
            InstructionItems TestItems = new InstructionItems()
            {
                accumulatorOctal = 0000,
                LinkBit = true,
                MemoryAddress = 0,

                MemoryValueOctal = 0000,
                pcCounter = 5649,
                InstructionRegister = 7402
            };


            InstructionResult ExpectedItems = new InstructionResult()
            {
                accumulatorOctal = 0000,
                LinkBit = true,
                MemoryAddress = 0,
                MemoryValueOctal = 0000,
                pcCounter = 5650,
                InstructionRegister = 7402,
                SetMemValue = false

            };

            IInstruction TestDcaInstruction = new DcaInstruction();

            InstructionResult ActualResult = TestDcaInstruction.ExecuteInstruction(TestItems);

            Assert.AreEqual(0, ActualResult.accumulatorOctal);

            /* Test cases place octals into AC, result will affect EA and AC: EA = AC, AC - 0 */

            //Test 0: EA = AC, AC - 0
            TestItems.accumulatorOctal = 0;
            TestItems.MemoryValueOctal = 7777;

            ActualResult = TestDcaInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(0, ActualResult.MemoryValueOctal);
            Assert.AreEqual(0, ActualResult.accumulatorOctal);


            //Test 1
            TestItems.accumulatorOctal = 1;
            TestItems.MemoryValueOctal = 7777;

            ActualResult = TestDcaInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(0, ActualResult.MemoryValueOctal);
            Assert.AreEqual(0, ActualResult.accumulatorOctal);


            //Test all 1s
            TestItems.accumulatorOctal = 7777;
            TestItems.MemoryValueOctal = 1111;

            ActualResult = TestDcaInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(0, ActualResult.MemoryValueOctal);
            Assert.AreEqual(0, ActualResult.accumulatorOctal);
            

            //Test alternating pattern of 1s and 0s - Start with 1
            TestItems.accumulatorOctal = 5252;
            TestItems.MemoryValueOctal = 2525;

            ActualResult = TestDcaInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(0, ActualResult.MemoryValueOctal);
            Assert.AreEqual(0, ActualResult.accumulatorOctal);


            //Test alternating pattern of 1s and 0s - Start with 0
            TestItems.accumulatorOctal = 2525;
            TestItems.MemoryValueOctal = 4444;

            ActualResult = TestDcaInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(0, ActualResult.MemoryValueOctal);
            Assert.AreEqual(0, ActualResult.accumulatorOctal);


            //Test 0 for first and last octals, ensure not losing octals on ends
            TestItems.accumulatorOctal = 0770;
            TestItems.MemoryValueOctal = 2222;

            ActualResult = TestDcaInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(0, ActualResult.MemoryValueOctal);
            Assert.AreEqual(0, ActualResult.accumulatorOctal);


            //Test end 1's
            TestItems.accumulatorOctal = 4001;
            TestItems.MemoryValueOctal = 0770;

            ActualResult = TestDcaInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(0, ActualResult.MemoryValueOctal);
            Assert.AreEqual(0, ActualResult.accumulatorOctal);

        }
    }
}
