using ECE486_PDP_8_Emulator.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ECE486_PDP_8_Emulator
{
    public partial class MainForm : Form
    {

        private bool IsRunning = false;
        
        public MainForm()
        {
            InitializeComponent();
        }


        private void txtProgramPath_TextChanged(object sender, EventArgs e)
        {
            ValidateEntries();
        }

        private void ValidateEntries()
        {
            btnRunProgram.Enabled = File.Exists(txtProgramPath.Text) && Directory.Exists(txtTraceFolder.Text) && !IsRunning;

        }

        private void txtTraceFolder_TextChanged(object sender, EventArgs e)
        {
            ValidateEntries();
        }

        private void btnRunProgram_Click(object sender, EventArgs e)
        {

            IsRunning = true;

            //Disable run button
            btnRunProgram.Enabled = false;


            //Set status bar
            statusLabel.Visible = true;
            statusProgBar.Visible = true;

            //Clear statistics boxes
            lblClockCycles.Text = "Total Clock Cycles: " ;
            lblInstExecuted.Text = "Total Instructions Executed: " ;
            lblInstAnd.Text = "AND: " ;
            lblInstAnd.Text = "ISZ: ";
            lblInstAnd.Text = "DCA: ";
            lblInstAnd.Text = "JMS: ";
            lblInstAnd.Text = "JMP: " ;
            lblInstAnd.Text = "TAD: ";
            lblInstAnd.Text = "IOT: ";
            lblInstAnd.Text = "Microcodes: ";

            //Clear memory contents
            txtMemContents.Text = String.Empty;


            //Run the task asynchronously
            var t = Task.Factory.StartNew(() => ProgramExecuter.ExecuteProgram(txtProgramPath.Text, txtTraceFolder.Text))
            .ContinueWith(task =>
            {
                IsRunning = false;
                
                //Update labels
                statusLabel.Visible = false;
                statusProgBar.Visible = false;

                Statistics Result = task.Result;

                //Update the statistics boxes
                lblClockCycles.Text = "Total Clock Cycles: " + Result.ClockCyclesExecuted.ToString();
                lblInstExecuted.Text = "Total Instructions Executed: " + Result.InstructionsExecuted.ToString();
                lblInstAnd.Text = "AND: " + Result.InstructionTypeExecutions.First(i => i.Operation == Constants.OpCode.AND).Executions.ToString();
                lblInstAnd.Text = "ISZ: " + Result.InstructionTypeExecutions.First(i => i.Operation == Constants.OpCode.ISZ).Executions.ToString();
                lblInstAnd.Text = "DCA: " + Result.InstructionTypeExecutions.First(i => i.Operation == Constants.OpCode.DCA).Executions.ToString();
                lblInstAnd.Text = "JMS: " + Result.InstructionTypeExecutions.First(i => i.Operation == Constants.OpCode.JMS).Executions.ToString();
                lblInstAnd.Text = "JMP: " + Result.InstructionTypeExecutions.First(i => i.Operation == Constants.OpCode.JMP).Executions.ToString();
                lblInstAnd.Text = "TAD: " + Result.InstructionTypeExecutions.First(i => i.Operation == Constants.OpCode.TAD).Executions.ToString();
                lblInstAnd.Text = "IOT: " + Result.InstructionTypeExecutions.First(i => i.Operation == Constants.OpCode.IOT).Executions.ToString();
                lblInstAnd.Text = "Microcodes: " + Result.InstructionTypeExecutions.First(i => i.Operation == Constants.OpCode.OPR).Executions.ToString();

                //Print memory contents
                StringBuilder FinalMemContents = new StringBuilder();
                FinalMemContents.AppendLine("Address		Value");

                foreach (MemArrayRow row in Result.MemContents)
                {
                    FinalMemContents.AppendLine(row.Address.ToString() + "		" + row.Value.ToString());
                }

                txtMemContents.Text = FinalMemContents.ToString();

                //Check to see if the program can be ran again
                ValidateEntries();

            },TaskScheduler.FromCurrentSynchronizationContext()
            
            );

           

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }
    }
}
