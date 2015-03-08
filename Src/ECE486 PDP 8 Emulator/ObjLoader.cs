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
              
               

                int[,] MemArray = new int[4096,2];  // Complete Memory Array, Fix to determine complete array length


                if (File.ReadAllLines(filePath).LongCount() == 0)
                    throw new ArgumentNullException("Blank file detected!");

                using (StreamReader TxtFile = new StreamReader(filePath))
                {
                      int MemArrayCnt = 0;  // Index for MemArray. Looking for 1xx+
                      bool StartOfFile = true;
                     string line = null;
                    string line2 = null;
                    bool IncrementCtr = true;
                    // Read all lines into MemArray until EOF is reached
                    while ((line = TxtFile.ReadLine()) != null)
                    {
                      
                        //Catch exception with odd rows in file
                        if((line2 = TxtFile.ReadLine()) == null)
                            throw new InvalidDataException("The file contains an invalid number of rows!");

                        //Convert values to ints
                        int StartInt = Convert.ToInt32(line.Trim().First().ToString());
                        int value = Convert.ToInt32(line.Trim().Substring(1,2) + line2.Substring(1,2),8);


                        //If the beginning of the program does not contain a memory reference, assume starting at page 1
                        if (StartInt == 0 && StartOfFile)
                        {
                            //200 octal(000 010 000 000) in hex(0000 1000 0000) 
                            MemArrayCnt = 0x80;
                            IncrementCtr = false;
                        }

                        //If it contains a memory reference, set the value to whatever the memory reference is, and make sure not to increment the address for the next loop
                        if(StartInt == 1)
                        {
                            MemArrayCnt = value;
                            IncrementCtr = false;

                        }
                        else
                        {
                            //If the prior address was not a memory location, increment the counter and store the value.  Otherwise, just store the value.
                            if(IncrementCtr)
                                MemArrayCnt++;
                            else
                                IncrementCtr = true;

                            //Catch out of range exceptions
                            if (MemArrayCnt > 0xFFF)
                                throw new IndexOutOfRangeException("The file contains a reference to an invalid memory location!");


                            MemArray[MemArrayCnt,0] = value;
                            MemArray[MemArrayCnt, 1] = 1;
                        }


                        StartOfFile = false;

                        

                    }


                    return new LoaderResult() { FinishedArray = new MemArray(MemArray)};

                }
            
        }
    }
}
