﻿using ECE486_PDP_8_Emulator.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECE486_PDP_8_Emulator.Instructions
{
    public class SpaInstruction : IInstruction
    {

        private int ClockCycles = 1;
        private Constants.Microcode InstructionType = Constants.Microcode.SPA;

        public InstructionResult ExecuteInstruction(InstructionItems instItems)
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


        public int clockCycles
        {
            get
            {
                return ClockCycles;
            }

        }


        public Constants.Microcode instructionType
        {
            get { return InstructionType; }
        }
    }
}