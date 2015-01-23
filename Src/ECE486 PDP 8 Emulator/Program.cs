using ECE486_PDP_8_Emulator.Classes;
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
            string MemTraceFilePath = "";

            Statistics Pdp8Stats = new Statistics();


            LoaderResult LoadRslt;

            LoadRslt = ObjLoader.LoadFile(FilePath,MemTraceFilePath);

            Pdp8Stats = ExecuteInstructions(LoadRslt, Pdp8Stats);

            PrintStatistics(Pdp8Stats);

            Console.ReadLine();


        }

        static Statistics ExecuteInstructions(LoaderResult loadRslt, Statistics pdp8Stats)
        {

            
            MemArray Pdp8MemArray = loadRslt.FinishedArray;

            //Initialize all counters
            int ProgramCounter =  loadRslt.FirstInstructionAddress;
            int CurInstruction =Pdp8MemArray.GetValue(loadRslt.FirstInstructionAddress);
            int Accumulator = 0;
            int CurPage = Utils.GetPage(loadRslt.FirstInstructionAddress);


            //Loop until the program is halted
            while(CurInstruction != Constants.HLT)
            {
                Operation CurOp = Utils.DecodeOperationAddress(CurInstruction, Pdp8MemArray, CurPage);

                InstructionItems InstructionParams = new InstructionItems(){
                 accumulator = Accumulator,
                  MemoryAddress = CurOp.FinalMemAddress,
                   MemoryValue = Pdp8MemArray.GetValue(CurOp.FinalMemAddress),
                    pcCounter = ProgramCounter,
                     MicroCodes = CurInstruction
                  
                };


                InstructionResult Result = CurOp.Instruction.ExecuteInstruction(InstructionParams);

                Accumulator = Result.accumulator;
                ProgramCounter = Result.pcCounter;
                CurInstruction = Pdp8MemArray.GetValue(ProgramCounter);

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
