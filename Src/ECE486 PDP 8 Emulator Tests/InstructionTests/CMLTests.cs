﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using ECE486_PDP_8_Emulator.Classes;
using ECE486_PDP_8_Emulator;
using ECE486_PDP_8_Emulator.Instructions;

namespace ECE486_PDP_8_Emulator_Tests.InstructionTests
{
    [TestClass]
    public class CMLTests
    {
        [TestMethod]
        public void TestCMLArgumentPassthrough()
        {
            InstructionItems TestItems = new InstructionItems()
            {
                accumulatorOctal = 0000,
                LinkBit = true,
                MemoryAddress = 0,
                MemoryValueOctal = Convert.ToInt32(7777.ToString(), 8),
                pcCounter = 1500,
                InstructionRegister = Convert.ToInt32(7020.ToString(), 8)

            };


            InstructionResult ExpectedItems = new InstructionResult()
            {
                accumulatorOctal = 0000,
                LinkBit = false,
                MemoryAddress = 0,
                MemoryValueOctal = Convert.ToInt32(7777.ToString(), 8),
                pcCounter = 1501,
                InstructionRegister = Convert.ToInt32(7020.ToString(), 8),
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
        public void TestCMLOperation()
        {

            InstructionItems TestItems = new InstructionItems()
            {
                accumulatorOctal = 0000,
                LinkBit = true,
                MemoryAddress = 0,

                MemoryValueOctal = 0000,
                pcCounter = 1500,
                InstructionRegister = Convert.ToInt32(7020.ToString(), 8)
            };


            InstructionResult ExpectedItems = new InstructionResult()
            {
                accumulatorOctal = 0000,
                LinkBit = false,
                MemoryAddress = 0,
                MemoryValueOctal = 0000,
                pcCounter = 1501,
                InstructionRegister = Convert.ToInt32(7020.ToString(), 8),
                SetMemValue = false

            };

            IInstruction TestOprInstruction = new OprInstruction();

            InstructionResult ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);

            Assert.AreEqual(false, ActualResult.LinkBit);

            // Test link complements from true
            TestItems.LinkBit = true; 
            TestItems.pcCounter = Convert.ToInt32(0.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(false, ActualResult.LinkBit);
            Assert.AreEqual(Convert.ToInt32(1.ToString(), 8), ActualResult.pcCounter);


            // Test link complements from false
            TestItems.LinkBit = false;
            TestItems.pcCounter = Convert.ToInt32(7777.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(true, ActualResult.LinkBit); 
            Assert.AreEqual(Convert.ToInt32(0.ToString(), 8), ActualResult.pcCounter);

        }
    }
}
