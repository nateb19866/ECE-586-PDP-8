using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ECE486_PDP_8_Emulator.Classes;
using ECE486_PDP_8_Emulator;
using ECE486_PDP_8_Emulator.Instructions;

namespace ECE486_PDP_8_Emulator_Tests.InstructionTests
{
    [TestClass]
    public class SnlTests
    {
        [TestMethod]
        public void TestM2SNLArgumentPassthrough()
            // all values are decimals
            //Skips only if Link Bit is non zero (True)
        {
            InstructionItems TestItems = new InstructionItems()
            {
                accumulatorOctal = 0000,
                LinkBit = true,
                MemoryAddress = 0,
                MemoryValueOctal = Convert.ToInt32(7777.ToString(), 8),
                pcCounter = 1600,
                InstructionRegister = Convert.ToInt32(7420.ToString(), 8)


            };


            InstructionResult ExpectedItems = new InstructionResult()
            {
                accumulatorOctal = 0000,
                LinkBit = true,
                MemoryAddress = 0,
                MemoryValueOctal = Convert.ToInt32(7777.ToString(), 8),
                pcCounter = 1602,
                InstructionRegister = Convert.ToInt32(7420.ToString(), 8),
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
         public void TestM2SNLOperation()
         {
             //Link bit - false (0), PCout - PC + 1
            
            
            InstructionItems TestItems = new InstructionItems()
            {
                 accumulatorOctal = 0000,
                 LinkBit = false,
                 MemoryAddress = 0,

                 MemoryValueOctal = 0000,
                 pcCounter = 1564,
                 InstructionRegister = Convert.ToInt32(7420.ToString(), 8)
             };


             InstructionResult ExpectedItems = new InstructionResult()
             {
                 accumulatorOctal = 0000,
                 LinkBit = false,
                 MemoryAddress = 0,
                 MemoryValueOctal = 0000,
                 pcCounter = 1565,
                 InstructionRegister = Convert.ToInt32(7420.ToString(), 8),
                 SetMemValue = false
             };

             IInstruction TestOprInstruction = new OprInstruction();

             InstructionResult ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);

             Assert.AreEqual(0, ActualResult.accumulatorOctal);
             
            /* Test for PC skipping next instruction if Link is not false */

             //link - 1, PC - 2 => PC skip 1 since link bit is not zero

             TestItems.LinkBit = true;
             TestItems.pcCounter = 0;


             ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
             Assert.AreEqual(2, ActualResult.pcCounter);

             //link - 0, PC - 1 => PC only increments

             TestItems.LinkBit = false;
             TestItems.pcCounter = 0;


             ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
             Assert.AreEqual(1, ActualResult.pcCounter);

             //Test link true with PC last octal 0
             TestItems.LinkBit = true;
             TestItems.pcCounter = Convert.ToInt32(7770.ToString(), 8);


             ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
             Assert.AreEqual(Convert.ToInt32(7772.ToString(), 8), ActualResult.pcCounter);
        
             //Test link false with PC last octal 0
             TestItems.LinkBit = false;
             TestItems.pcCounter = Convert.ToInt32(7770.ToString(), 8);

             ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
             Assert.AreEqual(Convert.ToInt32(7771.ToString(), 8), ActualResult.pcCounter);

             //Test link true with PC end bits 1
             TestItems.LinkBit = true;
             TestItems.pcCounter = Convert.ToInt32(4001.ToString(), 8);

             ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
             Assert.AreEqual(Convert.ToInt32(4003.ToString(), 8), ActualResult.pcCounter);

             //Test link false with end bits 1
             TestItems.LinkBit = false;
             TestItems.pcCounter = Convert.ToInt32(4002.ToString(), 8);

             ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
             Assert.AreEqual(Convert.ToInt32(4003.ToString(), 8), ActualResult.pcCounter);

             //Test link false
             TestItems.LinkBit = false;
             TestItems.pcCounter = Convert.ToInt32(240.ToString(), 8);

             ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
             Assert.AreEqual(Convert.ToInt32(241.ToString(), 8), ActualResult.pcCounter);

             //Test link true with PC starting 0 and alternating 1's
             TestItems.LinkBit = true;
             TestItems.pcCounter = Convert.ToInt32(2525.ToString(), 8);

             ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
             Assert.AreEqual(Convert.ToInt32(2527.ToString(), 8), ActualResult.pcCounter);

             //Link bit is non zero, PC reached 7777 so it again increments to 0 and gets incremented by 2.
             TestItems.LinkBit = true;
             TestItems.pcCounter = Convert.ToInt32(7777.ToString(), 8);

             ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
             Assert.AreEqual(Convert.ToInt32(1.ToString(), 8), ActualResult.pcCounter);

             //Link bit is zero, PC reached 7777 so it increments to 0
             TestItems.LinkBit = false;
             TestItems.pcCounter = Convert.ToInt32(7777.ToString(), 8);

             ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
             Assert.AreEqual(Convert.ToInt32(0000.ToString(), 8), ActualResult.pcCounter);

        }

         [TestMethod]
         public void TestM2SNLCombination()
         {
             //Test for OR Sub group Skips if at least one condition from SMA SZA SNL is True.
             //Test for combination of SMA and SNL - 111 101 010 000
             //AC - positive (no skip), Link - 1 (skip), PCout - PC + 2 (SNL is True)

             InstructionItems TestItems = new InstructionItems()
             {
                 accumulatorOctal = 0x200,
                 LinkBit = true,
                 MemoryAddress = 0,

                 MemoryValueOctal = 0000,
                 pcCounter = 1400,
                 InstructionRegister = Convert.ToInt32(7520.ToString(), 8)
             };


             InstructionResult ExpectedItems = new InstructionResult()
             {
                 accumulatorOctal = 0x200,
                 LinkBit = true,
                 MemoryAddress = 0,
                 MemoryValueOctal = 0000,
                 pcCounter = 1402,
                 InstructionRegister = Convert.ToInt32(7520.ToString(), 8),
                 SetMemValue = false
             };

             IInstruction TestOprInstruction = new OprInstruction();

             InstructionResult ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);

             Assert.AreEqual(ExpectedItems.accumulatorOctal, ActualResult.accumulatorOctal);
             Assert.AreEqual(ExpectedItems.LinkBit, ActualResult.LinkBit);
             Assert.AreEqual(ExpectedItems.MemoryAddress, ActualResult.MemoryAddress);
             Assert.AreEqual(ExpectedItems.MemoryValueOctal, ActualResult.MemoryValueOctal);
             Assert.AreEqual(ExpectedItems.pcCounter, ActualResult.pcCounter);

             //AC - positive (no skip) Link bit - 0 (no skip), PCout - PC + 1, (Both conditions are False)
             TestItems.accumulatorOctal = Convert.ToInt32(0.ToString(), 8);
             TestItems.LinkBit = false;
             TestItems.pcCounter = Convert.ToInt32(0.ToString(), 8);

             ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
             Assert.AreEqual(Convert.ToInt32(1.ToString(), 8), ActualResult.pcCounter);

             //AC - Negative (skip) Link bit - 1 (skip), PCout - PC + 2 (Both are True)
             TestItems.accumulatorOctal = Convert.ToInt32(0.ToString(), 8);
             TestItems.LinkBit = true;
             TestItems.pcCounter = Convert.ToInt32(0.ToString(), 8);

             ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
             Assert.AreEqual(Convert.ToInt32(2.ToString(), 8), ActualResult.pcCounter);
         }
    }
}
