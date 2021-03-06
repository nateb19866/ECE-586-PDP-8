﻿using ECE486_PDP_8_Emulator.Classes;
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
       public static Statistics ExecuteProgram(string filePath, string traceFolder, int osrSwitchBits)
       {

           int StartAddressOctal = Convert.ToInt32(200.ToString(), 8);

           Statistics Pdp8Stats = new Statistics();
           Logger TraceLogger = new Logger(traceFolder);


           LoaderResult LoadRslt;

           ILoader FileLoader = new ObjLoader();

           LoadRslt = FileLoader.LoadFile(filePath);

           //Subscribe the logger class to the memory trace event
           LoadRslt.FinishedArray.TraceAppend += new TraceNotificationHandler(TraceLogger.AppendToMemTraceFile);



           Pdp8Stats = ExecuteInstructions(LoadRslt, Pdp8Stats, StartAddressOctal, TraceLogger, osrSwitchBits);

           return Pdp8Stats;
       }

       /// <summary>
       /// This executes the instructions in the memory array
       /// </summary>
       /// <param name="loadRslt">The loaded array plus the starting address to use.</param>
       /// <param name="pdp8Stats">The statistics object to populate.</param>
       /// <returns>An updated statistic object</returns>
       static Statistics ExecuteInstructions(LoaderResult loadRslt, Statistics pdp8Stats, int firstInstructionAddress, Logger traceLogger, int osrSwitchBits )
       {
          

           MemArray Pdp8MemArray = loadRslt.FinishedArray;

           //Initialize all counters
           int ProgramCounter = firstInstructionAddress;
           int InstructionRegisterOctal = Pdp8MemArray.GetValue(firstInstructionAddress, true, false);
           int AccumulatorOctal = 0;
           int CurPage = Utils.GetPage(firstInstructionAddress);
           bool LinkBit = false;


           //Loop until the program is halted
           while (Utils.DecimalToOctal(InstructionRegisterOctal) != (int)Constants.Microcode.HLT)
           {
               Operation CurOp = Utils.DecodeOperationAddress(InstructionRegisterOctal, Pdp8MemArray, CurPage);

               int MemValueOctal = 0;

               //Only retrieve value of that memory address for the instructions that need it
               if(CurOp.Instruction.instructionType == Constants.OpCode.AND ||
                   CurOp.Instruction.instructionType == Constants.OpCode.TAD ||
                   CurOp.Instruction.instructionType == Constants.OpCode.ISZ
                   )
                   MemValueOctal =  Pdp8MemArray.GetValue(CurOp.FinalMemAddress, false, false);


               InstructionItems InstructionParams = new InstructionItems()
               {
                   accumulatorOctal = AccumulatorOctal,
                   MemoryAddress = CurOp.FinalMemAddress,
                   MemoryValueOctal = MemValueOctal,
                   pcCounter = ProgramCounter,
                   InstructionRegister = InstructionRegisterOctal,
                   LinkBit = LinkBit,
                   OsrSwitchBits = osrSwitchBits

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
                       if (Result.BranchType == null)
                           break;


                       traceLogger.AppendToBranchTraceFile(null,
                           EventArgs.Empty,
                           new BranchTraceRow()
                           {
                               branchTaken = Result.BranchTaken,
                               BranchType = (Constants.BranchType)Result.BranchType, 
                               MemoryAddress = Result.pcCounter,
                               ProgramCounter = ProgramCounter
                           });
                       break;

                   default:
                       break;
               }

               //Store results of instruction
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
               pdp8Stats.InstructionTypeExecutions[CurOp.Instruction.instructionType.ToString()] =
                 (Convert.ToInt32(pdp8Stats.InstructionTypeExecutions[CurOp.Instruction.instructionType.ToString()]) + 1).ToString();

               //Log group 3 microcodes
               if((InstructionParams.InstructionRegister & 0xF01) == 0xF01)
                   pdp8Stats.InstructionTypeExecutions["Grp3Microcode"] =
                 (Convert.ToInt32(pdp8Stats.InstructionTypeExecutions["Grp3Microcode"]) + 1).ToString();

               if (Result.IsHalted)
                   break;

           }

           //If the HLT instruction by itself was called, need to update these stats, otherwise it was part of a microcode sequence.
           if (Utils.DecimalToOctal(InstructionRegisterOctal) == (int)Constants.Microcode.HLT)
           {
               //Update stats one last time
               pdp8Stats.ClockCyclesExecuted++;
               pdp8Stats.InstructionsExecuted++;
               pdp8Stats.InstructionTypeExecutions["OPR"] = (Convert.ToInt32(pdp8Stats.InstructionTypeExecutions["OPR"]) + 1).ToString();
           }

           pdp8Stats.MemTraceFilePath = traceLogger.FinalMemTrFileName;
           pdp8Stats.BranchTraceFilePath = traceLogger.FinalBranchTrFileName;

           //Transfer memory contents
           pdp8Stats.MemContents = Pdp8MemArray.DumpValidMemContents();

           //Dump any cached logs to file
           traceLogger.DumpAllCachesToFile();
           return pdp8Stats;
       }
    }
}
