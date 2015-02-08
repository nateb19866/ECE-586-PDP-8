using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECE486_PDP_8_Emulator.Classes
{
   public class Statistics
    {
       
       public Statistics()
       {
           InstructionTypeExecutions = new List<InstructionTypeStat>();

           InstructionTypeExecutions.Add(new InstructionTypeStat() { Operation = Constants.OpCode.AND, Executions= 0});
           InstructionTypeExecutions.Add(new InstructionTypeStat() { Operation = Constants.OpCode.DCA, Executions= 0});
           InstructionTypeExecutions.Add(new InstructionTypeStat() { Operation = Constants.OpCode.IOT, Executions= 0});
           InstructionTypeExecutions.Add(new InstructionTypeStat() { Operation = Constants.OpCode.ISZ, Executions= 0});
           InstructionTypeExecutions.Add(new InstructionTypeStat() { Operation = Constants.OpCode.JMP, Executions= 0});
           InstructionTypeExecutions.Add(new InstructionTypeStat() { Operation = Constants.OpCode.JMS, Executions= 0});
           InstructionTypeExecutions.Add(new InstructionTypeStat() { Operation = Constants.OpCode.OPR, Executions= 0});
           InstructionTypeExecutions.Add(new InstructionTypeStat() { Operation = Constants.OpCode.TAD, Executions = 0 });

       }
       public int InstructionsExecuted = 0;
       public int ClockCyclesExecuted = 0;

       public List<InstructionTypeStat> InstructionTypeExecutions;


    }
}
