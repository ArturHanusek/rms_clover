using System;

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
            this.panelCenterWindow = new System.Windows.Forms.Panel();
            this.cloverStatusPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.labelStatus = new System.Windows.Forms.Label();
            this.labelTotal = new System.Windows.Forms.Label();
            this.CloverButtonsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.setConnectingLabelStatusTimer = new System.Windows.Forms.Timer(this.components);
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelButtonsRight = new System.Windows.Forms.Panel();
            this.CloverDeviceSerialNumberLabel = new System.Windows.Forms.Label();
            this.MerchantIdLabel = new System.Windows.Forms.Label();
            this.ManualCardEntryButton = new System.Windows.Forms.Button();
            this.showHideLogsButton = new System.Windows.Forms.Button();
            this.RetrievePaymentButton = new System.Windows.Forms.Button();
            this.ResetDeviceButton = new System.Windows.Forms.Button();
            this.ConnectCloverButton = new System.Windows.Forms.Button();
            this.ForceCloseButton = new System.Windows.Forms.Button();
            this.menuButton = new System.Windows.Forms.Button();
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.autoConnectCloverTimer = new System.Windows.Forms.Timer(this.components);
            this.forceCloseButtonActivateTimer = new System.Windows.Forms.Timer(this.components);
            this.panelCenterWindow.SuspendLayout();
            this.cloverStatusPanel.SuspendLayout();
            this.CloverButtonsPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panelButtonsRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelCenterWindow
            // 
            this.panelCenterWindow.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panelCenterWindow.Controls.Add(this.cloverStatusPanel);
            this.panelCenterWindow.Controls.Add(this.CloverButtonsPanel);
            this.panelCenterWindow.Location = new System.Drawing.Point(575, 143);
            this.panelCenterWindow.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelCenterWindow.Name = "panelCenterWindow";
            this.panelCenterWindow.Size = new System.Drawing.Size(787, 326);
            this.panelCenterWindow.TabIndex = 10;
            // 
            // cloverStatusPanel
            // 
            this.cloverStatusPanel.BackColor = System.Drawing.Color.White;
            this.cloverStatusPanel.Controls.Add(this.label1);
            this.cloverStatusPanel.Controls.Add(this.labelStatus);
            this.cloverStatusPanel.Controls.Add(this.labelTotal);
            this.cloverStatusPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cloverStatusPanel.Location = new System.Drawing.Point(0, 0);
            this.cloverStatusPanel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cloverStatusPanel.Name = "cloverStatusPanel";
            this.cloverStatusPanel.Size = new System.Drawing.Size(620, 326);
            this.cloverStatusPanel.TabIndex = 25;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("Arial", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 241);
            this.label1.Margin = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(361, 58);
            this.label1.TabIndex = 19;
            this.label1.Text = "REFUND";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelStatus
            // 
            this.labelStatus.BackColor = System.Drawing.Color.White;
            this.labelStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStatus.ForeColor = System.Drawing.Color.Black;
            this.labelStatus.Location = new System.Drawing.Point(16, 9);
            this.labelStatus.Margin = new System.Windows.Forms.Padding(20, 18, 20, 18);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Padding = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.labelStatus.Size = new System.Drawing.Size(581, 225);
            this.labelStatus.TabIndex = 18;
            this.labelStatus.Text = "Customer is selecting payment...";
            // 
            // labelTotal
            // 
            this.labelTotal.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelTotal.BackColor = System.Drawing.Color.White;
            this.labelTotal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTotal.ForeColor = System.Drawing.Color.Black;
            this.labelTotal.Location = new System.Drawing.Point(381, 241);
            this.labelTotal.Margin = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.labelTotal.Name = "labelTotal";
            this.labelTotal.Size = new System.Drawing.Size(215, 58);
            this.labelTotal.TabIndex = 15;
            this.labelTotal.Text = "17.54";
            this.labelTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CloverButtonsPanel
            // 
            this.CloverButtonsPanel.Controls.Add(this.button1);
            this.CloverButtonsPanel.Controls.Add(this.button2);
            this.CloverButtonsPanel.Controls.Add(this.button3);
            this.CloverButtonsPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.CloverButtonsPanel.Location = new System.Drawing.Point(620, 0);
            this.CloverButtonsPanel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.CloverButtonsPanel.Name = "CloverButtonsPanel";
            this.CloverButtonsPanel.Size = new System.Drawing.Size(167, 326);
            this.CloverButtonsPanel.TabIndex = 23;
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.Window;
            this.button1.Location = new System.Drawing.Point(4, 4);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(160, 74);
            this.button1.TabIndex = 3;
            this.button1.Text = "CANCEL";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            // 
            // button2
            // 
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.SystemColors.Window;
            this.button2.Location = new System.Drawing.Point(4, 86);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(160, 74);
            this.button2.TabIndex = 4;
            this.button2.Text = "CLEAR";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            // 
            // button3
            // 
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.SystemColors.Window;
            this.button3.Location = new System.Drawing.Point(4, 168);
            this.button3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(160, 74);
            this.button3.TabIndex = 5;
            this.button3.Text = "DONE";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            // 
            // setConnectingLabelStatusTimer
            // 
            this.setConnectingLabelStatusTimer.Enabled = true;
            this.setConnectingLabelStatusTimer.Interval = 500;
            this.setConnectingLabelStatusTimer.Tick += new System.EventHandler(this.SetConnectingLabelStatusTimer_Tick);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(13, 12);
            this.splitter1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(4, 818);
            this.splitter1.TabIndex = 19;
            this.splitter1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panelButtonsRight);
            this.panel1.Controls.Add(this.menuButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(1539, 12);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 818);
            this.panel1.TabIndex = 20;
            // 
            // panelButtonsRight
            // 
            this.panelButtonsRight.Controls.Add(this.CloverDeviceSerialNumberLabel);
            this.panelButtonsRight.Controls.Add(this.MerchantIdLabel);
            this.panelButtonsRight.Controls.Add(this.ManualCardEntryButton);
            this.panelButtonsRight.Controls.Add(this.showHideLogsButton);
            this.panelButtonsRight.Controls.Add(this.RetrievePaymentButton);
            this.panelButtonsRight.Controls.Add(this.ResetDeviceButton);
            this.panelButtonsRight.Controls.Add(this.ConnectCloverButton);
            this.panelButtonsRight.Controls.Add(this.ForceCloseButton);
            this.panelButtonsRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelButtonsRight.Location = new System.Drawing.Point(0, 37);
            this.panelButtonsRight.Margin = new System.Windows.Forms.Padding(4, 0, 4, 4);
            this.panelButtonsRight.Name = "panelButtonsRight";
            this.panelButtonsRight.Size = new System.Drawing.Size(200, 781);
            this.panelButtonsRight.TabIndex = 27;
            this.panelButtonsRight.Visible = false;
            // 
            // CloverDeviceSerialNumberLabel
            // 
            this.CloverDeviceSerialNumberLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.CloverDeviceSerialNumberLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloverDeviceSerialNumberLabel.ForeColor = System.Drawing.Color.White;
            this.CloverDeviceSerialNumberLabel.Location = new System.Drawing.Point(0, 713);
            this.CloverDeviceSerialNumberLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.CloverDeviceSerialNumberLabel.Name = "CloverDeviceSerialNumberLabel";
            this.CloverDeviceSerialNumberLabel.Size = new System.Drawing.Size(200, 31);
            this.CloverDeviceSerialNumberLabel.TabIndex = 41;
            this.CloverDeviceSerialNumberLabel.Text = "S/N: QC223612216333";
            this.CloverDeviceSerialNumberLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MerchantIdLabel
            // 
            this.MerchantIdLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MerchantIdLabel.ForeColor = System.Drawing.Color.White;
            this.MerchantIdLabel.Location = new System.Drawing.Point(4, 4);
            this.MerchantIdLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.MerchantIdLabel.Name = "MerchantIdLabel";
            this.MerchantIdLabel.Size = new System.Drawing.Size(229, 31);
            this.MerchantIdLabel.TabIndex = 40;
            this.MerchantIdLabel.Text = "MID: 928388321";
            this.MerchantIdLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ManualCardEntryButton
            // 
            this.ManualCardEntryButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ManualCardEntryButton.ForeColor = System.Drawing.Color.White;
            this.ManualCardEntryButton.Location = new System.Drawing.Point(0, 62);
            this.ManualCardEntryButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ManualCardEntryButton.Name = "ManualCardEntryButton";
            this.ManualCardEntryButton.Size = new System.Drawing.Size(187, 37);
            this.ManualCardEntryButton.TabIndex = 38;
            this.ManualCardEntryButton.Text = "Manual Card Entry";
            this.ManualCardEntryButton.UseVisualStyleBackColor = true;
            this.ManualCardEntryButton.Click += new System.EventHandler(this.ManualCardEntryButton_Click);
            // 
            // showHideLogsButton
            // 
            this.showHideLogsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.showHideLogsButton.ForeColor = System.Drawing.Color.White;
            this.showHideLogsButton.Location = new System.Drawing.Point(0, 369);
            this.showHideLogsButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.showHideLogsButton.Name = "showHideLogsButton";
            this.showHideLogsButton.Size = new System.Drawing.Size(187, 37);
            this.showHideLogsButton.TabIndex = 36;
            this.showHideLogsButton.Text = "Live Logs";
            this.showHideLogsButton.UseVisualStyleBackColor = true;
            this.showHideLogsButton.Click += new System.EventHandler(this.ShowHideLogsButton_Click);
            // 
            // RetrievePaymentButton
            // 
            this.RetrievePaymentButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.RetrievePaymentButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RetrievePaymentButton.ForeColor = System.Drawing.Color.White;
            this.RetrievePaymentButton.Location = new System.Drawing.Point(0, 123);
            this.RetrievePaymentButton.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.RetrievePaymentButton.Name = "RetrievePaymentButton";
            this.RetrievePaymentButton.Size = new System.Drawing.Size(187, 37);
            this.RetrievePaymentButton.TabIndex = 31;
            this.RetrievePaymentButton.Text = "Retrieve Payment";
            this.RetrievePaymentButton.UseVisualStyleBackColor = true;
            this.RetrievePaymentButton.Click += new System.EventHandler(this.ButtonRetrieveTransaction_Click);
            // 
            // ResetDeviceButton
            // 
            this.ResetDeviceButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ResetDeviceButton.ForeColor = System.Drawing.Color.White;
            this.ResetDeviceButton.Location = new System.Drawing.Point(0, 308);
            this.ResetDeviceButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ResetDeviceButton.Name = "ResetDeviceButton";
            this.ResetDeviceButton.Size = new System.Drawing.Size(187, 37);
            this.ResetDeviceButton.TabIndex = 30;
            this.ResetDeviceButton.Text = "Reset Device";
            this.ResetDeviceButton.UseVisualStyleBackColor = true;
            this.ResetDeviceButton.Click += new System.EventHandler(this.ResetDeviceButton_Click);
            // 
            // ConnectCloverButton
            // 
            this.ConnectCloverButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ConnectCloverButton.ForeColor = System.Drawing.Color.White;
            this.ConnectCloverButton.Location = new System.Drawing.Point(0, 246);
            this.ConnectCloverButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ConnectCloverButton.Name = "ConnectCloverButton";
            this.ConnectCloverButton.Size = new System.Drawing.Size(187, 37);
            this.ConnectCloverButton.TabIndex = 29;
            this.ConnectCloverButton.Text = "Reconnect Clover";
            this.ConnectCloverButton.UseVisualStyleBackColor = true;
            this.ConnectCloverButton.Click += new System.EventHandler(this.ConnectCloverButton_Click);
            // 
            // ForceCloseButton
            // 
            this.ForceCloseButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ForceCloseButton.Enabled = false;
            this.ForceCloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ForceCloseButton.ForeColor = System.Drawing.Color.White;
            this.ForceCloseButton.Location = new System.Drawing.Point(0, 744);
            this.ForceCloseButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ForceCloseButton.Name = "ForceCloseButton";
            this.ForceCloseButton.Size = new System.Drawing.Size(200, 37);
            this.ForceCloseButton.TabIndex = 23;
            this.ForceCloseButton.Text = "FORCE CLOSE";
            this.ForceCloseButton.UseVisualStyleBackColor = true;
            this.ForceCloseButton.Click += new System.EventHandler(this.CloseWindowButton_Click);
            // 
            // menuButton
            // 
            this.menuButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.menuButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.menuButton.ForeColor = System.Drawing.Color.White;
            this.menuButton.Location = new System.Drawing.Point(0, 0);
            this.menuButton.Margin = new System.Windows.Forms.Padding(4, 0, 4, 4);
            this.menuButton.Name = "menuButton";
            this.menuButton.Size = new System.Drawing.Size(200, 37);
            this.menuButton.TabIndex = 26;
            this.menuButton.TabStop = false;
            this.menuButton.Text = "- - -";
            this.menuButton.UseVisualStyleBackColor = true;
            this.menuButton.Click += new System.EventHandler(this.MerchantStatusLabel_Click);
            // 
            // textBoxLog
            // 
            this.textBoxLog.BackColor = System.Drawing.Color.DimGray;
            this.textBoxLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxLog.ForeColor = System.Drawing.Color.White;
            this.textBoxLog.Location = new System.Drawing.Point(13, 12);
            this.textBoxLog.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.Size = new System.Drawing.Size(1726, 818);
            this.textBoxLog.TabIndex = 24;
            this.textBoxLog.Visible = false;
            this.textBoxLog.WordWrap = false;
            // 
            // autoConnectCloverTimer
            // 
            this.autoConnectCloverTimer.Tick += new System.EventHandler(this.AutoConnectCloverTimer_Tick);
            // 
            // forceCloseButtonActivateTimer
            // 
            this.forceCloseButtonActivateTimer.Enabled = true;
            this.forceCloseButtonActivateTimer.Interval = 1000;
            this.forceCloseButtonActivateTimer.Tag = "";
            this.forceCloseButtonActivateTimer.Tick += new System.EventHandler(this.forceCloseButtonActivateTimer_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(1752, 842);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panelCenterWindow);
            this.Controls.Add(this.textBoxLog);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Clover - RMS";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.panelCenterWindow.ResumeLayout(false);
            this.cloverStatusPanel.ResumeLayout(false);
            this.CloverButtonsPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panelButtonsRight.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion
        private System.Windows.Forms.Panel panelCenterWindow;
        private System.Windows.Forms.Timer setConnectingLabelStatusTimer;
        private System.Windows.Forms.Label labelTotal;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.FlowLayoutPanel CloverButtonsPanel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button menuButton;
        private System.Windows.Forms.Panel panelButtonsRight;
        private System.Windows.Forms.Button RetrievePaymentButton;
        private System.Windows.Forms.Button ResetDeviceButton;
        private System.Windows.Forms.Button ConnectCloverButton;
        private System.Windows.Forms.Button ForceCloseButton;
        private System.Windows.Forms.TextBox textBoxLog;
        private System.Windows.Forms.Button showHideLogsButton;
        private System.Windows.Forms.Button ManualCardEntryButton;
        private System.Windows.Forms.Label MerchantIdLabel;
        private System.Windows.Forms.Label CloverDeviceSerialNumberLabel;
        private System.Windows.Forms.Panel cloverStatusPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Timer autoConnectCloverTimer;
        private System.Windows.Forms.Timer forceCloseButtonActivateTimer;
    }
}

