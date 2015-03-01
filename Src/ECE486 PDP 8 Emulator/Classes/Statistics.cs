using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECE486_PDP_8_Emulator.Classes
{
   public class Statistics
    {
       
       public Statistics()
       {
           InstructionTypeExecutions = new NameValueCollection();

           InstructionTypeExecutions[Constants.OpCode.AND.ToString()] = "0";
           InstructionTypeExecutions[Constants.OpCode.DCA.ToString()] = "0";
           InstructionTypeExecutions[Constants.OpCode.IOT.ToString()] = "0";
           InstructionTypeExecutions[Constants.OpCode.ISZ.ToString()] = "0";
           InstructionTypeExecutions[Constants.OpCode.JMP.ToString()] = "0";
           InstructionTypeExecutions[Constants.OpCode.JMS.ToString()] = "0";
           InstructionTypeExecutions[Constants.OpCode.OPR.ToString()] = "0";
           InstructionTypeExecutions[Constants.OpCode.TAD.ToString()] = "0";

       }
       public int InstructionsExecuted = 0;
       public int ClockCyclesExecuted = 0;

       public NameValueCollection InstructionTypeExecutions;

       public List<MemArrayRow> MemContents;

    }
}
