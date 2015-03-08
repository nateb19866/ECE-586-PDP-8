using System;
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
                accumulatorOctal = 1,
                LinkBit = true,
                MemoryAddress = 100,
                MemoryValueOctal = 7777,
                pcCounter = 1,
                InstructionRegister = 7402,
                OsrSwitchBits = 0xFFF


            };


            InstructionResult ExpectedItems = new InstructionResult()
            {
                accumulatorOctal = 0000,
                LinkBit = true,
                MemoryAddress = 100,
                MemoryValueOctal = 1,
                pcCounter = 2,
                InstructionRegister = 7402,
                SetMemValue = true
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
            Assert.AreEqual(false, ActualResult.BranchTaken);
            Assert.AreEqual(0xFFF, ActualResult.OsrSwitchBits);



            //Test instruction properties
            Assert.AreEqual(2, TestDcaInstruction.clockCycles);
            Assert.AreEqual(Constants.OpCode.DCA, TestDcaInstruction.instructionType);
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

            /* Test cases place AC to Value, results will show Value update to equal AC 
               For all test cases: AC will be 0 and PC + 1 */

            //Test 0: Value = AC, AC = 0, PC + 1
            TestItems.accumulatorOctal = 0;
            TestItems.MemoryValueOctal = Convert.ToInt32(7777.ToString(), 8);
            TestItems.pcCounter = Convert.ToInt32(0.ToString(), 8);

            ActualResult = TestDcaInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(0, ActualResult.MemoryValueOctal);
            Assert.AreEqual(0, ActualResult.accumulatorOctal);
            Assert.AreEqual(1, ActualResult.pcCounter);


            //Test for Value updates to alternating 1's and 0's
            TestItems.accumulatorOctal = Convert.ToInt32(5252.ToString(), 8);
            TestItems.MemoryValueOctal = Convert.ToInt32(7777.ToString(), 8);
            TestItems.pcCounter = Convert.ToInt32(7777.ToString(), 8);

            ActualResult = TestDcaInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(0.ToString(), 8), ActualResult.accumulatorOctal);
            Assert.AreEqual(Convert.ToInt32(5252.ToString(), 8), ActualResult.MemoryValueOctal);
            Assert.AreEqual(Convert.ToInt32(0.ToString(), 8), ActualResult.pcCounter);


            //Test for Value updates to all 1's
            TestItems.accumulatorOctal = Convert.ToInt32(7777.ToString(), 8);
            TestItems.MemoryValueOctal = Convert.ToInt32(1111.ToString(), 8);
            TestItems.pcCounter = Convert.ToInt32(7070.ToString(), 8);

            ActualResult = TestDcaInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(0.ToString(), 8), ActualResult.accumulatorOctal);
            Assert.AreEqual(Convert.ToInt32(7777.ToString(), 8), ActualResult.MemoryValueOctal);
            Assert.AreEqual(Convert.ToInt32(7071.ToString(), 8), ActualResult.pcCounter);
            

            //Test alternating pattern of 1s and 0s - Start with 1
            TestItems.accumulatorOctal = Convert.ToInt32(5252.ToString(), 8);
            TestItems.MemoryValueOctal = Convert.ToInt32(2525.ToString(), 8);
            TestItems.pcCounter = Convert.ToInt32(2.ToString(), 8);

            
            ActualResult = TestDcaInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(0.ToString(), 8), ActualResult.accumulatorOctal);
            Assert.AreEqual(Convert.ToInt32(5252.ToString(), 8), ActualResult.MemoryValueOctal);
            Assert.AreEqual(Convert.ToInt32(3.ToString(), 8), ActualResult.pcCounter);
           

            //Test alternating pattern of 1s and 0s - Start with 0
            TestItems.accumulatorOctal = Convert.ToInt32(2525.ToString(), 8);
            TestItems.MemoryValueOctal = Convert.ToInt32(5252.ToString(), 8);
            TestItems.pcCounter = Convert.ToInt32(2525.ToString(), 8);
           
            ActualResult = TestDcaInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(0.ToString(), 8), ActualResult.accumulatorOctal);
            Assert.AreEqual(Convert.ToInt32(2525.ToString(), 8), ActualResult.MemoryValueOctal);
            Assert.AreEqual(Convert.ToInt32(2526.ToString(), 8), ActualResult.pcCounter);
           

            //Test 0 for first and last octals, ensure not losing octals on ends
            TestItems.accumulatorOctal = Convert.ToInt32(0770.ToString(), 8);
            TestItems.MemoryValueOctal = Convert.ToInt32(2222.ToString(), 8);
            TestItems.pcCounter = Convert.ToInt32(4000.ToString(), 8);
           
            ActualResult = TestDcaInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(0.ToString(), 8), ActualResult.accumulatorOctal);
            Assert.AreEqual(Convert.ToInt32(0770.ToString(), 8), ActualResult.MemoryValueOctal);
            Assert.AreEqual(Convert.ToInt32(4001.ToString(), 8), ActualResult.pcCounter);


            //Test end 1's, ensure 1 bit on ends update
             TestItems.accumulatorOctal = Convert.ToInt32(4001.ToString(), 8);
             TestItems.MemoryValueOctal = Convert.ToInt32(0770.ToString(), 8);
             TestItems.pcCounter = Convert.ToInt32(0101.ToString(), 8);
           
            ActualResult = TestDcaInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(0.ToString(), 8), ActualResult.accumulatorOctal);
            Assert.AreEqual(Convert.ToInt32(4001.ToString(), 8), ActualResult.MemoryValueOctal);
            Assert.AreEqual(Convert.ToInt32(0102.ToString(), 8), ActualResult.pcCounter);


            //Test that Value updates to 0, given all 1's to begin
            TestItems.accumulatorOctal = Convert.ToInt32(0.ToString(), 8);
            TestItems.MemoryValueOctal = Convert.ToInt32(7777.ToString(), 8);
            TestItems.pcCounter = Convert.ToInt32(1111.ToString(), 8);

            ActualResult = TestDcaInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(0.ToString(), 8), ActualResult.accumulatorOctal);
            Assert.AreEqual(Convert.ToInt32(0.ToString(), 8), ActualResult.MemoryValueOctal);
            Assert.AreEqual(Convert.ToInt32(1112.ToString(), 8), ActualResult.pcCounter);


            //Test Value updates to 1
            TestItems.accumulatorOctal = Convert.ToInt32(1.ToString(), 8);
            TestItems.MemoryValueOctal = Convert.ToInt32(7000.ToString(), 8);
            TestItems.pcCounter = Convert.ToInt32(1010.ToString(), 8);
           

            ActualResult = TestDcaInstruction.ExecuteInstruction(TestItems);

            Assert.AreEqual(Convert.ToInt32(0.ToString(), 8), ActualResult.accumulatorOctal);
            Assert.AreEqual(Convert.ToInt32(1.ToString(), 8), ActualResult.MemoryValueOctal);
            Assert.AreEqual(Convert.ToInt32(1011.ToString(), 8), ActualResult.pcCounter);

        }
    }
}

