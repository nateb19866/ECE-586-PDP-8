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
        /// Decodes operation addresses.  The addresses will be converted from octal to decimal.
        /// </summary>
        /// <param name="addressValueOctal">The octal address value to be decoded</param>
        /// <param name="memoryArray">The memory array in the event an indirect address is requested</param>
        /// <param name="curPageOctal">The current memory page</param>
        /// <returns>A populated Operation object containing the instruction, the final decoded memory address in decimal, and any additional
        /// clock cycles that need to be accounted for.</returns>
        public static Operation DecodeOperationAddress(int addressValueOctal, MemArray memoryArray, int curPageOctal)
        {

            
            int curPageInt = Convert.ToInt32(curPageOctal.ToString(), 8);
            int OpCode = addressValueOctal >> 9;
            int Offset = addressValueOctal & 0x7F;

            bool IsIndirect = ((addressValueOctal & 0x100) >> 8) == 1 && (Constants.OpCode)OpCode != Constants.OpCode.IOT && (Constants.OpCode)OpCode != Constants.OpCode.OPR;

            bool IsCurPage = ((addressValueOctal & 0x080) >> 7) == 1;


            Operation FinalOperation = new Operation();
            FinalOperation.IsIndirect = IsIndirect;
            FinalOperation.Instruction = InstructionFactory.GetInstruction((Constants.OpCode)OpCode);
            FinalOperation.ExtraClockCyles = 0;

            int EffectiveMemoryAddress = Offset;

            if (IsCurPage)
                EffectiveMemoryAddress = Offset | (curPageInt << 7);

           
           
            if (IsIndirect)
            {
                FinalOperation.FinalMemAddress = memoryArray.GetValue(EffectiveMemoryAddress, false, true);
                FinalOperation.ExtraClockCyles = 1;

                if(IsAutoIncrementRegister(EffectiveMemoryAddress ))
                    FinalOperation.ExtraClockCyles += 2;

            }
            else
                FinalOperation.FinalMemAddress = EffectiveMemoryAddress;

            return FinalOperation;
        }

        public static int GetPage(int address)
        {
            return address >> 7;
        }

        public static int DecimalToOctal(int inputBase10Dec)
        {

            int[] result = new int[4];
            string OutputStr = "";
            int IntSize = 4;

            //Loops until 0, dividing the input by 8 and adding it to the int array
            for (; inputBase10Dec > 0; inputBase10Dec /= 8)
            {
                int Remainder = inputBase10Dec % 8;

                result[--IntSize] = Remainder;
            }

            //Loop through and construct the octal number
            for (int i = 0; i < result.Length; i++)
            {
                OutputStr += result[i];
                
            }
           
            return Convert.ToInt32( OutputStr);
        }

        public static bool IsAutoIncrementRegister(int memoryAddressDec)
        {
            return memoryAddressDec >= 8 && memoryAddressDec <= 15;
        }
    }
}
