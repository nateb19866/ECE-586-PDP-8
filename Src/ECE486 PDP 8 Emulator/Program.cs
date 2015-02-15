using ECE486_PDP_8_Emulator.Classes;
using ECE486_PDP_8_Emulator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECE486_PDP_8_Emulator
{
    class Program
    {
        static void Main(string[] args)
        {

            string FilePath = "";
            string TraceFolder = "";
            int StartAddressOctal = 200;

            Statistics Pdp8Stats = new Statistics();
            Logger TraceLogger = new Logger(TraceFolder);


            LoaderResult LoadRslt;

            ILoader FileLoader = new ObjLoader();

            LoadRslt = FileLoader.LoadFile(FilePath);

            //Subscribe the logger class to the memory trace event
            LoadRslt.FinishedArray.TraceAppend += new TraceNotificationHandler(TraceLogger.AppendToMemTraceFile);



            Pdp8Stats = ExecuteInstructions(LoadRslt, Pdp8Stats, Convert.ToInt32(StartAddressOctal.ToString(),8));

            PrintStatistics(Pdp8Stats);

            Console.ReadLine();


        }

        /// <summary>
        /// This executes the instructions in the memory array
        /// </summary>
        /// <param name="loadRslt">The loaded array plus the starting address to use.</param>
        /// <param name="pdp8Stats">The statistics object to populate.</param>
        /// <returns>An updated statistic object</returns>
        static Statistics ExecuteInstructions(LoaderResult loadRslt, Statistics pdp8Stats, int firstInstructionAddress)
        {

            
            MemArray Pdp8MemArray = loadRslt.FinishedArray;

            //Initialize all counters
            int ProgramCounter =  firstInstructionAddress;
            int InstructionRegisterOctal =Pdp8MemArray.GetValue(firstInstructionAddress,true,false);
            int AccumulatorOctal = 0;
            int CurPage = Utils.GetPage(firstInstructionAddress);
            bool LinkBit = false;


            //Loop until the program is halted
            while(InstructionRegisterOctal != Constants.HLT)
            {
                Operation CurOp = Utils.DecodeOperationAddress(InstructionRegisterOctal, Pdp8MemArray, CurPage);

                InstructionItems InstructionParams = new InstructionItems()
                {
                    accumulatorOctal = AccumulatorOctal,
                    MemoryAddress = CurOp.FinalMemAddress,
                    MemoryValueOctal = Pdp8MemArray.GetValue(CurOp.FinalMemAddress, false, CurOp.IsIndirect),
                    pcCounter = ProgramCounter,
                    MicroCodes = InstructionRegisterOctal,
                    LinkBit = LinkBit

                };


                InstructionResult Result = CurOp.Instruction.ExecuteInstruction(InstructionParams);

                AccumulatorOctal = Result.accumulatorOctal;
                ProgramCounter = Result.pcCounter;
                LinkBit = Result.LinkBit;
                CurPage = Utils.GetPage(ProgramCounter);

                //If a memory value needs to be stored, store it in the memory array
                if (Result.SetMemValue)
                    Pdp8MemArray.SetValue(Result.MemoryAddress, Result.MemoryValueOctal);

                //Get the next instruction value
                InstructionRegisterOctal = Pdp8MemArray.GetValue(ProgramCounter, true, Result.NextInstructionIsIndirect);

                //UpdateStats
                pdp8Stats.ClockCyclesExecuted += CurOp.ExtraClockCyles + CurOp.Instruction.clockCycles;
                pdp8Stats.InstructionsExecuted++;

                //Update the stats on the type of instruction being executed
                pdp8Stats.InstructionTypeExecutions.Single(i => i.Operation == CurOp.Instruction.instructionType).Executions++;
  
            }

            


            return pdp8Stats;
        }

        public static void PrintStatistics(Statistics pdp8Stats)
        {
            throw new NotImplementedException();
        }
    }
}
