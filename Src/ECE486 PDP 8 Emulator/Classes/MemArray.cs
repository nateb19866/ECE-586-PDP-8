using ECE486_PDP_8_Emulator.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECE486_PDP_8_Emulator
{

    public delegate void TraceNotificationHandler(object sender, EventArgs e, int address, Constants.OpType operationType);
    public class MemArray
    {
        //Column 0 is address, col 1 is valid bit
        private int[,] MemoryArray;
        public int MemReads = 0;
        public int MemWrites = 0;
        private string TraceFilePath;

        public event TraceNotificationHandler TraceAppend;


        public MemArray( int[,] memoryArray )
        {
            MemoryArray = memoryArray;
           
        }


        protected virtual void OnTraced(EventArgs e, int address, Constants.OpType operationType)
        {
            if (TraceAppend != null)
                TraceAppend(this, e, address, operationType);

            
        }
      
       public int GetValue(int address, bool isInstruction, bool isIndirect)
        {
           
            //Handle auto-increment addresses - since array is in decimal, addresses are 8-15
            if (address >= 8 && address <= 15 && isIndirect)
            {
                MemoryArray[address, 0] = Utils.DecimalToOctal( Convert.ToInt32(MemoryArray[address, 0].ToString(), 8) + 1);
                MemoryArray[address, 1] = 1;
            }


           OnTraced(EventArgs.Empty, address, isInstruction?Constants.OpType.InstructionFetch:Constants.OpType.DataRead);
            return MemoryArray[address,0];
        }

      public void SetValue(int address, int value)
        {
            OnTraced(EventArgs.Empty, address, Constants.OpType.DataWrite);
            MemoryArray[address,0] = value;
            MemoryArray[address, 1] = 1;
        }

        public List<MemArrayRow> DumpValidMemContents()
      {

          List<MemArrayRow> OutputArray = new List<MemArrayRow>();
            for (int i = 0; i < MemoryArray.Length; i++)
          {
              if (MemoryArray[i, 1] == 1)
                  OutputArray.Add(new MemArrayRow() { Address = i, Value = Utils.DecimalToOctal(MemoryArray[i, 0]) });
          }

            return OutputArray;


	{
		 
	}
      }
      
    }
}
