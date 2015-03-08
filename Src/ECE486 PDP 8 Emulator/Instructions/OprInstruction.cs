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
                   // Always increment PC initially and mask
                    pcCounter = (++instItems.pcCounter) & 0xFFF,
                     SetMemValue = false,
              BranchType = null,
              OsrSwitchBits = instItems.OsrSwitchBits
            };
            if (((Constants.Microcode)Utils.DecimalToOctal(instItems.InstructionRegister)) == Constants.Microcode.NOP)
                return Rslt;

            //Group 1 microinstruction - mask with 000 100 000 000, or 0001 000 0000 in hex
            else if ((instItems.InstructionRegister & 0x100) == 0)
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
            else if ((instItems.InstructionRegister & 0x101) == 0x100)
            {
                Rslt = Group2Microcodes(Rslt);

            }


            // all u-3 instructions will just return result b/c not found in u-1 or u-2
            // can clean up functions at end for u-3s
           
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

   


                }
            }
            

               
                //M2_CLA - mask 000 010 000 000 - hex 0000 1000 0000
                if((instItems.InstructionRegister & 0x80) == 0x80)
                    instItems = M2_CLAInstruction(instItems);

                //OSR - mask 000 000 000 100 - hex 0000 0000 0100
                if ((instItems.InstructionRegister & 0x4) == 0x4)
                    instItems = OSRInstruction(instItems);


                //HLT - mask 000 000 000 100 - hex 0000 0000 0010
                if ((instItems.InstructionRegister & 0x2) == 0x2)
                    instItems.IsHalted = true;
            

            return instItems;
        }
       


        /*
        * M1_CLA instruction Clears AC.
        */

        public InstructionResult M1_CLAInstruction(InstructionResult instItems)
        {
            instItems.pcCounter = (instItems.pcCounter) & 0xFFF;

            instItems.accumulatorOctal = 0 & 0xFFF;

            return new InstructionResult()
            {
                accumulatorOctal = instItems.accumulatorOctal,
                LinkBit = instItems.LinkBit,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                InstructionRegister = instItems.InstructionRegister,
                BranchTaken = false,
                pcCounter = instItems.pcCounter,
                SetMemValue = false,
                OsrSwitchBits = instItems.OsrSwitchBits
            };
        }

        /*
       * CLL instruction Clears Link, sets to false.
       */

        public InstructionResult CLLInstruction(InstructionResult instItems)
        {
            instItems.pcCounter = (instItems.pcCounter) & 0xFFF;

            // Clear Link, ie, set false
            return new InstructionResult()
            {
                accumulatorOctal = instItems.accumulatorOctal,
                LinkBit = false,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                InstructionRegister = instItems.InstructionRegister,
                BranchTaken = false,
                pcCounter = instItems.pcCounter,
                SetMemValue = false,
                OsrSwitchBits = instItems.OsrSwitchBits
            };
        }

        /*
       * CMA instruction Complements every bit of AC.
       */

        public InstructionResult CMAInstruction(InstructionResult instItems)
        {
            instItems.pcCounter = (instItems.pcCounter) & 0xFFF;

            instItems.accumulatorOctal = ~(instItems.accumulatorOctal) & 0xFFF;

            // complement every bit of AC
            return new InstructionResult()
            {
                // complement every bit of AC
                accumulatorOctal = instItems.accumulatorOctal,
                //If used in conjunction with CLA, set all 12 bits of AC to 1 ( = 7777 )
                // b/c CLA will always operate first
                LinkBit = false,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                InstructionRegister = instItems.InstructionRegister,
                BranchTaken = false,
                pcCounter = instItems.pcCounter,
                SetMemValue = false,
                OsrSwitchBits = instItems.OsrSwitchBits
            };
        }

        /*
       * CML instruction Complements Link Bit
       */

        public InstructionResult CMLInstruction(InstructionResult instItems)
        {
            instItems.pcCounter = (instItems.pcCounter) & 0xFFF;

            return new InstructionResult()
            {
                accumulatorOctal = instItems.accumulatorOctal,
                LinkBit = !(instItems.LinkBit),
                // If used in conjunction with CLL, this sets the link bit to one.
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                InstructionRegister = instItems.InstructionRegister,
                BranchTaken = false,
                pcCounter = instItems.pcCounter,
                SetMemValue = false,
                OsrSwitchBits = instItems.OsrSwitchBits
            };
        }

        /*
         * IAC instruction increments 13 bit of Link an AC
         */

        public InstructionResult IACInstruction(InstructionResult instItems)
        {
            int tempLink = 0;
            int tempAC = 0;
            //Used with CMA, this computes the 2's complement.
            //Used with CLA, this loads the constant 1.

            // Put Link Bit into 13th bit of AC
            if (instItems.LinkBit == true)
                instItems.accumulatorOctal |= (1 << 12);
            else // Link bit false = 0
                instItems.accumulatorOctal &= ~(1 << 12);

            // Link and AC value by 1
            tempAC = ++instItems.accumulatorOctal;

            // AND with mask to get last 12 bits
            instItems.accumulatorOctal = tempAC & 0xFFF;
           
            // AND with mask to get 13th bit
            tempLink = ((tempAC & 0x01FFF) >> 12) & 0x1;

            // Set Link Bit to bool accordingly
            if (tempLink == 0)
                instItems.LinkBit = false;
            else
                instItems.LinkBit = true;

            // Mask PC to ensure no overflow
            instItems.pcCounter = (instItems.pcCounter) & 0xFFF;


            return new InstructionResult()
            {
                accumulatorOctal = instItems.accumulatorOctal,
                LinkBit = instItems.LinkBit,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                InstructionRegister = instItems.InstructionRegister,
                BranchTaken = false,
                pcCounter = instItems.pcCounter,
                SetMemValue = false,
                OsrSwitchBits = instItems.OsrSwitchBits
            };
        }

        /*
         * RAR instruction Rotates the 13 bit Link and AC right once.
         */

        public InstructionResult RARInstruction(InstructionResult instItems)
        {

            int tempLink = 0;
           
            int tempAC;

            // Put Link Bit into 13th bit of AC
            if (instItems.LinkBit == true)
                instItems.accumulatorOctal |= (1 << 12);
            else // Link bit false = 0
                instItems.accumulatorOctal &= ~(1 << 12);

            // Rotate 13 Bit Link/AC right by 1

            // AND with mask to get 12 bits with rotate right once
            tempAC = ((instItems.accumulatorOctal >> 1) | (instItems.accumulatorOctal << 12));
            instItems.accumulatorOctal = tempAC & 0xFFF;

            // AND with mask to get 13th bit with rotate right once
            tempLink = ((tempAC & 0x01FFF)>>12) & 0x1;

            // Set Link Bit to bool accordingly
            if (tempLink == 0)
                instItems.LinkBit = false;
            else
                instItems.LinkBit = true;

            // Mask and ensure no overflow
            instItems.pcCounter = (instItems.pcCounter) & 0xFFF;

            return new InstructionResult()
            {
                accumulatorOctal = instItems.accumulatorOctal,
                LinkBit = instItems.LinkBit,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                InstructionRegister = instItems.InstructionRegister,
                BranchTaken = false,
                pcCounter = instItems.pcCounter,
                SetMemValue = false,
                OsrSwitchBits = instItems.OsrSwitchBits
            };
        }

        /*
        * RAR instruction Rotates the 13 bit Link and AC right twice.
        */

        public InstructionResult RTRInstruction(InstructionResult instItems)
        {
            // Call RAR to rotate once more, confirmed PC does not increment again
            RARInstruction(instItems);

            // Mask and ensure no overflow
            instItems.accumulatorOctal = instItems.accumulatorOctal & 0xFFF;
            instItems.pcCounter = (instItems.pcCounter) & 0xFFF;

            return new InstructionResult()
            {
                accumulatorOctal = instItems.accumulatorOctal,
                LinkBit = instItems.LinkBit,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                InstructionRegister = instItems.InstructionRegister,
                BranchTaken = false,
                pcCounter = instItems.pcCounter,
                SetMemValue = false,
                OsrSwitchBits = instItems.OsrSwitchBits

            };
        }

        /*
       * RAL instruction Rotates the 13 bit Link and AC right once.
       */

        public InstructionResult RALInstruction(InstructionResult instItems)
        {
            int tempLink = 0;
         
            int tempAC;

          
            // Put Link Bit into 13th bit of AC
            if (instItems.LinkBit == true)
                instItems.accumulatorOctal |= (1 << 12);
            else // Link bit false = 0
                instItems.accumulatorOctal &= ~(1 << 12);

            // Rotate 13 Bit Link/AC left by 1

            // AND with mask to get 12 bits with rotate left once
            tempAC = ((instItems.accumulatorOctal << 1) | (instItems.accumulatorOctal >> 12));
            instItems.accumulatorOctal = tempAC & 0xFFF;

            // AND with mask to get 13th bit
            tempLink = ((tempAC & 0x01FFF)>>12) & 0x1;

             // Set Link Bit to bool accordingly
            if (tempLink == 0)
                instItems.LinkBit = false;
            else
                instItems.LinkBit = true;

            // Mask and ensure no overflow
            instItems.pcCounter = (instItems.pcCounter) & 0xFFF;

            return new InstructionResult()
            {
                accumulatorOctal = instItems.accumulatorOctal,
                LinkBit = instItems.LinkBit,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                InstructionRegister = instItems.InstructionRegister,
                BranchTaken = false,
                pcCounter = instItems.pcCounter,
                SetMemValue = false,
                OsrSwitchBits = instItems.OsrSwitchBits
            };
        }

        /*
        * RAL instruction Rotates the 13 bit Link and AC right twice.
        */

        public InstructionResult RTLInstruction(InstructionResult instItems)
        {
            // Call RAR to rotate once more, confirmed PC does not increment again
            RALInstruction(instItems);

            // Mask and ensure no overflow
            instItems.accumulatorOctal = instItems.accumulatorOctal & 0xFFF;
            instItems.pcCounter = (instItems.pcCounter) & 0xFFF;

            return new InstructionResult()
            {
                accumulatorOctal = instItems.accumulatorOctal,
                LinkBit = instItems.LinkBit,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                InstructionRegister = instItems.InstructionRegister,
                BranchTaken = false,
                pcCounter = instItems.pcCounter,
                SetMemValue = false,
                OsrSwitchBits = instItems.OsrSwitchBits

            };
        }


        /*
       *M2_CLA instruction: clears AC.
       */
        public InstructionResult M2_CLAInstruction(InstructionResult instItems)
        {
            instItems.pcCounter = (instItems.pcCounter) & 0xFFF;
            instItems.accumulatorOctal = 0 & 0xFFF;

            return new InstructionResult()
            {
                accumulatorOctal = instItems.accumulatorOctal,
                LinkBit = instItems.LinkBit,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                InstructionRegister = instItems.InstructionRegister,
                BranchTaken = instItems.BranchTaken,
                pcCounter = instItems.pcCounter,
                SetMemValue = false,
                OsrSwitchBits = instItems.OsrSwitchBits
            };
        }

        private InstructionResult OSRInstruction(InstructionResult instItems)
        {
            instItems.pcCounter = (instItems.pcCounter) & 0xFFF;

            return new InstructionResult()
            {
                accumulatorOctal = instItems.accumulatorOctal,
                BranchTaken = false,
                LinkBit = instItems.LinkBit,
                MemoryAddress = instItems.MemoryAddress,
                MemoryValueOctal = instItems.MemoryValueOctal,
                InstructionRegister = instItems.InstructionRegister,
                pcCounter = instItems.pcCounter,
                SetMemValue = false,
                OsrSwitchBits = instItems.OsrSwitchBits
            };
        }

        // Microinstruction set 3: Handle by only incrementing PC and instr count.
      
    }
}