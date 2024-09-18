using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace eticaret.Models
{
    public class PaymentService
    {
        private readonly Options _options;

        public PaymentService(IConfiguration configuration)
        {
            _options = new Options
            {
                ApiKey = configuration["Iyzipay:ApiKey"],
                SecretKey = configuration["Iyzipay:SecretKey"],
                BaseUrl = configuration["Iyzipay:BaseUrl"]
            };
        }

        public Payment CreatePayment(string price, string paidPrice, string currency, string basketId)
        {
            var request = new CreatePaymentRequest
            {
                Locale = Locale.TR.ToString(),
                ConversationId = Guid.NewGuid().ToString(),
                Price = price,
                PaidPrice = paidPrice,
                Currency = currency,
                Installment = 1,
                BasketId = basketId,
                PaymentChannel = PaymentChannel.WEB.ToString(),
                PaymentGroup = PaymentGroup.PRODUCT.ToString(),
                CallbackUrl = "https://localhost:7228/" // Test URL
            };

            var paymentCard = new PaymentCard
            {
                CardHolderName = "John Doe",
                CardNumber = "5528790000000008", // İyzico'nun test kart numarası
                ExpireMonth = "12",
                ExpireYear = "2030",
                Cvc = "123",
                RegisterCard = 0
            };

            request.PaymentCard = paymentCard;

            var buyer = new Buyer
            {
                Id = "BY789",
                Name = "John",
                Surname = "Doe",
                GsmNumber = "+905350000000",
                Email = "john.doe@email.com",
                IdentityNumber = "74300864791",
                LastLoginDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                RegistrationDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                RegistrationAddress = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1",
                Ip = "85.34.78.112",
                City = "Istanbul",
                Country = "Turkey",
                ZipCode = "34732"
            };

            request.Buyer = buyer;

            var shippingAddress = new Address
            {
                ContactName = "John Doe",
                City = "Istanbul",
                Country = "Turkey",
                Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1",
                ZipCode = "34742"
            };

            request.ShippingAddress = shippingAddress;

            var billingAddress = new Address
            {
                ContactName = "John Doe",
                City = "Istanbul",
                Country = "Turkey",
                Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1",
                ZipCode = "34742"
            };

            request.BillingAddress = billingAddress;

            var basketItems = new List<BasketItem>
    {
        new BasketItem
        {
            Id = "BI101",
            Name = "Telefon",
            Category1 = "Elektronik",
            ItemType = BasketItemType.PHYSICAL.ToString(),
            Price = price
        }
    };

            request.BasketItems = basketItems;

            try
            {
                var payment = Payment.Create(request, _options);
                if (payment.Status == "success")
                {
                    return payment;
                }
                else
                {
                    // Hata durumunda loglama veya mesaj gösterme
                    throw new Exception("Ödeme başarısız: " + payment.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                // Hata durumunda loglama veya mesaj gösterme
                throw new Exception("Ödeme oluşturulurken bir hata oluştu: " + ex.Message);
            }
        }


    }
}
