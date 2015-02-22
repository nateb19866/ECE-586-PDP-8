using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECE486_PDP_8_Emulator.Classes;

namespace ECE486_PDP_8_Emulator.Instructions
{
    public class OprInstruction:IInstruction
    {
        // Question: How does OprInstruction Class know which microcode instruction?
        private int ClockCycles = 1;    // CC = 1 for all microcodes
        private Constants.OpCode InstructionType = Constants.OpCode.OPR;
       // Constants.Microcode instructionType { get; }
        public InstructionItems items = new InstructionItems();
        // Get Microcode Instruction
        public static IInstruction GetInstruction(Constants.Microcode opCode)
        {
            switch (opCode)
            {
                case Constants.Microcode.NOP:
                    //NOPInstruction(InstructionItems instItems);
                    return NOPInstruction();
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
                default:
                    throw new InvalidOperationException();
            }

        }


        public int clockCycles
        {
            get { return ClockCycles; }
        }


        public Constants.Microcode instructionType
        {
            get { return InstructionType; }
        }


        // INSTRUCTION DEFINITIONS:

        public InstructionResult NOPInstruction(InstructionItems instItems)
        {
            // NOP, keep all same
            return new InstructionResult()
            {
                accumulatorOctal =  instItems.accumulatorOctal,
                LinkBit = instItems.LinkBit,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                MicroCodes = instItems.MicroCodes,
                BranchTaken = false,    //?
                pcCounter = instItems.pcCounter++,
                SetMemValue = false     //?
            };
        }

        public InstructionResult M1_CLAInstruction(InstructionItems instItems)
        {
            // Clear AC of uInstruction-1
            return new InstructionResult()
            {
                accumulatorOctal = Utils.DecimalToOctal(0),
                LinkBit = instItems.LinkBit,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                MicroCodes = instItems.MicroCodes,
                BranchTaken = false,    //?
                pcCounter = instItems.pcCounter++,
                SetMemValue = false     //?
            };
        }
        public InstructionResult CLLInstruction(InstructionItems instItems)
        {
            // Clear Link of uInstruction-1
            return new InstructionResult()
            {
                accumulatorOctal = instItems.accumulatorOctal,
                LinkBit = false,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                MicroCodes = instItems.MicroCodes,
                BranchTaken = false,    //?
                pcCounter = instItems.pcCounter++,
                SetMemValue = false     //?
            };

        }
        public InstructionResult CMAInstruction(InstructionItems instItems)
        {
            // complement every bit of AC
            return new InstructionResult()
            {
                // complement every bit of AC
                accumulatorOctal = ~(instItems.accumulatorOctal),

                //If used in conjunction with CLA, set all 12 bits of AC to 1 ( = 7777 )

                LinkBit = false,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                MicroCodes = instItems.MicroCodes,
                BranchTaken = false,    //?
                pcCounter = instItems.pcCounter++,
                SetMemValue = false     //?
            };
        }
        public InstructionResult CMLInstruction(InstructionItems instItems)
        {
            // complement every bit of AC
            return new InstructionResult()
            {

                accumulatorOctal = instItems.accumulatorOctal,
                LinkBit = !(instItems.LinkBit),
                // If used in conjunction with CLL, this sets the link bit to one. 
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                MicroCodes = instItems.MicroCodes,
                BranchTaken = false,    //?
                pcCounter = instItems.pcCounter++,
                SetMemValue = false     //?
            };
        }
        public InstructionResult IACInstruction(InstructionItems instItems)
        {
            int tempLink = 0;
            bool LinkReturn;
            //Increment 13 BitConverter Link/AC
            //Used with CMA, this computes the 2's complement. 
            //Used with CLA, this loads the constant 1. 

            //Converting to byte array to make things easier.
            Int16 TestWord1Bytes = Convert.ToInt16(instItems.accumulatorOctal.ToString(), 12);

            // Concatenante Linkbit and AC
            if (instItems.LinkBit == true)
                tempLink = 1;
            else // =0
                tempLink = 0;

            //Increment 13 Bit Link/AC
            int FinalWord = (tempLink + TestWord1Bytes) + 1;

            int finalAC = Convert.ToInt16(FinalWord.ToString(), 12);
            tempLink = Convert.ToInt16(FinalWord.ToString().Remove(12, 1), 1); // return 13th bit

            if (tempLink == 0)
                LinkReturn = false;
            else
                LinkReturn = true;

            return new InstructionResult()
            {
                accumulatorOctal = Utils.DecimalToOctal(finalAC),
                LinkBit = LinkReturn,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                MicroCodes = instItems.MicroCodes,
                BranchTaken = false,    //?
                pcCounter = instItems.pcCounter++,
                SetMemValue = false     //?
            };


        }
        public InstructionResult RARInstruction(InstructionItems instItems)
        {
            int tempLink = 0;
            bool LinkReturn;

            //Rotate 13 bit Link/AC left by 2 ( 2 of RAL )

            //Converting to byte array to make things easier.
            Int16 TestWord1Bytes = Convert.ToInt16(instItems.accumulatorOctal.ToString(), 12);

            // Concatenante Linkbit and AC
            if (instItems.LinkBit == true)
                tempLink = 1;
            else // =0
                tempLink = 0;

            // Rotate 13 Bit Link/AC right by 1
            int FinalWord = (tempLink + TestWord1Bytes) << 1;

            int finalAC = Convert.ToInt16(FinalWord.ToString(), 12);
            tempLink = Convert.ToInt16(FinalWord.ToString().Remove(12, 1), 1); // return 13th bit

            if (tempLink == 0)
                LinkReturn = false;
            else
                LinkReturn = true;

            return new InstructionResult()
            {
                accumulatorOctal = Utils.DecimalToOctal(finalAC),
                LinkBit = LinkReturn,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                MicroCodes = instItems.MicroCodes,
                BranchTaken = false,    //?
                pcCounter = instItems.pcCounter++,
                SetMemValue = false     //?
            };

        }
        public InstructionResult RTRInstruction(InstructionItems instItems)
        {
            int tempLink = 0;
            bool LinkReturn;

            //Rotate 13 bit Link/AC right by 2 ( 2 of RAR )

            //Converting to byte array to make things easier.
            Int16 TestWord1Bytes = Convert.ToInt16(instItems.accumulatorOctal.ToString(), 12);

            // Concatenante Linkbit and AC
            if (instItems.LinkBit == true)
                tempLink = 1;
            else // =0
                tempLink = 0;

            // Rotate 13 Bit Link/AC right by 1
            int FinalWord = (tempLink + TestWord1Bytes) >> 2;

            int finalAC = Convert.ToInt16(FinalWord.ToString(), 12);
            tempLink = Convert.ToInt16(FinalWord.ToString().Remove(12, 1), 1); // return 13th bit

            if (tempLink == 0)
                LinkReturn = false;
            else
                LinkReturn = true;

            return new InstructionResult()
            {
                accumulatorOctal = Utils.DecimalToOctal(finalAC),
                LinkBit = LinkReturn,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                MicroCodes = instItems.MicroCodes,
                BranchTaken = false,    //?
                pcCounter = instItems.pcCounter++,
                SetMemValue = false     //?
            };

        }
        public InstructionResult RALInstruction(InstructionItems instItems)
        {
            int tempLink = 0;
            bool LinkReturn;
            //Rotate 13 bit Link/AC left by 2 ( 2 of RAL )

            //Converting to byte array to make things easier.
            Int16 TestWord1Bytes = Convert.ToInt16(instItems.accumulatorOctal.ToString(), 12);

            // Concatenante Linkbit and AC
            if (instItems.LinkBit == true)
                tempLink = 1;
            else // =0
                tempLink = 0;

            // Rotate 13 Bit Link/AC right by 1
            int FinalWord = (tempLink + TestWord1Bytes) << 1;

            int finalAC = Convert.ToInt16(FinalWord.ToString(), 12);
            tempLink = Convert.ToInt16(FinalWord.ToString().Remove(12, 1), 1); // return 13th bit

            if (tempLink == 0)
                LinkReturn = false;
            else
                LinkReturn = true;

            return new InstructionResult()
            {
                accumulatorOctal = Utils.DecimalToOctal(finalAC),
                LinkBit = LinkReturn,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                MicroCodes = instItems.MicroCodes,
                BranchTaken = false,    //?
                pcCounter = instItems.pcCounter++,
                SetMemValue = false     //?
            };


        }
        public InstructionResult RTLInstruction(InstructionItems instItems)
        {
            int tempLink = 0;
            bool LinkReturn;
            //Rotate 13 bit Link/AC left by 2 ( 2 of RAL )

            //Converting to byte array to make things easier.
            Int16 TestWord1Bytes = Convert.ToInt16(instItems.accumulatorOctal.ToString(), 12);

            // Concatenante Linkbit and AC
            if (instItems.LinkBit == true)
                tempLink = 1;
            else // =0
                tempLink = 0;

            // Rotate 13 Bit Link/AC right by 1
            int FinalWord = (tempLink + TestWord1Bytes) << 2;

            int finalAC = Convert.ToInt16(FinalWord.ToString(), 12);
            tempLink = Convert.ToInt16(FinalWord.ToString().Remove(12, 1), 1); // return 13th bit

            if (tempLink == 0)
                LinkReturn = false;
            else
                LinkReturn = true;

            return new InstructionResult()
            {
                accumulatorOctal = Utils.DecimalToOctal(finalAC),
                LinkBit = LinkReturn,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                MicroCodes = instItems.MicroCodes,
                BranchTaken = false,    //?
                pcCounter = instItems.pcCounter++,
                SetMemValue = false     //?
            };

        }
        public InstructionResult SMAInstruction(InstructionItems instItems)
        {
            //If AC sign bit = 1, 
            //Skips next instruction (increment PC )

            int tempPC = 0;

            Int16 TestWord1Bytes = Convert.ToInt16(instItems.accumulatorOctal.ToString(), 12);
            int tempACsign = Convert.ToInt16(instItems.accumulatorOctal.ToString().Remove(12, 1), 1); // return 13th bit

            if (tempACsign == 1)
                tempPC = instItems.pcCounter++;
            else
                tempPC = instItems.pcCounter;

            return new InstructionResult()
            {
                accumulatorOctal = instItems.accumulatorOctal,
                LinkBit = instItems.LinkBit,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                MicroCodes = instItems.MicroCodes,
                BranchTaken = false,    //?
                pcCounter = tempPC,
                SetMemValue = false     //?
            };



        }
        public InstructionResult SZAInstruction(InstructionItems instItems)
        {
            //If AC = 0, 
            //Skips next instruction (increment PC )

            int tempPC = 0;

            Int16 TestWord1Bytes = Convert.ToInt16(instItems.accumulatorOctal.ToString(), 12);

            if (TestWord1Bytes == 0)
                tempPC = instItems.pcCounter++;
            else
                tempPC = instItems.pcCounter;

            return new InstructionResult()
            {
                accumulatorOctal = instItems.accumulatorOctal,
                LinkBit = instItems.LinkBit,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                MicroCodes = instItems.MicroCodes,
                BranchTaken = false,    //?
                pcCounter = tempPC,
                SetMemValue = false     //?
            };

        }
        public InstructionResult SNLInstruction(InstructionItems instItems)
        {
            //If LinkBit is not 0, 
            //Skips next instruction (increment PC )

            int tempPC = 0;


            if (instItems.LinkBit == true)
                tempPC = instItems.pcCounter++;
            else
                tempPC = instItems.pcCounter;

            return new InstructionResult()
            {
                accumulatorOctal = instItems.accumulatorOctal,
                LinkBit = instItems.LinkBit,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                MicroCodes = instItems.MicroCodes,
                BranchTaken = false,    //?
                pcCounter = tempPC,
                SetMemValue = false     //?
            };

        }
        public InstructionResult SPAInstruction(InstructionItems instItems)
        {
            //If AC sign bit = 0 (pos), 
            //Skips next instruction (increment PC )

            int tempPC = 0;

            Int16 TestWord1Bytes = Convert.ToInt16(instItems.accumulatorOctal.ToString(), 12);
            int tempACsign = Convert.ToInt16(instItems.accumulatorOctal.ToString().Remove(12, 1), 1); // return 13th bit

            if (tempACsign == 0)
                tempPC = instItems.pcCounter++;
            else
                tempPC = instItems.pcCounter;

            return new InstructionResult()
            {
                accumulatorOctal = instItems.accumulatorOctal,
                LinkBit = instItems.LinkBit,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                MicroCodes = instItems.MicroCodes,
                BranchTaken = false,    //?
                pcCounter = tempPC,
                SetMemValue = false     //?
            };


        }
        public InstructionResult SNAInstruction(InstructionItems instItems)
        {
            //If AC sign bit = 0 (pos), 
            //Skips next instruction (increment PC )

            int tempPC = 0;

            Int16 TestWord1Bytes = Convert.ToInt16(instItems.accumulatorOctal.ToString(), 12);
            if (TestWord1Bytes != 0)
                tempPC = instItems.pcCounter++;
            else
                tempPC = instItems.pcCounter;

            return new InstructionResult()
            {
                accumulatorOctal = instItems.accumulatorOctal,
                LinkBit = instItems.LinkBit,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                MicroCodes = instItems.MicroCodes,
                BranchTaken = false,    //?
                pcCounter = tempPC,
                SetMemValue = false     //?
            };
        }
        public InstructionResult SZLInstruction(InstructionItems instItems)
        {
            //If LinkBit is 0, 
            //Skips next instruction (increment PC )

            int tempPC = 0;


            if (instItems.LinkBit == false)
                tempPC = instItems.pcCounter++;
            else
                tempPC = instItems.pcCounter;

            return new InstructionResult()
            {
                accumulatorOctal = instItems.accumulatorOctal,
                LinkBit = instItems.LinkBit,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                MicroCodes = instItems.MicroCodes,
                BranchTaken = false,    //?
                pcCounter = tempPC,
                SetMemValue = false     //?
            };

        }
        public InstructionResult SKPInstruction(InstructionItems instItems)
        {
            // Need to implement

            return new InstructionResult()
            {
               
            };
        }
        public InstructionResult M2_CLAInstruction(InstructionItems instItems)
        {
            // Clear AC of uInstruction-2
            return new InstructionResult()
            {
                //Only important part
                accumulatorOctal = Utils.DecimalToOctal(0),
                //Rest is just copying from inputs
                LinkBit = instItems.LinkBit,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                MicroCodes = instItems.MicroCodes,
                BranchTaken = false,    //?
                pcCounter = instItems.pcCounter++,
                SetMemValue = false     //?
            };

        }
        public InstructionResult OSRInstruction(InstructionItems instItems)
        {
            // Need to implement
            ClockCycles = 2;
            return new InstructionResult()
            {

            };
        }

        public InstructionResult HLTInstruction(InstructionItems instItems)
        {
            // Need to implement

            return new InstructionResult()
            {

            };
        }
    }

}

