namespace Trc.WinForms.Forms
{
    partial class MainForm
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
            this.tabs = new System.Windows.Forms.TabControl();
            this.tabDashboard = new System.Windows.Forms.TabPage();
            this.dashboardControl = new Trc.WinForms.Controls.DashboardControl();
            this.tabPremises = new System.Windows.Forms.TabPage();
            this.premisesControl = new Trc.WinForms.Controls.PremisesControl();
            this.tabTenants = new System.Windows.Forms.TabPage();
            this.tenantsControl = new Trc.WinForms.Controls.TenantsControl();
            this.tabLeases = new System.Windows.Forms.TabPage();
            this.leasesControl = new Trc.WinForms.Controls.LeasesControl();
            this.tabPayments = new System.Windows.Forms.TabPage();
            this.paymentsControl = new Trc.WinForms.Controls.PaymentsControl();
            this.tabEmployees = new System.Windows.Forms.TabPage();
            this.employeesControl = new Trc.WinForms.Controls.EmployeesControl();
            this.tabEvents = new System.Windows.Forms.TabPage();
            this.eventsControl = new Trc.WinForms.Controls.EventsControl();
            this.tabParking = new System.Windows.Forms.TabPage();
            this.parkingControl = new Trc.WinForms.Controls.ParkingControl();
            this.tabReports = new System.Windows.Forms.TabPage();
            this.reportsControl = new Trc.WinForms.Controls.ReportsControl();
            this.tabUsers = new System.Windows.Forms.TabPage();
            this.usersControl = new Trc.WinForms.Controls.UsersControl();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblSpring = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblDb = new System.Windows.Forms.ToolStripStatusLabel();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuChangePassword = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLogout = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSep = new System.Windows.Forms.ToolStripSeparator();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.tabs.SuspendLayout();
            this.tabDashboard.SuspendLayout();
            this.tabPremises.SuspendLayout();
            this.tabTenants.SuspendLayout();
            this.tabLeases.SuspendLayout();
            this.tabPayments.SuspendLayout();
            this.tabEmployees.SuspendLayout();
            this.tabEvents.SuspendLayout();
            this.tabParking.SuspendLayout();
            this.tabReports.SuspendLayout();
            this.tabUsers.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabs
            // 
            this.tabs.Controls.Add(this.tabDashboard);
            this.tabs.Controls.Add(this.tabPremises);
            this.tabs.Controls.Add(this.tabTenants);
            this.tabs.Controls.Add(this.tabLeases);
            this.tabs.Controls.Add(this.tabPayments);
            this.tabs.Controls.Add(this.tabEmployees);
            this.tabs.Controls.Add(this.tabEvents);
            this.tabs.Controls.Add(this.tabParking);
            this.tabs.Controls.Add(this.tabReports);
            this.tabs.Controls.Add(this.tabUsers);
            this.tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabs.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.tabs.Location = new System.Drawing.Point(0, 24);
            this.tabs.Name = "tabs";
            this.tabs.Size = new System.Drawing.Size(1184, 648);
            this.tabs.TabIndex = 1;
            this.tabs.SelectedIndexChanged += new System.EventHandler(this.tabs_SelectedIndexChanged);
            // 
            // tabDashboard
            // 
            this.tabDashboard.Controls.Add(this.dashboardControl);
            this.tabDashboard.Location = new System.Drawing.Point(4, 24);
            this.tabDashboard.Name = "tabDashboard";
            this.tabDashboard.Padding = new System.Windows.Forms.Padding(3);
            this.tabDashboard.Size = new System.Drawing.Size(1176, 620);
            this.tabDashboard.TabIndex = 0;
            this.tabDashboard.Text = "Головна";
            this.tabDashboard.UseVisualStyleBackColor = true;
            // 
            // dashboardControl
            // 
            this.dashboardControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dashboardControl.Location = new System.Drawing.Point(3, 3);
            this.dashboardControl.Name = "dashboardControl";
            this.dashboardControl.Size = new System.Drawing.Size(1170, 614);
            this.dashboardControl.TabIndex = 0;
            // 
            // tabPremises
            // 
            this.tabPremises.Controls.Add(this.premisesControl);
            this.tabPremises.Location = new System.Drawing.Point(4, 24);
            this.tabPremises.Name = "tabPremises";
            this.tabPremises.Padding = new System.Windows.Forms.Padding(3);
            this.tabPremises.Size = new System.Drawing.Size(1176, 620);
            this.tabPremises.TabIndex = 1;
            this.tabPremises.Text = "Приміщення";
            this.tabPremises.UseVisualStyleBackColor = true;
            // 
            // premisesControl
            // 
            this.premisesControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.premisesControl.Location = new System.Drawing.Point(3, 3);
            this.premisesControl.Name = "premisesControl";
            this.premisesControl.Size = new System.Drawing.Size(1170, 614);
            this.premisesControl.TabIndex = 0;
            // 
            // tabTenants
            // 
            this.tabTenants.Controls.Add(this.tenantsControl);
            this.tabTenants.Location = new System.Drawing.Point(4, 24);
            this.tabTenants.Name = "tabTenants";
            this.tabTenants.Padding = new System.Windows.Forms.Padding(3);
            this.tabTenants.Size = new System.Drawing.Size(1176, 620);
            this.tabTenants.TabIndex = 2;
            this.tabTenants.Text = "Орендарі";
            this.tabTenants.UseVisualStyleBackColor = true;
            // 
            // tenantsControl
            // 
            this.tenantsControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tenantsControl.Location = new System.Drawing.Point(3, 3);
            this.tenantsControl.Name = "tenantsControl";
            this.tenantsControl.Size = new System.Drawing.Size(1170, 614);
            this.tenantsControl.TabIndex = 0;
            // 
            // tabLeases
            // 
            this.tabLeases.Controls.Add(this.leasesControl);
            this.tabLeases.Location = new System.Drawing.Point(4, 24);
            this.tabLeases.Name = "tabLeases";
            this.tabLeases.Padding = new System.Windows.Forms.Padding(3);
            this.tabLeases.Size = new System.Drawing.Size(1176, 620);
            this.tabLeases.TabIndex = 3;
            this.tabLeases.Text = "Договори оренди";
            this.tabLeases.UseVisualStyleBackColor = true;
            // 
            // leasesControl
            // 
            this.leasesControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leasesControl.Location = new System.Drawing.Point(3, 3);
            this.leasesControl.Name = "leasesControl";
            this.leasesControl.Size = new System.Drawing.Size(1170, 614);
            this.leasesControl.TabIndex = 0;
            // 
            // tabPayments
            // 
            this.tabPayments.Controls.Add(this.paymentsControl);
            this.tabPayments.Location = new System.Drawing.Point(4, 24);
            this.tabPayments.Name = "tabPayments";
            this.tabPayments.Padding = new System.Windows.Forms.Padding(3);
            this.tabPayments.Size = new System.Drawing.Size(1176, 620);
            this.tabPayments.TabIndex = 4;
            this.tabPayments.Text = "Платежі";
            this.tabPayments.UseVisualStyleBackColor = true;
            // 
            // paymentsControl
            // 
            this.paymentsControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.paymentsControl.Location = new System.Drawing.Point(3, 3);
            this.paymentsControl.Name = "paymentsControl";
            this.paymentsControl.Size = new System.Drawing.Size(1170, 614);
            this.paymentsControl.TabIndex = 0;
            // 
            // tabEmployees
            // 
            this.tabEmployees.Controls.Add(this.employeesControl);
            this.tabEmployees.Location = new System.Drawing.Point(4, 24);
            this.tabEmployees.Name = "tabEmployees";
            this.tabEmployees.Padding = new System.Windows.Forms.Padding(3);
            this.tabEmployees.Size = new System.Drawing.Size(1176, 620);
            this.tabEmployees.TabIndex = 5;
            this.tabEmployees.Text = "Персонал";
            this.tabEmployees.UseVisualStyleBackColor = true;
            // 
            // employeesControl
            // 
            this.employeesControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.employeesControl.Location = new System.Drawing.Point(3, 3);
            this.employeesControl.Name = "employeesControl";
            this.employeesControl.Size = new System.Drawing.Size(1170, 614);
            this.employeesControl.TabIndex = 0;
            // 
            // tabEvents
            // 
            this.tabEvents.Controls.Add(this.eventsControl);
            this.tabEvents.Location = new System.Drawing.Point(4, 24);
            this.tabEvents.Name = "tabEvents";
            this.tabEvents.Padding = new System.Windows.Forms.Padding(3);
            this.tabEvents.Size = new System.Drawing.Size(1176, 620);
            this.tabEvents.TabIndex = 6;
            this.tabEvents.Text = "Заходи та квитки";
            this.tabEvents.UseVisualStyleBackColor = true;
            // 
            // eventsControl
            // 
            this.eventsControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.eventsControl.Location = new System.Drawing.Point(3, 3);
            this.eventsControl.Name = "eventsControl";
            this.eventsControl.Size = new System.Drawing.Size(1170, 614);
            this.eventsControl.TabIndex = 0;
            // 
            // tabParking
            // 
            this.tabParking.Controls.Add(this.parkingControl);
            this.tabParking.Location = new System.Drawing.Point(4, 24);
            this.tabParking.Name = "tabParking";
            this.tabParking.Padding = new System.Windows.Forms.Padding(3);
            this.tabParking.Size = new System.Drawing.Size(1176, 620);
            this.tabParking.TabIndex = 7;
            this.tabParking.Text = "Паркінг";
            this.tabParking.UseVisualStyleBackColor = true;
            // 
            // parkingControl
            // 
            this.parkingControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.parkingControl.Location = new System.Drawing.Point(3, 3);
            this.parkingControl.Name = "parkingControl";
            this.parkingControl.Size = new System.Drawing.Size(1170, 614);
            this.parkingControl.TabIndex = 0;
            // 
            // tabReports
            // 
            this.tabReports.Controls.Add(this.reportsControl);
            this.tabReports.Location = new System.Drawing.Point(4, 24);
            this.tabReports.Name = "tabReports";
            this.tabReports.Padding = new System.Windows.Forms.Padding(3);
            this.tabReports.Size = new System.Drawing.Size(1176, 620);
            this.tabReports.TabIndex = 8;
            this.tabReports.Text = "Звіти";
            this.tabReports.UseVisualStyleBackColor = true;
            // 
            // reportsControl
            // 
            this.reportsControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportsControl.Location = new System.Drawing.Point(3, 3);
            this.reportsControl.Name = "reportsControl";
            this.reportsControl.Size = new System.Drawing.Size(1170, 614);
            this.reportsControl.TabIndex = 0;
            // 
            // tabUsers
            // 
            this.tabUsers.Controls.Add(this.usersControl);
            this.tabUsers.Location = new System.Drawing.Point(4, 24);
            this.tabUsers.Name = "tabUsers";
            this.tabUsers.Padding = new System.Windows.Forms.Padding(3);
            this.tabUsers.Size = new System.Drawing.Size(1176, 620);
            this.tabUsers.TabIndex = 9;
            this.tabUsers.Text = "Користувачі";
            this.tabUsers.UseVisualStyleBackColor = true;
            // 
            // usersControl
            // 
            this.usersControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.usersControl.Location = new System.Drawing.Point(3, 3);
            this.usersControl.Name = "usersControl";
            this.usersControl.Size = new System.Drawing.Size(1170, 614);
            this.usersControl.TabIndex = 0;
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.lblUser,
                this.lblSpring,
                this.lblDb});
            this.statusStrip.Location = new System.Drawing.Point(0, 672);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1184, 22);
            this.statusStrip.TabIndex = 2;
            // 
            // lblUser
            // 
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(150, 17);
            this.lblUser.Text = "Користувач";
            // 
            // lblSpring
            // 
            this.lblSpring.Name = "lblSpring";
            this.lblSpring.Size = new System.Drawing.Size(900, 17);
            this.lblSpring.Spring = true;
            // 
            // lblDb
            // 
            this.lblDb.ForeColor = System.Drawing.Color.DimGray;
            this.lblDb.Name = "lblDb";
            this.lblDb.Size = new System.Drawing.Size(120, 17);
            this.lblDb.Text = "База даних";
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.mnuFile,
                this.mnuHelp});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(1184, 24);
            this.menu.TabIndex = 0;
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.mnuChangePassword,
                this.mnuLogout,
                this.mnuSep,
                this.mnuExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(48, 20);
            this.mnuFile.Text = "&Файл";
            // 
            // mnuChangePassword
            // 
            this.mnuChangePassword.Name = "mnuChangePassword";
            this.mnuChangePassword.Size = new System.Drawing.Size(220, 22);
            this.mnuChangePassword.Text = "Змінити пароль...";
            this.mnuChangePassword.Click += new System.EventHandler(this.mnuChangePassword_Click);
            // 
            // mnuLogout
            // 
            this.mnuLogout.Name = "mnuLogout";
            this.mnuLogout.Size = new System.Drawing.Size(220, 22);
            this.mnuLogout.Text = "Змінити користувача";
            this.mnuLogout.Click += new System.EventHandler(this.mnuLogout_Click);
            // 
            // mnuSep
            // 
            this.mnuSep.Name = "mnuSep";
            this.mnuSep.Size = new System.Drawing.Size(217, 6);
            // 
            // mnuExit
            // 
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.mnuExit.Size = new System.Drawing.Size(220, 22);
            this.mnuExit.Text = "Вихід";
            this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.mnuAbout});
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(65, 20);
            this.mnuHelp.Text = "&Довідка";
            // 
            // mnuAbout
            // 
            this.mnuAbout.Name = "mnuAbout";
            this.mnuAbout.Size = new System.Drawing.Size(180, 22);
            this.mnuAbout.Text = "Про програму";
            this.mnuAbout.Click += new System.EventHandler(this.mnuAbout_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 694);
            this.MainMenuStrip = this.menu;
            this.MinimumSize = new System.Drawing.Size(1000, 600);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ТРЦ — Автоматизована система управління";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Controls.Add(this.tabs);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menu);
            this.Name = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.tabUsers.ResumeLayout(false);
            this.tabUsers.PerformLayout();
            this.tabReports.ResumeLayout(false);
            this.tabReports.PerformLayout();
            this.tabParking.ResumeLayout(false);
            this.tabParking.PerformLayout();
            this.tabEvents.ResumeLayout(false);
            this.tabEvents.PerformLayout();
            this.tabEmployees.ResumeLayout(false);
            this.tabEmployees.PerformLayout();
            this.tabPayments.ResumeLayout(false);
            this.tabPayments.PerformLayout();
            this.tabLeases.ResumeLayout(false);
            this.tabLeases.PerformLayout();
            this.tabTenants.ResumeLayout(false);
            this.tabTenants.PerformLayout();
            this.tabPremises.ResumeLayout(false);
            this.tabPremises.PerformLayout();
            this.tabDashboard.ResumeLayout(false);
            this.tabDashboard.PerformLayout();
            this.tabs.ResumeLayout(false);
            this.tabs.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage tabDashboard;
        private Trc.WinForms.Controls.DashboardControl dashboardControl;
        private System.Windows.Forms.TabPage tabPremises;
        private Trc.WinForms.Controls.PremisesControl premisesControl;
        private System.Windows.Forms.TabPage tabTenants;
        private Trc.WinForms.Controls.TenantsControl tenantsControl;
        private System.Windows.Forms.TabPage tabLeases;
        private Trc.WinForms.Controls.LeasesControl leasesControl;
        private System.Windows.Forms.TabPage tabPayments;
        private Trc.WinForms.Controls.PaymentsControl paymentsControl;
        private System.Windows.Forms.TabPage tabEmployees;
        private Trc.WinForms.Controls.EmployeesControl employeesControl;
        private System.Windows.Forms.TabPage tabEvents;
        private Trc.WinForms.Controls.EventsControl eventsControl;
        private System.Windows.Forms.TabPage tabParking;
        private Trc.WinForms.Controls.ParkingControl parkingControl;
        private System.Windows.Forms.TabPage tabReports;
        private Trc.WinForms.Controls.ReportsControl reportsControl;
        private System.Windows.Forms.TabPage tabUsers;
        private Trc.WinForms.Controls.UsersControl usersControl;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblUser;
        private System.Windows.Forms.ToolStripStatusLabel lblSpring;
        private System.Windows.Forms.ToolStripStatusLabel lblDb;
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuChangePassword;
        private System.Windows.Forms.ToolStripMenuItem mnuLogout;
        private System.Windows.Forms.ToolStripSeparator mnuSep;
        private System.Windows.Forms.ToolStripMenuItem mnuExit;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.ToolStripMenuItem mnuAbout;
    }
}
