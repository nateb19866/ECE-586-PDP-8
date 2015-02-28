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

            switch ((Constants.Microcode)(Utils.DecimalToOctal(instItems.MicroCodes)))
            {
                case Constants.Microcode.NOP:
                    Rslt = NopInstruction(instItems);
                    break;
                case Constants.Microcode.M1_CLA:
                    Rslt = M1_CLAInstruction(instItems);
                    break;
                case Constants.Microcode.CLL:
                    Rslt = CLLInstruction(instItems);
                    break;
                case Constants.Microcode.CMA:
                    Rslt = CMAInstruction(instItems);
                    break;
                case Constants.Microcode.CML:
                    Rslt = CMLInstruction(instItems);
                    break;
                case Constants.Microcode.IAC:
                    Rslt = IACInstruction(instItems);
                    break;
                case Constants.Microcode.RAR:
                    Rslt = RARInstruction(instItems);
                    break;
                case Constants.Microcode.RTR:
                    Rslt = RTRInstruction(instItems);
                    break;
                case Constants.Microcode.RAL:
                    Rslt = RALInstruction(instItems);
                    break;
                case Constants.Microcode.RTL:
                    Rslt = RTLInstruction(instItems);
                    break;
                case Constants.Microcode.SMA:
                    Rslt = SMAInstruction(instItems);
                    break;
                case Constants.Microcode.SZA:
                    Rslt = SZAInstruction(instItems);
                    break;
                case Constants.Microcode.SNL:
                    Rslt = SNLInstruction(instItems);
                    break;
                case Constants.Microcode.SPA:
                    Rslt = SPAInstruction(instItems);
                    break;
                case Constants.Microcode.SNA:
                    Rslt = SNAInstruction(instItems);
                    break;
                case Constants.Microcode.SZL:
                    Rslt = SZLInstruction(instItems);
                    break;
                case Constants.Microcode.SKP:
                    Rslt = SKPInstruction(instItems);
                    break;
                case Constants.Microcode.M2_CLA:
                    Rslt = M2_CLAInstruction(instItems);
                    break;
                case Constants.Microcode.OSR:
                    Rslt = OSRInstruction(instItems);
                    break;
                case Constants.Microcode.HLT:
                    Rslt = HLTInstruction(instItems);
                    break;
                case Constants.Microcode.M3_CLA:
                    Rslt = M3_CLA(instItems);
                    break;
                case Constants.Microcode.MQL:
                    Rslt = MQL(instItems);
                    break;
                case Constants.Microcode.MQA:
                    Rslt = MQA(instItems);
                    break;
                case Constants.Microcode.SWP:
                    Rslt = SWP(instItems);
                    break;
                case Constants.Microcode.CAM:
                    Rslt = CAM(instItems);
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

        /* 
        * NOP instruction does nothing. Passes all parameters as is.
        */
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

        /* 
        * M1_CLA instruction Clears AC.
        */
        public InstructionResult M1_CLAInstruction(InstructionItems instItems)
        {
            return new InstructionResult()
            {
                accumulatorOctal = Utils.DecimalToOctal(0),
                LinkBit = instItems.LinkBit,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                MicroCodes = instItems.MicroCodes,
                BranchTaken = false,    
                pcCounter = ++instItems.pcCounter,
                SetMemValue = false 
            };
        }

        /* 
       * CLL instruction Clears Link, sets to false.
       */
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
                BranchTaken = false,   
                pcCounter = ++instItems.pcCounter,
                SetMemValue = false    
            };
        }

        /* 
       * CMA instruction Complements every bit of AC.
       */
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
                BranchTaken = false,  
                pcCounter = ++instItems.pcCounter,
                SetMemValue = false    
            };
        }

        /* 
       * CML instruction Complements Link Bit
       */
        public InstructionResult CMLInstruction(InstructionItems instItems)
        {
            return new InstructionResult()
            {
                accumulatorOctal = instItems.accumulatorOctal,
                LinkBit = !(instItems.LinkBit),
                // If used in conjunction with CLL, this sets the link bit to one. 
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                MicroCodes = instItems.MicroCodes,
                BranchTaken = false,
                pcCounter = ++instItems.pcCounter,
                SetMemValue = false    
            };
        }

        /* 
         * IAC instruction increments 13 bit of Link an AC
         */
        public InstructionResult IACInstruction(InstructionItems instItems)
        {
            int tempLink = 0;
            bool LinkReturn;

            //Used with CMA, this computes the 2's complement. 
            //Used with CLA, this loads the constant 1. 

            //Converting to byte array to make things easier.
            Int16 TestWord1Bytes = Convert.ToInt16(instItems.accumulatorOctal.ToString(), 8);

            // Put Link Bit into 13th bit of AC
            if (instItems.LinkBit == true)
                TestWord1Bytes |= (1 << 12);
            else // Link bit false = 0
                TestWord1Bytes &= ~(1 << 12);

            // Link and AC value by 1
            TestWord1Bytes = ++TestWord1Bytes; 

            // AND with mask to get last 12 bits
            int finalAC = TestWord1Bytes & 0xfff;
            tempLink = (TestWord1Bytes>> 12) & 1;
           
            // Set Link Bit to bool accordingly
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
                BranchTaken = false,   
                pcCounter = ++instItems.pcCounter,
                SetMemValue = false    
            };
        }

        /* 
         * RAR instruction Rotates the 13 bit Link and AC right once.
         */
        public InstructionResult RARInstruction(InstructionItems instItems)
        {
            int tempLink = 0;
            bool LinkReturn;

            //Converting to byte array to make things easier.
            Int16 TestWord1Bytes = Convert.ToInt16(instItems.accumulatorOctal.ToString(), 8); // = 511 as decimal

            // Put Link Bit into 13th bit of AC
            if (instItems.LinkBit == true)
                TestWord1Bytes |= (1 << 12);
            else // Link bit false = 0
                TestWord1Bytes &= ~(1 << 12);

            // Rotate 13 Bit Link/AC right by 1

            // AND with mask to get 12 bits with rotate right once
            int finalAC = ((TestWord1Bytes >> 1) | (TestWord1Bytes << 12)) & 0xfff;
            // AND with mask to get 13th bit with rotate right once
            tempLink = ((((TestWord1Bytes >> 1) | (TestWord1Bytes << 12)) >> 12) & 1);

            // Set Link Bit to bool accordingly
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
                BranchTaken = false, 
                pcCounter = ++instItems.pcCounter,
                SetMemValue = false    
            };

        }

        /* 
        * RAR instruction Rotates the 13 bit Link and AC right twice.
        */
        public InstructionResult RTRInstruction(InstructionItems instItems)
        {
            int tempLink = 0;
            bool LinkReturn;

            //Converting to byte array to make things easier.
            Int16 TestWord1Bytes = Convert.ToInt16(instItems.accumulatorOctal.ToString(), 8);

            // Put Link Bit into 13th bit of AC
            if (instItems.LinkBit == true)
                TestWord1Bytes |= (1 << 12);
            else // Link bit false = 0
                TestWord1Bytes &= ~(1 << 12);

            // Rotate 13 Bit Link/AC right by 1
           
            // AND with mask to get 12 bits with rotate right once
            int finalAC = ((TestWord1Bytes >> 2) | (TestWord1Bytes << 11)) & 0xfff;
            // AND with mask to get 13th bit with rotate right once
            tempLink = ((((TestWord1Bytes >> 2) | (TestWord1Bytes << 11)) >> 12) & 1);

            // Set Link Bit to bool accordingly
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
                BranchTaken = false,   
                pcCounter = ++instItems.pcCounter,
                SetMemValue = false    
            };

        }

        /* 
       * RAL instruction Rotates the 13 bit Link and AC right once.
       */
        public InstructionResult RALInstruction(InstructionItems instItems)
        {
            int tempLink = 0;
            bool LinkReturn;

            //Converting to byte array to make things easier.
            Int16 TestWord1Bytes = Convert.ToInt16(instItems.accumulatorOctal.ToString(), 8); 

            // Put Link Bit into 13th bit of AC
            if (instItems.LinkBit == true)
                TestWord1Bytes |= (1 << 12);
            else // Link bit false = 0
                TestWord1Bytes &= ~(1 << 12);

            // Rotate 13 Bit Link/AC right by 1

            // AND with mask to get 12 bits with rotate left once
            int finalAC = ((TestWord1Bytes << 1) | (TestWord1Bytes >> 12)) & 0xfff;
            // AND with mask to get 13th bit with rotate left once
            tempLink = ((((TestWord1Bytes << 1) | (TestWord1Bytes >> 12)) >> 12) & 1);

            // Set Link Bit to bool accordingly
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
                BranchTaken = false,   
                pcCounter = instItems.pcCounter++,
                SetMemValue = false     
            };


        }

        /* 
        * RAL instruction Rotates the 13 bit Link and AC right twice.
        */
        public InstructionResult RTLInstruction(InstructionItems instItems)
        {
            int tempLink = 0;
            bool LinkReturn;
            //Rotate 13 bit Link/AC left by 2 ( 2 of RAL )

            //Converting to byte array to make things easier.
            Int16 TestWord1Bytes = Convert.ToInt16(instItems.accumulatorOctal.ToString(), 8);

            // Put Link Bit into 13th bit of AC
            if (instItems.LinkBit == true)
                TestWord1Bytes |= (1 << 12);
            else // Link bit false = 0
                TestWord1Bytes &= ~(1 << 12);

            // Rotate 13 Bit Link/AC right by 1

            // AND with mask to get 12 bits with rotate right once
            int finalAC = ((TestWord1Bytes << 2) | (TestWord1Bytes >> 11)) & 0xfff;
            // AND with mask to get 13th bit with rotate right once
            tempLink = ((((TestWord1Bytes << 2) | (TestWord1Bytes >> 11)) >> 12) & 1);

            // Set Link Bit to bool accordingly
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
                BranchTaken = false, 
                pcCounter = ++instItems.pcCounter,
                SetMemValue = false    
            };

        }

        /* 
        * SMA instruction: Skips next instruction, increment PC by 2, if AC is negative
         */
        public InstructionResult SMAInstruction(InstructionItems instItems)
        {
            //If sign bit of AC = 1
            //Skips next instruction (increment PC )

            int tempPC = 0;

            Int16 TestWord1Bytes = Convert.ToInt16(instItems.accumulatorOctal.ToString(), 8);

            // AND with mask to get 12th sign bit of AC
            int ACsign = TestWord1Bytes & 0x800;

            // PC + 2 if sign is 1 ( neg )
            if (ACsign == 1)
                tempPC = instItems.pcCounter + 2;
            else
                tempPC = instItems.pcCounter;

            return new InstructionResult()
            {
                accumulatorOctal = instItems.accumulatorOctal,
                LinkBit = instItems.LinkBit,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                MicroCodes = instItems.MicroCodes,
                BranchTaken = true,
                BranchType = Constants.BranchType.Conditional,
                pcCounter = tempPC,
                SetMemValue = false
            };

        }

        /* 
        * SZA instruction: Skips next instruction, increment PC by 2, if AC is 0.
         */
        public InstructionResult SZAInstruction(InstructionItems instItems)
        {
            int tempPC = 0;

            Int16 TestWord1Bytes = Convert.ToInt16(instItems.accumulatorOctal.ToString(), 8);

            if (TestWord1Bytes == 0)
                tempPC = instItems.pcCounter + 2;
            else
                tempPC = instItems.pcCounter;

            return new InstructionResult()
            {
                accumulatorOctal = instItems.accumulatorOctal,
                LinkBit = instItems.LinkBit,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                MicroCodes = instItems.MicroCodes,
                BranchTaken = true,
                BranchType = Constants.BranchType.Conditional,
                pcCounter = tempPC,
                SetMemValue = false    
            };
        }

        /* 
        * SNL instruction: Skips next instruction, increment PC by 2, if link bit is 0.
         */
        public InstructionResult SNLInstruction(InstructionItems instItems)
        {
            int tempPC = 0;

            if (instItems.LinkBit == true)
                tempPC = instItems.pcCounter + 2;
            else
                tempPC = instItems.pcCounter;

            return new InstructionResult()
            {
                accumulatorOctal = instItems.accumulatorOctal,
                LinkBit = instItems.LinkBit,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                MicroCodes = instItems.MicroCodes,
                BranchTaken = true,
                BranchType = Constants.BranchType.Conditional,
                pcCounter = tempPC,
                SetMemValue = false    
            };

        }

        /*
       * SPA instruction skips next instruction, increment PC by 2, if AC is positive.
       * */
        public InstructionResult SPAInstruction(InstructionItems instItems)
        {
            //If AC sign bit = 0 (pos), 
            //Skips next instruction (increment PC )

            int tempPC = 0;

            Int16 TestWord1Bytes = Convert.ToInt16(instItems.accumulatorOctal.ToString(), 8);

            // AND with mask to get 12th sign bit of AC
            int ACsign = TestWord1Bytes & 0x800;

            // PC + 2 if sign is 0 ( pos )
            if (ACsign == 0)
                tempPC = instItems.pcCounter + 2;
            else
                tempPC = instItems.pcCounter;

            return new InstructionResult()
            {
                accumulatorOctal = instItems.accumulatorOctal,
                LinkBit = instItems.LinkBit,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                MicroCodes = instItems.MicroCodes,
                BranchTaken = true,
                BranchType = Constants.BranchType.Conditional,
                pcCounter = tempPC,
                SetMemValue = false    
            };

        }

        /*
       * SNA instruction skips next instruction, increment PC by 2, if any bit of AC is not 0.
       * */
        public InstructionResult SNAInstruction(InstructionItems instItems)
        {
            // If AC is not 0 
            //Skips next instruction (increment PC )

            int tempPC = 0;

            Int16 TestWord1Bytes = Convert.ToInt16(instItems.accumulatorOctal.ToString(), 8);
            if (TestWord1Bytes != 0)
                tempPC = instItems.pcCounter + 2;
            else
                tempPC = instItems.pcCounter;

            return new InstructionResult()
            {
                accumulatorOctal = instItems.accumulatorOctal,
                LinkBit = instItems.LinkBit,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                MicroCodes = instItems.MicroCodes,
                BranchTaken = true,
                BranchType = Constants.BranchType.Conditional,
                pcCounter = tempPC,
                SetMemValue = false    
            };
        }

        /*
        * SZL instruction skips next instruction, increment PC by 2, if link bit is 0.
        * */
        public InstructionResult SZLInstruction(InstructionItems instItems)
        {
            int tempPC = 0;

            if (instItems.LinkBit == false)
                tempPC = instItems.pcCounter+2;
            else
                tempPC = instItems.pcCounter;

            return new InstructionResult()
            {
                accumulatorOctal = instItems.accumulatorOctal,
                LinkBit = instItems.LinkBit,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                MicroCodes = instItems.MicroCodes,
                BranchTaken = true,
                BranchType = Constants.BranchType.Conditional,
                pcCounter = tempPC,
                SetMemValue = false    
            };

        }
        
        /*
         * SKP instruction skips next instruction, increment PC by 2.
         * */
        public InstructionResult SKPInstruction(InstructionItems instItems)
        {
            return new InstructionResult()
            {
                accumulatorOctal = instItems.accumulatorOctal,
                LinkBit = instItems.LinkBit,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                MicroCodes = instItems.MicroCodes,
                BranchTaken = true, 
                pcCounter = instItems.pcCounter + 2,
                SetMemValue = false,
                BranchType = Constants.BranchType.Unconditional
            };
        }

        /*
         *M2_CLA instruction: clears AC.
         */
        public InstructionResult M2_CLAInstruction(InstructionItems instItems)
        {
            return new InstructionResult()
            {
                accumulatorOctal = Utils.DecimalToOctal(0),
                LinkBit = instItems.LinkBit,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                MicroCodes = instItems.MicroCodes,
                BranchTaken = false,  
                pcCounter = ++instItems.pcCounter,
                SetMemValue = false    
            };

        }


        private InstructionResult OSRInstruction(InstructionItems instItems)
        {

            return new InstructionResult()

            {


            };
        }
        // Halt is caught in program executer
        public InstructionResult HLTInstruction(InstructionItems instItems)
        {
            return new InstructionResult()
            {

            };
        }
         
        // Microinstruction set 3: Handle by only incrementing PC and instr count.
        public InstructionResult M3_CLA(InstructionItems instItems)
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

        public InstructionResult MQL(InstructionItems instItems)
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

        public InstructionResult MQA(InstructionItems instItems)
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

        public InstructionResult SWP(InstructionItems instItems)
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
         
        public InstructionResult CAM(InstructionItems instItems)
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
