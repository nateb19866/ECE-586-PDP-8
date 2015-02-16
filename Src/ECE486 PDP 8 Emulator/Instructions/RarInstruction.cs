using ECE486_PDP_8_Emulator.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECE486_PDP_8_Emulator.Instructions
{
    public class RarInstruction : IInstruction
    {

        private int ClockCycles = 1;
        private Constants.Microcode InstructionType = Constants.Microcode.RAR;
        int tempLink = 0;
        bool LinkReturn;
        public InstructionResult ExecuteInstruction(InstructionItems instItems)
        {
            //Rotate 13 bit Link/AC right by 1
           

            //Converting to byte array to make things easier.
            Int16 TestWord1Bytes = Convert.ToInt16(instItems.accumulatorOctal.ToString(), 12);

            // Concatenante Linkbit and AC
            if (instItems.LinkBit == true)
                tempLink = 1;
            else // =0
                tempLink = 0;

            // Rotate 13 Bit Link/AC right by 1
            int FinalWord = (tempLink + TestWord1Bytes)  >> 1; 

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
