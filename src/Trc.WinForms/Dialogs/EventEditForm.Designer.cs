namespace Trc.WinForms.Dialogs
{
    partial class EventEditForm
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
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.lbl2 = new System.Windows.Forms.Label();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.lbl3 = new System.Windows.Forms.Label();
            this.dtStart = new System.Windows.Forms.DateTimePicker();
            this.lbl4 = new System.Windows.Forms.Label();
            this.numCapacity = new System.Windows.Forms.NumericUpDown();
            this.lbl5 = new System.Windows.Forms.Label();
            this.numPrice = new System.Windows.Forms.NumericUpDown();
            this.lbl6 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numCapacity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPrice)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.Location = new System.Drawing.Point(16, 22);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(91, 15);
            this.lbl1.Text = "Назва заходу:";
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(170, 18);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(300, 23);
            this.txtTitle.TabIndex = 0;
            // 
            // lbl2
            // 
            this.lbl2.AutoSize = true;
            this.lbl2.Location = new System.Drawing.Point(16, 58);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(119, 15);
            this.lbl2.Text = "Місце проведення:";
            // 
            // txtLocation
            // 
            this.txtLocation.Location = new System.Drawing.Point(170, 54);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Size = new System.Drawing.Size(300, 23);
            this.txtLocation.TabIndex = 1;
            // 
            // lbl3
            // 
            this.lbl3.AutoSize = true;
            this.lbl3.Location = new System.Drawing.Point(16, 94);
            this.lbl3.Name = "lbl3";
            this.lbl3.Size = new System.Drawing.Size(77, 15);
            this.lbl3.Text = "Дата і час:";
            // 
            // dtStart
            // 
            this.dtStart.CustomFormat = "dd.MM.yyyy HH:mm";
            this.dtStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtStart.Location = new System.Drawing.Point(170, 90);
            this.dtStart.Name = "dtStart";
            this.dtStart.Size = new System.Drawing.Size(160, 23);
            this.dtStart.TabIndex = 2;
            // 
            // lbl4
            // 
            this.lbl4.AutoSize = true;
            this.lbl4.Location = new System.Drawing.Point(16, 130);
            this.lbl4.Name = "lbl4";
            this.lbl4.Size = new System.Drawing.Size(112, 15);
            this.lbl4.Text = "Кількість місць:";
            // 
            // numCapacity
            // 
            this.numCapacity.Location = new System.Drawing.Point(170, 126);
            this.numCapacity.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            this.numCapacity.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numCapacity.Name = "numCapacity";
            this.numCapacity.Size = new System.Drawing.Size(100, 23);
            this.numCapacity.TabIndex = 3;
            this.numCapacity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numCapacity.ThousandsSeparator = true;
            // 
            // lbl5
            // 
            this.lbl5.AutoSize = true;
            this.lbl5.Location = new System.Drawing.Point(16, 166);
            this.lbl5.Name = "lbl5";
            this.lbl5.Size = new System.Drawing.Size(119, 15);
            this.lbl5.Text = "Ціна квитка, грн:";
            // 
            // numPrice
            // 
            this.numPrice.DecimalPlaces = 2;
            this.numPrice.Increment = new decimal(new int[] { 10, 0, 0, 0 });
            this.numPrice.Location = new System.Drawing.Point(170, 162);
            this.numPrice.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            this.numPrice.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            this.numPrice.Name = "numPrice";
            this.numPrice.Size = new System.Drawing.Size(120, 23);
            this.numPrice.TabIndex = 4;
            this.numPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numPrice.ThousandsSeparator = true;
            // 
            // lbl6
            // 
            this.lbl6.AutoSize = true;
            this.lbl6.Location = new System.Drawing.Point(16, 202);
            this.lbl6.Name = "lbl6";
            this.lbl6.Size = new System.Drawing.Size(35, 15);
            this.lbl6.Text = "Опис:";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(170, 198);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(300, 70);
            this.txtDescription.TabIndex = 5;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(262, 480);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(105, 30);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "Зберегти";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(375, 480);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(105, 30);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Скасувати";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // EventEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AcceptButton = this.btnOk;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(490, 524);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Захід";
            this.Controls.Add(this.lbl1);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.lbl2);
            this.Controls.Add(this.txtLocation);
            this.Controls.Add(this.lbl3);
            this.Controls.Add(this.dtStart);
            this.Controls.Add(this.lbl4);
            this.Controls.Add(this.numCapacity);
            this.Controls.Add(this.lbl5);
            this.Controls.Add(this.numPrice);
            this.Controls.Add(this.lbl6);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Name = "EventEditForm";
            ((System.ComponentModel.ISupportInitialize)(this.numPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCapacity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label lbl2;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.Label lbl3;
        private System.Windows.Forms.DateTimePicker dtStart;
        private System.Windows.Forms.Label lbl4;
        private System.Windows.Forms.NumericUpDown numCapacity;
        private System.Windows.Forms.Label lbl5;
        private System.Windows.Forms.NumericUpDown numPrice;
        private System.Windows.Forms.Label lbl6;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
    }
}
