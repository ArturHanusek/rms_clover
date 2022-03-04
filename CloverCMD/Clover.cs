using com.clover.remotepay.sdk;
using com.clover.remotepay.transport;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CloverRMS
{
    public class Clover : ICloverConnectorListener
    {
        readonly MainForm Form;

        private ICloverConnector cloverConnector;


        public int _amount = 0;
        public string _guid = "";

        public bool IsReady = false;


        public delegate void DoOnSaleResponseMethod(SaleResponse response);

        public DoOnSaleResponseMethod DoOnSaleResponse;



        public delegate void DoOnManualRefundResponseMethod(ManualRefundResponse response);

        public DoOnManualRefundResponseMethod DoOnManualRefundResponse;



        public Clover(MainForm ParentForm)
        {
            Form = ParentForm;
            var usbConfiguration = new USBCloverDeviceConfiguration("CE0EE6PJ57C70.6MQBCNVGGSBEW", false);
            cloverConnector = CloverConnectorFactory.createICloverConnector(usbConfiguration);
            cloverConnector.InitializeConnection();

            cloverConnector.AddCloverConnectorListener(this);
            cloverConnector.AddCloverConnectorListener(Form);
        }

        public Clover SetAmount(int amount)
        {
            this._amount = amount;
            this.Log("Transaction Amount: " + this._amount);
            return this;
        }

        public Clover SetTransactionGuid(string guid, int amount)
        {
            this._amount = amount;
            this._guid = StringToGUID(guid + "-" + amount.ToString()).ToString("N");

            this.Log("Transaction Amount: " + this._amount);
            this.Log("Transaction GUID: " + guid);
            this.Log("Payment GUID: " + this._guid);
            return this;    
        }

        private Guid StringToGUID(string v)
        {            
            // Create a new instance of the MD5CryptoServiceProvider object.
            MD5 md5Hasher = MD5.Create();
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(v));
            return new Guid(data);
        }

        public bool IsDeviceConnected()
        {
            return this.IsReady;
        } 

        public void Log(String msg)
        {
            Form.Log(msg);
        }

        public ICloverConnector CloverConnector { get { return this.cloverConnector; }}

        public void Dispose()
        {
            Log("Clover." + System.Reflection.MethodBase.GetCurrentMethod().Name + "()");

            if (cloverConnector != null)
            {
                try
                {
                    cloverConnector.RemoveCloverConnectorListener(Form);
                    cloverConnector.RemoveCloverConnectorListener(this);
                    cloverConnector.Dispose();
                    cloverConnector = null;
                }
                catch (Exception)
                {
                    cloverConnector = null;
                }
            }
        }

        public void ProcessTransaction(bool isManualCardEntry = false)
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
                ExternalId = this._guid,
                Amount = Amount,
                DisableCashback = true,
                DisableDuplicateChecking = true,
                DisableReceiptSelection = true,
            };

            if (isManualCardEntry)
            {
                request.CardEntryMethods = (com.clover.remotepay.sdk.CloverConnector.CARD_ENTRY_METHOD_MANUAL);
                request.CardNotPresent = true;
            }
            
            cloverConnector.Sale(request);
        }

        private void Refund(int Amount, bool isManualCardEntry)
        {
            ManualRefundRequest request = new ManualRefundRequest()
            {
                ExternalId = this._guid,
                Amount = -Amount,
                DisableDuplicateChecking = true,
                DisableReceiptSelection = true,
            };

            if (isManualCardEntry)
            {
                request.CardEntryMethods = (com.clover.remotepay.sdk.CloverConnector.CARD_ENTRY_METHOD_MANUAL);
                request.CardNotPresent = true;
            }

            cloverConnector.ManualRefund(request);
        }


        public void InvokeESC()
        {
            this.Log("InvokeESC()");

            InputOption PressEsc;
            PressEsc = new InputOption()
            {
                keyPress = com.clover.remotepay.transport.KeyPress.ESC
            };

            CloverConnector.InvokeInputOption(PressEsc);
        }

        public void SendRetrievePaymentRequest()
        {
            Log("Clover." + System.Reflection.MethodBase.GetCurrentMethod().Name + "(\"" + _guid + "\")");

            CloverConnector.RetrievePayment(new RetrievePaymentRequest() {
                externalPaymentId = _guid
            });
        }

        public void ResetDevice()
        {
            CloverConnector.ResetDevice();
        }

        internal void SimulateClick(com.clover.remotepay.transport.KeyPress key)
        {
            InputOption io = new InputOption()
            {
                keyPress = key
            };

            CloverConnector.InvokeInputOption(io);
        }

        internal void Connect()
        {
            CloverConnector.InitializeConnection();
        }

        internal void RetrieveDeviceStatus()
        {
            cloverConnector.RetrieveDeviceStatus(new RetrieveDeviceStatusRequest(true));
        }

        public void OnDeviceActivityStart(CloverDeviceEvent deviceEvent)
        {
            
        }

        public void OnDeviceActivityEnd(CloverDeviceEvent deviceEvent)
        {
            
        }

        public void OnDeviceError(CloverDeviceErrorEvent deviceErrorEvent)
        {
            
        }

        public void OnPreAuthResponse(PreAuthResponse response)
        {
            
        }

        public void OnAuthResponse(AuthResponse response)
        {
            
        }

        public void OnTipAdjustAuthResponse(TipAdjustAuthResponse response)
        {
            
        }

        public void OnCapturePreAuthResponse(CapturePreAuthResponse response)
        {
            
        }

        public void OnIncrementPreAuthResponse(IncrementPreAuthResponse response)
        {
            
        }

        public void OnVerifySignatureRequest(VerifySignatureRequest request)
        {
            
        }

        public void OnConfirmPaymentRequest(ConfirmPaymentRequest request)
        {
            
        }

        public void OnCloseoutResponse(CloseoutResponse response)
        {
            
        }

        public void OnSaleResponse(SaleResponse response)
        {
            
        }

        public void OnManualRefundResponse(ManualRefundResponse response)
        {
            
        }

        public void OnRefundPaymentResponse(RefundPaymentResponse response)
        {
            
        }

        public void OnVoidPaymentRefundResponse(VoidPaymentRefundResponse response)
        {
            
        }

        public void OnTipAdded(TipAddedMessage message)
        {
            Log("Clover." + System.Reflection.MethodBase.GetCurrentMethod().Name + "()");
        }

        public void OnVoidPaymentResponse(VoidPaymentResponse response)
        {
            Log("Clover." + System.Reflection.MethodBase.GetCurrentMethod().Name + "()");
        }

        public void OnDeviceConnected()
        {
            
        }

        public void OnDeviceReady(MerchantInfo merchantInfo)
        {
            
        }

        public void OnDeviceDisconnected() 
        {
            IsReady = false;
        }

        public void OnVaultCardResponse(VaultCardResponse response)
        { }

        public void OnRetrievePendingPaymentsResponse(RetrievePendingPaymentsResponse response)
        { }

        public void OnReadCardDataResponse(ReadCardDataResponse response) { }

        public void OnPrintManualRefundReceipt(PrintManualRefundReceiptMessage message) { }
        public void OnPrintManualRefundDeclineReceipt(PrintManualRefundDeclineReceiptMessage message) { }
        public void OnPrintPaymentReceipt(PrintPaymentReceiptMessage message)
        { }
        public void OnPrintPaymentDeclineReceipt(PrintPaymentDeclineReceiptMessage message)
        { }
        public void OnPrintPaymentMerchantCopyReceipt(PrintPaymentMerchantCopyReceiptMessage message)
        { }

        public void OnPrintRefundPaymentReceipt(PrintRefundPaymentReceiptMessage message)
        { }

        public void OnPrintJobStatusResponse(PrintJobStatusResponse response)
        {
            
        }

        public void OnRetrievePrintersResponse(RetrievePrintersResponse response)
        {
            
        }

        public void OnCustomActivityResponse(CustomActivityResponse response)
        {
            
        }

        public void OnRetrieveDeviceStatusResponse(RetrieveDeviceStatusResponse response)
        {
            
        }

        public void OnMessageFromActivity(MessageFromActivity response)
        {
            
        }

        public void OnResetDeviceResponse(ResetDeviceResponse response)
        {
            
        }

        public void OnRetrievePaymentResponse(RetrievePaymentResponse response)
        {
            
        }

        public void OnPrintJobStatusRequest(PrintJobStatusRequest request)
        {
            
        }

        public void OnDisplayReceiptOptionsResponse(DisplayReceiptOptionsResponse response)
        {
            
        }

        public void OnInvalidStateTransitionResponse(InvalidStateTransitionNotification message)
        {
            
        }

        public void OnCustomerProvidedData(CustomerProvidedDataEvent response)
        {
            
        }
    }
}
