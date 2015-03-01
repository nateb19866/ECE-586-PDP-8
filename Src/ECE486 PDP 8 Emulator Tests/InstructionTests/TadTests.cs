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

            IInstruction TestTadInstruction = new IotInstruction();

            InstructionResult ActualResult = TestTadInstruction.ExecuteInstruction(TestItems);


            Assert.AreEqual(ExpectedItems.accumulatorOctal, ActualResult.accumulatorOctal);
            Assert.AreEqual(ExpectedItems.LinkBit, ActualResult.LinkBit);
            Assert.AreEqual(ExpectedItems.MemoryAddress, ActualResult.MemoryAddress);
            Assert.AreEqual(ExpectedItems.MemoryValueOctal, ActualResult.MemoryValueOctal);
            Assert.AreEqual(ExpectedItems.pcCounter, ActualResult.pcCounter);
            Assert.AreEqual(ExpectedItems.InstructionRegister, ActualResult.InstructionRegister);
            Assert.AreEqual(ExpectedItems.SetMemValue, ActualResult.SetMemValue);
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

            /* Test cases place octals (1)  EA = EA + AC, (2) PC = EA + 1 */
            /* Test for Link and EA update correctly */

            //Test (1) Carry out complements link
            TestItems.MemoryValueOctal = 7777;
            TestItems.accumulatorOctal = 7777;
            TestItems.LinkBit = false;

            ActualResult = TestTadInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(0, ActualResult.MemoryValueOctal);
            Assert.AreEqual(0, ActualResult.LinkBit);


            //Test (2) Carry out complements link
            TestItems.MemoryValueOctal = 7777;
            TestItems.accumulatorOctal = 7777;
            TestItems.LinkBit = true;

            ActualResult = TestTadInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(0, ActualResult.MemoryValueOctal);
            Assert.AreEqual(0, ActualResult.LinkBit);


            //Test (3) No carry out, link = link
            TestItems.MemoryValueOctal = 7777;
            TestItems.accumulatorOctal = 0000;
            TestItems.LinkBit = true;

            ActualResult = TestTadInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(0, ActualResult.MemoryValueOctal);
            Assert.AreEqual(0, ActualResult.LinkBit);


            //Test (4) No carry out, link = link
            TestItems.MemoryValueOctal = 7777;
            TestItems.accumulatorOctal = 0000;
            TestItems.LinkBit = false;

            ActualResult = TestTadInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(0, ActualResult.MemoryValueOctal);
            Assert.AreEqual(0, ActualResult.LinkBit);

            //Test (5) switch EA and AC above, No carry out, link = link
            TestItems.MemoryValueOctal = 0000;
            TestItems.accumulatorOctal = 7777;
            TestItems.LinkBit = true;

            ActualResult = TestTadInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(0, ActualResult.MemoryValueOctal);
            Assert.AreEqual(0, ActualResult.LinkBit);


            //Test (6) switch EA and AC above, No carry out, link = link
            TestItems.MemoryValueOctal = 0000;
            TestItems.accumulatorOctal = 7777;
            TestItems.LinkBit = false;

            ActualResult = TestTadInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(0, ActualResult.MemoryValueOctal);
            Assert.AreEqual(0, ActualResult.LinkBit);


            //Test (7) test AC for carry out 
            TestItems.MemoryValueOctal = 7000;
            TestItems.accumulatorOctal = 7777;
            TestItems.LinkBit = false;

            ActualResult = TestTadInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(0, ActualResult.MemoryValueOctal);
            Assert.AreEqual(0, ActualResult.LinkBit);


            //Test (8) test AC for carry out 
            TestItems.MemoryValueOctal = 7000;
            TestItems.accumulatorOctal = 7777;
            TestItems.LinkBit = true;

            ActualResult = TestTadInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(0, ActualResult.MemoryValueOctal);
            Assert.AreEqual(0, ActualResult.LinkBit);

        }
    }
}

