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
       private string MemTraceFileName = "MemTrace";
       private string BranchTraceFileName = "BranchTrace";
       private string TraceExtension = ".tr";

       public string FinalMemTrFileName = "";
       public string FinalBranchTrFileName = "";


      private bool MemTraceFileCreated = false;
       private bool BranchTraceFileCreated = false;


       private List<MemTraceRow> MemTrace = new List<MemTraceRow>();


       private List<BranchTraceRow> BranchTrace = new List<BranchTraceRow>();


       public Logger(string traceFolderPath)
       {
           this.TraceFolderPath = traceFolderPath;

          FinalMemTrFileName = TraceFolderPath + "\\" + MemTraceFileName + "_"
               + DateTime.Now.Year.ToString()
               + DateTime.Now.Month.ToString()
               + DateTime.Now.Day.ToString() + "_"
               + DateTime.Now.Hour.ToString()
               + DateTime.Now.Minute.ToString()
               + DateTime.Now.Second.ToString() + "_" + Guid.NewGuid()
               + TraceExtension;


          FinalBranchTrFileName = TraceFolderPath + "\\" + BranchTraceFileName + "_"
             + DateTime.Now.Year.ToString()
             + DateTime.Now.Month.ToString()
             + DateTime.Now.Day.ToString() + "_"
             + DateTime.Now.Hour.ToString()
             + DateTime.Now.Minute.ToString()
             + DateTime.Now.Second.ToString() + "_" + Guid.NewGuid()
             + TraceExtension;
       }
        ~Logger()
       {
           //At the end of the program, dump all caches to disk
           DumpAllCachesToFile();
       }
       public void AppendToMemTraceFile(object sender, EventArgs e, int address, Constants.OpType operationType)
       {

           //To avoid unnecessary I/O, track a number of memory events before dumping to disk
           MemTrace.Add(new MemTraceRow() { Address = address, OperationType = operationType });
          

           if(MemTrace.Count > Convert.ToInt32(Resources.RowsBeforeDumpToFile))
           {

               DumpMemCacheToFile();

               //Truncate memory array and reset counter
               MemTrace = new List<MemTraceRow>();
             
           }

       }

       public void AppendToBranchTraceFile(object sender, EventArgs e, BranchTraceRow rowToAppend)
       {

           //To avoid unnecessary I/O, track a number of memory events before dumping to disk
           BranchTrace.Add(rowToAppend);
         

           if (BranchTrace.Count > Convert.ToInt32(Resources.RowsBeforeDumpToFile))
           {

               DumpBranchCacheToFile();

               //Truncate memory array and reset counter
               BranchTrace = new List<BranchTraceRow>();
               
           }

       }

       //Dumps the contents of all trace files to disk
       public void DumpAllCachesToFile()
       {
           if(MemTrace.Count>0)
            DumpMemCacheToFile();

           if(BranchTrace.Count>0)
           DumpBranchCacheToFile();
       }

       private void DumpMemCacheToFile()
       {
           
           
           using (StreamWriter sw = File.CreateText(FinalMemTrFileName))
           {
               if(!MemTraceFileCreated)
               {
                   sw.WriteLine("Addr	OpType");
                   MemTraceFileCreated = true;
               }

               foreach (var MemTraceRow in MemTrace)
               {
                   sw.WriteLine("{0}	{1}", Utils.DecimalToOctal(MemTraceRow.Address).ToString(), MemTraceRow.OperationType.ToString());
               }
           }
       }

       private void DumpBranchCacheToFile()
       {
         
           
           using (StreamWriter sw = File.CreateText(FinalBranchTrFileName))
           {

               if (!BranchTraceFileCreated)
               {
                   sw.WriteLine("PCtr	Type	Addr	Taken?");
                   BranchTraceFileCreated = true;
               } 
               foreach (var TraceRow in BranchTrace)
               {
                   sw.WriteLine("{0}	{1}		{2}		{3}", Utils.DecimalToOctal(TraceRow.ProgramCounter).ToString(), TraceRow.BranchType.ToString(), Utils.DecimalToOctal(TraceRow.MemoryAddress).ToString(), TraceRow.branchTaken.ToString());
                   
               }
           }
       }
    }
}
