using ECE486_PDP_8_Emulator.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECE486_PDP_8_Emulator
{
    public static class Utils
    {

        /// <summary>
        /// Decodes operation addresses.  The addresses will be converted from octal to decimal, while the actual
        /// instruction will remain in octal.  This will help with array storage and retrieval.
        /// </summary>
        /// <param name="addressValueOctal">The octal address value to be decoded</param>
        /// <param name="memoryArray">The memory array in the event an indirect address is requested</param>
        /// <param name="curPage">The current memory page (decimal)</param>
        /// <returns>A populated Operation object containing the instruction, the final decoded memory address in decimal, and any additional
        /// clock cycles that need to be accounted for.</returns>
        public static Operation DecodeOperationAddress(int addressValueOctal, MemArray memoryArray, int curPage)
        {



            Operation FinalOperation = new Operation();
            FinalOperation.Instruction = InstructionFactory.GetInstruction(Constants.OpCode.JMP);
            memoryArray.GetValue(addressValueOctal, false);
            FinalOperation.FinalMemAddress = 0;
            
            throw new NotImplementedException();
        }

        public static int GetPage(int address)
        {
            throw new NotImplementedException();
        }

        public static int DecimalToOctal(int inputBase10Dec)
        {

            int[] result = new int[32];
            int OutputInt = 0;
            int IntSize = 32;

            //Loops until 0, dividing the input by 8 and adding it to the int array
            for (; inputBase10Dec > 0; inputBase10Dec /= 8)
            {
                int Remainder = inputBase10Dec % 8;

                result[--IntSize] = Remainder;
            }

            //Loop through and construct the octal number
            for (int i = 0; i < result.Length; i++)
            {
                OutputInt += result[i];
                OutputInt = OutputInt << 1;
            }
           
            return OutputInt;
        }
    }
}
