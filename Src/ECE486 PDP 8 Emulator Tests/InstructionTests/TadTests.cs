using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using ECE486_PDP_8_Emulator.Classes;
using ECE486_PDP_8_Emulator;
using ECE486_PDP_8_Emulator.Instructions;

// Not yet implemented
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
                MicroCodes = 7402
            };


            InstructionResult ExpectedItems = new InstructionResult()
            {
                accumulatorOctal = 0000,
                LinkBit = true,
                MemoryAddress = 0,
                MemoryValueOctal = 7777,
                pcCounter = 5650,
                MicroCodes = 7402,
                SetMemValue = false
            };

            IInstruction TestTadInstruction = new IotInstruction();

            InstructionResult ActualResult = TestTadInstruction.ExecuteInstruction(TestItems);


            Assert.AreEqual(ExpectedItems.accumulatorOctal, ActualResult.accumulatorOctal);
            Assert.AreEqual(ExpectedItems.LinkBit, ActualResult.LinkBit);
            Assert.AreEqual(ExpectedItems.MemoryAddress, ActualResult.MemoryAddress);
            Assert.AreEqual(ExpectedItems.MemoryValueOctal, ActualResult.MemoryValueOctal);
            Assert.AreEqual(ExpectedItems.pcCounter, ActualResult.pcCounter);
            Assert.AreEqual(ExpectedItems.MicroCodes, ActualResult.MicroCodes);
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
                MicroCodes = 7402
            };

            InstructionResult ExpectedItems = new InstructionResult()
            {
                accumulatorOctal = 0000,
                LinkBit = true,
                MemoryAddress = 0,
                MemoryValueOctal = 0000,
                pcCounter = 5650,
                MicroCodes = 7402,
                SetMemValue = false

            };

            IInstruction TestTadInstruction = new TadInstruction();

            InstructionResult ActualResult = TestTadInstruction.ExecuteInstruction(TestItems);

            Assert.AreEqual(0, ActualResult.accumulatorOctal);

            /* Test cases place octals (1)  EA = PC, (2) PC = EA + 1 */
            /* Test for PC and EA update correctly */

            //Test (1) mem can hold max PC, then PC = 1112
            TestItems.MemoryValueOctal = 1111;
            TestItems.pcCounter = 7777;

            ActualResult = TestTadInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(0, ActualResult.MemoryValueOctal);
            Assert.AreEqual(0, ActualResult.pcCounter);


            //Test (2) mem can hold max PC, then PC = 0
            TestItems.MemoryValueOctal = 7777;
            TestItems.pcCounter = 7777;

            ActualResult = TestTadInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(0, ActualResult.MemoryValueOctal);
            Assert.AreEqual(0, ActualResult.pcCounter);


            //Test (3) mem can hold max PC and mem first octal increments, then PC = 4227
            TestItems.MemoryValueOctal = 4226;
            TestItems.pcCounter = 7777;

            ActualResult = TestTadInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(0, ActualResult.MemoryValueOctal);
            Assert.AreEqual(0, ActualResult.pcCounter);


            //Test (4) mem can hold max PC and mem 2nd octal increments, then PC = 4230
            TestItems.MemoryValueOctal = 4227;
            TestItems.pcCounter = 7777;

            ActualResult = TestTadInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(0, ActualResult.MemoryValueOctal);
            Assert.AreEqual(0, ActualResult.pcCounter);


            //Test (5) mem can hold max PC and mem 3rd octal increments, then PC = 4300
            TestItems.MemoryValueOctal = 4277;
            TestItems.pcCounter = 7777;

            ActualResult = TestTadInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(0, ActualResult.MemoryValueOctal);
            Assert.AreEqual(0, ActualResult.pcCounter);


            //Test (6) mem updates and mem 4th octal increments, then PC = 0000
            TestItems.MemoryValueOctal = 1;
            TestItems.pcCounter = 7777;

            ActualResult = TestTadInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(0, ActualResult.MemoryValueOctal);
            Assert.AreEqual(0, ActualResult.pcCounter);


            //Test (7) mem updates and mem 4th octal increments, then PC = 1
            TestItems.MemoryValueOctal = 0;
            TestItems.pcCounter = 4001;

            ActualResult = TestTadInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(0, ActualResult.MemoryValueOctal);
            Assert.AreEqual(0, ActualResult.pcCounter);

        }
    }
}

