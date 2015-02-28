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

            IInstruction TestIszInstruction = new IotInstruction();

            InstructionResult ActualResult = TestIszInstruction.ExecuteInstruction(TestItems);


            Assert.AreEqual(ExpectedItems.accumulatorOctal, ActualResult.accumulatorOctal);
            Assert.AreEqual(ExpectedItems.LinkBit, ActualResult.LinkBit);
            Assert.AreEqual(ExpectedItems.MemoryAddress, ActualResult.MemoryAddress);
            Assert.AreEqual(ExpectedItems.MemoryValueOctal, ActualResult.MemoryValueOctal);
            Assert.AreEqual(ExpectedItems.pcCounter, ActualResult.pcCounter);
            Assert.AreEqual(ExpectedItems.MicroCodes, ActualResult.MicroCodes);
            Assert.AreEqual(ExpectedItems.SetMemValue, ActualResult.SetMemValue);
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

            IInstruction TestIszInstruction = new IszInstruction();

            InstructionResult ActualResult = TestIszInstruction.ExecuteInstruction(TestItems);

            Assert.AreEqual(0, ActualResult.accumulatorOctal);

            /* Test cases increment memory by 1, skip next instruction if EA = 0 */

            //Test 0s: result EA = 1, PC same
            TestItems.MemoryValueOctal = 0;
            TestItems.pcCounter = 0;

            ActualResult = TestIszInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(0, ActualResult.MemoryValueOctal);
            Assert.AreEqual(0, ActualResult.pcCounter);

            //Test 1: results EA = 2, PC same
            TestItems.MemoryValueOctal = 1;
            TestItems.pcCounter = 1;

            ActualResult = TestIszInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(0, ActualResult.MemoryValueOctal);
            Assert.AreEqual(0, ActualResult.pcCounter);


            //Test all 1's: result mem = 0, increment PC =0
            TestItems.MemoryValueOctal = 7777;
            TestItems.pcCounter = 7777;
            
            ActualResult = TestIszInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(0, ActualResult.MemoryValueOctal);
            Assert.AreEqual(0, ActualResult.pcCounter);

            //Test all 1's: result mem = 0, increment PC =1
            TestItems.MemoryValueOctal = 7777;
            TestItems.pcCounter = 0;

            ActualResult = TestIszInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(0, ActualResult.MemoryValueOctal);
            Assert.AreEqual(0, ActualResult.pcCounter);


            //Test alternating pattern of 1s and 0s - Start with 1, PC result same
            TestItems.MemoryValueOctal = 5252;
            TestItems.pcCounter = 770;

            ActualResult = TestIszInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(0, ActualResult.MemoryValueOctal);
            Assert.AreEqual(0, ActualResult.pcCounter);


            //Test alternating pattern of 1s and 0s - Start with 0, PC result same
            TestItems.MemoryValueOctal = 2525;
            TestItems.pcCounter = 7777;

            ActualResult = TestIszInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(0, ActualResult.MemoryValueOctal);
            Assert.AreEqual(0, ActualResult.pcCounter);


            //Test 0 for first and last octals, ensure not losing octals on ends, PC result same
            TestItems.MemoryValueOctal = 0770;
            TestItems.pcCounter = 4441;

            ActualResult = TestIszInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(0, ActualResult.MemoryValueOctal);
            Assert.AreEqual(0, ActualResult.pcCounter);


            //Test end 1's,  PC result same
            TestItems.MemoryValueOctal = 4001;
            TestItems.pcCounter = 4444;

            ActualResult = TestIszInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(0, ActualResult.MemoryValueOctal);
            Assert.AreEqual(0, ActualResult.pcCounter);


            //Test last octal 0,  PC result same
            TestItems.MemoryValueOctal = 70;
            TestItems.pcCounter = 2222;

            ActualResult = TestIszInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(0, ActualResult.MemoryValueOctal);
            Assert.AreEqual(0, ActualResult.pcCounter);

            //Test last 2 octals 0,  PC result same
            TestItems.MemoryValueOctal = 700;
            TestItems.pcCounter = 1010;

            ActualResult = TestIszInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(0, ActualResult.MemoryValueOctal);
            Assert.AreEqual(0, ActualResult.pcCounter);

            //Test last octal 1,  PC result same
            TestItems.MemoryValueOctal = 771;
            TestItems.pcCounter = 0101;

            ActualResult = TestIszInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(0, ActualResult.MemoryValueOctal);
            Assert.AreEqual(0, ActualResult.pcCounter);

            //Test last 3 octals 7, end case, 1777,  PC result same
            TestItems.MemoryValueOctal = 777;
            TestItems.pcCounter = 5555;

            ActualResult = TestIszInstruction.ExecuteInstruction(TestItems);
            Assert.AreEqual(0, ActualResult.MemoryValueOctal);
            Assert.AreEqual(0, ActualResult.pcCounter);

        }
    }
}

