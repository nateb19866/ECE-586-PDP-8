using ECE486_PDP_8_Emulator.Classes;
using ECE486_PDP_8_Emulator.Instructions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECE486_PDP_8_Emulator
{
    public static class InstructionFactory
    {
     
       public static IInstruction GetInstruction(Constants.OpCode opCode )
        {
            switch (opCode)
            {
                case Constants.OpCode.AND:

                    return new AndInstruction();
                   
                case Constants.OpCode.TAD:
                    return new TadInstruction();
                case Constants.OpCode.ISZ:
                    return new IszInstruction();
                case Constants.OpCode.DCA:
                    return new DcaInstruction();
                case Constants.OpCode.JMS:
                    return new JmsInstruction();
                case Constants.OpCode.JMP:
                    return new JmpInstruction();
                case Constants.OpCode.IOT:
                    return new IotInstruction();
                case Constants.OpCode.OPR:
                    return new OprInstruction();
                default:
                    throw new InvalidOperationException();
            }

        }
    }
}
