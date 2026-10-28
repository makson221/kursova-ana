namespace Trc.WinForms.Controls
{
    partial class ParkingControl
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
            this.components = new System.ComponentModel.Container();
            this.split = new System.Windows.Forms.SplitContainer();
            this.grpActive = new System.Windows.Forms.GroupBox();
            this.gridActive = new System.Windows.Forms.DataGridView();
            this.pnlActiveButtons = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnRefreshActive = new System.Windows.Forms.Button();
            this.grpHistory = new System.Windows.Forms.GroupBox();
            this.gridHistory = new System.Windows.Forms.DataGridView();
            this.lblHistoryTotal = new System.Windows.Forms.Label();
            this.pnlHistoryFilter = new System.Windows.Forms.Panel();
            this.lblFrom = new System.Windows.Forms.Label();
            this.dtFrom = new System.Windows.Forms.DateTimePicker();
            this.lblTo = new System.Windows.Forms.Label();
            this.dtTo = new System.Windows.Forms.DateTimePicker();
            this.txtHistoryPlate = new System.Windows.Forms.TextBox();
            this.btnShowHistory = new System.Windows.Forms.Button();
            this.pnlEntry = new System.Windows.Forms.Panel();
            this.lblPlate = new System.Windows.Forms.Label();
            this.txtPlate = new System.Windows.Forms.TextBox();
            this.btnEntry = new System.Windows.Forms.Button();
            this.lblFreeSpaces = new System.Windows.Forms.Label();
            this.lblTariff = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.split)).BeginInit();
            this.split.Panel1.SuspendLayout();
            this.split.Panel2.SuspendLayout();
            this.split.SuspendLayout();
            this.grpActive.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridActive)).BeginInit();
            this.pnlActiveButtons.SuspendLayout();
            this.grpHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridHistory)).BeginInit();
            this.pnlHistoryFilter.SuspendLayout();
            this.pnlEntry.SuspendLayout();
            this.SuspendLayout();
            // 
            // split
            // 
            this.split.Panel1.Controls.Add(this.grpActive);
            this.split.Panel2.Controls.Add(this.grpHistory);
            this.split.Dock = System.Windows.Forms.DockStyle.Fill;
            this.split.Location = new System.Drawing.Point(0, 70);
            this.split.Name = "split";
            this.split.Size = new System.Drawing.Size(1000, 558);
            this.split.SplitterDistance = 470;
            this.split.TabIndex = 1;
            // 
            // grpActive
            // 
            this.grpActive.Controls.Add(this.gridActive);
            this.grpActive.Controls.Add(this.pnlActiveButtons);
            this.grpActive.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpActive.Location = new System.Drawing.Point(0, 0);
            this.grpActive.Name = "grpActive";
            this.grpActive.Padding = new System.Windows.Forms.Padding(6);
            this.grpActive.Size = new System.Drawing.Size(470, 558);
            this.grpActive.TabIndex = 0;
            this.grpActive.Text = "Автомобілі на паркінгу";
            // 
            // gridActive
            // 
            this.gridActive.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridActive.Location = new System.Drawing.Point(6, 22);
            this.gridActive.Name = "gridActive";
            this.gridActive.Size = new System.Drawing.Size(458, 486);
            this.gridActive.TabIndex = 0;
            this.gridActive.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridActive_CellDoubleClick);
            // 
            // pnlActiveButtons
            // 
            this.pnlActiveButtons.Controls.Add(this.btnExit);
            this.pnlActiveButtons.Controls.Add(this.btnRefreshActive);
            this.pnlActiveButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlActiveButtons.Location = new System.Drawing.Point(6, 508);
            this.pnlActiveButtons.Name = "pnlActiveButtons";
            this.pnlActiveButtons.Size = new System.Drawing.Size(458, 44);
            this.pnlActiveButtons.TabIndex = 1;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(0, 7);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(230, 30);
            this.btnExit.TabIndex = 0;
            this.btnExit.Text = "Зареєструвати виїзд / оплата";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnRefreshActive
            // 
            this.btnRefreshActive.Location = new System.Drawing.Point(236, 7);
            this.btnRefreshActive.Name = "btnRefreshActive";
            this.btnRefreshActive.Size = new System.Drawing.Size(100, 30);
            this.btnRefreshActive.TabIndex = 1;
            this.btnRefreshActive.Text = "Оновити";
            this.btnRefreshActive.UseVisualStyleBackColor = true;
            this.btnRefreshActive.Click += new System.EventHandler(this.btnRefreshActive_Click);
            // 
            // grpHistory
            // 
            this.grpHistory.Controls.Add(this.gridHistory);
            this.grpHistory.Controls.Add(this.lblHistoryTotal);
            this.grpHistory.Controls.Add(this.pnlHistoryFilter);
            this.grpHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpHistory.Location = new System.Drawing.Point(0, 0);
            this.grpHistory.Name = "grpHistory";
            this.grpHistory.Padding = new System.Windows.Forms.Padding(6);
            this.grpHistory.Size = new System.Drawing.Size(526, 558);
            this.grpHistory.TabIndex = 0;
            this.grpHistory.Text = "Історія паркування";
            // 
            // gridHistory
            // 
            this.gridHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridHistory.Location = new System.Drawing.Point(6, 62);
            this.gridHistory.Name = "gridHistory";
            this.gridHistory.Size = new System.Drawing.Size(514, 446);
            this.gridHistory.TabIndex = 0;
            // 
            // lblHistoryTotal
            // 
            this.lblHistoryTotal.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblHistoryTotal.Location = new System.Drawing.Point(6, 508);
            this.lblHistoryTotal.Name = "lblHistoryTotal";
            this.lblHistoryTotal.Size = new System.Drawing.Size(514, 44);
            this.lblHistoryTotal.TabIndex = 2;
            this.lblHistoryTotal.Text = "—";
            this.lblHistoryTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlHistoryFilter
            // 
            this.pnlHistoryFilter.Controls.Add(this.lblFrom);
            this.pnlHistoryFilter.Controls.Add(this.dtFrom);
            this.pnlHistoryFilter.Controls.Add(this.lblTo);
            this.pnlHistoryFilter.Controls.Add(this.dtTo);
            this.pnlHistoryFilter.Controls.Add(this.txtHistoryPlate);
            this.pnlHistoryFilter.Controls.Add(this.btnShowHistory);
            this.pnlHistoryFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHistoryFilter.Location = new System.Drawing.Point(6, 22);
            this.pnlHistoryFilter.Name = "pnlHistoryFilter";
            this.pnlHistoryFilter.Size = new System.Drawing.Size(514, 40);
            this.pnlHistoryFilter.TabIndex = 1;
            // 
            // lblFrom
            // 
            this.lblFrom.AutoSize = true;
            this.lblFrom.Location = new System.Drawing.Point(0, 11);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(14, 15);
            this.lblFrom.Text = "З:";
            // 
            // dtFrom
            // 
            this.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFrom.Location = new System.Drawing.Point(22, 7);
            this.dtFrom.Name = "dtFrom";
            this.dtFrom.Size = new System.Drawing.Size(110, 23);
            this.dtFrom.TabIndex = 0;
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Location = new System.Drawing.Point(140, 11);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(21, 15);
            this.lblTo.Text = "по:";
            // 
            // dtTo
            // 
            this.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtTo.Location = new System.Drawing.Point(168, 7);
            this.dtTo.Name = "dtTo";
            this.dtTo.Size = new System.Drawing.Size(110, 23);
            this.dtTo.TabIndex = 1;
            // 
            // txtHistoryPlate
            // 
            this.txtHistoryPlate.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtHistoryPlate.Location = new System.Drawing.Point(286, 7);
            this.txtHistoryPlate.Name = "txtHistoryPlate";
            this.txtHistoryPlate.PlaceholderText = "Номер авто";
            this.txtHistoryPlate.Size = new System.Drawing.Size(110, 23);
            this.txtHistoryPlate.TabIndex = 2;
            // 
            // btnShowHistory
            // 
            this.btnShowHistory.Location = new System.Drawing.Point(404, 4);
            this.btnShowHistory.Name = "btnShowHistory";
            this.btnShowHistory.Size = new System.Drawing.Size(100, 30);
            this.btnShowHistory.TabIndex = 3;
            this.btnShowHistory.Text = "Показати";
            this.btnShowHistory.UseVisualStyleBackColor = true;
            this.btnShowHistory.Click += new System.EventHandler(this.btnShowHistory_Click);
            // 
            // pnlEntry
            // 
            this.pnlEntry.Controls.Add(this.lblPlate);
            this.pnlEntry.Controls.Add(this.txtPlate);
            this.pnlEntry.Controls.Add(this.btnEntry);
            this.pnlEntry.Controls.Add(this.lblFreeSpaces);
            this.pnlEntry.Controls.Add(this.lblTariff);
            this.pnlEntry.BackColor = System.Drawing.Color.AliceBlue;
            this.pnlEntry.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEntry.Location = new System.Drawing.Point(0, 0);
            this.pnlEntry.Name = "pnlEntry";
            this.pnlEntry.Size = new System.Drawing.Size(1000, 70);
            this.pnlEntry.TabIndex = 0;
            // 
            // lblPlate
            // 
            this.lblPlate.AutoSize = true;
            this.lblPlate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblPlate.Location = new System.Drawing.Point(12, 12);
            this.lblPlate.Name = "lblPlate";
            this.lblPlate.Size = new System.Drawing.Size(98, 15);
            this.lblPlate.Text = "Номерний знак:";
            // 
            // txtPlate
            // 
            this.txtPlate.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPlate.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.txtPlate.Location = new System.Drawing.Point(12, 34);
            this.txtPlate.MaxLength = 12;
            this.txtPlate.Name = "txtPlate";
            this.txtPlate.PlaceholderText = "AA1234BB";
            this.txtPlate.Size = new System.Drawing.Size(160, 23);
            this.txtPlate.TabIndex = 0;
            // 
            // btnEntry
            // 
            this.btnEntry.Location = new System.Drawing.Point(180, 33);
            this.btnEntry.Name = "btnEntry";
            this.btnEntry.Size = new System.Drawing.Size(170, 30);
            this.btnEntry.TabIndex = 1;
            this.btnEntry.Text = "Зареєструвати в'їзд";
            this.btnEntry.UseVisualStyleBackColor = true;
            this.btnEntry.Click += new System.EventHandler(this.btnEntry_Click);
            // 
            // lblFreeSpaces
            // 
            this.lblFreeSpaces.AutoSize = true;
            this.lblFreeSpaces.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.lblFreeSpaces.ForeColor = System.Drawing.Color.SeaGreen;
            this.lblFreeSpaces.Location = new System.Drawing.Point(380, 12);
            this.lblFreeSpaces.Name = "lblFreeSpaces";
            this.lblFreeSpaces.Size = new System.Drawing.Size(200, 25);
            this.lblFreeSpaces.Text = "Вільно місць: —";
            // 
            // lblTariff
            // 
            this.lblTariff.AutoSize = true;
            this.lblTariff.ForeColor = System.Drawing.Color.DimGray;
            this.lblTariff.Location = new System.Drawing.Point(382, 42);
            this.lblTariff.Name = "lblTariff";
            this.lblTariff.Size = new System.Drawing.Size(300, 15);
            this.lblTariff.Text = "Тариф";
            // 
            // timer
            // 
            this.timer.Interval = 30000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // ParkingControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Size = new System.Drawing.Size(1000, 628);
            this.Controls.Add(this.split);
            this.Controls.Add(this.pnlEntry);
            this.Name = "ParkingControl";
            this.pnlEntry.ResumeLayout(false);
            this.pnlEntry.PerformLayout();
            this.pnlHistoryFilter.ResumeLayout(false);
            this.pnlHistoryFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridHistory)).EndInit();
            this.grpHistory.ResumeLayout(false);
            this.grpHistory.PerformLayout();
            this.pnlActiveButtons.ResumeLayout(false);
            this.pnlActiveButtons.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridActive)).EndInit();
            this.grpActive.ResumeLayout(false);
            this.grpActive.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.split)).EndInit();
            this.split.Panel1.ResumeLayout(false);
            this.split.Panel2.ResumeLayout(false);
            this.split.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.SplitContainer split;
        private System.Windows.Forms.GroupBox grpActive;
        private System.Windows.Forms.DataGridView gridActive;
        private System.Windows.Forms.Panel pnlActiveButtons;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnRefreshActive;
        private System.Windows.Forms.GroupBox grpHistory;
        private System.Windows.Forms.DataGridView gridHistory;
        private System.Windows.Forms.Label lblHistoryTotal;
        private System.Windows.Forms.Panel pnlHistoryFilter;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.DateTimePicker dtFrom;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.DateTimePicker dtTo;
        private System.Windows.Forms.TextBox txtHistoryPlate;
        private System.Windows.Forms.Button btnShowHistory;
        private System.Windows.Forms.Panel pnlEntry;
        private System.Windows.Forms.Label lblPlate;
        private System.Windows.Forms.TextBox txtPlate;
        private System.Windows.Forms.Button btnEntry;
        private System.Windows.Forms.Label lblFreeSpaces;
        private System.Windows.Forms.Label lblTariff;
        private System.Windows.Forms.Timer timer;
    }
}
