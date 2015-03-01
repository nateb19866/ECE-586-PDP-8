using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using ECE486_PDP_8_Emulator.Classes;
using ECE486_PDP_8_Emulator;
using ECE486_PDP_8_Emulator.Instructions;

namespace ECE486_PDP_8_Emulator_Tests.InstructionTests
{
    [TestClass]
    public class JmpTests
    {
        [TestMethod]
        public void TestJmpArgumentPassthrough()
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

            IInstruction TestJmpInstruction = new IotInstruction();

            InstructionResult ActualResult = TestJmpInstruction.ExecuteInstruction(TestItems);


            Assert.AreEqual(ExpectedItems.accumulatorOctal, ActualResult.accumulatorOctal);
            Assert.AreEqual(ExpectedItems.LinkBit, ActualResult.LinkBit);
            Assert.AreEqual(ExpectedItems.MemoryAddress, ActualResult.MemoryAddress);
            Assert.AreEqual(ExpectedItems.MemoryValueOctal, ActualResult.MemoryValueOctal);
            Assert.AreEqual(ExpectedItems.pcCounter, ActualResult.pcCounter);
            Assert.AreEqual(ExpectedItems.InstructionRegister, ActualResult.InstructionRegister);
            Assert.AreEqual(ExpectedItems.SetMemValue, ActualResult.SetMemValue);
        }


        [TestMethod]
        public void TestJmpOperation()
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

            IInstruction TestJmpInstruction = new IotInstruction();

            InstructionResult ActualResult = TestJmpInstruction.ExecuteInstruction(TestItems);

            Assert.AreEqual(0, ActualResult.accumulatorOctal);

            /* Test cases place octals from EA into PC and observe PC update */

            //Test 0
            TestItems.MemoryValueOctal = 0;
            TestItems.pcCounter = 1;

            ActualResult = TestJmpInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(0, ActualResult.pcCounter);

            //Test 1
            TestItems.MemoryValueOctal = 1;
            TestItems.pcCounter = 10;

            ActualResult = TestJmpInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(0, ActualResult.pcCounter);


            //Test all 1's
            TestItems.MemoryValueOctal = 7777;
            TestItems.pcCounter = 7770;

            ActualResult = TestJmpInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(0, ActualResult.pcCounter);


            //Test alternating pattern of 1s and 0s - Start with 1
            TestItems.MemoryValueOctal = 5252;
            TestItems.pcCounter = 0;

            ActualResult = TestJmpInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(0, ActualResult.pcCounter);


            //Test alternating pattern of 1s and 0s - Start with 0
            TestItems.MemoryValueOctal = 2525;
            TestItems.pcCounter = 1;

            ActualResult = TestJmpInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(0, ActualResult.pcCounter);


            //Test 0 for first and last octals, ensure not losing octals on ends
            TestItems.MemoryValueOctal = 0770;
            TestItems.pcCounter = 2525;

            ActualResult = TestJmpInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(0, ActualResult.pcCounter);


            //Test end 1's
            TestItems.MemoryValueOctal = 4001;
            TestItems.pcCounter = 777;

            ActualResult = TestJmpInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(0, ActualResult.pcCounter);


            //Test last octal 0
            TestItems.MemoryValueOctal = 70;
            TestItems.pcCounter = 7777;


            ActualResult = TestJmpInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(0, ActualResult.pcCounter);

            //Test last 2 octals 0
            TestItems.MemoryValueOctal = 700;
            TestItems.pcCounter = 1111;

            ActualResult = TestJmpInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(0, ActualResult.pcCounter);

            //Test last octal 1
            TestItems.MemoryValueOctal = 7771;
            TestItems.pcCounter = 4444;

            ActualResult = TestJmpInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(0, ActualResult.pcCounter);

            //Test last 3 octals 7, end case, 1777
            TestItems.MemoryValueOctal = 777;
            TestItems.pcCounter = 3333;

            ActualResult = TestJmpInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(0, ActualResult.pcCounter);

        }
    }
}

