namespace Trc.WinForms.Controls
{
    partial class DashboardControl
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
            this.grpDebtors = new System.Windows.Forms.GroupBox();
            this.gridDebtors = new System.Windows.Forms.DataGridView();
            this.grpEvents = new System.Windows.Forms.GroupBox();
            this.gridEvents = new System.Windows.Forms.DataGridView();
            this.flowCards = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.split)).BeginInit();
            this.split.Panel1.SuspendLayout();
            this.split.Panel2.SuspendLayout();
            this.split.SuspendLayout();
            this.grpDebtors.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridDebtors)).BeginInit();
            this.grpEvents.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridEvents)).BeginInit();
            this.flowCards.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // split
            // 
            this.split.Panel1.Controls.Add(this.grpDebtors);
            this.split.Panel2.Controls.Add(this.grpEvents);
            this.split.Dock = System.Windows.Forms.DockStyle.Fill;
            this.split.Location = new System.Drawing.Point(0, 268);
            this.split.Name = "split";
            this.split.Size = new System.Drawing.Size(1000, 360);
            this.split.SplitterDistance = 520;
            this.split.TabIndex = 2;
            // 
            // grpDebtors
            // 
            this.grpDebtors.Controls.Add(this.gridDebtors);
            this.grpDebtors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpDebtors.Location = new System.Drawing.Point(0, 0);
            this.grpDebtors.Name = "grpDebtors";
            this.grpDebtors.Padding = new System.Windows.Forms.Padding(6);
            this.grpDebtors.Size = new System.Drawing.Size(520, 360);
            this.grpDebtors.TabIndex = 0;
            this.grpDebtors.Text = "Боржники (прострочені платежі)";
            // 
            // gridDebtors
            // 
            this.gridDebtors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridDebtors.Location = new System.Drawing.Point(6, 22);
            this.gridDebtors.Name = "gridDebtors";
            this.gridDebtors.Size = new System.Drawing.Size(508, 332);
            this.gridDebtors.TabIndex = 0;
            // 
            // grpEvents
            // 
            this.grpEvents.Controls.Add(this.gridEvents);
            this.grpEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpEvents.Location = new System.Drawing.Point(0, 0);
            this.grpEvents.Name = "grpEvents";
            this.grpEvents.Padding = new System.Windows.Forms.Padding(6);
            this.grpEvents.Size = new System.Drawing.Size(476, 360);
            this.grpEvents.TabIndex = 0;
            this.grpEvents.Text = "Найближчі заходи";
            // 
            // gridEvents
            // 
            this.gridEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridEvents.Location = new System.Drawing.Point(6, 22);
            this.gridEvents.Name = "gridEvents";
            this.gridEvents.Size = new System.Drawing.Size(464, 332);
            this.gridEvents.TabIndex = 0;
            // 
            // flowCards
            // 
            this.flowCards.AutoScroll = true;
            this.flowCards.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowCards.Location = new System.Drawing.Point(0, 48);
            this.flowCards.Name = "flowCards";
            this.flowCards.Padding = new System.Windows.Forms.Padding(6);
            this.flowCards.Size = new System.Drawing.Size(1000, 220);
            this.flowCards.TabIndex = 1;
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.lblDate);
            this.pnlHeader.Controls.Add(this.btnRefresh);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1000, 48);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(12, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(300, 30);
            this.lblTitle.Text = "Огляд роботи ТРЦ";
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.ForeColor = System.Drawing.Color.DimGray;
            this.lblDate.Location = new System.Drawing.Point(330, 20);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(100, 15);
            this.lblDate.Text = "дата";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnRefresh.Location = new System.Drawing.Point(880, 10);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 30);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.Text = "Оновити";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // DashboardControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Size = new System.Drawing.Size(1000, 628);
            this.Controls.Add(this.split);
            this.Controls.Add(this.flowCards);
            this.Controls.Add(this.pnlHeader);
            this.Name = "DashboardControl";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.flowCards.ResumeLayout(false);
            this.flowCards.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridEvents)).EndInit();
            this.grpEvents.ResumeLayout(false);
            this.grpEvents.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridDebtors)).EndInit();
            this.grpDebtors.ResumeLayout(false);
            this.grpDebtors.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.split)).EndInit();
            this.split.Panel1.ResumeLayout(false);
            this.split.Panel2.ResumeLayout(false);
            this.split.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.SplitContainer split;
        private System.Windows.Forms.GroupBox grpDebtors;
        private System.Windows.Forms.DataGridView gridDebtors;
        private System.Windows.Forms.GroupBox grpEvents;
        private System.Windows.Forms.DataGridView gridEvents;
        private System.Windows.Forms.FlowLayoutPanel flowCards;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Button btnRefresh;
    }
}
