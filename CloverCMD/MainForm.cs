using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using com.clover.remotepay.sdk;
using com.clover.remotepay.transport;
using com.clover.sdk.v3.payments;
using System.Threading;
using System.IO;

namespace CloverRMS
{
    public partial class MainForm : Form, ICloverConnectorListener
    {
        private Clover Clover;

        public SynchronizationContext uiThread;

        private readonly string filename;
        private readonly string receiptFilename = $"C:\\CloverCMD\\LastSaleReceipt.txt";
        private int secondsInactive = 0;

        private readonly StreamWriter logFile;

        public delegate void DoWhenCloverIDLEMethod(RetrieveDeviceStatusResponse response);
        public DoWhenCloverIDLEMethod OnRetrieveDeviceStatusResponse_IDLE;

        public MainForm()
        {
            uiThread = SynchronizationContext.Current;

            this.filename = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\Your IT Solutions\\Your IT - Clover Integration\\Logs\\{DateTime.Now.ToString("yyyy_MM_dd")}_CloverCMD.log";
            this.logFile = new StreamWriter(this.filename, true)
            {
                AutoFlush = true
            };

            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
        }

        public void AddCloverButtons(InputOption[] inputOptionsArray)
        {
            try
            {
                foreach (Button button in CloverButtonsPanel.Controls.OfType<Button>())
                {
                    CloverButtonsPanel.Controls.Remove(button);
                }

                foreach (InputOption inputOption in inputOptionsArray)
                {
                    Button newButton = new Button();
                    newButton.Click += (s, e) =>
                    {
                        Log("cloverInputOption_Button.Clicked(" + inputOption.description +")");
                        Clover.CloverConnector.InvokeInputOption(inputOption);
                    };
                    newButton.Name = "cloverInputOption_Button" + Convert.ToString(CloverButtonsPanel.Controls.Count);
                    newButton.Text = inputOption.description.ToUpper();
                    newButton.FlatStyle = FlatStyle.Flat;
                    newButton.ForeColor = Color.White;
                    newButton.BackColor = Color.Transparent;
                    newButton.FlatAppearance.MouseDownBackColor = Color.Gray;
                    newButton.FlatAppearance.MouseOverBackColor = Color.DarkGray;
                    newButton.Font = new Font("Microsoft Sans Serif", 13, FontStyle.Bold);
                    newButton.Dock = DockStyle.Top;
                    newButton.Height = 60;
                    newButton.Width = 120;

                    CloverButtonsPanel.Controls.Add(newButton);
                }
            }
            catch (Exception exception)
            {
                Log(exception.Message);
            }
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            SetStatus("");
            this.Log("");
            this.Log("Release 2023.08.07");
            this.Log("Clover SDK: 4.0.6");

            InputOption[] empty = { };

            AddCloverButtons(empty);

            OnRetrieveDeviceStatusResponse_IDLE = ProcessTrasactionWhenIDLE;
            autoConnectCloverTimer.Enabled = true;
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (Clover != null)
                {
                    Clover.Dispose();
                }
            }
            catch (Exception exception)
            {
                Log("UI." + System.Reflection.MethodBase.GetCurrentMethod().Name + ".Exception() " + exception.Message);
                //throw;
            }

            Log("UI." + System.Reflection.MethodBase.GetCurrentMethod().Name + "()");
        }

        public void Log(String Text)
        {
            uiThread.Send(delegate (object state)
            {
                this.textBoxLog.Text = (Text + Environment.NewLine + this.textBoxLog.Text);
                this.WriteToFile(Text);
            }, null);
        }

        public void WriteToFile(string logMessage)
        {
            string formattedMessage = $"{System.Environment.NewLine}{DateTime.Now.ToLongTimeString()} {logMessage}";

            this.logFile.Write(formattedMessage);
        }

        private void LoadTransactionAmount()
        {
            Log("UI." + System.Reflection.MethodBase.GetCurrentMethod().Name + "()");
            string[] args = Environment.GetCommandLineArgs();

            if (args.Length == 1)
            {
                Log("Amount is not specified");
                SetStatus("Amount is not specified");
                return;
            }

            if (args.Length == 2)
            {
                Log("Transaction GUID is not specified");
                SetStatus("Transaction GUID is not specified");
                return;
            }

            if (Int32.TryParse(args[1], out int amount) == false)
            {
                SetStatus("Incorrect tender value. Expecting amount in cents");
                return;
            };

            Clover.SetTransactionGuid(args[2], amount);

            if (Clover._amount < 0)
            {
                cloverStatusPanel.BackColor = Color.Red;
            }

            this.labelTotal.Text = string.Format("{0:#.00}", Convert.ToDouble(amount) / 100);
        }

        public void SetStatus(String Text)
        {
            //Log("Status: " + Text);

            uiThread.Send(delegate (object state)
            {
                this.labelStatus.Text = Text;
            }, null);

        }

        public void ShowError(String Title, String Msg)
        {
            uiThread.Send(delegate (object state)
            {
                MessageBox.Show(Msg, Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }, null);
        }

        public void ShowErrorAndExit(String Title, String Msg, int ExitCode)
        {
            ShowError(Title, Msg);
            Environment.Exit(ExitCode);
            //Program.ExitFailed(ExitCode);
        }

        void ProcessTransactionCNP()
        {
            Log("UI." + System.Reflection.MethodBase.GetCurrentMethod().Name + "()");
            Clover.DoOnSaleResponse = OnSaleResponseMethod;
            Clover.DoOnManualRefundResponse = OnManualRefundResponseMethod;

            Clover.ProcessTransactionCNP();
        }

        void ProcessManualCardEntryTransaction(SaleResponse response)
        {
            Log("UI." + System.Reflection.MethodBase.GetCurrentMethod().Name + "()");
            this.ProcessTransactionCNP();
        }

        void ProcessManualCardEntryRefund(ManualRefundResponse response)
        {
            Log("UI." + System.Reflection.MethodBase.GetCurrentMethod().Name + "()");
            this.ProcessTransactionCNP();
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            
        }

        public void SaveToFile(SaleResponse response)
        {
            try
            {
                var receiptFile = new StreamWriter(receiptFilename, false)
                {
                    AutoFlush = true
                };

                receiptFile.Write($"CARD:  { response.Payment.cardTransaction.cardType} {response.Payment.cardTransaction.last4} AUTH: {response.Payment.cardTransaction.authCode} ID: {response.Payment.id}");
            }
            catch (Exception exception)
            {
                Log("Exception " + exception.Message);
            }
        }

        public void SaveManualRefundResponseToFile(ManualRefundResponse response)
        {
            var receiptFile = new StreamWriter(receiptFilename, false)
            {
                AutoFlush = true
            };

            receiptFile.Write($"CARD:  { response.Credit.cardTransaction.cardType} {response.Credit.cardTransaction.last4} AUTH: {response.Credit.cardTransaction.authCode} ID: {response.Credit.id}");
        }

        private bool MessageBoxConfirmed(string question)
        {
            Log("MessageBox: " + question);
            var dialogResult = MessageBox.Show(this, question, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            Log("Clicked: " + dialogResult.ToString());
            return dialogResult == DialogResult.Yes;
        }

        public void OnSaleResponseMethod(SaleResponse response)
        {
            try
            {
                Log("UI." + System.Reflection.MethodBase.GetCurrentMethod().Name + "; Response: Reason: " + response.Reason + "; Message: " + response.Message + "; Result: " + response.Result);

                if (response.Payment is object)
                {
                    Log("UI." + System.Reflection.MethodBase.GetCurrentMethod().Name + "; Payment.id: " + response.Payment.id);
                    Log("UI." + System.Reflection.MethodBase.GetCurrentMethod().Name + "; Payment.externalPaymentId: " + response.Payment.externalPaymentId);
                    Log("UI." + System.Reflection.MethodBase.GetCurrentMethod().Name + "; Payment.amount:" + response.Payment.amount);

                    if (Clover._guid != response.Payment.externalPaymentId)
                    {
                        Log("Payment ID does not transaction GUID, transaction fails");
                        Program.ExitFailed(1000 + Convert.ToInt32(response.Result));
                    }
                }

                switch (response.Result)
                {
                    case ResponseCode.SUCCESS:
                        SaveToFile(response);
                        Program.ExitSuccess();
                        break;

                    default:
                        Program.ExitFailed(1000 + Convert.ToInt32(response.Result));
                        break;
                }
            }
            catch (Exception exception)
            {
                Log("Exception " + exception.Message);
            }
        }

        public void OnManualRefundResponseMethod(ManualRefundResponse response)
        {
            try
            {
                Log("UI." + System.Reflection.MethodBase.GetCurrentMethod().Name + "; Response: Reason: " + response.Reason + "; Message: " + response.Message + "; Result: " + response.Result);
           
                if (response.Credit is object)
                {
                    Log("UI." + System.Reflection.MethodBase.GetCurrentMethod().Name + "; Payment.id: " + response.Credit.id);
                    Log("UI." + System.Reflection.MethodBase.GetCurrentMethod().Name + "; Payment.externalReferenceId: " + response.Credit.externalReferenceId);
                    Log("UI." + System.Reflection.MethodBase.GetCurrentMethod().Name + "; Payment.amount:" + response.Credit.amount);

                    if (Clover._guid != response.Credit.externalReferenceId)
                    {
                        Log("Payment ID does not transaction GUID, transaction fails");
                        Program.ExitFailed(1000 + Convert.ToInt32(response.Result));
                    }
                }

                switch (response.Result)
                {
                    case ResponseCode.SUCCESS:
                        SaveManualRefundResponseToFile(response);
                        Program.ExitSuccess();
                        break;

                    default:
                        Program.ExitFailed(1000 + Convert.ToInt32(response.Result));
                        break;
                }
            }
            catch (Exception exception)
            {
                Log("Exception " + exception.Message);
            }
        }

        public void OnDeviceActivityStart(CloverDeviceEvent deviceEvent)
        {
            Log("UI." + System.Reflection.MethodBase.GetCurrentMethod().Name + "() MessaOge: " + deviceEvent.Message + " Code: " + deviceEvent.Code + " EventState: " + deviceEvent.EventState);

            SetStatus(deviceEvent.Message);

            secondsInactive = 0;

            ManualCardEntryButton.Enabled = deviceEvent.EventState == CloverDeviceEvent.DeviceEventState.START;
            this.BeginInvoke((Action)(() =>
            {
                AddCloverButtons(deviceEvent.InputOptions);
            }));

            if (deviceEvent.EventState == CloverDeviceEvent.DeviceEventState.RECEIPT_OPTIONS)
            {
                if (!MessageBoxConfirmed("Print receipt?"))
                {
                    Clover.InvokeESC();
                }
                else
                {
                    MessageBox.Show(this, "Please select receipt destination on device", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        public void OnDeviceActivityEnd(CloverDeviceEvent deviceEvent) { }

        public void OnDeviceError(CloverDeviceErrorEvent deviceErrorEvent)
        {
            Log("Clover." + System.Reflection.MethodBase.GetCurrentMethod().Name + ": Code: " + deviceErrorEvent.Code + "; Message: " + deviceErrorEvent.Message + "; Cause: " + deviceErrorEvent.Cause.Message);
            SetStatus(deviceErrorEvent.Message);
        }

        public void OnPreAuthResponse(PreAuthResponse response) { }
        public void OnAuthResponse(AuthResponse response) { }
        public void OnTipAdjustAuthResponse(TipAdjustAuthResponse response) { }
        public void OnCapturePreAuthResponse(CapturePreAuthResponse response) { }
        public void OnIncrementPreAuthResponse(IncrementPreAuthResponse response) { }
        public void OnVerifySignatureRequest(VerifySignatureRequest request)
        {
            Log("UI." + System.Reflection.MethodBase.GetCurrentMethod().Name + "()");

            if (MessageBoxConfirmed("Is signature correct?"))
            {
                request.Accept();
            }

            request.Reject();
        }

        public void OnConfirmPaymentRequest(ConfirmPaymentRequest request)
        {
            Log("UI." + System.Reflection.MethodBase.GetCurrentMethod().Name + "()");

            foreach (Challenge challenge in request.Challenges)
            {
                if (!MessageBoxConfirmed(request.Challenges[0].message))
                {
                    Clover.CloverConnector.RejectPayment(request.Payment, challenge);
                }
            }

            Clover.CloverConnector.AcceptPayment(request.Payment);
        }

        public void OnCloseoutResponse(CloseoutResponse response) { }
        public void OnRefundPaymentResponse(RefundPaymentResponse response) { }
        public void OnVoidPaymentRefundResponse(VoidPaymentRefundResponse response) { }
        public void OnTipAdded(TipAddedMessage message) { }
        public void OnVoidPaymentResponse(VoidPaymentResponse response) { }

        public void OnDeviceConnected()
        {
            Log("UI." + System.Reflection.MethodBase.GetCurrentMethod().Name + "()");
            SetStatus("Device connected...");
        }

        public void OnDeviceReady(MerchantInfo merchantInfo)
        {
            Log("UI." + System.Reflection.MethodBase.GetCurrentMethod().Name + "()");

            SetStatus("Device ready...");

            MerchantIdLabel.Text = "MID: " + merchantInfo.merchantMId.ToString();
            CloverDeviceSerialNumberLabel.Text = "S/N: " + merchantInfo.Device.Serial;

            if (Clover.IsReady == false)
            {
                Clover.IsReady = true;
                Clover.SendRetrievePaymentRequest();
                Clover.RetrieveDeviceStatus();
            }
        }

        public void OnDeviceDisconnected()
        {
            Log("Clover." + System.Reflection.MethodBase.GetCurrentMethod().Name + "()");
            SetStatus("Device disconnected. Please make sure: \nYour USB cable is plugged into both devices \nUSB Pay Display is turned on on Clover device");

            Environment.Exit(999);
        }

        public void OnVaultCardResponse(VaultCardResponse response) { }
        public void OnRetrievePendingPaymentsResponse(RetrievePendingPaymentsResponse response) { }
        public void OnReadCardDataResponse(ReadCardDataResponse response) { }
        public void OnPrintManualRefundReceipt(PrintManualRefundReceiptMessage message) { }
        public void OnPrintManualRefundDeclineReceipt(PrintManualRefundDeclineReceiptMessage message) { }
        public void OnPrintPaymentReceipt(PrintPaymentReceiptMessage message) { }
        public void OnPrintPaymentDeclineReceipt(PrintPaymentDeclineReceiptMessage message) { }
        public void OnPrintPaymentMerchantCopyReceipt(PrintPaymentMerchantCopyReceiptMessage message) { }
        public void OnPrintRefundPaymentReceipt(PrintRefundPaymentReceiptMessage message) { }
        public void OnPrintJobStatusResponse(PrintJobStatusResponse response) { }
        public void OnRetrievePrintersResponse(RetrievePrintersResponse response) { }
        public void OnCustomActivityResponse(CustomActivityResponse response) { }

        public void ProcessTrasactionWhenIDLE(RetrieveDeviceStatusResponse response)
        {
            Log("UI." + System.Reflection.MethodBase.GetCurrentMethod().Name + "() State: " + response.State.ToString() + "; Data.CustomActivityId: " + response.Data.CustomActivityId + "; Data.ExternalPaymentId: " + response.Data.ExternalPaymentId);

            Clover.ProcessTransaction();
        }

        public void OnRetrieveDeviceStatusResponse(RetrieveDeviceStatusResponse response)
        {
            Log("UI." + System.Reflection.MethodBase.GetCurrentMethod().Name + "() State: " + response.State.ToString() + "; Data.CustomActivityId: " + response.Data.CustomActivityId + "; Data.ExternalPaymentId: " + response.Data.ExternalPaymentId);

            if (response.State == com.clover.remotepay.sdk.ExternalDeviceState.IDLE)
            {
                if (OnRetrieveDeviceStatusResponse_IDLE != null)
                {
                    OnRetrieveDeviceStatusResponse_IDLE(response);
                    OnRetrieveDeviceStatusResponse_IDLE = null;
                }
            }
        }

        public void OnMessageFromActivity(MessageFromActivity response) { }
        public void OnResetDeviceResponse(ResetDeviceResponse response)
        {
            Log("Clover." + System.Reflection.MethodBase.GetCurrentMethod().Name + "() " + "Message: " + response.Message + " Reason: " + response.Reason + " Result: " + response.Result.ToString() + " State: " + response.State.ToString() + " Success: " + response.Success.ToString());
        }

        public void OnRetrievePaymentResponse(RetrievePaymentResponse response)
        {
            Log("Clover." + System.Reflection.MethodBase.GetCurrentMethod().Name + "() Payment.result: " + response.Payment.result + "; Payment: " + response.Payment.externalPaymentId + "; Payment.amount: " + response.Payment.amount);

            if ((response.Payment.externalPaymentId == Clover._guid) && (response.Payment.amount == Clover._amount) && (response.Payment.result == Result.SUCCESS))
            {
                Program.ExitSuccess();
            }
        }

        public void OnPrintJobStatusRequest(PrintJobStatusRequest request) { }
        public void OnDisplayReceiptOptionsResponse(DisplayReceiptOptionsResponse response) { }
        public void OnInvalidStateTransitionResponse(InvalidStateTransitionNotification message) { }
        public void OnCustomerProvidedData(CustomerProvidedDataEvent response) { }
        public void OnSaleResponse(SaleResponse response)
        {
            Log("UI." + System.Reflection.MethodBase.GetCurrentMethod().Name + "()");
            Clover.DoOnSaleResponse(response);
        }

        public void OnManualRefundResponse(ManualRefundResponse response)
        {
            Log("UI." + System.Reflection.MethodBase.GetCurrentMethod().Name + "()");
            Clover.DoOnManualRefundResponse(response);
        }

        private void ProcessTransactionButton_Click(object sender, EventArgs e)
        {
            Log("UI." + System.Reflection.MethodBase.GetCurrentMethod().Name + "()");
            Clover.ProcessTransaction();
        }

        private void ButtonRetrieveTransaction_Click(object sender, EventArgs e)
        {
            Log("UI." + System.Reflection.MethodBase.GetCurrentMethod().Name + "()");
            Clover.SendRetrievePaymentRequest();
            SetStatus("Retrieve payment request sent...");
        }

        private void ResetDeviceButton_Click(object sender, EventArgs e)
        {
            Log("UI." + System.Reflection.MethodBase.GetCurrentMethod().Name + "()");
            SetStatus("Sending Reset Device request");
            Clover.ResetDevice();
        }

        private void OnScreenEscapeButton_Click(object sender, EventArgs e)
        {
            Log("UI." + System.Reflection.MethodBase.GetCurrentMethod().Name + "()");
            Clover.SimulateClick(com.clover.remotepay.transport.KeyPress.ESC);
        }

        private void ConnectCloverButton_Click(object sender, EventArgs e)
        {
            Log("UI." + System.Reflection.MethodBase.GetCurrentMethod().Name + "()");

            try
            {
                Log("Closing current connection...");
                SetStatus("Closing current connection...");
                Clover.Dispose();
            } 
            catch (Exception exception)
            {
                Log("Exception " + exception.Message);
            }
            finally
            {
                Clover = null;
            }


            Log("Creating new connection...");
            SetStatus("Creating new connection...");
            Clover = new Clover(this)
            {
                DoOnSaleResponse = OnSaleResponseMethod,
                DoOnManualRefundResponse = OnManualRefundResponseMethod
            };
        }

        private void CloseWindowButton_Click(object sender, EventArgs e)
        {
            Log("UI." + System.Reflection.MethodBase.GetCurrentMethod().Name + "()");

            if (MessageBoxConfirmed("Are you sure?")) 
            {
                if (MessageBoxConfirmed("You FORCE CLOSING the program and entering manual mode. Your responsibility is to make sure that appropriate actions are taken on Clover device and transaction is charged correctly"))
                {
                    string message = "If payment already went trough on the Clover device, you can use 'Retrieve Payment' button once connection with the device is fixed";
                    Log("MessageBox: " + message);
                    MessageBox.Show(this, message, "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    Log("Clicked: OK");

                    Log("FORCE CLOSE");
                    Environment.Exit(999);
                }
            }
        }

        private void SetConnectingLabelStatusTimer_Tick(object sender, EventArgs e)
        {
            // this is purely for visual efects, delayed false first status for impresion that works quicker
            // white lie but pleasure creations
            setConnectingLabelStatusTimer.Enabled = false;
            SetStatus("Please make sure your USB cable is plugged into both devices and USB Pay Display is turn on on Clover device");
        }

        private void MerchantStatusLabel_Click(object sender, EventArgs e)
        {
            Log("UI." + System.Reflection.MethodBase.GetCurrentMethod().Name + "()");
            panelButtonsRight.Visible = !panelButtonsRight.Visible;
        }

        private void ManualCardEntryButton_Click(object sender, EventArgs e)
        {
            Log("UI." + System.Reflection.MethodBase.GetCurrentMethod().Name + "()");
            SetStatus("Entering manual payment mode...");

            Clover.DoOnSaleResponse = ProcessManualCardEntryTransaction;
            Clover.DoOnManualRefundResponse = ProcessManualCardEntryRefund;
            Clover.InvokeESC();
        }

        private void ShowHideLogsButton_Click(object sender, EventArgs e)
        {
            Log("UI." + System.Reflection.MethodBase.GetCurrentMethod().Name + "()");
            textBoxLog.Visible = !textBoxLog.Visible;
        }

        private void TextBoxLog_TextChanged(object sender, EventArgs e)
        {

        }

        private void LabelStatus_Click(object sender, EventArgs e)
        {

        }

        private void TimerTimeLableUpdate_Tick(object sender, EventArgs e)
        {

        }

        private void AutoConnectCloverTimer_Tick(object sender, EventArgs e)
        {
            autoConnectCloverTimer.Enabled = false;

            if (Clover == null)
            {
                Clover = new Clover(this)
                {
                    DoOnSaleResponse = OnSaleResponseMethod,
                    DoOnManualRefundResponse = OnManualRefundResponseMethod
                };

                LoadTransactionAmount();
            } 
        }

        private void forceCloseButtonActivateTimer_Tick(object sender, EventArgs e)
        {
            secondsInactive += 1;

            int activationSecondsLeft = 30 - secondsInactive;
            
            if (activationSecondsLeft % 10 == 0)
            {
                Log("UI: Sending RetrieveDeviceStatus request");
                Clover.RetrieveDeviceStatus();
            }

            if (activationSecondsLeft < 0)
            {
                ForceCloseButton.Text = "FORCE CLOSE";
                ForceCloseButton.Enabled = true;
                panelButtonsRight.Visible = true;
                return;
            }

            ForceCloseButton.Text = "FORCE CLOSE (" + activationSecondsLeft.ToString() + ")";
            ForceCloseButton.Enabled = false;
        }
    }
}
