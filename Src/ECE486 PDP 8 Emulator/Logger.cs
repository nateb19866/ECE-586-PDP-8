using ECE486_PDP_8_Emulator.Classes;
using ECE486_PDP_8_Emulator.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECE486_PDP_8_Emulator
{
   public class Logger
    {
       private string TraceFolderPath;
       private string MemTraceFileName = "MemTrace.tr";

       private List<MemTraceRow> MemTrace = new List<MemTraceRow>();
       private int MemTraceCount = 0;

       public Logger(string traceFolderPath)
       {
           this.TraceFolderPath = traceFolderPath;
       }
       public ~Logger()
       {
           //At the end of the program, dump all caches to disk
           DumpAllCachesToFile();
       }
       public void AppendToMemTraceFile(object sender, EventArgs e, int address, Constants.OpType operationType)
       {

           //To avoid unnecessary I/O, track a number of memory events before dumping to disk
           MemTrace.Add(new MemTraceRow() { Address = address, OperationType = operationType });
           MemTraceCount++;

           if(MemTraceCount > Convert.ToInt32(Resources.RowsBeforeDumpToFile))
           {

               DumpMemCacheToFile();

               //Truncate memory array and reset counter
               MemTrace = new List<MemTraceRow>();
               MemTraceCount = 0;
           }

       }

       //Dumps the contents of all trace files to disk
       public void DumpAllCachesToFile()
       {
           DumpMemCacheToFile();
       }

       private void DumpMemCacheToFile()
       {
           using (StreamWriter sw = File.CreateText(TraceFolderPath + MemTraceFileName))
           {

               foreach (var MemTraceRow in MemTrace)
               {
                   sw.WriteLine("{0}    {1}", Utils.DecimalToOctal(MemTraceRow.Address).ToString(), MemTraceRow.OperationType.ToString());
               }
           }
       }


    }
}
