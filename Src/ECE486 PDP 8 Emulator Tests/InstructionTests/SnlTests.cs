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

        {
            InstructionItems TestItems = new InstructionItems()
            {
                accumulatorOctal = 0000,
                LinkBit = true,
                MemoryAddress = 0,
                MemoryValueOctal = Convert.ToInt32(7777.ToString(), 8),
                pcCounter = 5649,
                InstructionRegister = Convert.ToInt32(7402.ToString(), 8)


            };


            InstructionResult ExpectedItems = new InstructionResult()
            {
                accumulatorOctal = 0000,
                LinkBit = true,
                MemoryAddress = 0,
                MemoryValueOctal = Convert.ToInt32(7777.ToString(), 8),
                pcCounter = 5650,
                InstructionRegister = Convert.ToInt32(7402.ToString(), 8),
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
             //Need to initialize the instruction items for the first time
            
            
            InstructionItems TestItems = new InstructionItems()
            {
                 accumulatorOctal = 0000,
                 LinkBit = true,
                 MemoryAddress = 0,

                 MemoryValueOctal = 0000,
                 pcCounter = 5649,
                 InstructionRegister = Convert.ToInt32(7402.ToString(), 8)
             };


             InstructionResult ExpectedItems = new InstructionResult()
             {
                 accumulatorOctal = 0000,
                 LinkBit = true,
                 MemoryAddress = 0,
                 MemoryValueOctal = 0000,
                 pcCounter = 5650,
                 InstructionRegister = Convert.ToInt32(7402.ToString(), 8),
                 SetMemValue = false
             };

             IInstruction TestOprInstruction = new OprInstruction();

             InstructionResult ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);

             Assert.AreEqual(0, ActualResult.accumulatorOctal);
             
           

             //link - 1, PC - 0 => PC is incremented since link bit is not zero

             TestItems.LinkBit = true;
             TestItems.pcCounter = 0;


             ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
             Assert.AreEqual(1, ActualResult.pcCounter);

             //link - 0, PC - 0 => PC remains unchanged

             TestItems.LinkBit = false;
             TestItems.pcCounter = 0;


             ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
             Assert.AreEqual(0, ActualResult.pcCounter);

             //link - 1, PC - 1 => PCout - 1

             TestItems.LinkBit = true;
             TestItems.pcCounter = Convert.ToInt32(7770.ToString(), 8);


             ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
             Assert.AreEqual(Convert.ToInt32(7771.ToString(), 8), ActualResult.pcCounter);
        
             //Test lowest octal value
             TestItems.LinkBit = false;
             TestItems.pcCounter = Convert.ToInt32(7770.ToString(), 8);

             ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
             Assert.AreEqual(Convert.ToInt32(7770.ToString(), 8), ActualResult.pcCounter);

             //Test 1st octal value
             TestItems.LinkBit = true;
             TestItems.pcCounter = Convert.ToInt32(4001.ToString(), 8);

             ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
             Assert.AreEqual(Convert.ToInt32(4002.ToString(), 8), ActualResult.pcCounter);

             //test 2nd octal value
             TestItems.LinkBit = false;
             TestItems.pcCounter = Convert.ToInt32(4002.ToString(), 8);

             ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
             Assert.AreEqual(Convert.ToInt32(4002.ToString(), 8), ActualResult.pcCounter);

             //test 3rd octal value
             TestItems.LinkBit = false;
             TestItems.pcCounter = Convert.ToInt32(240.ToString(), 8);

             ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
             Assert.AreEqual(Convert.ToInt32(240.ToString(), 8), ActualResult.pcCounter);

             //test 4th octal value
             TestItems.LinkBit = true;
             TestItems.pcCounter = Convert.ToInt32(2525.ToString(), 8);

             ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
             Assert.AreEqual(Convert.ToInt32(2526.ToString(), 8), ActualResult.pcCounter);

             //test 5th octal value
             TestItems.LinkBit = true;
             TestItems.pcCounter = Convert.ToInt32(7777.ToString(), 8);

             ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
             Assert.AreEqual(Convert.ToInt32(7777.ToString(), 8), ActualResult.pcCounter);

             //test 6th octal value
             TestItems.LinkBit = false;
             TestItems.pcCounter = Convert.ToInt32(7777.ToString(), 8);

             ActualResult = TestOprInstruction.ExecuteInstruction(TestItems);
             Assert.AreEqual(Convert.ToInt32(0000.ToString(), 8), ActualResult.pcCounter);

        }
    }
}
