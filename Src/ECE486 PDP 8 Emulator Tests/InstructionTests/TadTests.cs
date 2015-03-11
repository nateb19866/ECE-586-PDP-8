using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using ECE486_PDP_8_Emulator.Classes;
using ECE486_PDP_8_Emulator;
using ECE486_PDP_8_Emulator.Instructions;

namespace ECE486_PDP_8_Emulator_Tests.InstructionTests
{
    [TestClass]
    public class TadTests
    {
        [TestMethod]
        public void TestTadArgumentPassthrough()
        {
            InstructionItems TestItems = new InstructionItems()
            {
                accumulatorOctal = 0000,
                LinkBit = true,
                MemoryAddress = 0,
                MemoryValueOctal = 0,
                pcCounter = 1,
                InstructionRegister = 7402,
                OsrSwitchBits = 0xFFF
            };


            InstructionResult ExpectedItems = new InstructionResult()
            {
                accumulatorOctal = 0000,
                LinkBit = true,
                MemoryAddress = 0,
                MemoryValueOctal = 0,
                pcCounter = 2,
                InstructionRegister = 7402,
                SetMemValue = false
            };

            IInstruction TestTadInstruction = new TadInstruction();

            InstructionResult ActualResult = TestTadInstruction.ExecuteInstruction(TestItems);


            Assert.AreEqual(ExpectedItems.accumulatorOctal, ActualResult.accumulatorOctal);
            Assert.AreEqual(ExpectedItems.LinkBit, ActualResult.LinkBit);
            Assert.AreEqual(ExpectedItems.MemoryAddress, ActualResult.MemoryAddress);
            Assert.AreEqual(ExpectedItems.MemoryValueOctal, ActualResult.MemoryValueOctal);
            Assert.AreEqual(ExpectedItems.pcCounter, ActualResult.pcCounter);
            Assert.AreEqual(ExpectedItems.InstructionRegister, ActualResult.InstructionRegister);
            Assert.AreEqual(ExpectedItems.SetMemValue, ActualResult.SetMemValue);
            Assert.AreEqual(0xFFF, ActualResult.OsrSwitchBits);

            //Test instruction properties
            Assert.AreEqual(2, TestTadInstruction.clockCycles);
            Assert.AreEqual(Constants.OpCode.TAD, TestTadInstruction.instructionType);

        }


        [TestMethod]
        public void TestTadOperation()
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

            IInstruction TestTadInstruction = new TadInstruction();

            InstructionResult ActualResult = TestTadInstruction.ExecuteInstruction(TestItems);

            Assert.AreEqual(0, ActualResult.accumulatorOctal);

            /* Test cases Add AC with mem Value, places this into AC
             * If Carry out, complement Link
             * Carry out occurs when adding 2 neg's or overflow of 2 pos's
             *Test for Link and EA update correctly, PC always add 1 */

          
            //Test (1) Carry out complements link, ensure mem Value remains
            TestItems.MemoryValueOctal = Convert.ToInt32(7777.ToString(), 8);
            TestItems.accumulatorOctal = Convert.ToInt32(7777.ToString(), 8);
            TestItems.LinkBit = false;
            TestItems.pcCounter = Convert.ToInt32(0.ToString(), 8);


            ActualResult = TestTadInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(7776.ToString(), 8), ActualResult.accumulatorOctal);
            Assert.AreEqual(Convert.ToInt32(7777.ToString(), 8), ActualResult.MemoryValueOctal);
            Assert.AreEqual(true, ActualResult.LinkBit);
            Assert.AreEqual(Convert.ToInt32(1.ToString(), 8), ActualResult.pcCounter);


            //Test (2) Carry out complements link, check link change
            TestItems.MemoryValueOctal = Convert.ToInt32(7777.ToString(), 8);
            TestItems.accumulatorOctal = Convert.ToInt32(7777.ToString(), 8);
            TestItems.LinkBit = true;
            TestItems.pcCounter = Convert.ToInt32(7777.ToString(), 8);

            ActualResult = TestTadInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(7776.ToString(), 8), ActualResult.accumulatorOctal);
            Assert.AreEqual(Convert.ToInt32(7777.ToString(), 8), ActualResult.MemoryValueOctal);
            Assert.AreEqual(false, ActualResult.LinkBit);
            Assert.AreEqual(Convert.ToInt32(0.ToString(), 8), ActualResult.pcCounter);


            //Test (3) negative AC does not produce carry out, link remains
            TestItems.MemoryValueOctal = Convert.ToInt32(7777.ToString(), 8);
            TestItems.accumulatorOctal = Convert.ToInt32(0000.ToString(), 8);
            TestItems.LinkBit = true;
            TestItems.pcCounter = Convert.ToInt32(7070.ToString(), 8);

            ActualResult = TestTadInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(7777.ToString(), 8), ActualResult.accumulatorOctal);
            Assert.AreEqual(Convert.ToInt32(7777.ToString(), 8), ActualResult.MemoryValueOctal);
            Assert.AreEqual(true, ActualResult.LinkBit);
            Assert.AreEqual(Convert.ToInt32(7071.ToString(), 8), ActualResult.pcCounter);


            //Test (4) negative AC does not produce carry out, link remains
            TestItems.MemoryValueOctal = Convert.ToInt32(7777.ToString(), 8);
            TestItems.accumulatorOctal = Convert.ToInt32(0000.ToString(), 8);
            TestItems.LinkBit = false;
            TestItems.pcCounter = Convert.ToInt32(2.ToString(), 8);

            ActualResult = TestTadInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(7777.ToString(), 8), ActualResult.accumulatorOctal);
            Assert.AreEqual(Convert.ToInt32(7777.ToString(), 8), ActualResult.MemoryValueOctal);
            Assert.AreEqual(false, ActualResult.LinkBit);
            Assert.AreEqual(Convert.ToInt32(3.ToString(), 8), ActualResult.pcCounter);


            //Test (5) switch EA and AC above, No carry out, link = link
            TestItems.MemoryValueOctal = Convert.ToInt32(0000.ToString(), 8);
            TestItems.accumulatorOctal = Convert.ToInt32(7777.ToString(), 8);
            TestItems.LinkBit = true;
            TestItems.pcCounter = Convert.ToInt32(2525.ToString(), 8);

            ActualResult = TestTadInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(7777.ToString(), 8), ActualResult.accumulatorOctal);
            Assert.AreEqual(Convert.ToInt32(0000.ToString(), 8), ActualResult.MemoryValueOctal);
            Assert.AreEqual(true, ActualResult.LinkBit);
            Assert.AreEqual(Convert.ToInt32(2526.ToString(), 8), ActualResult.pcCounter);


            //Test (6) switch EA and AC above, No carry out, link = link
            TestItems.MemoryValueOctal = Convert.ToInt32(0000.ToString(), 8);
            TestItems.accumulatorOctal = Convert.ToInt32(7777.ToString(), 8);
            TestItems.LinkBit = false;
            TestItems.pcCounter = Convert.ToInt32(4000.ToString(), 8);

            ActualResult = TestTadInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(7777.ToString(), 8), ActualResult.accumulatorOctal);
            Assert.AreEqual(Convert.ToInt32(0000.ToString(), 8), ActualResult.MemoryValueOctal);
            Assert.AreEqual(false, ActualResult.LinkBit);
            Assert.AreEqual(Convert.ToInt32(4001.ToString(), 8), ActualResult.pcCounter);


            //Test (7) test AC for carry out 
            TestItems.MemoryValueOctal = Convert.ToInt32(7000.ToString(), 8);
            TestItems.accumulatorOctal = Convert.ToInt32(7777.ToString(), 8);
            TestItems.LinkBit = true;
            TestItems.pcCounter = Convert.ToInt32(0101.ToString(), 8);

            ActualResult = TestTadInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(6777.ToString(), 8), ActualResult.accumulatorOctal);
            Assert.AreEqual(Convert.ToInt32(7000.ToString(), 8), ActualResult.MemoryValueOctal);
            Assert.AreEqual(false, ActualResult.LinkBit);
            Assert.AreEqual(Convert.ToInt32(0102.ToString(), 8), ActualResult.pcCounter);


            //Test (8) test AC for carry out using opposite link
            TestItems.MemoryValueOctal = Convert.ToInt32(7000.ToString(), 8);
            TestItems.accumulatorOctal = Convert.ToInt32(7777.ToString(), 8);
            TestItems.LinkBit = false;
            TestItems.pcCounter = Convert.ToInt32(1010.ToString(), 8);

            ActualResult = TestTadInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(6777.ToString(), 8), ActualResult.accumulatorOctal);
            Assert.AreEqual(Convert.ToInt32(7000.ToString(), 8), ActualResult.MemoryValueOctal);
            Assert.AreEqual(true, ActualResult.LinkBit);
            Assert.AreEqual(Convert.ToInt32(1011.ToString(), 8), ActualResult.pcCounter);

            //Test (9) test AC for carry out with 2 positive values, link start false
            TestItems.MemoryValueOctal = Convert.ToInt32(6000.ToString(), 8);
            TestItems.accumulatorOctal = Convert.ToInt32(6000.ToString(), 8);
            TestItems.LinkBit = false;
            TestItems.pcCounter = Convert.ToInt32(1010.ToString(), 8);

            ActualResult = TestTadInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(4000.ToString(), 8), ActualResult.accumulatorOctal);
            Assert.AreEqual(Convert.ToInt32(6000.ToString(), 8), ActualResult.MemoryValueOctal);
            Assert.AreEqual(true, ActualResult.LinkBit);
            Assert.AreEqual(Convert.ToInt32(1011.ToString(), 8), ActualResult.pcCounter);

            //Test (10) test AC for carry out with 2 positive values, linke start true
            TestItems.MemoryValueOctal = Convert.ToInt32(6777.ToString(), 8);
            TestItems.accumulatorOctal = Convert.ToInt32(6777.ToString(), 8);
            TestItems.LinkBit = true;
            TestItems.pcCounter = Convert.ToInt32(1010.ToString(), 8);

            ActualResult = TestTadInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(5776.ToString(), 8), ActualResult.accumulatorOctal);
            Assert.AreEqual(Convert.ToInt32(6777.ToString(), 8), ActualResult.MemoryValueOctal);
            Assert.AreEqual(false, ActualResult.LinkBit);
            Assert.AreEqual(Convert.ToInt32(1011.ToString(), 8), ActualResult.pcCounter);

        }
    }
}

