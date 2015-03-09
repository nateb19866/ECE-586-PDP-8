using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ECE486_PDP_8_Emulator.Classes;
using ECE486_PDP_8_Emulator.Instructions;
using ECE486_PDP_8_Emulator;

namespace ECE486_PDP_8_Emulator_Tests.InstructionTests
{
    [TestClass]
    public class SmaTests
    {
         [TestMethod]
        public void TestM2SMAArgumentPassthrough()

        {
             //Skips only if Accumulator is a negative number.

            InstructionItems TestItems = new InstructionItems()
            {
                accumulatorOctal = 0x800,   //1000 0000 0000 - sign bit is 1 negative AC.
                LinkBit = true,
                MemoryAddress = 0,
                MemoryValueOctal = Convert.ToInt32(7777.ToString(), 8),
                pcCounter = 1250,
                InstructionRegister = Convert.ToInt32(7500.ToString(), 8)


            };


            InstructionResult ExpectedItems = new InstructionResult()
            {
                accumulatorOctal = 0x800,
                LinkBit = true,
                MemoryAddress = 0,
                MemoryValueOctal = Convert.ToInt32(7777.ToString(), 8),
                pcCounter = 1252,
                InstructionRegister = Convert.ToInt32(7500.ToString(), 8),
                SetMemValue = false,
                BranchTaken = true,
                BranchType = Constants.BranchType.Conditional

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
            Assert.AreEqual(ExpectedItems.BranchTaken, ActualResult.BranchTaken);
            Assert.AreEqual(ExpectedItems.BranchType, ActualResult.BranchType);

        }


         [TestMethod]
         public void TestM2SMAOperation()
         {
             //AC - Positive (No skip) PCout - PC + 1
            
            
            InstructionItems TestItems = new InstructionItems()
            {
                 accumulatorOctal = 0000,
                 LinkBit = true,
                 MemoryAddress = 0,

                 MemoryValueOctal = 0000,
                 pcCounter = 0000,
                 InstructionRegister = Convert.ToInt32(7500.ToString(), 8)
             };


             InstructionResult ExpectedItems = new InstructionResult()
             {
                 accumulatorOctal = 0000,
                 LinkBit = true,
                 MemoryAddress = 0,
                 MemoryValueOctal = 0000,
                 pcCounter = 0001,
                 InstructionRegister = Convert.ToInt32(7500.ToString(), 8),
                 SetMemValue = false,
                 BranchTaken = false,
                 BranchType = Constants.BranchType.Conditional
             };

             IInstruction TestOprInstruction = new OprInstruction();

             InstructionResult ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);

             Assert.AreEqual(0, ActualResult.accumulatorOctal);
             Assert.AreEqual(ExpectedItems.BranchTaken, ActualResult.BranchTaken);
             Assert.AreEqual(ExpectedItems.BranchType, ActualResult.BranchType);
             
           // NOTE: PC always increments to next instruction ( + 1 ), it will only skip ( +2 ) if conditions are met

             //AC - 0, PC + 1 goes to next instr, not skips ( +2 )
             TestItems.accumulatorOctal = Convert.ToInt32(0.ToString(), 8);
             TestItems.pcCounter = Convert.ToInt32(0.ToString(), 8); 

             ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
             Assert.AreEqual(Convert.ToInt32(1.ToString(), 8), ActualResult.pcCounter);
             Assert.AreEqual(false, ActualResult.BranchTaken);
             Assert.AreEqual(Constants.BranchType.Conditional, ActualResult.BranchType);

             //AC positive , PC + 1
             TestItems.accumulatorOctal = Convert.ToInt32(1.ToString(), 8);
             TestItems.pcCounter = Convert.ToInt32(0.ToString(), 8);

             ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
             Assert.AreEqual(Convert.ToInt32(1.ToString(), 8), ActualResult.pcCounter);
             Assert.AreEqual(false, ActualResult.BranchTaken);
             Assert.AreEqual(Constants.BranchType.Conditional, ActualResult.BranchType);


             //AC - 10, PC + 1
             TestItems.accumulatorOctal = Convert.ToInt32(10.ToString(), 8);
             TestItems.pcCounter = Convert.ToInt32(0.ToString(), 8);

             ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
             Assert.AreEqual(Convert.ToInt32(1.ToString(), 8), ActualResult.pcCounter);
             Assert.AreEqual(false, ActualResult.BranchTaken);
             Assert.AreEqual(Constants.BranchType.Conditional, ActualResult.BranchType);


             //PC skips next instruction when AC is negative
             TestItems.accumulatorOctal = Convert.ToInt32(7777.ToString(), 8);
             TestItems.pcCounter = Convert.ToInt32(7770.ToString(), 8);

             ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
             Assert.AreEqual(Convert.ToInt32(7772.ToString(), 8), ActualResult.pcCounter);
             Assert.AreEqual(true, ActualResult.BranchTaken);
             Assert.AreEqual(Constants.BranchType.Conditional, ActualResult.BranchType);

             //PC remains with 3 octals 1 but AC not neg
             TestItems.accumulatorOctal = Convert.ToInt32(0777.ToString(), 8);
             TestItems.pcCounter = Convert.ToInt32(7770.ToString(), 8);

             ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
             Assert.AreEqual(Convert.ToInt32(7771.ToString(), 8), ActualResult.pcCounter);
             Assert.AreEqual(false, ActualResult.BranchTaken);
             Assert.AreEqual(Constants.BranchType.Conditional, ActualResult.BranchType);

             //PC skips next instruction when AC negative
             TestItems.accumulatorOctal = Convert.ToInt32(4001.ToString(), 8);
             TestItems.pcCounter = Convert.ToInt32(7771.ToString(), 8);

             ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
             Assert.AreEqual(Convert.ToInt32(7773.ToString(), 8), ActualResult.pcCounter);
             Assert.AreEqual(true, ActualResult.BranchTaken);
             Assert.AreEqual(Constants.BranchType.Conditional, ActualResult.BranchType);

             //PC + 1 when AC is positive, recognize rotating 1's with starting 0
             TestItems.accumulatorOctal = Convert.ToInt32(2525.ToString(), 8);
             TestItems.pcCounter = Convert.ToInt32(240.ToString(), 8);

             ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
             Assert.AreEqual(Convert.ToInt32(241.ToString(), 8), ActualResult.pcCounter);
             Assert.AreEqual(false, ActualResult.BranchTaken);
             Assert.AreEqual(Constants.BranchType.Conditional, ActualResult.BranchType);

             //PC + 2 when AC is negative, starting 1 ( middle octals 0s )
             TestItems.accumulatorOctal = Convert.ToInt32(4001.ToString(), 8);
             TestItems.pcCounter = Convert.ToInt32(2525.ToString(), 8);

             ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
             Assert.AreEqual(Convert.ToInt32(2527.ToString(), 8), ActualResult.pcCounter);
             Assert.AreEqual(true, ActualResult.BranchTaken);
             Assert.AreEqual(Constants.BranchType.Conditional, ActualResult.BranchType);

             //PC + 1 if AC is positive , 1
             TestItems.accumulatorOctal = Convert.ToInt32(1.ToString(), 8);
             TestItems.pcCounter = Convert.ToInt32(7777.ToString(), 8);

             ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
             Assert.AreEqual(false, ActualResult.BranchTaken);
             Assert.AreEqual(Constants.BranchType.Conditional, ActualResult.BranchType);
             Assert.AreEqual(ExpectedItems.BranchType, ActualResult.BranchType);

             //PC skips next instruction when AC is negative
             TestItems.accumulatorOctal = Convert.ToInt32(7000.ToString(), 8);
             TestItems.pcCounter = Convert.ToInt32(7777.ToString(), 8);

             ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
             Assert.AreEqual(Convert.ToInt32(1.ToString(), 8), ActualResult.pcCounter);
             Assert.AreEqual(true, ActualResult.BranchTaken);
             Assert.AreEqual(Constants.BranchType.Conditional, ActualResult.BranchType);

         }
         [TestMethod]
         public void TestM2SMACombination()
         {
             //Test for OR Sub group Skips if at least one condition from SMA SZA SNL is True.
             //Test for combination of SMA and SZA - 111 101 100 000
             //AC - negative (skips), AC - Non Zero (no skip), PCout - PC + 2 (SMA is true)


             InstructionItems TestItems = new InstructionItems()
             {
                 accumulatorOctal = 0x800,
                 LinkBit = true,
                 MemoryAddress = 0,

                 MemoryValueOctal = 0000,
                 pcCounter = 1400,
                 InstructionRegister = Convert.ToInt32(7540.ToString(), 8)
             };


             InstructionResult ExpectedItems = new InstructionResult()
             {
                 accumulatorOctal = 0x800,
                 LinkBit = true,
                 MemoryAddress = 0,
                 MemoryValueOctal = 0000,
                 pcCounter = 1402,
                 InstructionRegister = Convert.ToInt32(7540.ToString(), 8),
                 SetMemValue = false
             };

             IInstruction TestOprInstruction = new OprInstruction();

             InstructionResult ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);

             Assert.AreEqual(ExpectedItems.accumulatorOctal, ActualResult.accumulatorOctal);
             Assert.AreEqual(ExpectedItems.LinkBit, ActualResult.LinkBit);
             Assert.AreEqual(ExpectedItems.MemoryAddress, ActualResult.MemoryAddress);
             Assert.AreEqual(ExpectedItems.MemoryValueOctal, ActualResult.MemoryValueOctal);
             Assert.AreEqual(ExpectedItems.pcCounter, ActualResult.pcCounter);
             Assert.AreEqual(true, ActualResult.BranchTaken);
             Assert.AreEqual(Constants.BranchType.Conditional, ActualResult.BranchType);

             //AC - positive (No skip), AC - Non Zero (No skip), PCout - PC + 1 (Both conditions are false)
             TestItems.accumulatorOctal = Convert.ToInt32(10.ToString(), 8);
             TestItems.pcCounter = Convert.ToInt32(0.ToString(), 8);

             ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
             Assert.AreEqual(Convert.ToInt32(1.ToString(), 8), ActualResult.pcCounter);
             Assert.AreEqual(false, ActualResult.BranchTaken);
             Assert.AreEqual(Constants.BranchType.Conditional, ActualResult.BranchType);

             //AC - positive (No skip), AC - Zero (skip), PCout - PC + 2 (SZA is true)
             TestItems.accumulatorOctal = Convert.ToInt32(0.ToString(), 8);
             TestItems.pcCounter = Convert.ToInt32(0.ToString(), 8);
             
             ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
             Assert.AreEqual(Convert.ToInt32(2.ToString(), 8), ActualResult.pcCounter);
             Assert.AreEqual(true, ActualResult.BranchTaken);
             Assert.AreEqual(Constants.BranchType.Conditional, ActualResult.BranchType);
         }
    }
}
