using ECE486_PDP_8_Emulator.Classes;
using System;

namespace ECE486_PDP_8_Emulator.Instructions
{
    public class OprInstruction : IInstruction
    {
        private int ClockCycles = 1;
        private Constants.OpCode InstructionType = Constants.OpCode.OPR;

        public InstructionResult ExecuteInstruction(InstructionItems instItems)
        {
            
            //First initialize the result with the parameters and default values
            InstructionResult Rslt = new InstructionResult(){
              accumulatorOctal = instItems.accumulatorOctal,
               BranchTaken = false,
                InstructionRegister = instItems.InstructionRegister,
                 LinkBit = instItems.LinkBit,
                  MemoryAddress = instItems.MemoryAddress,
                   MemoryValueOctal = instItems.MemoryValueOctal,
                    pcCounter = (++instItems.pcCounter) & 0xFFF,
                     SetMemValue = false,
                     BranchType = null
            };
            if (((Constants.Microcode)instItems.InstructionRegister) == Constants.Microcode.NOP)
                Rslt = NopInstruction(Rslt);

            //Group 1 microinstruction - mask with 000 100 000 000, or 0001 000 0000 in hex
            else if((instItems.InstructionRegister & 0x100) == 0 )
            {
                //Perform Seq 1 operations

                //CLA - mask 000 010 000 000 - 0000 1000 0000
                if ((instItems.InstructionRegister & 0x80) == 0x80)
                    Rslt = M1_CLAInstruction(Rslt);

                //CLL- mask 000 001 000 000 -  0000 0100 0000
                if ((instItems.InstructionRegister & 0x40) == 0x40)
                    Rslt = CLLInstruction(Rslt);

                //perform Seq 2 operations

                //CMA - mask 000 000 100 000 - 0000 0010 0000
                if ((instItems.InstructionRegister & 0x20) == 0x20)
                    Rslt = CMAInstruction(Rslt);

                //CML - mask 000 000 010 000 - 0000 0001 0000
                if ((instItems.InstructionRegister & 0x10) == 0x10)
                    Rslt = CMLInstruction(Rslt);

                //Perform Seq 3 operations

                //IAC - mask 000 000 000 001 - 0000 0000 0001
                if ((instItems.InstructionRegister & 0x1) == 0x1)
                    Rslt = IACInstruction(Rslt);

                //Perform Seq 4 operations

                //RAR - mask 000 000 001 000 - 0000 0000 1000
                if ((instItems.InstructionRegister & 0x8) == 0x8)
                    Rslt = RARInstruction(Rslt);

                //RTR - mask 000 000 001 010 - 0000 0000 1010
                if ((instItems.InstructionRegister & 0xA) == 0xA)
                    Rslt = RTRInstruction(Rslt);

                //RAL - mask 000 000 000 100 - 0000 0000 0100
                if ((instItems.InstructionRegister & 0x4) == 0x4)
                    Rslt = RALInstruction(Rslt);

                //RTL - mask 000 000 000 110 - 0000 0000 0110

                if ((instItems.InstructionRegister & 0x6) == 0x6)
                    Rslt = RTLInstruction(Rslt);


            }
            //Group 2 microinstruction - mask with 000 100 000 001, or 0001 0000 0001 in hex
            else if((instItems.InstructionRegister & 0x101) == 0x100)
            {
                Rslt = Group2Microcodes(Rslt);

            }
           
             //skip Group 3 microinstructions
               
                

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

        private InstructionResult Group2Microcodes(InstructionResult instItems)
        {
            
            if ( (Constants.Microcode)instItems.InstructionRegister != Constants.Microcode.M2_CLA
                && (Constants.Microcode)instItems.InstructionRegister != Constants.Microcode.OSR
                )
            {
                //OR group - when 8th bit is 0
                if ((instItems.InstructionRegister & 0x8) == 0x0)
                {
                    bool PassSMA = false;
                    bool PassSZA = false;
                    bool PassSNL = false;
                     instItems.BranchType = Constants.BranchType.Conditional;

                    //SMA - mask 000 001 000 000 - hex 0000 0100 0000
                    if ((instItems.InstructionRegister & 0x40) == 0x40)
                        PassSMA = instItems.accumulatorOctal > 0x7FF;
                   

                    //SZA - mask 000 000 100 000 - hex 0000 0010 0000
                    if ((instItems.InstructionRegister & 0x20) == 0x20)
                        PassSZA = instItems.accumulatorOctal == 0;

                    //SNL - mask 000 000 010 000 - hex 0000 0001 0000
                    if ((instItems.InstructionRegister & 0x10) == 0x10)
                        PassSNL = instItems.LinkBit;


                    if (PassSMA || PassSZA || PassSNL)
                    {
                        instItems.pcCounter++;

                        instItems.BranchTaken = true;
                    }
                    else
                        instItems.BranchTaken = false;
                }
                else
                {

                    bool? PassSPA = null;
                    bool? PassSNA = null;
                    bool? PassSZL = null;
                    instItems.BranchType = Constants.BranchType.Conditional;
                    //SPA - mask 000 001 001 000 - hex 0000 0100 1000
                    if ((instItems.InstructionRegister & 0x48) == 0x48)
                        PassSPA = instItems.accumulatorOctal <= 0x7FF;


                    //SNA - mask 000 000 101 000 - hex 0000 0010 1000
                    if ((instItems.InstructionRegister & 0x28) == 0x28)
                        PassSNA = instItems.accumulatorOctal != 0;


                    //SZL - mask 000 000 011 000 - hex 0000 0001 1000
                    if ((instItems.InstructionRegister & 0x18) == 0x18)
                        PassSZL = !instItems.LinkBit;


                    if ((PassSNA == null || PassSNA == true)
                        && (PassSPA == null || PassSPA == true)
                        && (PassSZL == null || PassSZL == true)
                        )
                    {
                        instItems.pcCounter++;
                        instItems.BranchTaken = true;
                    }

                    else if (PassSNA == null && PassSPA == null && PassSZL == null)
                    {
                        instItems.pcCounter++;
                        instItems.BranchTaken = true;
                        instItems.BranchType = Constants.BranchType.Unconditional;

                    }


                }
            }
            

               
                //CLA - mask 000 010 000 000 - hex 0000 1000 0000
                if((instItems.InstructionRegister & 0x80) == 0x80)
                    instItems =M2_CLAInstruction(instItems);

                //OSR - mask 000 000 000 100 - hex 0000 0000 0100
                if ((instItems.InstructionRegister & 0x4) == 0x4)
                    instItems = OSRInstruction(instItems);


                //Skip HLT
            

            return instItems;
        }
       
        /*
        * NOP instruction does nothing. Passes all parameters as is.
        */

        private InstructionResult NopInstruction(InstructionResult instItems)
        {
            int IncrementedPcCounter = (++instItems.pcCounter) & 0xFFF;

            return new InstructionResult()
            {
                accumulatorOctal = instItems.accumulatorOctal,
                BranchTaken = false,
                LinkBit = instItems.LinkBit,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                InstructionRegister = instItems.InstructionRegister,
                pcCounter = IncrementedPcCounter,
                SetMemValue = false
            };
        }

        /*
        * M1_CLA instruction Clears AC.
        */

        public InstructionResult M1_CLAInstruction(InstructionResult instItems)
        {
            int IncrementedPcCounter = (++instItems.pcCounter) & 0xFFF;

            return new InstructionResult()
            {
                accumulatorOctal = Utils.DecimalToOctal(0),
                LinkBit = instItems.LinkBit,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                InstructionRegister = instItems.InstructionRegister,
                BranchTaken = false,
                pcCounter = IncrementedPcCounter,
                SetMemValue = false
            };
        }

        /*
       * CLL instruction Clears Link, sets to false.
       */

        public InstructionResult CLLInstruction(InstructionResult instItems)
        {
            int IncrementedPcCounter = (++instItems.pcCounter) & 0xFFF;

            // Clear Link of uInstruction-1
            return new InstructionResult()
            {
                accumulatorOctal = instItems.accumulatorOctal,
                LinkBit = false,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                InstructionRegister = instItems.InstructionRegister,
                BranchTaken = false,
                pcCounter = IncrementedPcCounter,
                SetMemValue = false
            };
        }

        /*
       * CMA instruction Complements every bit of AC.
       */

        public InstructionResult CMAInstruction(InstructionResult instItems)
        {
            int IncrementedPcCounter = (++instItems.pcCounter) & 0xFFF;
            int AC = instItems.accumulatorOctal;
            int accumulatorUpdate = (~AC) & 0xFFF;

            // complement every bit of AC
            return new InstructionResult()
            {
                // complement every bit of AC
                accumulatorOctal = accumulatorUpdate,
                //If used in conjunction with CLA, set all 12 bits of AC to 1 ( = 7777 )
                LinkBit = false,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                InstructionRegister = instItems.InstructionRegister,
                BranchTaken = false,
                pcCounter = IncrementedPcCounter,
                SetMemValue = false
            };
        }

        /*
       * CML instruction Complements Link Bit
       */

        public InstructionResult CMLInstruction(InstructionResult instItems)
        {
            int IncrementedPcCounter = (++instItems.pcCounter) & 0xFFF;

            return new InstructionResult()
            {
                accumulatorOctal = instItems.accumulatorOctal,
                LinkBit = !(instItems.LinkBit),
                // If used in conjunction with CLL, this sets the link bit to one.
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                InstructionRegister = instItems.InstructionRegister,
                BranchTaken = false,
                pcCounter = IncrementedPcCounter,
                SetMemValue = false
            };
        }

        /*
         * IAC instruction increments 13 bit of Link an AC
         */

        public InstructionResult IACInstruction(InstructionResult instItems)
        {
            int tempLink = 0;
            bool LinkReturn;

            //Used with CMA, this computes the 2's complement.
            //Used with CLA, this loads the constant 1.

            //Converting to byte array to make things easier.
            int TestWord1Bytes = instItems.accumulatorOctal;
            // Put Link Bit into 13th bit of AC
            if (instItems.LinkBit == true)
                TestWord1Bytes |= (1 << 12);
            else // Link bit false = 0
                TestWord1Bytes &= ~(1 << 12);

            // Link and AC value by 1
            TestWord1Bytes = ++TestWord1Bytes;

            // AND with mask to get last 12 bits
            int finalAC = TestWord1Bytes & 0xfff;
            tempLink = (TestWord1Bytes >> 12) & 1;

            // Set Link Bit to bool accordingly
            if (tempLink == 0)
                LinkReturn = false;
            else
                LinkReturn = true;

            int IncrementedPcCounter = (++instItems.pcCounter) & 0xFFF;


            return new InstructionResult()
            {
                accumulatorOctal = finalAC,
                LinkBit = LinkReturn,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                InstructionRegister = instItems.InstructionRegister,
                BranchTaken = false,
                pcCounter = IncrementedPcCounter,
                SetMemValue = false
            };
        }

        /*
         * RAR instruction Rotates the 13 bit Link and AC right once.
         */

        public InstructionResult RARInstruction(InstructionResult instItems)
        {

            int tempLink = 0;
            bool LinkReturn;

            //Converting to byte array to make things easier.
            int TestWord1Bytes = instItems.accumulatorOctal;
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

            int IncrementedPcCounter = (++instItems.pcCounter) & 0xFFF;

            return new InstructionResult()
            {
                accumulatorOctal = finalAC,
                LinkBit = LinkReturn,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                InstructionRegister = instItems.InstructionRegister,
                BranchTaken = false,
                pcCounter = IncrementedPcCounter,
                SetMemValue = false
            };
        }

        /*
        * RAR instruction Rotates the 13 bit Link and AC right twice.
        */

        public InstructionResult RTRInstruction(InstructionResult instItems)
        {
            int tempLink = 0;
            bool LinkReturn;

            //Converting to byte array to make things easier.
           int TestWord1Bytes = instItems.accumulatorOctal;
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

            int IncrementedPcCounter = (++instItems.pcCounter) & 0xFFF;

            return new InstructionResult()
            {
                accumulatorOctal = finalAC,
                LinkBit = LinkReturn,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                InstructionRegister = instItems.InstructionRegister,
                BranchTaken = false,
                pcCounter = IncrementedPcCounter,
                SetMemValue = false
            };
        }

        /*
       * RAL instruction Rotates the 13 bit Link and AC right once.
       */

        public InstructionResult RALInstruction(InstructionResult instItems)
        {
            int tempLink = 0;
            bool LinkReturn;

            //Converting to byte array to make things easier.
            int TestWord1Bytes = instItems.accumulatorOctal;
          
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

            int IncrementedPcCounter = (++instItems.pcCounter) & 0xFFF;


            return new InstructionResult()
            {
                accumulatorOctal = finalAC,
                LinkBit = LinkReturn,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                InstructionRegister = instItems.InstructionRegister,
                BranchTaken = false,
                pcCounter = IncrementedPcCounter,
                SetMemValue = false
            };
        }

        /*
        * RAL instruction Rotates the 13 bit Link and AC right twice.
        */

        public InstructionResult RTLInstruction(InstructionResult instItems)
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

            int IncrementedPcCounter = (++instItems.pcCounter) & 0xFFF;

            return new InstructionResult()
            {
                accumulatorOctal = Utils.DecimalToOctal(finalAC),
                LinkBit = LinkReturn,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                InstructionRegister = instItems.InstructionRegister,
                BranchTaken = false,
                pcCounter = IncrementedPcCounter,
                SetMemValue = false
            };
        }

       

        /*
      * SKP instruction does nothing. Passes all parameters as is.
      */

        private InstructionResult SkpInstruction(InstructionResult instItems)
        {
            int IncrementedPcCounter = (++instItems.pcCounter) & 0xFFF;
            IncrementedPcCounter = (++instItems.pcCounter) & 0xFFF;

            return new InstructionResult()
            {
                accumulatorOctal = instItems.accumulatorOctal,
                BranchTaken = false,
                LinkBit = instItems.LinkBit,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                InstructionRegister = instItems.InstructionRegister,
                pcCounter = IncrementedPcCounter,
                SetMemValue = false
            };
        }

        /*
       *M2_CLA instruction: clears AC.
       */
        public InstructionResult M2_CLAInstruction(InstructionResult instItems)
        {
            int IncrementedPcCounter = (++instItems.pcCounter) & 0xFFF;


            return new InstructionResult()
            {
                accumulatorOctal = Utils.DecimalToOctal(0),
                LinkBit = instItems.LinkBit,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                InstructionRegister = instItems.InstructionRegister,
                BranchTaken = instItems.BranchTaken,
                pcCounter = IncrementedPcCounter,
                SetMemValue = false
            };
        }

        private InstructionResult OSRInstruction(InstructionResult instItems)
        {
            int IncrementedPcCounter = (++instItems.pcCounter) & 0xFFF;

            return new InstructionResult()
            {
                accumulatorOctal = instItems.accumulatorOctal,
                BranchTaken = false,
                LinkBit = instItems.LinkBit,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                InstructionRegister = instItems.InstructionRegister,
                pcCounter = IncrementedPcCounter,
                SetMemValue = false
            };
        }

        // Microinstruction set 3: Handle by only incrementing PC and instr count.
        public InstructionResult M3_CLA(InstructionResult instItems)
        {
            int IncrementedPcCounter = (++instItems.pcCounter) & 0xFFF;


            return new InstructionResult()
            {
                accumulatorOctal = instItems.accumulatorOctal,
                BranchTaken = false,
                LinkBit = instItems.LinkBit,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                InstructionRegister = instItems.InstructionRegister,
                pcCounter = IncrementedPcCounter,
                SetMemValue = false
            };
        }

        public InstructionResult MQL(InstructionResult instItems)
        {
            int IncrementedPcCounter = (++instItems.pcCounter) & 0xFFF;


            return new InstructionResult()
            {
                accumulatorOctal = instItems.accumulatorOctal,
                BranchTaken = false,
                LinkBit = instItems.LinkBit,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                InstructionRegister = instItems.InstructionRegister,
                pcCounter = IncrementedPcCounter,
                SetMemValue = false
            };
        }

        public InstructionResult MQA(InstructionResult instItems)
        {
            int IncrementedPcCounter = (++instItems.pcCounter) & 0xFFF;


            return new InstructionResult()
            {
                accumulatorOctal = instItems.accumulatorOctal,
                BranchTaken = false,
                LinkBit = instItems.LinkBit,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                InstructionRegister = instItems.InstructionRegister,
                pcCounter = IncrementedPcCounter,
                SetMemValue = false
            };
        }

        public InstructionResult SWP(InstructionResult instItems)
        {
            int IncrementedPcCounter = (++instItems.pcCounter) & 0xFFF;


            return new InstructionResult()
            {
                accumulatorOctal = instItems.accumulatorOctal,
                BranchTaken = false,
                LinkBit = instItems.LinkBit,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                InstructionRegister = instItems.InstructionRegister,
                pcCounter = IncrementedPcCounter,
                SetMemValue = false
            };
        }

        public InstructionResult CAM(InstructionResult instItems)
        {
            int IncrementedPcCounter = (++instItems.pcCounter) & 0xFFF;


            return new InstructionResult()
            {
                accumulatorOctal = instItems.accumulatorOctal,
                BranchTaken = false,
                LinkBit = instItems.LinkBit,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                InstructionRegister = instItems.InstructionRegister,
                pcCounter = IncrementedPcCounter,
                SetMemValue = false
            };
        }
    }
}