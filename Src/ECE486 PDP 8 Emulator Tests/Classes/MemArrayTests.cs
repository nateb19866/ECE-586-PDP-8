using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECE486_PDP_8_Emulator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ECE486_PDP_8_Emulator.Classes;
namespace ECE486_PDP_8_Emulator.Tests
{
    [TestClass()]
    public class MemArrayTests
    {
    
        //Test out of range error
        [TestMethod()]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetValueErrorTest()
        {
            MemArray TestArray = new MemArray(new int[4096, 2]);

            var Rslt = TestArray.GetValue(4096, false, false);

        }

        [TestMethod()]
        public void SetValueTest()
        {

            MemArray TestArray = new MemArray(new int[4096, 2]);
            TestArray.SetValue(12, 4);

            Assert.AreEqual(4, TestArray.GetValue(12, false, false));
        }

        [TestMethod()]
        public void DumpValidMemContentsTest()
        {
            MemArray TestArray = new MemArray(new int[4096, 2]);
            TestArray.SetValue(12, 18);

            List<MemArrayRow> TestArrayList = TestArray.DumpValidMemContents();

            Assert.AreEqual(1, TestArrayList.Count);
            Assert.AreEqual(Utils.DecimalToOctal(18), TestArrayList.First().Value);
            Assert.AreEqual(Utils.DecimalToOctal(12), TestArrayList.First().Address);
        }
    }
}
