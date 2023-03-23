namespace UploadFiles
{
    partial class frmConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConfig));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSrcRootPath = new System.Windows.Forms.TextBox();
            this.txtDstRootPath = new System.Windows.Forms.TextBox();
            this.btnBrowseSrc = new System.Windows.Forms.Button();
            this.btnBrowseDst = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdoFTP = new System.Windows.Forms.RadioButton();
            this.rdoWeb = new System.Windows.Forms.RadioButton();
            this.txtServerPort = new System.Windows.Forms.TextBox();
            this.txtServerPassword = new System.Windows.Forms.TextBox();
            this.chkPassive = new System.Windows.Forms.CheckBox();
            this.txtServerUserName = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtServerURL = new System.Windows.Forms.TextBox();
            this.lblUserName = new System.Windows.Forms.Label();
            this.lblSep = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dateScheduleTime = new System.Windows.Forms.DateTimePicker();
            this.chkCopyAllAtFirst = new System.Windows.Forms.CheckBox();
            this.chkShowNextTime = new System.Windows.Forms.CheckBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSet = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.Info;
            this.label1.Location = new System.Drawing.Point(10, 21);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Source";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.Info;
            this.label2.Location = new System.Drawing.Point(10, 61);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Destination";
            // 
            // txtSrcRootPath
            // 
            this.txtSrcRootPath.Location = new System.Drawing.Point(90, 21);
            this.txtSrcRootPath.Margin = new System.Windows.Forms.Padding(4);
            this.txtSrcRootPath.Name = "txtSrcRootPath";
            this.txtSrcRootPath.Size = new System.Drawing.Size(381, 22);
            this.txtSrcRootPath.TabIndex = 0;
            // 
            // txtDstRootPath
            // 
            this.txtDstRootPath.Location = new System.Drawing.Point(90, 59);
            this.txtDstRootPath.Margin = new System.Windows.Forms.Padding(4);
            this.txtDstRootPath.Name = "txtDstRootPath";
            this.txtDstRootPath.Size = new System.Drawing.Size(381, 22);
            this.txtDstRootPath.TabIndex = 2;
            // 
            // btnBrowseSrc
            // 
            this.btnBrowseSrc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnBrowseSrc.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnBrowseSrc.Location = new System.Drawing.Point(482, 21);
            this.btnBrowseSrc.Margin = new System.Windows.Forms.Padding(4);
            this.btnBrowseSrc.Name = "btnBrowseSrc";
            this.btnBrowseSrc.Size = new System.Drawing.Size(27, 25);
            this.btnBrowseSrc.TabIndex = 1;
            this.btnBrowseSrc.Text = "...";
            this.btnBrowseSrc.UseVisualStyleBackColor = true;
            this.btnBrowseSrc.Click += new System.EventHandler(this.btnBrowseSrc_Click);
            // 
            // btnBrowseDst
            // 
            this.btnBrowseDst.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnBrowseDst.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnBrowseDst.Location = new System.Drawing.Point(481, 57);
            this.btnBrowseDst.Margin = new System.Windows.Forms.Padding(4);
            this.btnBrowseDst.Name = "btnBrowseDst";
            this.btnBrowseDst.Size = new System.Drawing.Size(27, 25);
            this.btnBrowseDst.TabIndex = 3;
            this.btnBrowseDst.Text = "...";
            this.btnBrowseDst.UseVisualStyleBackColor = true;
            this.btnBrowseDst.Click += new System.EventHandler(this.btnBrowseDst_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdoFTP);
            this.groupBox1.Controls.Add(this.rdoWeb);
            this.groupBox1.Controls.Add(this.txtServerPort);
            this.groupBox1.Controls.Add(this.txtServerPassword);
            this.groupBox1.Controls.Add(this.chkPassive);
            this.groupBox1.Controls.Add(this.txtServerUserName);
            this.groupBox1.Controls.Add(this.lblPassword);
            this.groupBox1.Controls.Add(this.txtServerURL);
            this.groupBox1.Controls.Add(this.lblUserName);
            this.groupBox1.Controls.Add(this.lblSep);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.Info;
            this.groupBox1.Location = new System.Drawing.Point(13, 103);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(510, 178);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Server Information";
            // 
            // rdoFTP
            // 
            this.rdoFTP.AutoSize = true;
            this.rdoFTP.Location = new System.Drawing.Point(23, 105);
            this.rdoFTP.Name = "rdoFTP";
            this.rdoFTP.Size = new System.Drawing.Size(52, 20);
            this.rdoFTP.TabIndex = 5;
            this.rdoFTP.Text = "FTP";
            this.rdoFTP.UseVisualStyleBackColor = true;
            this.rdoFTP.CheckedChanged += new System.EventHandler(this.rdoWeb_CheckedChanged);
            // 
            // rdoWeb
            // 
            this.rdoWeb.AutoSize = true;
            this.rdoWeb.Checked = true;
            this.rdoWeb.Location = new System.Drawing.Point(23, 53);
            this.rdoWeb.Name = "rdoWeb";
            this.rdoWeb.Size = new System.Drawing.Size(103, 20);
            this.rdoWeb.TabIndex = 5;
            this.rdoWeb.TabStop = true;
            this.rdoWeb.Text = "Web Posting";
            this.rdoWeb.UseVisualStyleBackColor = true;
            this.rdoWeb.CheckedChanged += new System.EventHandler(this.rdoWeb_CheckedChanged);
            // 
            // txtServerPort
            // 
            this.txtServerPort.Location = new System.Drawing.Point(459, 32);
            this.txtServerPort.Margin = new System.Windows.Forms.Padding(4);
            this.txtServerPort.Name = "txtServerPort";
            this.txtServerPort.Size = new System.Drawing.Size(35, 22);
            this.txtServerPort.TabIndex = 1;
            // 
            // txtServerPassword
            // 
            this.txtServerPassword.Location = new System.Drawing.Point(235, 138);
            this.txtServerPassword.Margin = new System.Windows.Forms.Padding(4);
            this.txtServerPassword.Name = "txtServerPassword";
            this.txtServerPassword.Size = new System.Drawing.Size(205, 22);
            this.txtServerPassword.TabIndex = 4;
            this.txtServerPassword.UseSystemPasswordChar = true;
            // 
            // chkPassive
            // 
            this.chkPassive.AutoSize = true;
            this.chkPassive.ForeColor = System.Drawing.SystemColors.Info;
            this.chkPassive.Location = new System.Drawing.Point(235, 67);
            this.chkPassive.Name = "chkPassive";
            this.chkPassive.Size = new System.Drawing.Size(114, 20);
            this.chkPassive.TabIndex = 2;
            this.chkPassive.Text = "Passive Mode";
            this.chkPassive.UseVisualStyleBackColor = true;
            // 
            // txtServerUserName
            // 
            this.txtServerUserName.Location = new System.Drawing.Point(235, 102);
            this.txtServerUserName.Margin = new System.Windows.Forms.Padding(4);
            this.txtServerUserName.Name = "txtServerUserName";
            this.txtServerUserName.Size = new System.Drawing.Size(205, 22);
            this.txtServerUserName.TabIndex = 3;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.ForeColor = System.Drawing.SystemColors.Info;
            this.lblPassword.Location = new System.Drawing.Point(151, 140);
            this.lblPassword.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(68, 16);
            this.lblPassword.TabIndex = 0;
            this.lblPassword.Text = "Password";
            // 
            // txtServerURL
            // 
            this.txtServerURL.Location = new System.Drawing.Point(235, 32);
            this.txtServerURL.Margin = new System.Windows.Forms.Padding(4);
            this.txtServerURL.Name = "txtServerURL";
            this.txtServerURL.Size = new System.Drawing.Size(205, 22);
            this.txtServerURL.TabIndex = 0;
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.ForeColor = System.Drawing.SystemColors.Info;
            this.lblUserName.Location = new System.Drawing.Point(151, 105);
            this.lblUserName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(77, 16);
            this.lblUserName.TabIndex = 0;
            this.lblUserName.Text = "User Name";
            // 
            // lblSep
            // 
            this.lblSep.AutoSize = true;
            this.lblSep.ForeColor = System.Drawing.SystemColors.Info;
            this.lblSep.Location = new System.Drawing.Point(445, 35);
            this.lblSep.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSep.Name = "lblSep";
            this.lblSep.Size = new System.Drawing.Size(11, 16);
            this.lblSep.TabIndex = 0;
            this.lblSep.Text = ":";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.Info;
            this.label3.Location = new System.Drawing.Point(161, 32);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "Server";
            // 
            // label7
            // 
            this.label7.ForeColor = System.Drawing.SystemColors.Info;
            this.label7.Location = new System.Drawing.Point(15, 299);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 36);
            this.label7.TabIndex = 5;
            this.label7.Text = "Schedule (Everyday)";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dateScheduleTime
            // 
            this.dateScheduleTime.CustomFormat = "HH:mm";
            this.dateScheduleTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateScheduleTime.Location = new System.Drawing.Point(109, 308);
            this.dateScheduleTime.Name = "dateScheduleTime";
            this.dateScheduleTime.ShowUpDown = true;
            this.dateScheduleTime.Size = new System.Drawing.Size(78, 22);
            this.dateScheduleTime.TabIndex = 6;
            // 
            // chkCopyAllAtFirst
            // 
            this.chkCopyAllAtFirst.AutoSize = true;
            this.chkCopyAllAtFirst.ForeColor = System.Drawing.SystemColors.Info;
            this.chkCopyAllAtFirst.Location = new System.Drawing.Point(244, 308);
            this.chkCopyAllAtFirst.Name = "chkCopyAllAtFirst";
            this.chkCopyAllAtFirst.Size = new System.Drawing.Size(209, 20);
            this.chkCopyAllAtFirst.TabIndex = 7;
            this.chkCopyAllAtFirst.Text = "Copy modified files at first time.";
            this.chkCopyAllAtFirst.UseVisualStyleBackColor = true;
            // 
            // chkShowNextTime
            // 
            this.chkShowNextTime.AutoSize = true;
            this.chkShowNextTime.ForeColor = System.Drawing.SystemColors.Info;
            this.chkShowNextTime.Location = new System.Drawing.Point(15, 357);
            this.chkShowNextTime.Name = "chkShowNextTime";
            this.chkShowNextTime.Size = new System.Drawing.Size(172, 20);
            this.chkShowNextTime.TabIndex = 8;
            this.chkShowNextTime.Text = "Do not show at next time.";
            this.chkShowNextTime.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(446, 350);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(79, 32);
            this.btnClose.TabIndex = 10;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSet
            // 
            this.btnSet.Location = new System.Drawing.Point(359, 350);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(79, 32);
            this.btnSet.TabIndex = 9;
            this.btnSet.Text = "Set";
            this.btnSet.UseVisualStyleBackColor = true;
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // frmConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.ClientSize = new System.Drawing.Size(535, 404);
            this.Controls.Add(this.btnSet);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.chkShowNextTime);
            this.Controls.Add(this.chkCopyAllAtFirst);
            this.Controls.Add(this.dateScheduleTime);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnBrowseDst);
            this.Controls.Add(this.btnBrowseSrc);
            this.Controls.Add(this.txtDstRootPath);
            this.Controls.Add(this.txtSrcRootPath);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuration";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmConfig_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSrcRootPath;
        private System.Windows.Forms.TextBox txtDstRootPath;
        private System.Windows.Forms.Button btnBrowseSrc;
        private System.Windows.Forms.Button btnBrowseDst;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtServerPort;
        private System.Windows.Forms.TextBox txtServerURL;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtServerPassword;
        private System.Windows.Forms.TextBox txtServerUserName;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblSep;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dateScheduleTime;
        private System.Windows.Forms.CheckBox chkCopyAllAtFirst;
        private System.Windows.Forms.CheckBox chkShowNextTime;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSet;
        private System.Windows.Forms.CheckBox chkPassive;
        private System.Windows.Forms.RadioButton rdoFTP;
        private System.Windows.Forms.RadioButton rdoWeb;
    }
}

