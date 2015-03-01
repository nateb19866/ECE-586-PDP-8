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
        private string LastMemTrFilePath = "";
        private string LastBranchTrFilePath = "";

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
            lblProgName.Text = "Program Name: " + Path.GetFileName(txtProgramPath.Text);
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

                LastBranchTrFilePath = Result.BranchTraceFilePath;
                LastMemTrFilePath = Result.MemTraceFilePath;
                btnOpenBranchTrFile.Enabled = true;
                btnOpenMemTrFile.Enabled = true;

                //Update the statistics boxes
                lblClockCycles.Text = "Total Clock Cycles: " + Result.ClockCyclesExecuted.ToString();
                lblInstExecuted.Text = "Total Instructions Executed: " + Result.InstructionsExecuted.ToString();
                lblInstAnd.Text = "AND: " + Result.InstructionTypeExecutions[Constants.OpCode.AND.ToString()];
                lblInstIsz.Text = "ISZ: " + Result.InstructionTypeExecutions[Constants.OpCode.ISZ.ToString()];
                lblInstDca.Text = "DCA: " + Result.InstructionTypeExecutions[Constants.OpCode.DCA.ToString()];
                lblInstJms.Text = "JMS: " + Result.InstructionTypeExecutions[Constants.OpCode.JMS.ToString()];
                lblInstJmp.Text = "JMP: " + Result.InstructionTypeExecutions[Constants.OpCode.JMP.ToString()];
                lblInstTad.Text = "TAD: " + Result.InstructionTypeExecutions[Constants.OpCode.TAD.ToString()];
                lblInstIot.Text = "IOT: " + Result.InstructionTypeExecutions[Constants.OpCode.IOT.ToString()];
                lblInstMicrocodes.Text = "Microcodes: " + Result.InstructionTypeExecutions[Constants.OpCode.OPR.ToString()];

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

        private void btnProgPathPicker_Click(object sender, EventArgs e)
        {
            OpenFileDialog ProgPathDlg = new OpenFileDialog();

            ProgPathDlg.Title = "Pick an Object File...";
            ProgPathDlg.CheckFileExists = true;
            ProgPathDlg.CheckPathExists = true;
            ProgPathDlg.Filter = "Object Files (.obj)|*.obj";

            if (ProgPathDlg.ShowDialog() == DialogResult.OK)
            {
                txtProgramPath.Text = ProgPathDlg.FileName;
                txtTraceFolder.Text = Path.GetDirectoryName(ProgPathDlg.FileName);
            }
        }

        private void btnTraceFolderPicker_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FolderPickerDlg = new FolderBrowserDialog();

            FolderPickerDlg.ShowNewFolderButton = true;

            if (FolderPickerDlg.ShowDialog() == DialogResult.OK)
                txtTraceFolder.Text = FolderPickerDlg.SelectedPath;
        }

        private void btnOpenMemTrFile_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(LastMemTrFilePath);
        }

        private void btnOpenBranchTrFile_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(LastBranchTrFilePath);
        }
    }
}
