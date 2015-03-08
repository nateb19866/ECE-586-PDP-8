using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECE486_PDP_8_Emulator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace ECE486_PDP_8_Emulator.Tests
{
    [TestClass()]
    public class InstructionFactoryTests
    {
        [TestMethod()]
        public void GetInstructionTest()
        {
            //Test AND instruction
            IInstruction TestInst = InstructionFactory.GetInstruction(Constants.OpCode.AND);

            Assert.AreEqual(Constants.OpCode.AND, TestInst.instructionType);

            //Test DCA instruction
           TestInst = InstructionFactory.GetInstruction(Constants.OpCode.DCA);
            Assert.AreEqual(Constants.OpCode.DCA, TestInst.instructionType);


            //Test IOT instruction
            TestInst = InstructionFactory.GetInstruction(Constants.OpCode.IOT);
            Assert.AreEqual(Constants.OpCode.IOT, TestInst.instructionType);

            //Test ISZ instruction
            TestInst = InstructionFactory.GetInstruction(Constants.OpCode.ISZ);
            Assert.AreEqual(Constants.OpCode.ISZ, TestInst.instructionType);

            //Test JMP instruction
            TestInst = InstructionFactory.GetInstruction(Constants.OpCode.JMP);
            Assert.AreEqual(Constants.OpCode.JMP, TestInst.instructionType);


            //Test JMS instruction
            TestInst = InstructionFactory.GetInstruction(Constants.OpCode.JMS);
            Assert.AreEqual(Constants.OpCode.JMS, TestInst.instructionType);

            //Test OPR instruction
            TestInst = InstructionFactory.GetInstruction(Constants.OpCode.OPR);
            Assert.AreEqual(Constants.OpCode.OPR, TestInst.instructionType);

            //Test TAD instruction
            TestInst = InstructionFactory.GetInstruction(Constants.OpCode.TAD);
            Assert.AreEqual(Constants.OpCode.TAD, TestInst.instructionType);


        }
    }
}
