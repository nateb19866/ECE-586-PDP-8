using ECE486_PDP_8_Emulator.Classes;
using ECE486_PDP_8_Emulator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECE486_PDP_8_Emulator
{
   public static class ProgramExecuter
    {
       public static Statistics ExecuteProgram(string filePath, string traceFolder)
       {
           
           int StartAddressOctal = 200;

           Statistics Pdp8Stats = new Statistics();
           Logger TraceLogger = new Logger(traceFolder);


           LoaderResult LoadRslt;

           ILoader FileLoader = new ObjLoader();

           LoadRslt = FileLoader.LoadFile(filePath);

           //Subscribe the logger class to the memory trace event
           LoadRslt.FinishedArray.TraceAppend += new TraceNotificationHandler(TraceLogger.AppendToMemTraceFile);



           Pdp8Stats = ExecuteInstructions(LoadRslt, Pdp8Stats, Convert.ToInt32(StartAddressOctal.ToString(), 8), TraceLogger);

           return Pdp8Stats;
       }

       /// <summary>
       /// This executes the instructions in the memory array
       /// </summary>
       /// <param name="loadRslt">The loaded array plus the starting address to use.</param>
       /// <param name="pdp8Stats">The statistics object to populate.</param>
       /// <returns>An updated statistic object</returns>
       static Statistics ExecuteInstructions(LoaderResult loadRslt, Statistics pdp8Stats, int firstInstructionAddress, Logger traceLogger)
       {


           MemArray Pdp8MemArray = loadRslt.FinishedArray;

           //Initialize all counters
           int ProgramCounter = firstInstructionAddress;
           int InstructionRegisterOctal = Pdp8MemArray.GetValue(firstInstructionAddress, true, false);
           int AccumulatorOctal = 0;
           int CurPage = Utils.GetPage(firstInstructionAddress);
           bool LinkBit = false;


           //Loop until the program is halted
           while (InstructionRegisterOctal != Constants.HLT)
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

               //Trace branch 

               switch (CurOp.Instruction.instructionType)
               {

                   case Constants.OpCode.ISZ:
                   case Constants.OpCode.JMS:
                   case Constants.OpCode.JMP:
                   case Constants.OpCode.OPR:
                       //Only log conditional microcodes
                       if (InstructionRegisterOctal != (int)Constants.Microcode.SMA
                           && InstructionRegisterOctal != (int)Constants.Microcode.SZA
                           && InstructionRegisterOctal != (int)Constants.Microcode.SNL
                           && InstructionRegisterOctal != (int)Constants.Microcode.SPA
                           && InstructionRegisterOctal != (int)Constants.Microcode.SNA
                           && InstructionRegisterOctal != (int)Constants.Microcode.SZL
                           && InstructionRegisterOctal != (int)Constants.Microcode.SKP

                           )
                           break;


                       traceLogger.AppendToBranchTraceFile(null,
                           EventArgs.Empty,
                           new BranchTraceRow()
                           {
                               branchTaken = Result.BranchTaken,
                               BranchType = CurOp.Instruction.instructionType,
                               MemoryAddress = Result.pcCounter,
                               ProgramCounter = ProgramCounter
                           });
                       break;

                   default:
                       break;
               }

               AccumulatorOctal = Result.accumulatorOctal;
               ProgramCounter = Result.pcCounter;
               LinkBit = Result.LinkBit;
               CurPage = Utils.GetPage(ProgramCounter);

               //If a memory value needs to be stored, store it in the memory array
               if (Result.SetMemValue)
                   Pdp8MemArray.SetValue(Result.MemoryAddress, Result.MemoryValueOctal);

               //Get the next instruction value
               InstructionRegisterOctal = Pdp8MemArray.GetValue(ProgramCounter, true, CurOp.IsIndirect);

               //UpdateStats
               pdp8Stats.ClockCyclesExecuted += CurOp.ExtraClockCyles + CurOp.Instruction.clockCycles;
               pdp8Stats.InstructionsExecuted++;

               //Update the stats on the type of instruction being executed
               pdp8Stats.InstructionTypeExecutions.Single(i => i.Operation == CurOp.Instruction.instructionType).Executions++;

           }



           //Transfer memory contents
           pdp8Stats.MemContents = Pdp8MemArray.DumpValidMemContents();
           return pdp8Stats;
       }
    }
}
