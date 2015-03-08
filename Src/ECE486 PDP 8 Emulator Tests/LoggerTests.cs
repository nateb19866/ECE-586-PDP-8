﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECE486_PDP_8_Emulator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ECE486_PDP_8_Emulator_Tests.Properties;
using ECE486_PDP_8_Emulator.Classes;
using System.IO;
namespace ECE486_PDP_8_Emulator.Tests
{
    [TestClass()]
    public class LoggerTests
    {



        [TestMethod()]
        public void TestLoggerTest()
        {
            Logger TestLogger = new Logger(Resources.TestFilePath);


            TestLogger.AppendToMemTraceFile(this, null, 145, Constants.OpType.DataRead);

            
            TestLogger.AppendToBranchTraceFile(this, null, new BranchTraceRow() { branchTaken = true, BranchType = Constants.BranchType.Conditional, MemoryAddress = 345, ProgramCounter = 23 });

            TestLogger.DumpAllCachesToFile();

            using (StreamReader TxtFile = new StreamReader(TestLogger.FinalMemTrFileName))
            {
                string line = TxtFile.ReadLine();
                Assert.AreEqual("Addr	OpType", line);

                line = TxtFile.ReadLine();

                string testline = string.Format("{0}	{1}", Utils.DecimalToOctal( 145), Constants.OpType.DataRead);

                Assert.AreEqual(testline, line);
              
            }

            using (StreamReader TxtFile = new StreamReader(TestLogger.FinalBranchTrFileName))
            {
                string line = TxtFile.ReadLine();
                Assert.AreEqual("PCtr	Type	Addr	Taken?", line);

                line = TxtFile.ReadLine();

                string testline = string.Format("{0}	{1}		{2}		{3}",Utils.DecimalToOctal(23),Constants.BranchType.Conditional,Utils.DecimalToOctal(345),"True");

                Assert.AreEqual(testline, line);


            }

            //Test flushing cache
            for (int i = 1; i <= Convert.ToInt32(Resources.RowsBeforeDumpToFile); i++)
            {
                TestLogger.AppendToMemTraceFile(this, null, 145, Constants.OpType.DataRead);
         

            TestLogger.AppendToBranchTraceFile(this, null, new BranchTraceRow() { branchTaken = true, BranchType = Constants.BranchType.Conditional, MemoryAddress = 345, ProgramCounter = 23 });
            }


           Assert.AreEqual(Convert.ToInt32(Resources.RowsBeforeDumpToFile)+1 ,File.ReadLines(TestLogger.FinalMemTrFileName).Count());
           Assert.AreEqual(Convert.ToInt32(Resources.RowsBeforeDumpToFile)+1, File.ReadLines(TestLogger.FinalBranchTrFileName).Count());

           TestLogger.DumpAllCachesToFile();


            File.Delete(TestLogger.FinalBranchTrFileName);
            File.Delete(TestLogger.FinalMemTrFileName);
        }


    }
}
