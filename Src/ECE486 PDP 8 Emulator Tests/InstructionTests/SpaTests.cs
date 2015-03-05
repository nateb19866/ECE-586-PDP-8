using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ECE486_PDP_8_Emulator.Classes;
using ECE486_PDP_8_Emulator;
using ECE486_PDP_8_Emulator.Instructions;

namespace ECE486_PDP_8_Emulator_Tests.InstructionTests
{
    [TestClass]
    public class SpaTests
    {
        [TestMethod]
        public void TestM2SPAArgumentPassthrough()

        {
            InstructionItems TestItems = new InstructionItems()

            {
                accumulatorOctal = 0000,
                LinkBit = true,
                MemoryAddress = 0,
                MemoryValueOctal = Convert.ToInt32(7777.ToString(), 8),
                pcCounter = 1700,
                InstructionRegister = Convert.ToInt32(7510.ToString(), 8)


            };


            InstructionResult ExpectedItems = new InstructionResult()
            {
                accumulatorOctal = 0000,
                LinkBit = true,
                MemoryAddress = 0,
                MemoryValueOctal = Convert.ToInt32(7777.ToString(), 8),
                pcCounter = 1702,
                InstructionRegister = Convert.ToInt32(7510.ToString(), 8),
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
         public void TestM2SPAOperation()
         {
             //Need to initialize the instruction items for the first time
            
            
            InstructionItems TestItems = new InstructionItems()
            {
                 accumulatorOctal = 0000,
                 LinkBit = true,
                 MemoryAddress = 0,

                 MemoryValueOctal = 0000,
                 pcCounter = 1700,
                 InstructionRegister = Convert.ToInt32(7510.ToString(), 8)
             };


             InstructionResult ExpectedItems = new InstructionResult()
             {
                 accumulatorOctal = 0000,
                 LinkBit = true,
                 MemoryAddress = 0,
                 MemoryValueOctal = 0000,
                 pcCounter = 1702,
                 InstructionRegister = Convert.ToInt32(7510.ToString(), 8),
                 SetMemValue = false
             };

             IInstruction TestOprInstruction = new OprInstruction();

             InstructionResult ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);

             Assert.AreEqual(0, ActualResult.accumulatorOctal);

             /* Test for PC + 2 only if AC is positive */

             //Test AC = 0 positive AC, PC + 2
             TestItems.accumulatorOctal = 0;
             TestItems.pcCounter = 0;

             ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
             Assert.AreEqual(2, ActualResult.pcCounter);

             //Test AC all 1s, neg, PC does not skip
             TestItems.accumulatorOctal = Convert.ToInt32(7777.ToString(), 8);
             TestItems.pcCounter = 0;

             ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
             Assert.AreEqual(1, ActualResult.pcCounter);

             //Test AC all 1s, neg, PC does not skip

             TestItems.accumulatorOctal = Convert.ToInt32(7777.ToString(), 8);
             TestItems.pcCounter = Convert.ToInt32(7777.ToString(), 8);


             ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
             Assert.AreEqual(Convert.ToInt32(0.ToString(), 8), ActualResult.pcCounter);
        
             //Test AC starting 0, PC skips
             TestItems.accumulatorOctal = Convert.ToInt32(1000.ToString(), 8);
             TestItems.pcCounter = Convert.ToInt32(7770.ToString(), 8);

             ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
             Assert.AreEqual(Convert.ToInt32(7772.ToString(), 8), ActualResult.pcCounter);

             //Test AC starting 0, PC skips
             TestItems.accumulatorOctal = Convert.ToInt32(2777.ToString(), 8);
             TestItems.pcCounter = Convert.ToInt32(7771.ToString(), 8);

             ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
             Assert.AreEqual(Convert.ToInt32(7773.ToString(), 8), ActualResult.pcCounter);

             //Test AC starting 1, PC does not skip
             TestItems.accumulatorOctal = Convert.ToInt32(4001.ToString(), 8);
             TestItems.pcCounter = Convert.ToInt32(2525.ToString(), 8);

             ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
             Assert.AreEqual(Convert.ToInt32(2526.ToString(), 8), ActualResult.pcCounter);

             //TEst AC starting 0, PC skips
             TestItems.accumulatorOctal = Convert.ToInt32(2525.ToString(), 8);
             TestItems.pcCounter = Convert.ToInt32(240.ToString(), 8);

             ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
             Assert.AreEqual(Convert.ToInt32(242.ToString(), 8), ActualResult.pcCounter);

             //Test AC starting 0, skips
             TestItems.accumulatorOctal = Convert.ToInt32(10.ToString(), 8);
             TestItems.pcCounter = Convert.ToInt32(10.ToString(), 8);

             ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
             Assert.AreEqual(Convert.ToInt32(12.ToString(), 8), ActualResult.pcCounter);

             //Test AC positive, PC is 7777, skips next instruction to PC = 1.
             TestItems.accumulatorOctal = Convert.ToInt32(1.ToString(), 8);
             TestItems.pcCounter = Convert.ToInt32(7777.ToString(), 8);

             ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
             Assert.AreEqual(Convert.ToInt32(1.ToString(), 8), ActualResult.pcCounter);

             //Test AC negative, PC is 7777 and only increments
             TestItems.accumulatorOctal = Convert.ToInt32(7777.ToString(), 8);
             TestItems.pcCounter = Convert.ToInt32(7777.ToString(), 8);

             ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
             Assert.AreEqual(Convert.ToInt32(0.ToString(), 8), ActualResult.pcCounter);

        }
    }
}
