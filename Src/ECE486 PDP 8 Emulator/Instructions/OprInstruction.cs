using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECE486_PDP_8_Emulator.Classes;

namespace ECE486_PDP_8_Emulator.Instructions
{
    public class OprInstruction : IInstruction
    {
        private int ClockCycles = 1;
        private Constants.OpCode InstructionType = Constants.OpCode.OPR;


        public InstructionResult ExecuteInstruction(InstructionItems instItems)
        {


            InstructionResult Rslt = new InstructionResult();

            switch ((Constants.Microcode)instItems.MicroCodes)
            {


                case Constants.Microcode.NOP:
                    Rslt = NopInstruction(instItems);
                    break;
                case Constants.Microcode.M1_CLA:
                    break;
                case Constants.Microcode.CLL:
                    break;
                case Constants.Microcode.CMA:
                    break;
                case Constants.Microcode.CML:
                    break;
                case Constants.Microcode.IAC:
                    break;
                case Constants.Microcode.RAR:
                    break;
                case Constants.Microcode.RTR:
                    break;
                case Constants.Microcode.RAL:
                    break;
                case Constants.Microcode.RTL:
                    break;
                case Constants.Microcode.SMA:
                    break;
                case Constants.Microcode.SZA:
                    break;
                case Constants.Microcode.SNL:
                    break;
                case Constants.Microcode.SPA:
                    break;
                case Constants.Microcode.SNA:
                    break;
                case Constants.Microcode.SZL:
                    break;
                case Constants.Microcode.SKP:
                    break;
                case Constants.Microcode.M2_CLA:
                    break;
                case Constants.Microcode.OSR:
                    break;
                case Constants.Microcode.HLT:
                    break;
                case Constants.Microcode.M3_CLA:
                    break;
                case Constants.Microcode.MQL:
                    break;
                case Constants.Microcode.MQA:
                    break;
                case Constants.Microcode.SWP:
                    break;
                case Constants.Microcode.CAM:
                    break;
                default:
                    break;
            }

            return Rslt;
        }

        public int clockCycles
        {
            get { return ClockCycles; }
        }


        public Constants.OpCode instructionType
        {
            get { return InstructionType; }
        }

        private InstructionResult NopInstruction(InstructionItems instItems)
        {
            return new InstructionResult()
            {
                accumulatorOctal = instItems.accumulatorOctal,
                BranchTaken = false,
                LinkBit = instItems.LinkBit,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                MicroCodes = instItems.MicroCodes,
                pcCounter = ++instItems.pcCounter,
                SetMemValue = false
            };
        }

    }
}
