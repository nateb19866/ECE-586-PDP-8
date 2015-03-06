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

            //Skips only if AC is positive.

            {
                accumulatorOctal = 0000,    //Sign bit is zero - Positive
                LinkBit = true,
                MemoryAddress = 0,
                MemoryValueOctal = Convert.ToInt32(7777.ToString(), 8),
                pcCounter = 1700,          //PC skips a instruction.
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
             //AC - Negative, PCout - PC + 1
            
            
            InstructionItems TestItems = new InstructionItems()
            {
                 accumulatorOctal = 7000,
                 LinkBit = true,
                 MemoryAddress = 0,

                 MemoryValueOctal = 0000,
                 pcCounter = 1700,
                 InstructionRegister = Convert.ToInt32(7510.ToString(), 8)
             };


             InstructionResult ExpectedItems = new InstructionResult()
             {
                 accumulatorOctal = 7000,
                 LinkBit = true,
                 MemoryAddress = 0,
                 MemoryValueOctal = 0000,
                 pcCounter = 1701,
                 InstructionRegister = Convert.ToInt32(7510.ToString(), 8),
                 SetMemValue = false
             };

             IInstruction TestOprInstruction = new OprInstruction();

             InstructionResult ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);

             Assert.AreEqual(ExpectedItems.accumulatorOctal, ActualResult.accumulatorOctal);

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

         [TestMethod]
         public void TestM2SPACombination()
         {
             //Test for AND Sub group Skips only if all conditions (SMA SZA SNL) are True.
             //Test for combination of SPA and SNA - 111 101 101 000
             //AC - Negative (No skip), AC - Non Zero (skip), PCout - PC + 1 (Only SNA is True) 


             InstructionItems TestItems = new InstructionItems()
             {
                 accumulatorOctal = 0x800,
                 LinkBit = true,
                 MemoryAddress = 0,

                 MemoryValueOctal = 0000,
                 pcCounter = 1400,
                 InstructionRegister = Convert.ToInt32(7550.ToString(), 8)
             };


             InstructionResult ExpectedItems = new InstructionResult()
             {
                 accumulatorOctal = 0x800,
                 LinkBit = true,
                 MemoryAddress = 0,
                 MemoryValueOctal = 0000,
                 pcCounter = 1401,
                 InstructionRegister = Convert.ToInt32(7550.ToString(), 8),
                 SetMemValue = false
             };

             IInstruction TestOprInstruction = new OprInstruction();

             InstructionResult ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);

             Assert.AreEqual(ExpectedItems.accumulatorOctal, ActualResult.accumulatorOctal);
             Assert.AreEqual(ExpectedItems.LinkBit, ActualResult.LinkBit);
             Assert.AreEqual(ExpectedItems.MemoryAddress, ActualResult.MemoryAddress);
             Assert.AreEqual(ExpectedItems.MemoryValueOctal, ActualResult.MemoryValueOctal);
             Assert.AreEqual(ExpectedItems.pcCounter, ActualResult.pcCounter);

             //AC - Positive (Skip), AC - Non Zero (Skip), PCout - PC + 2 (Both SPA and SNA are True)
             TestItems.accumulatorOctal = Convert.ToInt32(10.ToString(), 8);
             TestItems.pcCounter = Convert.ToInt32(0.ToString(), 8);

             ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
             Assert.AreEqual(Convert.ToInt32(2.ToString(), 8), ActualResult.pcCounter);

             //AC - Positive (Skip), AC - Zero (No skip), PCout - PC + 1 (Only SPA is True)
             TestItems.accumulatorOctal = Convert.ToInt32(0.ToString(), 8);
             TestItems.pcCounter = Convert.ToInt32(7777.ToString(), 8);

             ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
             Assert.AreEqual(Convert.ToInt32(0.ToString(), 8), ActualResult.pcCounter);
         }
    }
}
