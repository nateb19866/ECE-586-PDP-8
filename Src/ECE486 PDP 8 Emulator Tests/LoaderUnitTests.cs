using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ECE486_PDP_8_Emulator;
using ECE486_PDP_8_Emulator.Classes;

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


            LoaderResult FinalLoaderRslt = new LoaderResult()
            {
                FinishedArray = FinalMemArray,
                FirstInstructionAddress = 200

            };

            LoaderResult TestMemArray = BinLoader.LoadFile(".\\TestFile.bin", ".\\Trace.tr");

            Assert.AreEqual(FinalMemArray,TestMemArray);



        }
    }
}
