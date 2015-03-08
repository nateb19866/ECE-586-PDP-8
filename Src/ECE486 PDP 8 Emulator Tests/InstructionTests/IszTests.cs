using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using ECE486_PDP_8_Emulator.Classes;
using ECE486_PDP_8_Emulator;
using ECE486_PDP_8_Emulator.Instructions;

namespace ECE486_PDP_8_Emulator_Tests.InstructionTests
{
    [TestClass]
    public class IszTests
    {
        [TestMethod]
        public void TestIszArgumentPassthrough()
        {
            InstructionItems TestItems = new InstructionItems()
            {
                accumulatorOctal = 1,
                LinkBit = true,
                MemoryAddress = 123,
                MemoryValueOctal = 4095,
                pcCounter = 123,
                InstructionRegister = 7402,
                OsrSwitchBits = 0xFFF
            };


            InstructionResult ExpectedItems = new InstructionResult()
            {
                accumulatorOctal = 1,
                LinkBit = true,
                MemoryAddress = 123,
                MemoryValueOctal = 0,
                pcCounter = 125,
                InstructionRegister = 7402,
                SetMemValue = true,
                 BranchTaken = true,
                 BranchType = Constants.BranchType.Conditional
                
            };

            IInstruction TestIszInstruction = new IszInstruction();

            InstructionResult ActualResult = TestIszInstruction.ExecuteInstruction(TestItems);


            Assert.AreEqual(ExpectedItems.accumulatorOctal, ActualResult.accumulatorOctal);
            Assert.AreEqual(ExpectedItems.LinkBit, ActualResult.LinkBit);
            Assert.AreEqual(ExpectedItems.MemoryAddress, ActualResult.MemoryAddress);
            Assert.AreEqual(ExpectedItems.MemoryValueOctal, ActualResult.MemoryValueOctal);
            Assert.AreEqual(ExpectedItems.pcCounter, ActualResult.pcCounter);
            Assert.AreEqual(ExpectedItems.InstructionRegister, ActualResult.InstructionRegister);
            Assert.AreEqual(ExpectedItems.SetMemValue, ActualResult.SetMemValue);
            Assert.AreEqual(true, ActualResult.BranchTaken);
            Assert.AreEqual(ExpectedItems.BranchType, ActualResult.BranchType);
            Assert.AreEqual(0xFFF, ActualResult.OsrSwitchBits);

            //Test instruction properties
            Assert.AreEqual(2, TestIszInstruction.clockCycles);
            Assert.AreEqual(Constants.OpCode.ISZ, TestIszInstruction.instructionType);

        }


        [TestMethod]
        public void TestIszOperation()
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
                MemoryValueOctal = 1,
                pcCounter = 5650,
                InstructionRegister = 7402,
                SetMemValue = false

            };

            IInstruction TestIszInstruction = new IszInstruction();

            InstructionResult ActualResult = TestIszInstruction.ExecuteInstruction(TestItems);

            Assert.AreEqual(0, ActualResult.accumulatorOctal);

            /* Test cases increment mem Value by 1, skip next instruction ( PC + 2 ) if Value = 0 
             * Else all cases increment PC + 1 */

            //Test 0s: result mem Value = 1
            TestItems.MemoryValueOctal = Convert.ToInt32(0.ToString(), 8);
            TestItems.pcCounter = Convert.ToInt32(0.ToString(), 8);

            ActualResult = TestIszInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(1.ToString(), 8), ActualResult.MemoryValueOctal);
            Assert.AreEqual(Convert.ToInt32(1.ToString(), 8), ActualResult.pcCounter);


            //Test 1: results mem Valu = 2
            TestItems.MemoryValueOctal = Convert.ToInt32(1.ToString(), 8);
            TestItems.pcCounter = Convert.ToInt32(1.ToString(), 8);

            ActualResult = TestIszInstruction.ExecuteInstruction(TestItems);

            Assert.AreEqual(Convert.ToInt32(2.ToString(), 8), ActualResult.MemoryValueOctal);
            Assert.AreEqual(Convert.ToInt32(2.ToString(), 8), ActualResult.pcCounter);


            //Test all 1's: result mem = 0, increment PC + 2
            TestItems.MemoryValueOctal = Convert.ToInt32(7777.ToString(), 8);
            TestItems.pcCounter = Convert.ToInt32(7777.ToString(), 8);

            ActualResult = TestIszInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(0.ToString(), 8), ActualResult.MemoryValueOctal);
            Assert.AreEqual(Convert.ToInt32(1.ToString(), 8), ActualResult.pcCounter);


            //Test all 1's: result mem = 0, increment PC + 2, check PC can = 0
            TestItems.MemoryValueOctal = Convert.ToInt32(7777.ToString(), 8);
            TestItems.pcCounter = Convert.ToInt32(7776.ToString(), 8); 

            ActualResult = TestIszInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(0.ToString(), 8), ActualResult.MemoryValueOctal);
            Assert.AreEqual(Convert.ToInt32(0.ToString(), 8), ActualResult.pcCounter);


            //Test all 1's: result mem = 0, increment PC + 2
            TestItems.MemoryValueOctal = Convert.ToInt32(7777.ToString(), 8);
            TestItems.pcCounter = Convert.ToInt32(0.ToString(), 8);

            ActualResult = TestIszInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(0.ToString(), 8), ActualResult.MemoryValueOctal);
            Assert.AreEqual(Convert.ToInt32(2.ToString(), 8), ActualResult.pcCounter);


            //Test alternating pattern of 1s and 0s - Start with 1, increment PC + 1
            TestItems.MemoryValueOctal = Convert.ToInt32(5252.ToString(), 8);
            TestItems.pcCounter = Convert.ToInt32(770.ToString(), 8);

            ActualResult = TestIszInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(5253.ToString(), 8), ActualResult.MemoryValueOctal);
            Assert.AreEqual(Convert.ToInt32(771.ToString(), 8), ActualResult.pcCounter);


            //Test alternating pattern of 1s and 0s - Start with 0, increment PC + 1
            TestItems.MemoryValueOctal = Convert.ToInt32(2525.ToString(), 8);
            TestItems.pcCounter = Convert.ToInt32(7777.ToString(), 8);

            ActualResult = TestIszInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(2526.ToString(), 8), ActualResult.MemoryValueOctal);
            Assert.AreEqual(Convert.ToInt32(0.ToString(), 8), ActualResult.pcCounter);


            //Test 0 for first and last octals, ensure not losing octals on ends, increment PC + 1
            TestItems.MemoryValueOctal = Convert.ToInt32(0770.ToString(), 8);
            TestItems.pcCounter = Convert.ToInt32(4441.ToString(), 8);

            ActualResult = TestIszInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(0771.ToString(), 8), ActualResult.MemoryValueOctal);
            Assert.AreEqual(Convert.ToInt32(4442.ToString(), 8), ActualResult.pcCounter);


            //Test end 1's,  increment PC + 1
            TestItems.MemoryValueOctal = Convert.ToInt32(4001.ToString(), 8);
            TestItems.pcCounter = Convert.ToInt32(1111.ToString(), 8);

            ActualResult = TestIszInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(4002.ToString(), 8), ActualResult.MemoryValueOctal);
            Assert.AreEqual(Convert.ToInt32(1112.ToString(), 8), ActualResult.pcCounter);


            //Test last octal 0,  increment PC + 1
            TestItems.MemoryValueOctal = Convert.ToInt32(70.ToString(), 8);
            TestItems.pcCounter = Convert.ToInt32(2222.ToString(), 8);

            ActualResult = TestIszInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(71.ToString(), 8), ActualResult.MemoryValueOctal);
            Assert.AreEqual(Convert.ToInt32(2223.ToString(), 8), ActualResult.pcCounter);


            //Test last 2 octals 0,  increment PC + 1
            TestItems.MemoryValueOctal = Convert.ToInt32(700.ToString(), 8);
            TestItems.pcCounter = Convert.ToInt32(1010.ToString(), 8);

            ActualResult = TestIszInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(701.ToString(), 8), ActualResult.MemoryValueOctal);
            Assert.AreEqual(Convert.ToInt32(1011.ToString(), 8), ActualResult.pcCounter);


            //Test last octal 1,  increment PC + 1
            TestItems.MemoryValueOctal = Convert.ToInt32(771.ToString(), 8);
            TestItems.pcCounter = Convert.ToInt32(0101.ToString(), 8);

            ActualResult = TestIszInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(772.ToString(), 8), ActualResult.MemoryValueOctal);
            Assert.AreEqual(Convert.ToInt32(0102.ToString(), 8), ActualResult.pcCounter);


            //Test last 3 octals 7, end case, 1777,  increment PC + 1
            TestItems.MemoryValueOctal = Convert.ToInt32(777.ToString(), 8);
            TestItems.pcCounter = Convert.ToInt32(5555.ToString(), 8);

            ActualResult = TestIszInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(1000.ToString(), 8), ActualResult.MemoryValueOctal);
            Assert.AreEqual(Convert.ToInt32(5556.ToString(), 8), ActualResult.pcCounter);

        }
    }
}

