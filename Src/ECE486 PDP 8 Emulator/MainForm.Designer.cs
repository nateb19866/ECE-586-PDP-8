namespace ECE486_PDP_8_Emulator
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnRunProgram = new System.Windows.Forms.Button();
            this.txtTraceFolder = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtProgramPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusProgBar = new System.Windows.Forms.ToolStripProgressBar();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblProgName = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblInstMicrocodes = new System.Windows.Forms.Label();
            this.lblInstIot = new System.Windows.Forms.Label();
            this.lblInstJmp = new System.Windows.Forms.Label();
            this.lblInstJms = new System.Windows.Forms.Label();
            this.lblInstDca = new System.Windows.Forms.Label();
            this.lblInstIsz = new System.Windows.Forms.Label();
            this.lblInstAnd = new System.Windows.Forms.Label();
            this.lblClockCycles = new System.Windows.Forms.Label();
            this.lblInstExecuted = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMemContents = new System.Windows.Forms.TextBox();
            this.btnProgPathPicker = new System.Windows.Forms.Button();
            this.btnTraceFolderPicker = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Program Path:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnTraceFolderPicker);
            this.groupBox1.Controls.Add(this.btnProgPathPicker);
            this.groupBox1.Controls.Add(this.btnRunProgram);
            this.groupBox1.Controls.Add(this.txtTraceFolder);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtProgramPath);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(37, 61);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(391, 128);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Run a Program";
            // 
            // btnRunProgram
            // 
            this.btnRunProgram.Enabled = false;
            this.btnRunProgram.Location = new System.Drawing.Point(234, 91);
            this.btnRunProgram.Name = "btnRunProgram";
            this.btnRunProgram.Size = new System.Drawing.Size(129, 23);
            this.btnRunProgram.TabIndex = 4;
            this.btnRunProgram.Text = "Run Program";
            this.btnRunProgram.UseVisualStyleBackColor = true;
            this.btnRunProgram.Click += new System.EventHandler(this.btnRunProgram_Click);
            // 
            // txtTraceFolder
            // 
            this.txtTraceFolder.Location = new System.Drawing.Point(86, 56);
            this.txtTraceFolder.Name = "txtTraceFolder";
            this.txtTraceFolder.Size = new System.Drawing.Size(250, 20);
            this.txtTraceFolder.TabIndex = 3;
            this.txtTraceFolder.TextChanged += new System.EventHandler(this.txtTraceFolder_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Trace Folder:";
            // 
            // txtProgramPath
            // 
            this.txtProgramPath.Location = new System.Drawing.Point(86, 22);
            this.txtProgramPath.Name = "txtProgramPath";
            this.txtProgramPath.Size = new System.Drawing.Size(250, 20);
            this.txtProgramPath.TabIndex = 1;
            this.txtProgramPath.TextChanged += new System.EventHandler(this.txtProgramPath_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(264, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(254, 37);
            this.label2.TabIndex = 2;
            this.label2.Text = "PDP 8 Simulator";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusProgBar,
            this.statusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 471);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(768, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusProgBar
            // 
            this.statusProgBar.Name = "statusProgBar";
            this.statusProgBar.Size = new System.Drawing.Size(100, 16);
            this.statusProgBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.statusProgBar.Visible = false;
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(61, 17);
            this.statusLabel.Text = "Running...";
            this.statusLabel.Visible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblProgName);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.lblClockCycles);
            this.groupBox2.Controls.Add(this.lblInstExecuted);
            this.groupBox2.Location = new System.Drawing.Point(37, 206);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(391, 238);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Statistics of Last Program";
            // 
            // lblProgName
            // 
            this.lblProgName.AutoSize = true;
            this.lblProgName.Location = new System.Drawing.Point(66, 25);
            this.lblProgName.Name = "lblProgName";
            this.lblProgName.Size = new System.Drawing.Size(80, 13);
            this.lblProgName.TabIndex = 4;
            this.lblProgName.Text = "Program Name:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblInstMicrocodes);
            this.groupBox3.Controls.Add(this.lblInstIot);
            this.groupBox3.Controls.Add(this.lblInstJmp);
            this.groupBox3.Controls.Add(this.lblInstJms);
            this.groupBox3.Controls.Add(this.lblInstDca);
            this.groupBox3.Controls.Add(this.lblInstIsz);
            this.groupBox3.Controls.Add(this.lblInstAnd);
            this.groupBox3.Location = new System.Drawing.Point(10, 99);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(373, 129);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Instructions Executed";
            this.groupBox3.Enter += new System.EventHandler(this.groupBox3_Enter);
            // 
            // lblInstMicrocodes
            // 
            this.lblInstMicrocodes.AutoSize = true;
            this.lblInstMicrocodes.Location = new System.Drawing.Point(168, 72);
            this.lblInstMicrocodes.Name = "lblInstMicrocodes";
            this.lblInstMicrocodes.Size = new System.Drawing.Size(65, 13);
            this.lblInstMicrocodes.TabIndex = 8;
            this.lblInstMicrocodes.Text = "Microcodes:";
            // 
            // lblInstIot
            // 
            this.lblInstIot.AutoSize = true;
            this.lblInstIot.Location = new System.Drawing.Point(200, 49);
            this.lblInstIot.Name = "lblInstIot";
            this.lblInstIot.Size = new System.Drawing.Size(28, 13);
            this.lblInstIot.TabIndex = 7;
            this.lblInstIot.Text = "IOT:";
            // 
            // lblInstJmp
            // 
            this.lblInstJmp.AutoSize = true;
            this.lblInstJmp.Location = new System.Drawing.Point(197, 27);
            this.lblInstJmp.Name = "lblInstJmp";
            this.lblInstJmp.Size = new System.Drawing.Size(31, 13);
            this.lblInstJmp.TabIndex = 6;
            this.lblInstJmp.Text = "JMP:";
            // 
            // lblInstJms
            // 
            this.lblInstJms.AutoSize = true;
            this.lblInstJms.Location = new System.Drawing.Point(18, 95);
            this.lblInstJms.Name = "lblInstJms";
            this.lblInstJms.Size = new System.Drawing.Size(31, 13);
            this.lblInstJms.TabIndex = 5;
            this.lblInstJms.Text = "JMS:";
            // 
            // lblInstDca
            // 
            this.lblInstDca.AutoSize = true;
            this.lblInstDca.Location = new System.Drawing.Point(19, 72);
            this.lblInstDca.Name = "lblInstDca";
            this.lblInstDca.Size = new System.Drawing.Size(32, 13);
            this.lblInstDca.TabIndex = 4;
            this.lblInstDca.Text = "DCA:";
            // 
            // lblInstIsz
            // 
            this.lblInstIsz.AutoSize = true;
            this.lblInstIsz.Location = new System.Drawing.Point(24, 49);
            this.lblInstIsz.Name = "lblInstIsz";
            this.lblInstIsz.Size = new System.Drawing.Size(27, 13);
            this.lblInstIsz.TabIndex = 3;
            this.lblInstIsz.Text = "ISZ:";
            // 
            // lblInstAnd
            // 
            this.lblInstAnd.AutoSize = true;
            this.lblInstAnd.Location = new System.Drawing.Point(18, 27);
            this.lblInstAnd.Name = "lblInstAnd";
            this.lblInstAnd.Size = new System.Drawing.Size(33, 13);
            this.lblInstAnd.TabIndex = 2;
            this.lblInstAnd.Text = "AND:";
            // 
            // lblClockCycles
            // 
            this.lblClockCycles.AutoSize = true;
            this.lblClockCycles.Location = new System.Drawing.Point(48, 74);
            this.lblClockCycles.Name = "lblClockCycles";
            this.lblClockCycles.Size = new System.Drawing.Size(98, 13);
            this.lblClockCycles.TabIndex = 1;
            this.lblClockCycles.Text = "Total Clock Cycles:";
            // 
            // lblInstExecuted
            // 
            this.lblInstExecuted.AutoSize = true;
            this.lblInstExecuted.Location = new System.Drawing.Point(7, 50);
            this.lblInstExecuted.Name = "lblInstExecuted";
            this.lblInstExecuted.Size = new System.Drawing.Size(139, 13);
            this.lblInstExecuted.TabIndex = 0;
            this.lblInstExecuted.Text = "Total Instructions Executed:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(452, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Memory Contents";
            // 
            // txtMemContents
            // 
            this.txtMemContents.Location = new System.Drawing.Point(455, 78);
            this.txtMemContents.Multiline = true;
            this.txtMemContents.Name = "txtMemContents";
            this.txtMemContents.Size = new System.Drawing.Size(294, 366);
            this.txtMemContents.TabIndex = 6;
            this.txtMemContents.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // btnProgPathPicker
            // 
            this.btnProgPathPicker.Location = new System.Drawing.Point(342, 22);
            this.btnProgPathPicker.Name = "btnProgPathPicker";
            this.btnProgPathPicker.Size = new System.Drawing.Size(29, 20);
            this.btnProgPathPicker.TabIndex = 5;
            this.btnProgPathPicker.Text = "...";
            this.btnProgPathPicker.UseVisualStyleBackColor = true;
            this.btnProgPathPicker.Click += new System.EventHandler(this.btnProgPathPicker_Click);
            // 
            // btnTraceFolderPicker
            // 
            this.btnTraceFolderPicker.Location = new System.Drawing.Point(342, 56);
            this.btnTraceFolderPicker.Name = "btnTraceFolderPicker";
            this.btnTraceFolderPicker.Size = new System.Drawing.Size(29, 20);
            this.btnTraceFolderPicker.TabIndex = 6;
            this.btnTraceFolderPicker.Text = "...";
            this.btnTraceFolderPicker.UseVisualStyleBackColor = true;
            this.btnTraceFolderPicker.Click += new System.EventHandler(this.btnTraceFolderPicker_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(768, 493);
            this.Controls.Add(this.txtMemContents);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.MinimumSize = new System.Drawing.Size(784, 525);
            this.Name = "MainForm";
            this.Text = "PDP 8 Simulator";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnRunProgram;
        private System.Windows.Forms.TextBox txtTraceFolder;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtProgramPath;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar statusProgBar;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblInstAnd;
        private System.Windows.Forms.Label lblClockCycles;
        private System.Windows.Forms.Label lblInstExecuted;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtMemContents;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblInstMicrocodes;
        private System.Windows.Forms.Label lblInstIot;
        private System.Windows.Forms.Label lblInstJmp;
        private System.Windows.Forms.Label lblInstJms;
        private System.Windows.Forms.Label lblInstDca;
        private System.Windows.Forms.Label lblInstIsz;
        private System.Windows.Forms.Label lblProgName;
        private System.Windows.Forms.Button btnTraceFolderPicker;
        private System.Windows.Forms.Button btnProgPathPicker;
    }
}