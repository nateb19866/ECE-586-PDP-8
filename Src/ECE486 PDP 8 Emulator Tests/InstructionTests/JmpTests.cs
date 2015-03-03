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
                MemoryValueOctal = Convert.ToInt32(7777.ToString(), 8),
                pcCounter = 5649,
                InstructionRegister = Convert.ToInt32(7402.ToString(), 8)
            };


            InstructionResult ExpectedItems = new InstructionResult()
            {
                accumulatorOctal = 0000,
                LinkBit = true,
                MemoryAddress = 0,
                MemoryValueOctal = Convert.ToInt32(7777.ToString(), 8),
                pcCounter = 5650,
                InstructionRegister = Convert.ToInt32(7402.ToString(), 8),
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
                InstructionRegister = Convert.ToInt32(7402.ToString(), 8)
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

            IInstruction TestJmpInstruction = new JmpInstruction();

            InstructionResult ActualResult = TestJmpInstruction.ExecuteInstruction(TestItems);

            Assert.AreEqual(0, ActualResult.accumulatorOctal);

            /* Test cases place octals from EA into PC and observe PC update */

            //Test 0
            TestItems.MemoryAddress = Convert.ToInt32(0.ToString(), 8);
            TestItems.pcCounter = Convert.ToInt32(1.ToString(), 8); ;

            ActualResult = TestJmpInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(0.ToString(), 8), ActualResult.pcCounter);

            //Test 1

            TestItems.MemoryAddress = Convert.ToInt32(1.ToString(), 8);
            TestItems.pcCounter = Convert.ToInt32(10.ToString(), 8); ;

            ActualResult = TestJmpInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(1.ToString(), 8), ActualResult.pcCounter);

            //Test all 1's
            TestItems.MemoryAddress = Convert.ToInt32(7777.ToString(), 8);
            TestItems.pcCounter = Convert.ToInt32(7770.ToString(), 8); ;

            ActualResult = TestJmpInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(7777.ToString(), 8), ActualResult.pcCounter);


            //Test alternating pattern of 1s and 0s - Start with 1
            TestItems.MemoryAddress = Convert.ToInt32(5252.ToString(), 8);
            TestItems.pcCounter = Convert.ToInt32(0.ToString(), 8); ;

            ActualResult = TestJmpInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(5252.ToString(), 8), ActualResult.pcCounter);


            //Test alternating pattern of 1s and 0s - Start with 0
            TestItems.MemoryAddress = Convert.ToInt32(2525.ToString(), 8);
            TestItems.pcCounter = Convert.ToInt32(1.ToString(), 8); ;

            ActualResult = TestJmpInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(2525.ToString(), 8), ActualResult.pcCounter);

           
            //Test 0 for first and last octals, ensure not losing octals on ends
            TestItems.MemoryAddress = Convert.ToInt32(0770.ToString(), 8);
            TestItems.pcCounter = Convert.ToInt32(2525.ToString(), 8); ;

            ActualResult = TestJmpInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(0770.ToString(), 8), ActualResult.pcCounter);


            //Test end 1's
            TestItems.MemoryAddress = Convert.ToInt32(4001.ToString(), 8);
            TestItems.pcCounter = Convert.ToInt32(777.ToString(), 8); ;

            ActualResult = TestJmpInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(4001.ToString(), 8), ActualResult.pcCounter);


            //Test last octal 0
            TestItems.MemoryAddress = Convert.ToInt32(70.ToString(), 8);
            TestItems.pcCounter = Convert.ToInt32(7777.ToString(), 8); ;

            ActualResult = TestJmpInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(70.ToString(), 8), ActualResult.pcCounter);


            //Test last 2 octals 0
            TestItems.MemoryAddress = Convert.ToInt32(700.ToString(), 8);
            TestItems.pcCounter = Convert.ToInt32(1111.ToString(), 8); ;

            ActualResult = TestJmpInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(700.ToString(), 8), ActualResult.pcCounter);


            //Test last octal 1
            TestItems.MemoryAddress = Convert.ToInt32(7771.ToString(), 8);
            TestItems.pcCounter = Convert.ToInt32(4444.ToString(), 8); ;

            ActualResult = TestJmpInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(7771.ToString(), 8), ActualResult.pcCounter);


            //Test last 3 octals 7, end case, 1777
            TestItems.MemoryAddress = Convert.ToInt32(777.ToString(), 8);
            TestItems.pcCounter = Convert.ToInt32(3333.ToString(), 8); ;

            ActualResult = TestJmpInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(777.ToString(), 8), ActualResult.pcCounter);

        }
    }
}

