using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ECE486_PDP_8_Emulator;

namespace ECE486_PDP_8_Emulator_Tests
{
    [TestClass]
    public class LoaderUnitTests
    {
        [TestMethod]
        public void TestBinLoading()
        {
           
            int[] FinalArray = new int[4096];

            FinalArray[200] = 2;

            MemArray FinalMemArray = new MemArray(FinalArray, "MemTrace.tr");

            MemArray TestMemArray = BinLoader.LoadFile(".\\TestFile.bin");

            Assert.AreEqual(FinalMemArray,TestMemArray);



        }
    }
}
