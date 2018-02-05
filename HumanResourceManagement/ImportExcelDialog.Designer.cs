namespace HumanResourceManagement
{
    partial class ImportExcelDialog
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportExcelDialog));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.endRow = new System.Windows.Forms.NumericUpDown();
            this.startRow = new System.Windows.Forms.NumericUpDown();
            this.lblClose = new System.Windows.Forms.Label();
            this.lblExcelName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbSheets = new MetroFramework.Controls.MetroComboBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.bgLoader = new System.ComponentModel.BackgroundWorker();
            this.bgTransfer = new System.ComponentModel.BackgroundWorker();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.endRow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.startRow)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.endRow);
            this.panel1.Controls.Add(this.startRow);
            this.panel1.Controls.Add(this.lblClose);
            this.panel1.Controls.Add(this.lblExcelName);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cmbSheets);
            this.panel1.Controls.Add(this.btnStart);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(293, 213);
            this.panel1.TabIndex = 86;
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(41, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 94;
            this.label3.Text = "Start:";
            // 
            // endRow
            // 
            this.endRow.Location = new System.Drawing.Point(176, 113);
            this.endRow.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.endRow.Name = "endRow";
            this.endRow.Size = new System.Drawing.Size(59, 20);
            this.endRow.TabIndex = 93;
            this.toolTip1.SetToolTip(this.endRow, "Row\'s end");
            this.endRow.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.endRow.ValueChanged += new System.EventHandler(this.endRow_ValueChanged);
            // 
            // startRow
            // 
            this.startRow.Location = new System.Drawing.Point(75, 113);
            this.startRow.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.startRow.Name = "startRow";
            this.startRow.Size = new System.Drawing.Size(59, 20);
            this.startRow.TabIndex = 92;
            this.toolTip1.SetToolTip(this.startRow, "Row\'s start");
            this.startRow.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.startRow.ValueChanged += new System.EventHandler(this.startRow_ValueChanged);
            // 
            // lblClose
            // 
            this.lblClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblClose.AutoSize = true;
            this.lblClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClose.Location = new System.Drawing.Point(269, 4);
            this.lblClose.Name = "lblClose";
            this.lblClose.Size = new System.Drawing.Size(19, 18);
            this.lblClose.TabIndex = 91;
            this.lblClose.Text = "X";
            this.lblClose.Click += new System.EventHandler(this.lblClose_Click);
            // 
            // lblExcelName
            // 
            this.lblExcelName.AutoEllipsis = true;
            this.lblExcelName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExcelName.Location = new System.Drawing.Point(41, 41);
            this.lblExcelName.Name = "lblExcelName";
            this.lblExcelName.Size = new System.Drawing.Size(225, 18);
            this.lblExcelName.TabIndex = 90;
            this.toolTip1.SetToolTip(this.lblExcelName, "Selected File");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 89;
            this.label2.Text = "Excel Filename:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 88;
            this.label1.Text = "Sheet:";
            // 
            // cmbSheets
            // 
            this.cmbSheets.FontSize = MetroFramework.MetroComboBoxSize.Small;
            this.cmbSheets.FormattingEnabled = true;
            this.cmbSheets.IntegralHeight = false;
            this.cmbSheets.ItemHeight = 19;
            this.cmbSheets.Location = new System.Drawing.Point(41, 82);
            this.cmbSheets.Name = "cmbSheets";
            this.cmbSheets.Size = new System.Drawing.Size(194, 25);
            this.cmbSheets.TabIndex = 87;
            this.toolTip1.SetToolTip(this.cmbSheets, "Workbook\'s Sheet");
            this.cmbSheets.UseSelectable = true;
            this.cmbSheets.SelectedIndexChanged += new System.EventHandler(this.cmbSheets_SelectedIndexChanged);
            // 
            // btnStart
            // 
            this.btnStart.Enabled = false;
            this.btnStart.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(57)))), ((int)(((byte)(58)))));
            this.btnStart.Image = global::HumanResourceManagement.Properties.Resources.done;
            this.btnStart.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnStart.Location = new System.Drawing.Point(82, 146);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(125, 30);
            this.btnStart.TabIndex = 86;
            this.btnStart.Text = "Start Transfer";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(144, 116);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 95;
            this.label4.Text = "End:";
            // 
            // bgLoader
            // 
            this.bgLoader.WorkerSupportsCancellation = true;
            this.bgLoader.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgLoader_DoWork);
            this.bgLoader.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgLoader_RunWorkerCompleted);
            // 
            // bgTransfer
            // 
            this.bgTransfer.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgTransfer_DoWork);
            this.bgTransfer.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgTransfer_RunWorkerCompleted);
            // 
            // ImportExcelDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(297, 217);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ImportExcelDialog";
            this.Padding = new System.Windows.Forms.Padding(2);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ImportExcelDialog";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ImportExcelDialog_FormClosed);
            this.Load += new System.EventHandler(this.ImportExcelDialog_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.endRow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.startRow)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblClose;
        private System.Windows.Forms.Label lblExcelName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        public MetroFramework.Controls.MetroComboBox cmbSheets;
        private System.Windows.Forms.Button btnStart;
        private System.ComponentModel.BackgroundWorker bgLoader;
        private System.ComponentModel.BackgroundWorker bgTransfer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown endRow;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.NumericUpDown startRow;
        private System.Windows.Forms.Label label4;
    }
}