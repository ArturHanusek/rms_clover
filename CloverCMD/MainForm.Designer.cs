namespace CloverRMS
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.labelCompanySerial = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.panelCenterWindow = new System.Windows.Forms.Panel();
            this.buttonManualCard = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelTotal = new System.Windows.Forms.Label();
            this.labelStatus = new System.Windows.Forms.Label();
            this.lblBuildDate = new System.Windows.Forms.Label();
            this.panelCenterWindow.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxLog
            // 
            this.textBoxLog.Dock = System.Windows.Forms.DockStyle.Left;
            this.textBoxLog.Location = new System.Drawing.Point(0, 0);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.Size = new System.Drawing.Size(627, 684);
            this.textBoxLog.TabIndex = 1;
            this.textBoxLog.Visible = false;
            // 
            // labelCompanySerial
            // 
            this.labelCompanySerial.AutoSize = true;
            this.labelCompanySerial.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelCompanySerial.ForeColor = System.Drawing.Color.White;
            this.labelCompanySerial.Location = new System.Drawing.Point(627, 0);
            this.labelCompanySerial.Name = "labelCompanySerial";
            this.labelCompanySerial.Size = new System.Drawing.Size(170, 13);
            this.labelCompanySerial.TabIndex = 6;
            this.labelCompanySerial.Text = "Company (serial no) MID: XYZXYZ";
            this.labelCompanySerial.Click += new System.EventHandler(this.Click_labelCompanySerial);
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 500;
            this.timer.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // panelCenterWindow
            // 
            this.panelCenterWindow.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panelCenterWindow.Controls.Add(this.buttonManualCard);
            this.panelCenterWindow.Controls.Add(this.buttonCancel);
            this.panelCenterWindow.Controls.Add(this.labelTotal);
            this.panelCenterWindow.Controls.Add(this.labelStatus);
            this.panelCenterWindow.Location = new System.Drawing.Point(332, 281);
            this.panelCenterWindow.Name = "panelCenterWindow";
            this.panelCenterWindow.Size = new System.Drawing.Size(521, 272);
            this.panelCenterWindow.TabIndex = 10;
            // 
            // buttonManualCard
            // 
            this.buttonManualCard.AutoSize = true;
            this.buttonManualCard.BackColor = System.Drawing.Color.LightGray;
            this.buttonManualCard.Enabled = false;
            this.buttonManualCard.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonManualCard.Location = new System.Drawing.Point(0, 163);
            this.buttonManualCard.Margin = new System.Windows.Forms.Padding(0);
            this.buttonManualCard.Name = "buttonManualCard";
            this.buttonManualCard.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.buttonManualCard.Size = new System.Drawing.Size(521, 62);
            this.buttonManualCard.TabIndex = 16;
            this.buttonManualCard.Text = "Manual Card Entry";
            this.buttonManualCard.UseVisualStyleBackColor = false;
            this.buttonManualCard.Click += new System.EventHandler(this.ButtonManualCard_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.AutoSize = true;
            this.buttonCancel.BackColor = System.Drawing.Color.LightGray;
            this.buttonCancel.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCancel.Location = new System.Drawing.Point(0, 101);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(0);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.buttonCancel.Size = new System.Drawing.Size(521, 62);
            this.buttonCancel.TabIndex = 15;
            this.buttonCancel.Text = "CANCEL";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // labelTotal
            // 
            this.labelTotal.BackColor = System.Drawing.Color.White;
            this.labelTotal.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTotal.Location = new System.Drawing.Point(0, 51);
            this.labelTotal.Margin = new System.Windows.Forms.Padding(10);
            this.labelTotal.Name = "labelTotal";
            this.labelTotal.Size = new System.Drawing.Size(521, 50);
            this.labelTotal.TabIndex = 14;
            this.labelTotal.Text = "0.00";
            this.labelTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelStatus
            // 
            this.labelStatus.BackColor = System.Drawing.Color.White;
            this.labelStatus.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStatus.Location = new System.Drawing.Point(0, 0);
            this.labelStatus.Margin = new System.Windows.Forms.Padding(15);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Padding = new System.Windows.Forms.Padding(10);
            this.labelStatus.Size = new System.Drawing.Size(521, 51);
            this.labelStatus.TabIndex = 12;
            this.labelStatus.Text = "Card Payment";
            // 
            // lblBuildDate
            // 
            this.lblBuildDate.AutoSize = true;
            this.lblBuildDate.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblBuildDate.ForeColor = System.Drawing.Color.White;
            this.lblBuildDate.Location = new System.Drawing.Point(627, 671);
            this.lblBuildDate.Name = "lblBuildDate";
            this.lblBuildDate.Size = new System.Drawing.Size(91, 13);
            this.lblBuildDate.TabIndex = 11;
            this.lblBuildDate.Text = "Build 2020/02/21";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(1167, 684);
            this.Controls.Add(this.lblBuildDate);
            this.Controls.Add(this.panelCenterWindow);
            this.Controls.Add(this.labelCompanySerial);
            this.Controls.Add(this.textBoxLog);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Clover - RMS";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panelCenterWindow.ResumeLayout(false);
            this.panelCenterWindow.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBoxLog;
        private System.Windows.Forms.Label labelCompanySerial;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Panel panelCenterWindow;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelTotal;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Button buttonManualCard;
        private System.Windows.Forms.Label lblBuildDate;
    }
}

