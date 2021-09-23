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

namespace CloverRMS
{
    public partial class MainForm : Form
    {
        readonly Clover Clover;

        public SynchronizationContext uiThread;


        private bool isInitialized = false;
        private int centAmount = 0;




        public MainForm()
        {
            if (Clover == null)
            {
                try
                {
                    Clover = new Clover(this);
                }
                catch (Exception)
                {
                    throw new NotImplementedException();
                }
            }
            uiThread = SynchronizationContext.Current;

            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            FormBorderStyle     = FormBorderStyle.None;
            WindowState         = FormWindowState.Maximized;

            #if DEBUG
            textBoxLog.Visible  = true;
            #else
                Console.WriteLine("Mode=Release"); 
            #endif

            this.GetAmount();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Clover.AppShutdown();
        }



        private void GetAmount()
        {
            string[] args = Environment.GetCommandLineArgs();

            if (args.Length == 1)
            {
                SetStatus("Amount is not specified");
                return;
            }

            if (Int32.TryParse(args[1], out int amount) == false)
            {
                SetStatus("Incorrect tender value. Expecting amount in cents");
                return;
            };

            this.centAmount = amount;

            Clover.SetAmount(amount);

            this.labelTotal.Text = string.Format("{0:#.00}", Convert.ToDouble(amount) / 100);

        }

        public bool IsManualPayment() {

            string[] args = Environment.GetCommandLineArgs();

            if (args.Length > 2)
            {

                if (args[2].ToUpper() == "-M")
                {
                    return true;
                };

            }

            return false;

        }

        public void Log(String Text)
        {
            uiThread.Send(delegate (object state) {
                this.textBoxLog.Text = (Text + Environment.NewLine + this.textBoxLog.Text);
            }, null);
        }

        public void SetStatus(String Text)
        {
            this.Log("Status: " + Text);

            uiThread.Send(delegate (object state) {
                this.labelStatus.Text = Text;
            }, null);

        }

        public void ShowError(String Title, String Msg)
        {
            uiThread.Send(delegate (object state) {
                MessageBox.Show(Msg, Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }, null);
        }

        public void ShowErrorAndExit(String Title, String Msg, int ExitCode)
        {
            ShowError(Title, Msg);
            Program.ExitFailed(ExitCode);
        }

        public void SetConnectedStatusLabelText(String Text)
        {
            this.Log(Text);
            uiThread.Send(delegate (object state) {
                this.labelCompanySerial.Text = Text;
            }, null);
        }


        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (isInitialized == false)
            {
                Clover.InitializeConnector();
                isInitialized = true;
            } 
            else if (Clover.IsDeviceConnected())
            {
                timer.Enabled = false;

                if (centAmount > 0)
                {
                    SetStatus("Card Payment");
                    this.BackColor = Color.DimGray;
                } else if (centAmount < 0)
                {
                    SetStatus("Card Refund");
                    this.BackColor = Color.Red;
                } else
                {
                    SetStatus("Charge amount not specified");
                }

                Clover.DoOnSaleResponse = OnSaleResponse;
                Clover.DoOnManualRefundResponse = OnManualRefundResponse;

                Clover.ProcessTransaction(this.IsManualPayment());

                buttonManualCard.Enabled = true;

            } else
            {
                SetStatus("Connecting Clover...");
            }
        }

        void ProcessTransactionCNP()
        {
            Clover.DoOnSaleResponse = OnSaleResponse;

            Clover.DoOnManualRefundResponse = OnManualRefundResponse;

            Clover.ProcessTransactionCNP();
        }

        void ProcessManualCardEntryTransaction(SaleResponse response)
        {
            this.ProcessTransactionCNP();
        }

        void ProcessManualCardEntryRefund(ManualRefundResponse response)
        {
            this.ProcessTransactionCNP();
        }

        private void Click_labelCompanySerial(object sender, EventArgs e)
        {
            textBoxLog.Visible = !textBoxLog.Visible;
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            if (this.isInitialized)
            {
                if (Clover.IsDeviceConnected())
                {
                    Clover.CancelTransaction();
                }
            }

            Program.ExitFailed();
        }


        public void OnSaleResponse(SaleResponse response)
        {
            uiThread.Send(delegate (object state) {
                this.Log(System.Reflection.MethodBase.GetCurrentMethod().Name);
                
                switch(response.Result)
                {
                    case ResponseCode.SUCCESS:
                        Program.ExitSuccess();
                        break;

                    default:
                        ShowErrorAndExit(response.Reason, response.Message, Convert.ToInt32(response.Result));
                        break;
                }
             
            }, null);

        }

        public void OnManualRefundResponse(ManualRefundResponse response)
        {
            uiThread.Send(delegate (object state) {
                this.Log(System.Reflection.MethodBase.GetCurrentMethod().Name);

                switch (response.Result)
                {
                    case ResponseCode.SUCCESS:
                        Program.ExitSuccess();
                        break;

                    default:
                        ShowErrorAndExit(response.Reason, response.Message, Convert.ToInt32(response.Result));
                        break;
                }

            }, null);

        }

        private void ButtonManualCard_Click(object sender, EventArgs e)
        { 
            Clover.DoOnSaleResponse = ProcessManualCardEntryTransaction;

            Clover.DoOnManualRefundResponse = ProcessManualCardEntryRefund;

            Clover.CancelTransaction();

            buttonManualCard.Visible = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
