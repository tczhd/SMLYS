using SMLYS.ApplicationCore.DTOs.Common;
using SMLYS.ApplicationCore.Interfaces.Services.ThirdParty.PaymentGateway.Common;
using System;
using System.Text;
using System.Xml;
using System.Net;
using System.Collections.Specialized;
using SMLYS.ApplicationCore.DTOs.ThirdPartyService.PaymentGateway.Common;
using SMLYS.ApplicationCore.DTOs.ThirdPartyService.PaymentGateway.Helcim;

namespace SMLYS.Infrastructure.Services.ThirdParty.PaymentGateway.Helcim
{
    public class HelcimPaymentService : IPaymentService
    {
        private const string LIVE_URL = "https://secure.myhelcim.com/api/";

        private XmlDocument BasicRequest(NameValueCollection values)
        {
            using (var client = new WebClient())
            {
                // EXECUTE REQUEST
                var response = client.UploadValues(LIVE_URL, values);

                // BUILD RESPONSE STRING
                var responseString = Encoding.Default.GetString(response);

                // BUILD XML DOCUMENT FROM RESPONSE
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(responseString);

                // CHECK RESPONSE
                if (doc != null)
                {
                    return doc;
                }

                return null;
            }
        }

        private NameValueCollection GetBasicData(HelcimBasicRequestModel data)
        {
            var values = new NameValueCollection();
            values["accountId"] = data.AccountId;
            values["apiToken"] = data.ApiToken;
            values["transactionType"] = data.TransactionType;
            values["terminalId"] = data.TerminalId;
            values["test"] = data.Test?"1":"0";
            values["amount"] = data.Amount.ToString("0.00");

            return values;
        }
       
        public Result ProcessPayment(BasicRequestModel requestModel)
        {
            Result result = new Result();

            var paymentData = (HelcimPaymentRequestModel)requestModel;
                // SET UP POST FIELDS
            //var values = new NameValueCollection();
            //values["accountId"] = "2500318950";
            //values["apiToken"] = "NXK54k3T92M433HK2ec6fFgJS";
            //values["transactionType"] = "purchase";
            //values["terminalId"] = "70028";
            //values["test"] = "1";
            //values["amount"] = "66.00";
            //values["cardHolderName"] = "Jane Smith";
            //values["cardNumber"] = "5454545454545454";
            //values["cardExpiry"] = "1020";
            //values["cardCVV"] = "100";
            //values["cardHolderAddress"] = "123 Road Street";
            //values["cardHolderPostalCode"] = "90212";
            var values = GetBasicData(paymentData);
            values["cardHolderName"] = paymentData.CardHolderName;
            values["cardNumber"] = paymentData.cardNumber;
            values["cardExpiry"] = paymentData.cardExpiry;
            values["cardCVV"] = paymentData.cardCVV;
            values["cardHolderAddress"] = paymentData.cardHolderAddress;
            values["cardHolderPostalCode"] = paymentData.cardHolderPostalCode;


            var data = BasicRequest(values);
            if (data != null)
            {
                result.Success = true;
                result.Message = "Process success. ";
                result.Data = data;
            }

            return result;
        }

        public Result ProcessVoid(BasicRequestModel requestMdoel)
        {
            throw new NotImplementedException();
        }
    }
}
