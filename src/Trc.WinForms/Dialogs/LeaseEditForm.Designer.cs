namespace Trc.WinForms.Dialogs
{
    partial class LeaseEditForm
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbl1 = new System.Windows.Forms.Label();
            this.lblNumber = new System.Windows.Forms.Label();
            this.lbl2 = new System.Windows.Forms.Label();
            this.cmbTenant = new System.Windows.Forms.ComboBox();
            this.lbl3 = new System.Windows.Forms.Label();
            this.cmbPremises = new System.Windows.Forms.ComboBox();
            this.lbl4 = new System.Windows.Forms.Label();
            this.dtStart = new System.Windows.Forms.DateTimePicker();
            this.lbl5 = new System.Windows.Forms.Label();
            this.cmbTerm = new System.Windows.Forms.ComboBox();
            this.lbl6 = new System.Windows.Forms.Label();
            this.dtEnd = new System.Windows.Forms.DateTimePicker();
            this.lbl7 = new System.Windows.Forms.Label();
            this.numRent = new System.Windows.Forms.NumericUpDown();
            this.lbl8 = new System.Windows.Forms.Label();
            this.numDeposit = new System.Windows.Forms.NumericUpDown();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numRent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDeposit)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.Location = new System.Drawing.Point(16, 22);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(105, 15);
            this.lbl1.Text = "Номер договору:";
            // 
            // lblNumber
            // 
            this.lblNumber.AutoSize = true;
            this.lblNumber.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblNumber.Location = new System.Drawing.Point(170, 22);
            this.lblNumber.Name = "lblNumber";
            this.lblNumber.Size = new System.Drawing.Size(100, 17);
            this.lblNumber.Text = "—";
            // 
            // lbl2
            // 
            this.lbl2.AutoSize = true;
            this.lbl2.Location = new System.Drawing.Point(16, 58);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(56, 15);
            this.lbl2.Text = "Орендар:";
            // 
            // cmbTenant
            // 
            this.cmbTenant.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTenant.FormattingEnabled = true;
            this.cmbTenant.Location = new System.Drawing.Point(170, 54);
            this.cmbTenant.Name = "cmbTenant";
            this.cmbTenant.Size = new System.Drawing.Size(340, 23);
            this.cmbTenant.TabIndex = 1;
            // 
            // lbl3
            // 
            this.lbl3.AutoSize = true;
            this.lbl3.Location = new System.Drawing.Point(16, 94);
            this.lbl3.Name = "lbl3";
            this.lbl3.Size = new System.Drawing.Size(77, 15);
            this.lbl3.Text = "Приміщення:";
            // 
            // cmbPremises
            // 
            this.cmbPremises.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPremises.FormattingEnabled = true;
            this.cmbPremises.Location = new System.Drawing.Point(170, 90);
            this.cmbPremises.Name = "cmbPremises";
            this.cmbPremises.Size = new System.Drawing.Size(340, 23);
            this.cmbPremises.TabIndex = 2;
            this.cmbPremises.SelectedIndexChanged += new System.EventHandler(this.cmbPremises_SelectedIndexChanged);
            // 
            // lbl4
            // 
            this.lbl4.AutoSize = true;
            this.lbl4.Location = new System.Drawing.Point(16, 130);
            this.lbl4.Name = "lbl4";
            this.lbl4.Size = new System.Drawing.Size(91, 15);
            this.lbl4.Text = "Дата початку:";
            // 
            // dtStart
            // 
            this.dtStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtStart.Location = new System.Drawing.Point(170, 126);
            this.dtStart.Name = "dtStart";
            this.dtStart.Size = new System.Drawing.Size(140, 23);
            this.dtStart.TabIndex = 3;
            this.dtStart.ValueChanged += new System.EventHandler(this.dtStart_ValueChanged);
            // 
            // lbl5
            // 
            this.lbl5.AutoSize = true;
            this.lbl5.Location = new System.Drawing.Point(16, 166);
            this.lbl5.Name = "lbl5";
            this.lbl5.Size = new System.Drawing.Size(91, 15);
            this.lbl5.Text = "Строк оренди:";
            // 
            // cmbTerm
            // 
            this.cmbTerm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTerm.FormattingEnabled = true;
            this.cmbTerm.Location = new System.Drawing.Point(170, 162);
            this.cmbTerm.Name = "cmbTerm";
            this.cmbTerm.Size = new System.Drawing.Size(140, 23);
            this.cmbTerm.TabIndex = 4;
            this.cmbTerm.SelectedIndexChanged += new System.EventHandler(this.cmbTerm_SelectedIndexChanged);
            // 
            // lbl6
            // 
            this.lbl6.AutoSize = true;
            this.lbl6.Location = new System.Drawing.Point(16, 202);
            this.lbl6.Name = "lbl6";
            this.lbl6.Size = new System.Drawing.Size(112, 15);
            this.lbl6.Text = "Дата закінчення:";
            // 
            // dtEnd
            // 
            this.dtEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtEnd.Location = new System.Drawing.Point(170, 198);
            this.dtEnd.Name = "dtEnd";
            this.dtEnd.Size = new System.Drawing.Size(140, 23);
            this.dtEnd.TabIndex = 5;
            // 
            // lbl7
            // 
            this.lbl7.AutoSize = true;
            this.lbl7.Location = new System.Drawing.Point(16, 238);
            this.lbl7.Name = "lbl7";
            this.lbl7.Size = new System.Drawing.Size(168, 15);
            this.lbl7.Text = "Орендна плата, грн/міс.:";
            // 
            // numRent
            // 
            this.numRent.DecimalPlaces = 2;
            this.numRent.Increment = new decimal(new int[] { 100, 0, 0, 0 });
            this.numRent.Location = new System.Drawing.Point(170, 234);
            this.numRent.Maximum = new decimal(new int[] { 100000000, 0, 0, 0 });
            this.numRent.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            this.numRent.Name = "numRent";
            this.numRent.Size = new System.Drawing.Size(140, 23);
            this.numRent.TabIndex = 6;
            this.numRent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numRent.ThousandsSeparator = true;
            // 
            // lbl8
            // 
            this.lbl8.AutoSize = true;
            this.lbl8.Location = new System.Drawing.Point(16, 274);
            this.lbl8.Name = "lbl8";
            this.lbl8.Size = new System.Drawing.Size(91, 15);
            this.lbl8.Text = "Депозит, грн:";
            // 
            // numDeposit
            // 
            this.numDeposit.DecimalPlaces = 2;
            this.numDeposit.Increment = new decimal(new int[] { 100, 0, 0, 0 });
            this.numDeposit.Location = new System.Drawing.Point(170, 270);
            this.numDeposit.Maximum = new decimal(new int[] { 100000000, 0, 0, 0 });
            this.numDeposit.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            this.numDeposit.Name = "numDeposit";
            this.numDeposit.Size = new System.Drawing.Size(140, 23);
            this.numDeposit.TabIndex = 7;
            this.numDeposit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numDeposit.ThousandsSeparator = true;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(312, 312);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(105, 30);
            this.btnOk.TabIndex = 8;
            this.btnOk.Text = "Зберегти";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(425, 312);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(105, 30);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Скасувати";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // LeaseEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AcceptButton = this.btnOk;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(540, 356);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Новий договір оренди";
            this.Controls.Add(this.lbl1);
            this.Controls.Add(this.lblNumber);
            this.Controls.Add(this.lbl2);
            this.Controls.Add(this.cmbTenant);
            this.Controls.Add(this.lbl3);
            this.Controls.Add(this.cmbPremises);
            this.Controls.Add(this.lbl4);
            this.Controls.Add(this.dtStart);
            this.Controls.Add(this.lbl5);
            this.Controls.Add(this.cmbTerm);
            this.Controls.Add(this.lbl6);
            this.Controls.Add(this.dtEnd);
            this.Controls.Add(this.lbl7);
            this.Controls.Add(this.numRent);
            this.Controls.Add(this.lbl8);
            this.Controls.Add(this.numDeposit);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Name = "LeaseEditForm";
            ((System.ComponentModel.ISupportInitialize)(this.numDeposit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRent)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.Label lblNumber;
        private System.Windows.Forms.Label lbl2;
        private System.Windows.Forms.ComboBox cmbTenant;
        private System.Windows.Forms.Label lbl3;
        private System.Windows.Forms.ComboBox cmbPremises;
        private System.Windows.Forms.Label lbl4;
        private System.Windows.Forms.DateTimePicker dtStart;
        private System.Windows.Forms.Label lbl5;
        private System.Windows.Forms.ComboBox cmbTerm;
        private System.Windows.Forms.Label lbl6;
        private System.Windows.Forms.DateTimePicker dtEnd;
        private System.Windows.Forms.Label lbl7;
        private System.Windows.Forms.NumericUpDown numRent;
        private System.Windows.Forms.Label lbl8;
        private System.Windows.Forms.NumericUpDown numDeposit;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
    }
}
