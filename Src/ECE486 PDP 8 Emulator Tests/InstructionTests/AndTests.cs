using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using ECE486_PDP_8_Emulator.Classes;
using ECE486_PDP_8_Emulator;
using ECE486_PDP_8_Emulator.Instructions;

namespace ECE486_PDP_8_Emulator_Tests.InstructionTests
{
    [TestClass]
    public class AndTests
    {
        [TestMethod]
        public void TestAndArgumentPassthrough()

        {
            InstructionItems TestItems = new InstructionItems()
            {
                accumulatorOctal = 0000,
                LinkBit = true,
                MemoryAddress = 0,
                MemoryValueOctal = Convert.ToInt32(7777.ToString(), 8),
                pcCounter = 1,
                InstructionRegister = Convert.ToInt32(7402.ToString(), 8),
                OsrSwitchBits = 0xFFF
                


            };


            InstructionResult ExpectedItems = new InstructionResult()
            {
                accumulatorOctal = 0000,
                LinkBit = true,
                MemoryAddress = 0,
                MemoryValueOctal = Convert.ToInt32(7777.ToString(), 8),
                pcCounter = 2,
                InstructionRegister = Convert.ToInt32(7402.ToString(), 8),
                SetMemValue = false
            };

           IInstruction TestAndInstruction = new AndInstruction();

            InstructionResult ActualResult = TestAndInstruction.ExecuteInstruction(TestItems);


            Assert.AreEqual(ExpectedItems.accumulatorOctal, ActualResult.accumulatorOctal);
            Assert.AreEqual(ExpectedItems.LinkBit, ActualResult.LinkBit);
            Assert.AreEqual(ExpectedItems.MemoryAddress, ActualResult.MemoryAddress);
            Assert.AreEqual(ExpectedItems.MemoryValueOctal, ActualResult.MemoryValueOctal);
            Assert.AreEqual(ExpectedItems.pcCounter, ActualResult.pcCounter);
            Assert.AreEqual(ExpectedItems.InstructionRegister, ActualResult.InstructionRegister);
            Assert.AreEqual(ExpectedItems.SetMemValue, ActualResult.SetMemValue);

            //Test instruction properties
            Assert.AreEqual(2, TestAndInstruction.clockCycles);
            Assert.AreEqual(Constants.OpCode.AND, TestAndInstruction.instructionType);
            Assert.AreEqual(0xFFF, ActualResult.OsrSwitchBits);
        }


        [TestMethod]
        public void TestAndOperation()
        {
            //First is test 0 anded with 0 - need to initialize the instruction items for the first time
            InstructionItems TestItems = new InstructionItems()
            {
                accumulatorOctal =0000,
                LinkBit = true,
                MemoryAddress = 0,
                
                MemoryValueOctal = 0000,
                pcCounter = 5649,
                InstructionRegister = Convert.ToInt32(7402.ToString(),8)
            };


            InstructionResult ExpectedItems = new InstructionResult()
            {
                accumulatorOctal = 0000,
                LinkBit = true,
                MemoryAddress = 0,
                MemoryValueOctal = 0000,
                pcCounter = 5650,
                InstructionRegister = Convert.ToInt32(7402.ToString(), 8),
                SetMemValue = false

            };

            IInstruction TestAndInstruction = new AndInstruction();

            InstructionResult ActualResult = TestAndInstruction.ExecuteInstruction(TestItems);

            Assert.AreEqual( 0, ActualResult.accumulatorOctal);

            /* Test Cases for AND instruction, all cases produce PC + 1 */

            //Test all 1s ANDed with all 0s, results in all 0s
            TestItems.accumulatorOctal = 0000;
            TestItems.MemoryValueOctal = Convert.ToInt32(7777.ToString(), 8);
            TestItems.pcCounter = Convert.ToInt32(0.ToString(), 8);
          
            ActualResult = TestAndInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual( 0, ActualResult.accumulatorOctal);
            Assert.AreEqual(1, ActualResult.pcCounter);


            //Test alternating pattern of 1s and 0s - 101 010 101 010 - anded with the opposite - should produce a 0
            TestItems.accumulatorOctal = Convert.ToInt32(5252.ToString(), 8);
            TestItems.MemoryValueOctal = Convert.ToInt32(2525.ToString(), 8);
            TestItems.pcCounter = Convert.ToInt32(7777.ToString(), 8);

            ActualResult = TestAndInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual( 0, ActualResult.accumulatorOctal);
            Assert.AreEqual(0, ActualResult.pcCounter);


            //Test lowest octal value
            TestItems.accumulatorOctal = Convert.ToInt32(7777.ToString(), 8);
            TestItems.MemoryValueOctal = Convert.ToInt32(7770.ToString(), 8);
            TestItems.pcCounter = Convert.ToInt32(7070.ToString(), 8);

            ActualResult = TestAndInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(7770.ToString(), 8), ActualResult.accumulatorOctal);
            Assert.AreEqual(Convert.ToInt32(7071.ToString(), 8), ActualResult.pcCounter);

            //test 2nd octal value
            TestItems.accumulatorOctal = Convert.ToInt32(7777.ToString(), 8);
            TestItems.MemoryValueOctal = Convert.ToInt32(7707.ToString(), 8);
            TestItems.pcCounter = Convert.ToInt32(2.ToString(), 8);


            ActualResult = TestAndInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(7707.ToString(), 8), ActualResult.accumulatorOctal);
            Assert.AreEqual(Convert.ToInt32(3.ToString(), 8), ActualResult.pcCounter);

            //test 3rd octal value
            TestItems.accumulatorOctal = Convert.ToInt32(7777.ToString(), 8);
            TestItems.MemoryValueOctal = Convert.ToInt32(7077.ToString(), 8);
            TestItems.pcCounter = Convert.ToInt32(2525.ToString(), 8);

            ActualResult = TestAndInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(7077.ToString(), 8), ActualResult.accumulatorOctal);
            Assert.AreEqual(Convert.ToInt32(2526.ToString(), 8), ActualResult.pcCounter);

            //test 4th octal value
            TestItems.accumulatorOctal = Convert.ToInt32(7777.ToString(), 8);
            TestItems.MemoryValueOctal = Convert.ToInt32(777.ToString(), 8);
            TestItems.pcCounter = Convert.ToInt32(4000.ToString(), 8);

            ActualResult = TestAndInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(777.ToString(), 8), ActualResult.accumulatorOctal);
            Assert.AreEqual(Convert.ToInt32(4001.ToString(), 8), ActualResult.pcCounter);

        }
    }
}
