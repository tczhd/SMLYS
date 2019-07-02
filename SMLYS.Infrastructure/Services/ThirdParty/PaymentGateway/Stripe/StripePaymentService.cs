using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SMLYS.ApplicationCore.DTOs.Common;
using SMLYS.ApplicationCore.DTOs.Payment;
using SMLYS.ApplicationCore.DTOs.ThirdPartyService.PaymentGateway.Common;
using SMLYS.ApplicationCore.DTOs.ThirdPartyService.PaymentGateway.Stripe;
using SMLYS.ApplicationCore.Interfaces.Services.ThirdParty.PaymentGateway.Common;
using SMLYS.Infrastructure.Configuration.ThirdParty.PaymentGateway.Stripe;
using Stripe;

namespace SMLYS.Infrastructure.Services.ThirdParty.PaymentGateway.Stripe
{
    public class StripePaymentService : IThirdPartyPaymentService
    {
        public StripePaymentService(IOptions<StripeKeys> optionsAccessor)
        {
            Keys = optionsAccessor.Value;
            StripeConfiguration.ApiKey = Keys.PublicKey;
        }

        public StripeKeys Keys { get; }

        public async Task<string> ChargeCustomer(CreditCardOptions card,
            decimal amount, string currency, string description)
        {
            var tokenId = GetTokenId(null, card).Result;
            var options = new RequestOptions
            {
                ApiKey = Keys.PrivateKey
            };

            return await Task.Run(() =>
            {
                var myCharge = new ChargeCreateOptions
                {
                    Amount = (long)(amount * 100),
                    Currency = currency,
                    Description = description,
                    //Currency = "gbp",
                    //Description = "Charge for property sign and postage",
                    Source = tokenId
                };

                var chargeService = new ChargeService();
                var stripeCharge = chargeService.Create(myCharge, options);

                return stripeCharge.Id;
            });
        }

        public async Task<string> GetTokenId(BankAccountOptions bankAccount, CreditCardOptions card)
        {
            return await Task.Run(() =>
            {

                var myToken = new TokenCreateOptions
                {
                    BankAccount = bankAccount,
                    Card = card
                    //BankAccount = new BankAccountOptions { },
                    //Card = new CreditCardOptions
                    //{
                    //    Number = "4242424242424242",
                    //    ExpYear = 2020,
                    //    ExpMonth = 6,
                    //    Cvc = "123"
                    //}
                };

                var tokenService = new TokenService();
                var stripeToken = tokenService.Create(myToken);

                return stripeToken.Id;
            });
        }

        public PaymentResultModel ProcessPayment(BasicRequestModel requestModel)
        {
            var result = new PaymentResultModel();

            var paymentData = (StripeBasicRequestModel)requestModel;
            var tokenId = paymentData.TokenId;

            if (string.IsNullOrWhiteSpace(tokenId))
            {
                var card = new CreditCardOptions
                {
                    Number = paymentData.CreditCard.cardNumber,
                    ExpYear = paymentData.CreditCard.ExpiryYear,
                    ExpMonth = paymentData.CreditCard.ExpiryMonth,
                    Cvc = paymentData.CreditCard.cardCVV
                };

                tokenId = GetTokenId(null, card).Result;
            }

            var options = new RequestOptions
            {
                ApiKey = Keys.PrivateKey
            };

            var myCharge = new ChargeCreateOptions
            {
                Amount = (long)(paymentData.Amount * 100),
                Currency = paymentData.Currency.ToString().ToLower(),
                Description = paymentData.Description,
                Source = tokenId
            };

            var chargeService = new ChargeService();
            var stripeCharge = chargeService.Create(myCharge, options);

            result = new PaymentResultModel
            {
                Success = true,
                Message = "Payment success. ",
                Approved = stripeCharge.Status == "succeeded",
                AuthCode = stripeCharge.AuthorizationCode,
                CardToken = tokenId,
                FailureCode = stripeCharge.FailureCode,
                FailureMessage = stripeCharge.FailureMessage,
                TransactionId = stripeCharge.Id,
                Data = stripeCharge
            };

            return result;
        }

        public PaymentResultModel ProcessRefund(BasicRequestModel requestModel)
        {
            throw new NotImplementedException();
        }

        public PaymentResultModel ProcessVoid(BasicRequestModel requestModel)
        {
            throw new NotImplementedException();
        }
    }
}
