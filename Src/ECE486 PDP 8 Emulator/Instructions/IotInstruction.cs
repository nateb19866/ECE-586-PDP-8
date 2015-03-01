using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECE486_PDP_8_Emulator.Classes;


namespace ECE486_PDP_8_Emulator.Instructions
{
    public class IotInstruction : IInstruction
    {
        private int ClockCycles = 1;
        private Constants.OpCode InstructionType = Constants.OpCode.IOT;

        public InstructionResult ExecuteInstruction(InstructionItems instItems)
        {
            InstructionResult Rslt = new InstructionResult();

            switch ((Constants.IOCode)(Utils.DecimalToOctal(instItems.InstructionRegister)))
            {
                case Constants.IOCode.KCF:
                    Rslt = KCF(instItems);
                    break;
                case Constants.IOCode.KSF:
                    Rslt = KSF(instItems);
                    break;
                case Constants.IOCode.KCC:
                    Rslt = KCC(instItems);
                    break;
                case Constants.IOCode.KRS:
                    Rslt = KRS(instItems);
                    break;
                case Constants.IOCode.KRB:
                    Rslt = KRB(instItems);
                    break;
                case Constants.IOCode.TFL:
                    Rslt = TFL(instItems);
                    break;
                case Constants.IOCode.TSF:
                    Rslt = TSF(instItems);
                    break;
                case Constants.IOCode.TCF:
                    Rslt = TCF(instItems);
                    break;
                case Constants.IOCode.TPC:
                    Rslt = TPC(instItems);
                    break;
                case Constants.IOCode.TLS:
                    Rslt = TLS(instItems);
                    break;
                case Constants.IOCode.SKON:
                    Rslt = SKON(instItems);
                    break;
                case Constants.IOCode.ION:
                    Rslt = ION(instItems);
                    break;
                case Constants.IOCode.IOF:
                    Rslt = IOF(instItems);
                    break;
                default:
                    break;
            }

            return Rslt;
        }

        // Only increment PC for all IOs
        public InstructionResult KCF(InstructionItems instItems)
        {
            return new InstructionResult()
            {
                accumulatorOctal = instItems.accumulatorOctal,
                BranchTaken = false,
                LinkBit = instItems.LinkBit,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                InstructionRegister = instItems.InstructionRegister,
                pcCounter = ++instItems.pcCounter,
                SetMemValue = false
            };
        }

        public InstructionResult KSF(InstructionItems instItems)
        {
            return new InstructionResult()
            {
                accumulatorOctal = instItems.accumulatorOctal,
                BranchTaken = false,
                LinkBit = instItems.LinkBit,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                InstructionRegister = instItems.InstructionRegister,
                pcCounter = ++instItems.pcCounter,
                SetMemValue = false
            };
        }
        public InstructionResult KCC(InstructionItems instItems)
        {
            return new InstructionResult()
            {
                accumulatorOctal = instItems.accumulatorOctal,
                BranchTaken = false,
                LinkBit = instItems.LinkBit,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                InstructionRegister = instItems.InstructionRegister,
                pcCounter = ++instItems.pcCounter,
                SetMemValue = false
            };
        }
        public InstructionResult KRS(InstructionItems instItems)
        {
            return new InstructionResult()
            {
                accumulatorOctal = instItems.accumulatorOctal,
                BranchTaken = false,
                LinkBit = instItems.LinkBit,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                InstructionRegister = instItems.InstructionRegister,
                pcCounter = ++instItems.pcCounter,
                SetMemValue = false
            };
        }
        public InstructionResult KRB(InstructionItems instItems)
        {
            return new InstructionResult()
            {
                accumulatorOctal = instItems.accumulatorOctal,
                BranchTaken = false,
                LinkBit = instItems.LinkBit,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                InstructionRegister = instItems.InstructionRegister,
                pcCounter = ++instItems.pcCounter,
                SetMemValue = false
            };
        }
          
        public InstructionResult TFL(InstructionItems instItems)
        {
            return new InstructionResult()
            {
                accumulatorOctal = instItems.accumulatorOctal,
                BranchTaken = false,
                LinkBit = instItems.LinkBit,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                InstructionRegister = instItems.InstructionRegister,
                pcCounter = ++instItems.pcCounter,
                SetMemValue = false
            };
        }
        public InstructionResult TSF(InstructionItems instItems)
        {
            return new InstructionResult()
            {
                accumulatorOctal = instItems.accumulatorOctal,
                BranchTaken = false,
                LinkBit = instItems.LinkBit,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                InstructionRegister = instItems.InstructionRegister,
                pcCounter = ++instItems.pcCounter,
                SetMemValue = false
            };
        }
        public InstructionResult TCF(InstructionItems instItems)
        {
            return new InstructionResult()
            {
                accumulatorOctal = instItems.accumulatorOctal,
                BranchTaken = false,
                LinkBit = instItems.LinkBit,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                InstructionRegister = instItems.InstructionRegister,
                pcCounter = ++instItems.pcCounter,
                SetMemValue = false
            };
        }
        public InstructionResult TPC(InstructionItems instItems)
        {
            return new InstructionResult()
            {
                accumulatorOctal = instItems.accumulatorOctal,
                BranchTaken = false,
                LinkBit = instItems.LinkBit,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                InstructionRegister = instItems.InstructionRegister,
                pcCounter = ++instItems.pcCounter,
                SetMemValue = false
            };
        }
        public InstructionResult TLS(InstructionItems instItems)
        {
            return new InstructionResult()
            {
                accumulatorOctal = instItems.accumulatorOctal,
                BranchTaken = false,
                LinkBit = instItems.LinkBit,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                InstructionRegister = instItems.InstructionRegister,
                pcCounter = ++instItems.pcCounter,
                SetMemValue = false
            };
        }
           
        public InstructionResult SKON(InstructionItems instItems)
        {
            return new InstructionResult()
            {
                accumulatorOctal = instItems.accumulatorOctal,
                BranchTaken = false,
                LinkBit = instItems.LinkBit,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                InstructionRegister = instItems.InstructionRegister,
                pcCounter = ++instItems.pcCounter,
                SetMemValue = false
            };
        }
            
        public InstructionResult ION(InstructionItems instItems)
        {
            return new InstructionResult()
            {
                accumulatorOctal = instItems.accumulatorOctal,
                BranchTaken = false,
                LinkBit = instItems.LinkBit,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                InstructionRegister = instItems.InstructionRegister,
                pcCounter = ++instItems.pcCounter,
                SetMemValue = false
            };
        }
        public InstructionResult IOF(InstructionItems instItems)
        {
            return new InstructionResult()
            {
                accumulatorOctal = instItems.accumulatorOctal,
                BranchTaken = false,
                LinkBit = instItems.LinkBit,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                InstructionRegister = instItems.InstructionRegister,
                pcCounter = ++instItems.pcCounter,
                SetMemValue = false
            };
        }

        public int clockCycles
        {
            get { return ClockCycles; }
        }


        public Constants.OpCode instructionType
        {
            get { return InstructionType; }
        }
    }
}
