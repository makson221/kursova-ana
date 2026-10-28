namespace Trc.WinForms.Dialogs
{
    partial class PremisesEditForm
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
            this.txtNumber = new System.Windows.Forms.TextBox();
            this.lbl2 = new System.Windows.Forms.Label();
            this.numFloor = new System.Windows.Forms.NumericUpDown();
            this.lbl3 = new System.Windows.Forms.Label();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.lbl4 = new System.Windows.Forms.Label();
            this.numArea = new System.Windows.Forms.NumericUpDown();
            this.lbl5 = new System.Windows.Forms.Label();
            this.numRate = new System.Windows.Forms.NumericUpDown();
            this.lbl6 = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.lbl7 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblRent = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numFloor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numArea)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRate)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.Location = new System.Drawing.Point(16, 22);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(119, 15);
            this.lbl1.Text = "Номер приміщення:";
            // 
            // txtNumber
            // 
            this.txtNumber.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNumber.Location = new System.Drawing.Point(170, 18);
            this.txtNumber.Name = "txtNumber";
            this.txtNumber.Size = new System.Drawing.Size(140, 23);
            this.txtNumber.TabIndex = 0;
            // 
            // lbl2
            // 
            this.lbl2.AutoSize = true;
            this.lbl2.Location = new System.Drawing.Point(16, 58);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(49, 15);
            this.lbl2.Text = "Поверх:";
            // 
            // numFloor
            // 
            this.numFloor.Location = new System.Drawing.Point(170, 54);
            this.numFloor.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            this.numFloor.Minimum = new decimal(new int[] { 3, 0, 0, -2147483648 });
            this.numFloor.Name = "numFloor";
            this.numFloor.Size = new System.Drawing.Size(80, 23);
            this.numFloor.TabIndex = 1;
            this.numFloor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numFloor.ThousandsSeparator = true;
            // 
            // lbl3
            // 
            this.lbl3.AutoSize = true;
            this.lbl3.Location = new System.Drawing.Point(16, 94);
            this.lbl3.Name = "lbl3";
            this.lbl3.Size = new System.Drawing.Size(28, 15);
            this.lbl3.Text = "Тип:";
            // 
            // cmbType
            // 
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Location = new System.Drawing.Point(170, 90);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(300, 23);
            this.cmbType.TabIndex = 2;
            // 
            // lbl4
            // 
            this.lbl4.AutoSize = true;
            this.lbl4.Location = new System.Drawing.Point(16, 130);
            this.lbl4.Name = "lbl4";
            this.lbl4.Size = new System.Drawing.Size(70, 15);
            this.lbl4.Text = "Площа, м²:";
            // 
            // numArea
            // 
            this.numArea.DecimalPlaces = 2;
            this.numArea.Location = new System.Drawing.Point(170, 126);
            this.numArea.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            this.numArea.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            this.numArea.Name = "numArea";
            this.numArea.Size = new System.Drawing.Size(140, 23);
            this.numArea.TabIndex = 3;
            this.numArea.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numArea.ThousandsSeparator = true;
            this.numArea.ValueChanged += new System.EventHandler(this.numArea_ValueChanged);
            // 
            // lbl5
            // 
            this.lbl5.AutoSize = true;
            this.lbl5.Location = new System.Drawing.Point(16, 166);
            this.lbl5.Name = "lbl5";
            this.lbl5.Size = new System.Drawing.Size(161, 15);
            this.lbl5.Text = "Ставка, грн/м² на міс.:";
            // 
            // numRate
            // 
            this.numRate.DecimalPlaces = 2;
            this.numRate.Increment = new decimal(new int[] { 10, 0, 0, 0 });
            this.numRate.Location = new System.Drawing.Point(170, 162);
            this.numRate.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            this.numRate.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            this.numRate.Name = "numRate";
            this.numRate.Size = new System.Drawing.Size(140, 23);
            this.numRate.TabIndex = 4;
            this.numRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numRate.ThousandsSeparator = true;
            this.numRate.ValueChanged += new System.EventHandler(this.numArea_ValueChanged);
            // 
            // lbl6
            // 
            this.lbl6.AutoSize = true;
            this.lbl6.Location = new System.Drawing.Point(16, 202);
            this.lbl6.Name = "lbl6";
            this.lbl6.Size = new System.Drawing.Size(35, 15);
            this.lbl6.Text = "Стан:";
            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(170, 198);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(300, 23);
            this.cmbStatus.TabIndex = 5;
            // 
            // lbl7
            // 
            this.lbl7.AutoSize = true;
            this.lbl7.Location = new System.Drawing.Point(16, 238);
            this.lbl7.Name = "lbl7";
            this.lbl7.Size = new System.Drawing.Size(35, 15);
            this.lbl7.Text = "Опис:";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(170, 234);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(300, 60);
            this.txtDescription.TabIndex = 6;
            // 
            // lblRent
            // 
            this.lblRent.AutoSize = true;
            this.lblRent.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblRent.Location = new System.Drawing.Point(170, 572);
            this.lblRent.Name = "lblRent";
            this.lblRent.Size = new System.Drawing.Size(200, 17);
            this.lblRent.Text = "Базова орендна плата: —";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(262, 608);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(105, 30);
            this.btnOk.TabIndex = 7;
            this.btnOk.Text = "Зберегти";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(375, 608);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(105, 30);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Скасувати";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // PremisesEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AcceptButton = this.btnOk;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(490, 652);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Приміщення";
            this.Controls.Add(this.lbl1);
            this.Controls.Add(this.txtNumber);
            this.Controls.Add(this.lbl2);
            this.Controls.Add(this.numFloor);
            this.Controls.Add(this.lbl3);
            this.Controls.Add(this.cmbType);
            this.Controls.Add(this.lbl4);
            this.Controls.Add(this.numArea);
            this.Controls.Add(this.lbl5);
            this.Controls.Add(this.numRate);
            this.Controls.Add(this.lbl6);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.lbl7);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.lblRent);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Name = "PremisesEditForm";
            ((System.ComponentModel.ISupportInitialize)(this.numRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numArea)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFloor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.TextBox txtNumber;
        private System.Windows.Forms.Label lbl2;
        private System.Windows.Forms.NumericUpDown numFloor;
        private System.Windows.Forms.Label lbl3;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Label lbl4;
        private System.Windows.Forms.NumericUpDown numArea;
        private System.Windows.Forms.Label lbl5;
        private System.Windows.Forms.NumericUpDown numRate;
        private System.Windows.Forms.Label lbl6;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Label lbl7;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblRent;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
    }
}
