using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ECE486_PDP_8_Emulator;
using ECE486_PDP_8_Emulator.Classes;
namespace ECE486_PDP_8_Emulator.Tests
{
    [TestClass()]
    public class UtilsTests
    {
        [TestMethod()]
        public void GetPageTest()
        {
            int rslt = Utils.GetPage(0xDF0);

            Assert.AreEqual(0x1B, rslt);
        }
    }
}

namespace ECE486_PDP_8_Emulator_Tests
{
    [TestClass]
    public class UtilsTests
    {
        [TestMethod]
        public void TestIsAutoIncrementRegister()
        {
            //Test lower bound
            Assert.AreEqual(false, Utils.IsAutoIncrementRegister(7));

            //Test upper bound
            Assert.AreEqual(false, Utils.IsAutoIncrementRegister(16));

            //Test middle value
            Assert.AreEqual(true, Utils.IsAutoIncrementRegister(10));

            //Test lower edge case
            Assert.AreEqual(true, Utils.IsAutoIncrementRegister(8));

            //Test upper edge case
            Assert.AreEqual(true, Utils.IsAutoIncrementRegister(15));
        }


        [TestMethod]
        public void TestOperationAddressDecode()
        {
            int[,] TestMemArray = new int[4096,2];

            int CurPage = 1;

            //For page 0, no auto-increment test with indirect
            TestMemArray[1, 0] = 444;
            TestMemArray[1, 1] = 1;


            //For auto-increment test with indirect
            TestMemArray[9,0] = 4;
            TestMemArray[9,1] = 1;

            //Cur page, indirect
            TestMemArray[Convert.ToInt32(200.ToString(), 8),0] = 2222;
            TestMemArray[Convert.ToInt32(200.ToString(), 8),1] = 1;


            MemArray TestPdp8MemArray = new MemArray(TestMemArray);
            
            //Test 0 page, no indirect, no auto-increment register

            Operation RsltOp = Utils.DecodeOperationAddress(Convert.ToInt32(1100.ToString(), 8), TestPdp8MemArray, CurPage);
            Assert.AreEqual(Convert.ToInt32(100.ToString(),8),RsltOp.FinalMemAddress) ;
            Assert.AreEqual(0, RsltOp.ExtraClockCyles);
            Assert.AreEqual(false, RsltOp.IsIndirect);


            //test 0 page, indirect, no auto-increment register
            RsltOp = Utils.DecodeOperationAddress(Convert.ToInt32(1401.ToString(), 8), TestPdp8MemArray, CurPage);
            Assert.AreEqual(444, RsltOp.FinalMemAddress);
            Assert.AreEqual(1, RsltOp.ExtraClockCyles);
            Assert.AreEqual(true, RsltOp.IsIndirect);

            //test 0 page, no indirect, auto-increment register
            RsltOp = Utils.DecodeOperationAddress(Convert.ToInt32(1010.ToString(), 8), TestPdp8MemArray, CurPage);
            Assert.AreEqual(8, RsltOp.FinalMemAddress);
            Assert.AreEqual(0, RsltOp.ExtraClockCyles);
            Assert.AreEqual(false, RsltOp.IsIndirect);


            //test 0 page, indirect, auto-increment register
            RsltOp = Utils.DecodeOperationAddress(Convert.ToInt32(1411.ToString(), 8), TestPdp8MemArray, CurPage);
            Assert.AreEqual(5, RsltOp.FinalMemAddress);
            Assert.AreEqual(3, RsltOp.ExtraClockCyles);
            Assert.AreEqual(true, RsltOp.IsIndirect);


            //Test cur page, no indirect
            RsltOp = Utils.DecodeOperationAddress(Convert.ToInt32(1222.ToString(), 8), TestPdp8MemArray, CurPage);
            Assert.AreEqual(Convert.ToInt32(222.ToString(), 8), RsltOp.FinalMemAddress);
            Assert.AreEqual(0, RsltOp.ExtraClockCyles);
            Assert.AreEqual(false, RsltOp.IsIndirect);

            //Test cur page, indirect, and opcode
            RsltOp = Utils.DecodeOperationAddress(Convert.ToInt32(1600.ToString(), 8), TestPdp8MemArray, CurPage);
            Assert.AreEqual(2222, RsltOp.FinalMemAddress);
            Assert.AreEqual(1, RsltOp.ExtraClockCyles);
            Assert.AreEqual(true, RsltOp.IsIndirect);
            Assert.AreEqual(Constants.OpCode.TAD, RsltOp.Instruction.instructionType);


           

        }
    }
}
