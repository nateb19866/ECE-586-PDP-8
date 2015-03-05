using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ECE486_PDP_8_Emulator.Classes;
using ECE486_PDP_8_Emulator.Instructions;
using ECE486_PDP_8_Emulator;

namespace ECE486_PDP_8_Emulator_Tests.InstructionTests
{
    [TestClass]
    public class SzaTests
    {
        [TestMethod]
        public void TestM2SZAArgumentPassthrough()
        {
            InstructionItems TestItems = new InstructionItems()
            {
                accumulatorOctal = 0x100,
                LinkBit = true,
                MemoryAddress = 0,
                MemoryValueOctal = Convert.ToInt32(7777.ToString(), 8),
                pcCounter = 1400,
                InstructionRegister = Convert.ToInt32(7440.ToString(), 8)


            };


            InstructionResult ExpectedItems = new InstructionResult()
            {
                accumulatorOctal = 0x100,
                LinkBit = true,
                MemoryAddress = 0,
                MemoryValueOctal = Convert.ToInt32(7777.ToString(), 8),
                pcCounter = 1401,
                InstructionRegister = Convert.ToInt32(7440.ToString(), 8),
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
        public void TestM2SZAOperation()
        {
            //Need to initialize the instruction items for the first time


            InstructionItems TestItems = new InstructionItems()
            {
                accumulatorOctal = 0000,
                LinkBit = true,
                MemoryAddress = 0,

                MemoryValueOctal = 0000,
                pcCounter = 1400,
                InstructionRegister = Convert.ToInt32(7440.ToString(), 8)
            };


            InstructionResult ExpectedItems = new InstructionResult()
            {
                accumulatorOctal = 0000,
                LinkBit = true,
                MemoryAddress = 0,
                MemoryValueOctal = 0000,
                pcCounter = 1402,
                InstructionRegister = Convert.ToInt32(7440.ToString(), 8),
                SetMemValue = false
            };

            IInstruction TestOprInstruction = new OprInstruction();

            InstructionResult ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);

            Assert.AreEqual(0, ActualResult.accumulatorOctal);

            // NOTE: PC always increments to next instruction ( +1 ), it will only skip ( +2 ) if conditions are met

            //AC - 0, PC + 2
            TestItems.accumulatorOctal = Convert.ToInt32(0.ToString(), 8);
            TestItems.pcCounter = Convert.ToInt32(0.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(2.ToString(), 8), ActualResult.pcCounter);

            //AC - 1, PC + 1
            TestItems.accumulatorOctal = Convert.ToInt32(1.ToString(), 8);
            TestItems.pcCounter = Convert.ToInt32(0.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(1.ToString(), 8), ActualResult.pcCounter);


            //AC - 10, PC + 1
            TestItems.accumulatorOctal = Convert.ToInt32(10.ToString(), 8);
            TestItems.pcCounter = Convert.ToInt32(0.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(1.ToString(), 8), ActualResult.pcCounter);


            //AC is non zero PC + 1
            TestItems.accumulatorOctal = Convert.ToInt32(7777.ToString(), 8);
            TestItems.pcCounter = Convert.ToInt32(7770.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(7771.ToString(), 8), ActualResult.pcCounter);

            //Test 1st octal value
            TestItems.accumulatorOctal = Convert.ToInt32(0777.ToString(), 8);
            TestItems.pcCounter = Convert.ToInt32(7770.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(7771.ToString(), 8), ActualResult.pcCounter);

            //test 2nd octal value
            TestItems.accumulatorOctal = Convert.ToInt32(0.ToString(), 8);
            TestItems.pcCounter = Convert.ToInt32(7771.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(7773.ToString(), 8), ActualResult.pcCounter);

            //test 3rd octal value
            TestItems.accumulatorOctal = Convert.ToInt32(2525.ToString(), 8);
            TestItems.pcCounter = Convert.ToInt32(240.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(241.ToString(), 8), ActualResult.pcCounter);

            //AC - zero, PC reached 7777 so it again initializes to 0 and gets incremented by 1
            TestItems.accumulatorOctal = Convert.ToInt32(0.ToString(), 8);
            TestItems.pcCounter = Convert.ToInt32(7777.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(1.ToString(), 8), ActualResult.pcCounter);

            //AC non zero, PC reached 7777 so it again initializes to 0
            TestItems.accumulatorOctal = Convert.ToInt32(1.ToString(), 8);
            TestItems.pcCounter = Convert.ToInt32(7777.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(0.ToString(), 8), ActualResult.pcCounter);

            //AC non zero, PC reached 7777 so it again initializes to 0
            TestItems.accumulatorOctal = Convert.ToInt32(7777.ToString(), 8);
            TestItems.pcCounter = Convert.ToInt32(7777.ToString(), 8);

            ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(Convert.ToInt32(0.ToString(), 8), ActualResult.pcCounter);

        }
    }
}
