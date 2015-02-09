using ECE486_PDP_8_Emulator.Classes;
using ECE486_PDP_8_Emulator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace ECE486_PDP_8_Emulator
{
    public class  ObjLoader:ILoader
    {
        public LoaderResult LoadFile(string filePath)
        {

          
                // Read in Text File 
                //filePath = "C:/Users/Donut/PAL/File.obj"; // Note this path will be changed and moved to ILoader.cs
                string line = null;
                var ArrayCount = File.ReadLines(filePath).Count(); // Counts lines in file
                int[,] MemArray = new int[4096,2];  // Complete Memory Array, Fix to determine complete array length

                int TempArrayCnt = 0;  // Index for TempArray, reading in file
                int MemArrayCnt = 0;  // Index for MemArray. Looking for 1xx
                int StartMemArrayCnt = 0;  // Index for Start of MemArray = 200

                using (StreamReader TxtFile = new StreamReader(filePath))
                {
                    // Read all lines into MemArray until EOF is reached
                    while ((line = TxtFile.ReadLine()) != null)
                    {
                        string[] TempArray = File.ReadAllLines(filePath);      // Read all lines into TempArray

                        for (TempArrayCnt = 0; TempArrayCnt < ArrayCount; TempArrayCnt++)    // Loop through complete array
                        {
                            if (TempArray[TempArrayCnt].StartsWith("1") && (StartMemArrayCnt == 0) && (TempArrayCnt < ArrayCount) && (MemArrayCnt < ArrayCount))   // Find first 1XX, counter to start at MemArray 200
                            {
                                StartMemArrayCnt++;
                                MemArrayCnt = MemArrayCnt + 200;      // Start of MemArray[200]
                                TempArrayCnt = TempArrayCnt + 2;  // Go to 2 lines down for addr to store

                                while (TempArray[TempArrayCnt].StartsWith("0") && (TempArrayCnt < ArrayCount)) // continue to add to MemArray if start with 0
                                {
                                    MemArray[MemArrayCnt,0] = Convert.ToInt32(TempArray[TempArrayCnt].Remove(0, 1) + TempArray[TempArrayCnt + 1].Remove(0, 1));
                                    MemArray[MemArrayCnt, 1] = 1;
                                    TempArrayCnt = TempArrayCnt + 2;  // move 2 lines down
                                    MemArrayCnt++;
                                }
                            }

                            if (TempArray[TempArrayCnt].StartsWith("1") && (TempArrayCnt < ArrayCount) && (MemArrayCnt < ArrayCount))   // Any found 1XX beyond the first 1XX, counter to start at MemArray at +50
                            {
                                MemArrayCnt = MemArrayCnt + 50;      // add 50 to current MemArray position
                                TempArrayCnt = TempArrayCnt + 2;  // Go to 2 lines down for addr to store

                                while (TempArray[TempArrayCnt].StartsWith("0") && (TempArrayCnt < ArrayCount) && (MemArrayCnt < ArrayCount)) // continue to add to MemArray if start with 0
                                {
                                    MemArray[MemArrayCnt, 0] = Convert.ToInt32(TempArray[TempArrayCnt].Remove(0, 1) + TempArray[TempArrayCnt + 1].Remove(0, 1));
                                    MemArray[MemArrayCnt, 1] = 1;
                                    TempArrayCnt = TempArrayCnt + 2;  // move 2 lines down
                                    MemArrayCnt++;
                                }
                            }

                        }

                    }


                    return new LoaderResult() { FinishedArray = new MemArray(MemArray)};

                }
            
        }
    }
}
