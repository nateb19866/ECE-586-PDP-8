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
                MemoryValueOctal = 7777,
                pcCounter = 5649,
                MicroCodes = 7402


            };


            InstructionResult ExpectedItems = new InstructionResult()
            {
                accumulatorOctal = 0000,
                LinkBit = true,
                MemoryAddress = 0,
                MemoryValueOctal = 7777,
                pcCounter = 5649,
                MicroCodes = 7402,
                SetMemValue = false
            };

           IInstruction TestAndInstruction = new AndInstruction();

            InstructionResult ActualResult = TestAndInstruction.ExecuteInstruction(TestItems);


            Assert.AreEqual(ExpectedItems.accumulatorOctal, ActualResult.accumulatorOctal);
            Assert.AreEqual(ExpectedItems.LinkBit, ActualResult.LinkBit);
            Assert.AreEqual(ExpectedItems.MemoryAddress, ActualResult.MemoryAddress);
            Assert.AreEqual(ExpectedItems.MemoryValueOctal, ActualResult.MemoryValueOctal);
            Assert.AreEqual(ExpectedItems.pcCounter, ActualResult.pcCounter);
            Assert.AreEqual(ExpectedItems.MicroCodes, ActualResult.MicroCodes);
            Assert.AreEqual(ExpectedItems.SetMemValue, ActualResult.SetMemValue);
        }


        [TestMethod]
        public void TestAndOperation()
        {
            //First is test 0 anded with 0 - need to initialize the instruction items for the first time
            InstructionItems TestItems = new InstructionItems()
            {
                accumulatorOctal = 0000,
                LinkBit = true,
                MemoryAddress = 0,
                
                MemoryValueOctal = 0000,
                pcCounter = 5649,
                MicroCodes = 7402


            };


            InstructionResult ExpectedItems = new InstructionResult()
            {
                accumulatorOctal = 0000,
                LinkBit = true,
                MemoryAddress = 0,
                MemoryValueOctal = 0000,
                pcCounter = 5649,
                MicroCodes = 7402,
                SetMemValue = false

            };

            IInstruction TestAndInstruction = new AndInstruction();

            InstructionResult ActualResult = TestAndInstruction.ExecuteInstruction(TestItems);

            Assert.AreEqual( 0, ActualResult.accumulatorOctal);


            //Test all 1s anded with all 0s

            TestItems.accumulatorOctal = 0000;
            TestItems.MemoryValueOctal = 7777;
          

            ActualResult = TestAndInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual( 0, ActualResult.accumulatorOctal);

            //Test alternating pattern of 1s and 0s - 101 010 101 010 - anded with the opposite - should produce a 0
            TestItems.accumulatorOctal = 5252;
            TestItems.MemoryValueOctal = 2525;

            ActualResult = TestAndInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual( 0, ActualResult.accumulatorOctal);

            //Test lowest octal value
            TestItems.accumulatorOctal = 7777;
            TestItems.MemoryValueOctal = 7770;

            ActualResult = TestAndInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(7770, ActualResult.accumulatorOctal);
            
            //test 2nd octal value
            TestItems.accumulatorOctal = 7777;
            TestItems.MemoryValueOctal = 7707;

            ActualResult = TestAndInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual( 7707, ActualResult.accumulatorOctal);

            //test 3rd octal value
            TestItems.accumulatorOctal = 7777;
            TestItems.MemoryValueOctal = 7077;

            ActualResult = TestAndInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(7077, ActualResult.accumulatorOctal);

            //test 4th octal value
            TestItems.accumulatorOctal = 7777;
            TestItems.MemoryValueOctal = 777;

            ActualResult = TestAndInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual( 777, ActualResult.accumulatorOctal);

        }
    }
}
