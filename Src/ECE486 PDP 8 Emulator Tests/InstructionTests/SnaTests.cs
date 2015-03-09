using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ECE486_PDP_8_Emulator.Classes;
using ECE486_PDP_8_Emulator;
using ECE486_PDP_8_Emulator.Instructions;

namespace ECE486_PDP_8_Emulator_Tests.InstructionTests
{
    [TestClass]
    public class SnaTests
    {
        [TestMethod]
        public void TestM2SNAArgumentPassthrough()

        {
            InstructionItems TestItems = new InstructionItems()
            //Skips only if AC is non zero.
            {
                accumulatorOctal = 0000,       //No skip
                LinkBit = true,
                MemoryAddress = 0,
                MemoryValueOctal = Convert.ToInt32(7777.ToString(), 8),
                pcCounter = 1500,
                InstructionRegister = Convert.ToInt32(7450.ToString(), 8)


            };

            InstructionResult ExpectedItems = new InstructionResult()
            {
                accumulatorOctal = 0000,
                LinkBit = true,
                MemoryAddress = 0,
                MemoryValueOctal = Convert.ToInt32(7777.ToString(), 8),
                pcCounter = 1501,
                InstructionRegister = Convert.ToInt32(7450.ToString(), 8),
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
            Assert.AreEqual(false, ActualResult.BranchTaken);
            Assert.AreEqual(Constants.BranchType.Conditional, ActualResult.BranchType);
        }


         [TestMethod]
         public void TestM2SNAOperation()
         {
             //AC - Non Zero (Skips), PCout - PC + 2.
            
            
            InstructionItems TestItems = new InstructionItems()
            {
                 accumulatorOctal = 10,
                 LinkBit = true,
                 MemoryAddress = 0,

                 MemoryValueOctal = 0000,
                 pcCounter = 1500,
                 InstructionRegister = Convert.ToInt32(7450.ToString(), 8)
             };


             InstructionResult ExpectedItems = new InstructionResult()
             {
                 accumulatorOctal = 10,
                 LinkBit = true,
                 MemoryAddress = 0,
                 MemoryValueOctal = 0000,
                 pcCounter = 1502,
                 InstructionRegister = Convert.ToInt32(7450.ToString(), 8),
                 SetMemValue = false
             };

             IInstruction TestOprInstruction = new OprInstruction();

             InstructionResult ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);

             Assert.AreEqual(ExpectedItems.accumulatorOctal, ActualResult.accumulatorOctal);
             Assert.AreEqual(true, ActualResult.BranchTaken);
             Assert.AreEqual(Constants.BranchType.Conditional, ActualResult.BranchType);
             
            /* Test for PC skip next instruction if AC is not 0 */

             // Test AC = 0, PC does not skip
             TestItems.accumulatorOctal = Convert.ToInt32(0.ToString(), 8);
             TestItems.pcCounter = Convert.ToInt32(0.ToString(), 8);

             ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
             Assert.AreEqual(1, ActualResult.pcCounter);
             Assert.AreEqual(false, ActualResult.BranchTaken);
             Assert.AreEqual(Constants.BranchType.Conditional, ActualResult.BranchType);


             //Test AC = 1, PC skips next instruction
             TestItems.accumulatorOctal = Convert.ToInt32(1.ToString(), 8);
             TestItems.pcCounter = Convert.ToInt32(0.ToString(), 8);

             ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
             Assert.AreEqual(2, ActualResult.pcCounter);
             Assert.AreEqual(true, ActualResult.BranchTaken);
             Assert.AreEqual(Constants.BranchType.Conditional, ActualResult.BranchType);


             //Test AC = 10, PC skips next instruction
             TestItems.accumulatorOctal = Convert.ToInt32(10.ToString(), 8);
             TestItems.pcCounter = Convert.ToInt32(1.ToString(), 8);
            
             ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
             Assert.AreEqual(3, ActualResult.pcCounter);
             Assert.AreEqual(true, ActualResult.BranchTaken);
             Assert.AreEqual(Constants.BranchType.Conditional, ActualResult.BranchType);
        
             /* 
              * Below tests are for negative and positive AC recognition.  Ensure 
              * PC skips whether positive or negative
              */

             //Test AC is non zero, PC skips next instruction
             TestItems.accumulatorOctal = Convert.ToInt32(7777.ToString(), 8);
             TestItems.pcCounter = Convert.ToInt32(7770.ToString(), 8);

             ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
             Assert.AreEqual(Convert.ToInt32(7772.ToString(), 8), ActualResult.pcCounter);
             Assert.AreEqual(true, ActualResult.BranchTaken);
             Assert.AreEqual(Constants.BranchType.Conditional, ActualResult.BranchType);


             //Test AC non zero, PC skips next instruction
             TestItems.accumulatorOctal = Convert.ToInt32(0777.ToString(), 8);
             TestItems.pcCounter = Convert.ToInt32(7770.ToString(), 8);

             ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
             Assert.AreEqual(Convert.ToInt32(7772.ToString(), 8), ActualResult.pcCounter);
             Assert.AreEqual(true, ActualResult.BranchTaken);
             Assert.AreEqual(Constants.BranchType.Conditional, ActualResult.BranchType);


             //Test AC non zero, PC skips next instruction
             TestItems.accumulatorOctal = Convert.ToInt32(4001.ToString(), 8);
             TestItems.pcCounter = Convert.ToInt32(7771.ToString(), 8);

             ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
             Assert.AreEqual(Convert.ToInt32(7773.ToString(), 8), ActualResult.pcCounter);
             Assert.AreEqual(true, ActualResult.BranchTaken);
             Assert.AreEqual(Constants.BranchType.Conditional, ActualResult.BranchType);

             //Test AC positive, PC skips next instruction
             TestItems.accumulatorOctal = Convert.ToInt32(2525.ToString(), 8);
             TestItems.pcCounter = Convert.ToInt32(240.ToString(), 8);

             ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
             Assert.AreEqual(Convert.ToInt32(242.ToString(), 8), ActualResult.pcCounter);
             Assert.AreEqual(true, ActualResult.BranchTaken);
             Assert.AreEqual(Constants.BranchType.Conditional, ActualResult.BranchType);

             //Test AC negative, PC skips next instruction
             TestItems.accumulatorOctal = Convert.ToInt32(4001.ToString(), 8);
             TestItems.pcCounter = Convert.ToInt32(2525.ToString(), 8);

             ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
             Assert.AreEqual(Convert.ToInt32(2527.ToString(), 8), ActualResult.pcCounter);
             Assert.AreEqual(true, ActualResult.BranchTaken);
             Assert.AreEqual(Constants.BranchType.Conditional, ActualResult.BranchType);

             //Test AC is zero, PC is 7777 and does not skip.
             TestItems.accumulatorOctal = Convert.ToInt32(0.ToString(), 8);
             TestItems.pcCounter = Convert.ToInt32(7777.ToString(), 8);

             ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
             Assert.AreEqual(Convert.ToInt32(0.ToString(), 8), ActualResult.pcCounter);
             Assert.AreEqual(false, ActualResult.BranchTaken);
             Assert.AreEqual(Constants.BranchType.Conditional, ActualResult.BranchType);

             //Test AC non zero, PC is 7777 and skips next instruction.
             TestItems.accumulatorOctal = Convert.ToInt32(7777.ToString(), 8);
             TestItems.pcCounter = Convert.ToInt32(7777.ToString(), 8);

             ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
             Assert.AreEqual(Convert.ToInt32(1.ToString(), 8), ActualResult.pcCounter);
             Assert.AreEqual(true, ActualResult.BranchTaken);
             Assert.AreEqual(Constants.BranchType.Conditional, ActualResult.BranchType);

        }

         [TestMethod]
         public void TestM2SNACombination()
         {
             //Test for AND Sub group Skips only if all conditions from SMA SZA SNL are True.
             //Test for combination of SNA and SZL - 111 100 111 000
             //AC - Non Zero (skip), Link Bit - 1 (No skip), PCout - PC + 1 (Only SNA is True) 

             InstructionItems TestItems = new InstructionItems()
             {
                 accumulatorOctal = 0x200,
                 LinkBit = true,
                 MemoryAddress = 0,

                 MemoryValueOctal = 0000,
                 pcCounter = 1400,
                 InstructionRegister = Convert.ToInt32(7470.ToString(), 8)
             };


             InstructionResult ExpectedItems = new InstructionResult()
             {
                 accumulatorOctal = 0x200,
                 LinkBit = true,
                 MemoryAddress = 0,
                 MemoryValueOctal = 0000,
                 pcCounter = 1401,
                 InstructionRegister = Convert.ToInt32(7470.ToString(), 8),
                 SetMemValue = false
             };

             IInstruction TestOprInstruction = new OprInstruction();

             InstructionResult ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);

             Assert.AreEqual(ExpectedItems.accumulatorOctal, ActualResult.accumulatorOctal);
             Assert.AreEqual(ExpectedItems.LinkBit, ActualResult.LinkBit);
             Assert.AreEqual(ExpectedItems.MemoryAddress, ActualResult.MemoryAddress);
             Assert.AreEqual(ExpectedItems.MemoryValueOctal, ActualResult.MemoryValueOctal);
             Assert.AreEqual(ExpectedItems.pcCounter, ActualResult.pcCounter);
             Assert.AreEqual(false, ActualResult.BranchTaken);
             Assert.AreEqual(Constants.BranchType.Conditional, ActualResult.BranchType);

             //AC - Zero (no skip) Link bit - 1 (No skip), PCout - PC + 1 (Both the conditions are False)
             TestItems.accumulatorOctal = Convert.ToInt32(0.ToString(), 8);
             TestItems.LinkBit = true;
             TestItems.pcCounter = Convert.ToInt32(0.ToString(), 8);

             ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
             Assert.AreEqual(Convert.ToInt32(1.ToString(), 8), ActualResult.pcCounter);
             Assert.AreEqual(false, ActualResult.BranchTaken);
             Assert.AreEqual(Constants.BranchType.Conditional, ActualResult.BranchType);

             //AC - Non Zero (skip) Link bit - 0 (skip), PCout - PC + 2 (Both SNA and SZL are True)
             TestItems.accumulatorOctal = Convert.ToInt32(7777.ToString(), 8);
             TestItems.LinkBit = false;
             TestItems.pcCounter = Convert.ToInt32(7777.ToString(), 8);

             ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
             Assert.AreEqual(Convert.ToInt32(1.ToString(), 8), ActualResult.pcCounter);
             Assert.AreEqual(true, ActualResult.BranchTaken);
             Assert.AreEqual(Constants.BranchType.Conditional, ActualResult.BranchType);
         }
    }
}
