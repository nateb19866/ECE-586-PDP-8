using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ECE486_PDP_8_Emulator;
using ECE486_PDP_8_Emulator.Classes;
using ECE486_PDP_8_Emulator.Interfaces;

namespace ECE486_PDP_8_Emulator_Tests
{
    [TestClass]
    public class LoaderUnitTests
    {
        [TestMethod]
        public void TestBinLoading()
        {
           
            int[,] FinalArray = new int[4096,2];

            FinalArray[200,0] = 2;

            MemArray FinalMemArray = new MemArray(FinalArray);


            LoaderResult FinalLoaderRslt = new LoaderResult()
            {
                FinishedArray = FinalMemArray

            };

            ILoader FileLoader = new BinLoader();
            LoaderResult TestMemArray = FileLoader.LoadFile(".\\TestFile.bin");

            Assert.AreEqual(FinalMemArray,TestMemArray);



        }
    }
}
