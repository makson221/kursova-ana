namespace Trc.WinForms.Controls
{
    partial class ReportsControl
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Component Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.split = new System.Windows.Forms.SplitContainer();
            this.grid = new System.Windows.Forms.DataGridView();
            this.chart = new System.Windows.Forms.Panel();
            this.lblSummary = new System.Windows.Forms.Label();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblReport = new System.Windows.Forms.Label();
            this.cmbReport = new System.Windows.Forms.ComboBox();
            this.lblYear = new System.Windows.Forms.Label();
            this.numYear = new System.Windows.Forms.NumericUpDown();
            this.btnBuild = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.split)).BeginInit();
            this.split.Panel1.SuspendLayout();
            this.split.Panel2.SuspendLayout();
            this.split.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.chart.SuspendLayout();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numYear)).BeginInit();
            this.SuspendLayout();
            // 
            // split
            // 
            this.split.Panel1.Controls.Add(this.grid);
            this.split.Panel2.Controls.Add(this.chart);
            this.split.Dock = System.Windows.Forms.DockStyle.Fill;
            this.split.Location = new System.Drawing.Point(0, 48);
            this.split.Name = "split";
            this.split.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.split.Size = new System.Drawing.Size(1000, 552);
            this.split.SplitterDistance = 260;
            this.split.TabIndex = 1;
            // 
            // grid
            // 
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Location = new System.Drawing.Point(0, 0);
            this.grid.Name = "grid";
            this.grid.Size = new System.Drawing.Size(1000, 260);
            this.grid.TabIndex = 1;
            // 
            // chart
            // 
            this.chart.BackColor = System.Drawing.Color.White;
            this.chart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chart.Location = new System.Drawing.Point(0, 0);
            this.chart.Name = "chart";
            this.chart.Size = new System.Drawing.Size(1000, 288);
            this.chart.TabIndex = 0;
            this.chart.Paint += new System.Windows.Forms.PaintEventHandler(this.chart_Paint);
            this.chart.Resize += new System.EventHandler(this.chart_Resize);
            // 
            // lblSummary
            // 
            this.lblSummary.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblSummary.Location = new System.Drawing.Point(0, 600);
            this.lblSummary.Name = "lblSummary";
            this.lblSummary.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblSummary.Size = new System.Drawing.Size(1000, 28);
            this.lblSummary.TabIndex = 2;
            this.lblSummary.Text = "—";
            this.lblSummary.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.lblReport);
            this.pnlTop.Controls.Add(this.cmbReport);
            this.pnlTop.Controls.Add(this.lblYear);
            this.pnlTop.Controls.Add(this.numYear);
            this.pnlTop.Controls.Add(this.btnBuild);
            this.pnlTop.Controls.Add(this.btnExport);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1000, 48);
            this.pnlTop.TabIndex = 0;
            // 
            // lblReport
            // 
            this.lblReport.AutoSize = true;
            this.lblReport.Location = new System.Drawing.Point(12, 16);
            this.lblReport.Name = "lblReport";
            this.lblReport.Size = new System.Drawing.Size(35, 15);
            this.lblReport.Text = "Звіт:";
            // 
            // cmbReport
            // 
            this.cmbReport.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReport.FormattingEnabled = true;
            this.cmbReport.Location = new System.Drawing.Point(52, 12);
            this.cmbReport.Name = "cmbReport";
            this.cmbReport.Size = new System.Drawing.Size(300, 23);
            this.cmbReport.TabIndex = 0;
            this.cmbReport.SelectedIndexChanged += new System.EventHandler(this.cmbReport_SelectedIndexChanged);
            // 
            // lblYear
            // 
            this.lblYear.AutoSize = true;
            this.lblYear.Location = new System.Drawing.Point(370, 16);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(28, 15);
            this.lblYear.Text = "Рік:";
            // 
            // numYear
            // 
            this.numYear.Location = new System.Drawing.Point(402, 12);
            this.numYear.Maximum = new decimal(new int[] { 2100, 0, 0, 0 });
            this.numYear.Minimum = new decimal(new int[] { 2000, 0, 0, 0 });
            this.numYear.Name = "numYear";
            this.numYear.Size = new System.Drawing.Size(80, 23);
            this.numYear.TabIndex = 1;
            this.numYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numYear.ThousandsSeparator = true;
            this.numYear.ValueChanged += new System.EventHandler(this.numYear_ValueChanged);
            // 
            // btnBuild
            // 
            this.btnBuild.Location = new System.Drawing.Point(500, 8);
            this.btnBuild.Name = "btnBuild";
            this.btnBuild.Size = new System.Drawing.Size(120, 30);
            this.btnBuild.TabIndex = 2;
            this.btnBuild.Text = "Сформувати";
            this.btnBuild.UseVisualStyleBackColor = true;
            this.btnBuild.Click += new System.EventHandler(this.btnBuild_Click);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(626, 8);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(130, 30);
            this.btnExport.TabIndex = 3;
            this.btnExport.Text = "Експорт у CSV";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // ReportsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Size = new System.Drawing.Size(1000, 628);
            this.Controls.Add(this.split);
            this.Controls.Add(this.lblSummary);
            this.Controls.Add(this.pnlTop);
            this.Name = "ReportsControl";
            ((System.ComponentModel.ISupportInitialize)(this.numYear)).EndInit();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.chart.ResumeLayout(false);
            this.chart.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.split)).EndInit();
            this.split.Panel1.ResumeLayout(false);
            this.split.Panel2.ResumeLayout(false);
            this.split.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.SplitContainer split;
        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.Panel chart;
        private System.Windows.Forms.Label lblSummary;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblReport;
        private System.Windows.Forms.ComboBox cmbReport;
        private System.Windows.Forms.Label lblYear;
        private System.Windows.Forms.NumericUpDown numYear;
        private System.Windows.Forms.Button btnBuild;
        private System.Windows.Forms.Button btnExport;
    }
}
