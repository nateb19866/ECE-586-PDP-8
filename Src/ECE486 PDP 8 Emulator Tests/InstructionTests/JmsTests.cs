using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using ECE486_PDP_8_Emulator.Classes;
using ECE486_PDP_8_Emulator;
using ECE486_PDP_8_Emulator.Instructions;

namespace ECE486_PDP_8_Emulator_Tests.InstructionTests
{
    [TestClass]
    public class JmsTests
    {
        [TestMethod]
        public void TestJmsArgumentPassthrough()
        {
            InstructionItems TestItems = new InstructionItems()
            {
                accumulatorOctal = 0000,
                LinkBit = true,
                MemoryAddress = 543,
                MemoryValueOctal = Convert.ToInt32(7777.ToString(), 8),
                pcCounter = 1,
                InstructionRegister = Convert.ToInt32(7402.ToString(), 8),
                OsrSwitchBits = 0xFFF


            };


            InstructionResult ExpectedItems = new InstructionResult()
            {
                accumulatorOctal = 0000,
                LinkBit = true,
                MemoryAddress = 543,
                MemoryValueOctal = 1,
                pcCounter = 544,
                InstructionRegister = Convert.ToInt32(7402.ToString(), 8),
                SetMemValue = true
            };

            IInstruction TestJmsInstruction = new JmsInstruction();

            InstructionResult ActualResult = TestJmsInstruction.ExecuteInstruction(TestItems);


            Assert.AreEqual(ExpectedItems.accumulatorOctal, ActualResult.accumulatorOctal);
            Assert.AreEqual(ExpectedItems.LinkBit, ActualResult.LinkBit);
            Assert.AreEqual(ExpectedItems.MemoryAddress, ActualResult.MemoryAddress);
            Assert.AreEqual(ExpectedItems.MemoryValueOctal, ActualResult.MemoryValueOctal);
            Assert.AreEqual(ExpectedItems.pcCounter, ActualResult.pcCounter);
            Assert.AreEqual(ExpectedItems.InstructionRegister, ActualResult.InstructionRegister);
            Assert.AreEqual(ExpectedItems.SetMemValue, ActualResult.SetMemValue);
            Assert.AreEqual(true, ActualResult.BranchTaken);
            Assert.AreEqual(Constants.BranchType.Subroutine, ActualResult.BranchType);
            Assert.AreEqual(0xFFF, ActualResult.OsrSwitchBits);
            //Test instruction properties
            Assert.AreEqual(2, TestJmsInstruction.clockCycles);
            Assert.AreEqual(Constants.OpCode.JMS, TestJmsInstruction.instructionType);

        }


        [TestMethod]
        public void TestJmsOperation()
        {
            //First is test 0 anded with 0 - need to initialize the instruction items for the first time
            InstructionItems TestItems = new InstructionItems()
            {
                accumulatorOctal = 0000,
                LinkBit = true,
                MemoryAddress = 0,

                MemoryValueOctal = 0000,
                pcCounter = 5649,
                InstructionRegister = Convert.ToInt32(7402.ToString(), 8)
            };


            InstructionResult ExpectedItems = new InstructionResult()
            {
                accumulatorOctal = 0000,
                LinkBit = true,
                MemoryAddress = 1,
                MemoryValueOctal = 5649,
                pcCounter = 1,
                InstructionRegister = Convert.ToInt32(7402.ToString(), 8),
                SetMemValue = false

            };

            IInstruction TestJmsInstruction = new JmsInstruction();

            InstructionResult ActualResult = TestJmsInstruction.ExecuteInstruction(TestItems);

            Assert.AreEqual(0, ActualResult.accumulatorOctal);

            /* Test cases place (1) Value to PC, (2) PC = Eff Addr + 1 */
            /* Test that PC, Eff Addr, and mem Value update correctly */

            //Test (1) Value can hold max PC
            TestItems.pcCounter = Convert.ToInt32(7777.ToString(), 8);
            TestItems.MemoryValueOctal = Convert.ToInt32(1111.ToString(), 8);
            TestItems.MemoryAddress = Convert.ToInt32(1111.ToString(), 8);

            ActualResult = TestJmsInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(7777.ToString(), 8), ActualResult.MemoryValueOctal);
            Assert.AreEqual(Convert.ToInt32(1112.ToString(), 8), ActualResult.pcCounter);


            //Test (2) Value can hold max PC, then PC = 0
            TestItems.pcCounter = Convert.ToInt32(7777.ToString(), 8);
            TestItems.MemoryValueOctal = Convert.ToInt32(0.ToString(), 8);
            TestItems.MemoryAddress = Convert.ToInt32(7777.ToString(), 8);

            ActualResult = TestJmsInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(7777.ToString(), 8), ActualResult.MemoryValueOctal);
            Assert.AreEqual(Convert.ToInt32(0.ToString(), 8), ActualResult.pcCounter);


            //Test (3) Value can hold max PC and EA increments from 0
            TestItems.pcCounter = Convert.ToInt32(7777.ToString(), 8);
            TestItems.MemoryValueOctal = Convert.ToInt32(4226.ToString(), 8);
            TestItems.MemoryAddress = Convert.ToInt32(0.ToString(), 8);

            ActualResult = TestJmsInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(7777.ToString(), 8), ActualResult.MemoryValueOctal);
            Assert.AreEqual(Convert.ToInt32(1.ToString(), 8), ActualResult.pcCounter);


            //Test (4) Value can hold 0 PC and EA increments from 1
            TestItems.pcCounter = Convert.ToInt32(0.ToString(), 8);
            TestItems.MemoryValueOctal = Convert.ToInt32(4227.ToString(), 8);
            TestItems.MemoryAddress = Convert.ToInt32(1.ToString(), 8);

            ActualResult = TestJmsInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(0.ToString(), 8), ActualResult.MemoryValueOctal);
            Assert.AreEqual(Convert.ToInt32(2.ToString(), 8), ActualResult.pcCounter);


            //Test (5) Value can hold max PC and Value 1st octal increments correctly
            TestItems.pcCounter = Convert.ToInt32(7777.ToString(), 8);
            TestItems.MemoryValueOctal = Convert.ToInt32(0.ToString(), 8);
            TestItems.MemoryAddress = Convert.ToInt32(7770.ToString(), 8);

            ActualResult = TestJmsInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(7777.ToString(), 8), ActualResult.MemoryValueOctal);
            Assert.AreEqual(Convert.ToInt32(7771.ToString(), 8), ActualResult.pcCounter);


            //Test (6) Value updates and Value 2nd octal increments
            TestItems.pcCounter = Convert.ToInt32(7777.ToString(), 8);
            TestItems.MemoryValueOctal = Convert.ToInt32(0.ToString(), 8);
            TestItems.MemoryAddress = Convert.ToInt32(7707.ToString(), 8);

            ActualResult = TestJmsInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(7777.ToString(), 8), ActualResult.MemoryValueOctal);
            Assert.AreEqual(Convert.ToInt32(7710.ToString(), 8), ActualResult.pcCounter);

            //Test (7) Value updates and Value 3rd octal increments
            TestItems.pcCounter = Convert.ToInt32(7777.ToString(), 8);
            TestItems.MemoryValueOctal = Convert.ToInt32(1.ToString(), 8);
            TestItems.MemoryAddress = Convert.ToInt32(7077.ToString(), 8);

            ActualResult = TestJmsInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(7777.ToString(), 8), ActualResult.MemoryValueOctal);
            Assert.AreEqual(Convert.ToInt32(7100.ToString(), 8), ActualResult.pcCounter);

            //Test (8) Value updates and Value 3rd octal increments, then PC = 1
            TestItems.pcCounter = Convert.ToInt32(7777.ToString(), 8);
            TestItems.MemoryValueOctal = Convert.ToInt32(2.ToString(), 8);
            TestItems.MemoryAddress = Convert.ToInt32(0777.ToString(), 8);

            ActualResult = TestJmsInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(7777.ToString(), 8), ActualResult.MemoryValueOctal);
            Assert.AreEqual(Convert.ToInt32(1000.ToString(), 8), ActualResult.pcCounter);

        }
    }
}

