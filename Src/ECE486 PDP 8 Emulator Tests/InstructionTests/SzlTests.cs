using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ECE486_PDP_8_Emulator.Classes;
using ECE486_PDP_8_Emulator;
using ECE486_PDP_8_Emulator.Instructions;

namespace ECE486_PDP_8_Emulator_Tests.InstructionTests
{
    [TestClass]
    public class SzlTests
    {
        [TestMethod]
        public void TestM2SZLArgumentPassthrough()
        {

            InstructionItems TestItems = new InstructionItems()

            {
                accumulatorOctal = 0000,
                LinkBit = false,
                MemoryAddress = 0,
                MemoryValueOctal = Convert.ToInt32(7777.ToString(), 8),
                pcCounter = 1300,
                InstructionRegister = Convert.ToInt32(7430.ToString(), 8)


            };


            InstructionResult ExpectedItems = new InstructionResult()
            {
                accumulatorOctal = 0000,
                LinkBit = false,
                MemoryAddress = 0,
                MemoryValueOctal = Convert.ToInt32(7777.ToString(), 8),
                pcCounter = 1302,
                InstructionRegister = Convert.ToInt32(7430.ToString(), 8),
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
        public void TestM2SZLOperation()
        {
            //Need to initialize the instruction items for the first time


            InstructionItems TestItems = new InstructionItems()
            {
                accumulatorOctal = 0000,
                LinkBit = true,
                MemoryAddress = 0,

                MemoryValueOctal = 0000,
                pcCounter = 1300,
                InstructionRegister = Convert.ToInt32(7430.ToString(), 8)
            };


            InstructionResult ExpectedItems = new InstructionResult()
            {
                accumulatorOctal = 0000,
                LinkBit = true,
                MemoryAddress = 0,
                MemoryValueOctal = 0000,
                pcCounter = 1301,
                InstructionRegister = Convert.ToInt32(7430.ToString(), 8),
                SetMemValue = false
            };

            IInstruction TestOprInstruction = new OprInstruction();

            InstructionResult ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);

            Assert.AreEqual(0, ActualResult.accumulatorOctal);


            //link bit - 1 PC + 1

            TestItems.LinkBit = true;
            TestItems.pcCounter = 0;


            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(1, ActualResult.pcCounter);

            //link - 0 PC gets incremented by 2 cause link bit is non zero (1)

            TestItems.LinkBit = false;
            TestItems.pcCounter = 0;


            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(2, ActualResult.pcCounter);

            //link - 1, PC - 7770 => PCout - 7771

            TestItems.LinkBit = true;
            TestItems.pcCounter = Convert.ToInt32(7770.ToString(), 8);


            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(7771.ToString(), 8), ActualResult.pcCounter);

            //Test lowest octal value
            TestItems.LinkBit = false;
            TestItems.pcCounter = Convert.ToInt32(7770.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(7772.ToString(), 8), ActualResult.pcCounter);

            //Test 1st octal value
            TestItems.LinkBit = true;
            TestItems.pcCounter = Convert.ToInt32(4001.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(4002.ToString(), 8), ActualResult.pcCounter);

            //test 2nd octal value
            TestItems.LinkBit = false;
            TestItems.pcCounter = Convert.ToInt32(4002.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(4004.ToString(), 8), ActualResult.pcCounter);

            //test 3rd octal value
            TestItems.LinkBit = false;
            TestItems.pcCounter = Convert.ToInt32(240.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(242.ToString(), 8), ActualResult.pcCounter);

            //test 4th octal value
            TestItems.LinkBit = false;
            TestItems.pcCounter = Convert.ToInt32(2525.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(2527.ToString(), 8), ActualResult.pcCounter);

            //test 5th octal value
            TestItems.LinkBit = true;
            TestItems.pcCounter = Convert.ToInt32(7777.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(0.ToString(), 8), ActualResult.pcCounter);

            //test 6th octal value
            TestItems.LinkBit = false;
            TestItems.pcCounter = Convert.ToInt32(7777.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(1.ToString(), 8), ActualResult.pcCounter);


        }
    }
}
