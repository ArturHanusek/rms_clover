using com.clover.remotepay.sdk;
using com.clover.remotepay.transport;
using System;
using System.Threading;
using System.Windows.Forms;

namespace CloverRMS
{
    public class Clover : ICloverConnectorListener
    {
        readonly MainForm Form;

        ICloverConnector cloverConnector;


        private int _amount = 0;

        bool Connected = false;


        public delegate void DoOnSaleResponseMethod(SaleResponse response);

        public DoOnSaleResponseMethod DoOnSaleResponse;



        public delegate void DoOnManualRefundResponseMethod(ManualRefundResponse response);

        public DoOnManualRefundResponseMethod DoOnManualRefundResponse;



        public Clover SetAmount(int amount)
        {
            this._amount = amount;
            return this;
        }

        public bool IsDeviceConnected()
        {
            return this.Connected;
        } 

        public void Log(String msg)
        {
            Form.Log(msg);
        }

        public Clover(MainForm ParentForm)
        {
            Form = ParentForm;
        }

        public ICloverConnector CloverConnector
        {
            get
            {
                return this.cloverConnector;
            }
            set
            {
                this.cloverConnector = value;
            }
        }

        public void InitializeConnector()
        {
            var usbConfiguration = new USBCloverDeviceConfiguration("CE0EE6PJ57C70.6MQBCNVGGSBEW", false);
            
            if (cloverConnector != null) 
            {

                cloverConnector.RemoveCloverConnectorListener(this);
                OnDeviceDisconnected(); // for any disabling, messaging, etc.
                cloverConnector.Dispose();

            }

            cloverConnector = CloverConnectorFactory.createICloverConnector(usbConfiguration);
            cloverConnector.AddCloverConnectorListener(this);
            cloverConnector.InitializeConnection();
        }

        public void Dispose()
        {
            if (cloverConnector != null)
            {
                try
                {
                    cloverConnector.ShowWelcomeScreen();
                    cloverConnector.RemoveCloverConnectorListener(this);
                    cloverConnector.ResetDevice();
                    OnDeviceDisconnected();
                    cloverConnector.Dispose();
                }
                catch (Exception)
                {
                    cloverConnector = null;
                }
            }
        }

        public void ProcessTransaction(bool isManualCardEntry)
        {
            this.Log("ProcessTransaction(" + this._amount.ToString() + ")");
            if (this._amount > 0)
            {
                this.Charge(this._amount, isManualCardEntry);
            }
            else if (this._amount < 0)
            {
                this.Refund(this._amount, isManualCardEntry);
            }
        }

        public void ProcessTransactionCNP()
        {
            this.Log("ProcessTransactionCNP()");
            this.ProcessTransaction(true);
        }


        private void Charge(int Amount, bool isManualCardEntry)
        {
            SaleRequest request = new SaleRequest()
            {
                ExternalId = ExternalIDUtil.GenerateRandomString(32),
                Amount = Amount
            };

            if (isManualCardEntry)
            {
                request.CardEntryMethods = (com.clover.remotepay.sdk.CloverConnector.CARD_ENTRY_METHOD_MANUAL);
            }

            request.DisableCashback = true;
            
            cloverConnector.Sale(request);
        }

        private void Refund(int Amount, bool isManualCardEntry)
        {
            ManualRefundRequest request = new ManualRefundRequest()
            {
                ExternalId = ExternalIDUtil.GenerateRandomString(32),
                Amount = -Amount
            };

            if (isManualCardEntry)
            {
                request.CardEntryMethods = (com.clover.remotepay.sdk.CloverConnector.CARD_ENTRY_METHOD_MANUAL);
            }

            cloverConnector.ManualRefund(request);
        }


        public void CancelTransaction()
        {
            this.Log("CancelTransaction()");

            InputOption PressEsc;
            PressEsc = new InputOption()
            {
                keyPress = com.clover.remotepay.transport.KeyPress.ESC
            };

            CloverConnector.InvokeInputOption(PressEsc);
        }

        public void AppShutdown()
        {
            if (cloverConnector != null)
            {
                try
                {
                    cloverConnector.RemoveCloverConnectorListener(this);
                    cloverConnector.ResetDevice();
                    OnDeviceDisconnected();
                    cloverConnector.Dispose();
                }
                catch (Exception)
                {
                    cloverConnector = null;
                }
            }
        }

        public void OnConfirmPaymentRequest(ConfirmPaymentRequest request)
        {
            this.Log(System.Reflection.MethodBase.GetCurrentMethod().Name);

            foreach (Challenge challenge in request.Challenges)
            {
                Form.uiThread.Send(delegate (object state) {

                    this.Log(request.Challenges[0].message);

                    var dialogResult = MessageBox.Show(Form, request.Challenges[0].message, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                    this.Log(dialogResult.ToString());

                    if (dialogResult == DialogResult.No)
                    {
                        cloverConnector.RejectPayment(request.Payment, challenge);
                    }

                }, null);
            }

            cloverConnector.AcceptPayment(request.Payment);
        }

        public void OnDeviceConnected()
        {
            this.Log(System.Reflection.MethodBase.GetCurrentMethod().Name);
            Form.SetConnectedStatusLabelText("Connecting...");
        }

        public void OnDeviceDisconnected()
        {
            this.Log(System.Reflection.MethodBase.GetCurrentMethod().Name);
            Form.SetConnectedStatusLabelText("Disconnected");
            Connected = false;
        }

        public void OnDeviceError(CloverDeviceErrorEvent deviceErrorEvent)
        {
            this.Log(System.Reflection.MethodBase.GetCurrentMethod().Name+
                ": "+
                deviceErrorEvent.Message+
                "(Code: "+
                deviceErrorEvent.ToString()+
                ")"
                );

            Form.ShowErrorAndExit("Device Error", deviceErrorEvent.Message, Convert.ToInt32(deviceErrorEvent.Code));
        }

        public void OnDeviceReady(MerchantInfo merchantInfo)
        {
            this.Log(System.Reflection.MethodBase.GetCurrentMethod().Name);

            Form.SetConnectedStatusLabelText("Ready! " + merchantInfo.merchantName + "(" + merchantInfo.Device.Serial + ") " + "MID: " + merchantInfo.merchantMId.ToString());
            
            Connected = true;
        }

        public void OnSaleResponse(SaleResponse response)
        {
            this.Log(System.Reflection.MethodBase.GetCurrentMethod().Name +
                ": " +
                " Message: "+response.Message +
                " Result: " +response.Result.ToString()
               );

            DoOnSaleResponse(response);
        }

        public void OnManualRefundResponse(ManualRefundResponse response)
        {
            this.Log(System.Reflection.MethodBase.GetCurrentMethod().Name +
                ": " +
                " Message: " + response.Message +
                " Result: " + response.Result.ToString()
               );

            DoOnManualRefundResponse(response);
        }

        public void OnVerifySignatureRequest(VerifySignatureRequest request)
        {
            this.Log(System.Reflection.MethodBase.GetCurrentMethod().Name);

            Form.uiThread.Send(delegate (object state) {
                this.Log(System.Reflection.MethodBase.GetCurrentMethod().Name);

                var dialogResult = MessageBox.Show(Form, "Is signature correct?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                this.Log(dialogResult.ToString());

                if (dialogResult == DialogResult.Yes)
                {
                    request.Accept();
                }
                else
                {
                    request.Reject();
                }

            }, null);
        }

        public void OnAuthResponse(AuthResponse response)
        {
            this.Log(System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

        public void OnCapturePreAuthResponse(CapturePreAuthResponse response)
        {
            this.Log(System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

        public void OnCloseoutResponse(CloseoutResponse response)
        {
            this.Log(System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

        public void OnCustomActivityResponse(CustomActivityResponse response)
        {
            this.Log(System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

        public void OnDeviceActivityEnd(CloverDeviceEvent deviceEvent)
        {
            //this.log(System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

        public void OnDeviceActivityStart(CloverDeviceEvent deviceEvent)
        {
            this.Log(System.Reflection.MethodBase.GetCurrentMethod().Name +
                " Message: "+deviceEvent.Message+
                " Code: "+deviceEvent.Code.ToString()
                );
            Form.SetStatus(deviceEvent.Message);
        }

        public void OnMessageFromActivity(MessageFromActivity response)
        {
            this.Log(System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

        public void OnPreAuthResponse(PreAuthResponse response)
        {
            this.Log(System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

        public void OnPrintJobStatusRequest(PrintJobStatusRequest request)
        {
            this.Log(System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

        public void OnPrintJobStatusResponse(PrintJobStatusResponse response)
        {
            this.Log(System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

        public void OnPrintManualRefundDeclineReceipt(PrintManualRefundDeclineReceiptMessage printManualRefundDeclineReceiptMessage)
        {
            this.Log(System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

        public void OnPrintManualRefundReceipt(PrintManualRefundReceiptMessage printManualRefundReceiptMessage)
        {
            this.Log(System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

        public void OnPrintPaymentDeclineReceipt(PrintPaymentDeclineReceiptMessage printPaymentDeclineReceiptMessage)
        {
            this.Log(System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

        public void OnPrintPaymentMerchantCopyReceipt(PrintPaymentMerchantCopyReceiptMessage printPaymentMerchantCopyReceiptMessage)
        {
            this.Log(System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

        public void OnPrintPaymentReceipt(PrintPaymentReceiptMessage printPaymentReceiptMessage)
        {

            this.Log(System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

        public void OnPrintRefundPaymentReceipt(PrintRefundPaymentReceiptMessage printRefundPaymentReceiptMessage)
        {
            this.Log(System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

        public void OnReadCardDataResponse(ReadCardDataResponse response)
        {
            this.Log(System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

        public void OnRefundPaymentResponse(RefundPaymentResponse response)
        {
            this.Log(System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

        public void OnResetDeviceResponse(ResetDeviceResponse response)
        {
            this.Log(System.Reflection.MethodBase.GetCurrentMethod().Name+
                ": "+
                " Message: "+response.Message+
                " Reason: " + response.Reason+
                " Result: " + response.Result.ToString()+
                " State: "   + response.State.ToString()+
                " Success: " + response.Success.ToString()
                );
        }

        public void OnRetrieveDeviceStatusResponse(RetrieveDeviceStatusResponse response)
        {
            this.Log(System.Reflection.MethodBase.GetCurrentMethod().Name +
                "State: " + response.State.ToString());
        }

        public void OnRetrievePaymentResponse(RetrievePaymentResponse response)
        {
            this.Log(System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

        public void OnRetrievePendingPaymentsResponse(RetrievePendingPaymentsResponse response)
        {
            this.Log(System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

        public void OnRetrievePrintersResponse(RetrievePrintersResponse response)
        {
            this.Log(System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

        public void OnTipAdded(TipAddedMessage message)
        {
            this.Log(System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

        public void OnTipAdjustAuthResponse(TipAdjustAuthResponse response)
        {
            this.Log(System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

        public void OnVaultCardResponse(VaultCardResponse response)
        {
            this.Log(System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

        public void OnVoidPaymentResponse(VoidPaymentResponse response)
        {
            this.Log(System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

        public void OnVoidPaymentRefundResponse(VoidPaymentRefundResponse response)
        {
            this.Log(System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

        public void OnDisplayReceiptOptionsResponse(DisplayReceiptOptionsResponse response)
        {
            this.Log(System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

        public void OnInvalidStateTransitionResponse(InvalidStateTransitionNotification message)
        {
            this.Log(System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

        public void OnCustomerProvidedData(CustomerProvidedDataEvent response)
        {
            this.Log(System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

        public void OnIncrementPreAuthResponse(IncrementPreAuthResponse response)
        {
            this.Log(System.Reflection.MethodBase.GetCurrentMethod().Name);
        }
    }
}
