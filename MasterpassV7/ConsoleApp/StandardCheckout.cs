using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Com.MasterCard.Merchant.Checkout.Api;
using Com.MasterCard.Merchant.Checkout.Model;
using Com.MasterCard.Sdk.Core;
using Com.MasterCard.Sdk.Core.Client;

namespace ConsoleApp
{

    public class StandardCheckout
    {
		const String consumerKey = "n7642t-OahZXoMUsgig2c6pXj7L1j6i7gvSQ1Wwn3c2b48c0!d2b9255554fc4141a4276b8fb4708fa30000000000000000";
		const String keystorePassword = "ccknfDZDxCNvl5p9ZUao";
		const String keystorePath = "/Users/fabiogodoy/Downloads/Master/defaultSandboxKey-sandbox.p12";
		const String checkoutId = "be77c1f97cc848e7bce27684726953c7";

        private String cartId = "";
        private String oauth_verifier = "";

        public StandardCheckout(String _cartId, String _oauthVerifier)
        {
            cartId = _cartId;
            oauth_verifier = _oauthVerifier;

            _setConfigurations();
            _executePaymentData();
            _executePostback();
        }

		private void _setConfigurations()
		{

			AsymmetricAlgorithm privateKey = new X509Certificate2(keystorePath, keystorePassword).PrivateKey;

			MasterCardApiConfiguration.Sandbox = true;
			MasterCardApiConfiguration.ConsumerKey = consumerKey;
			MasterCardApiConfiguration.PrivateKey = privateKey;

			//Set Proxy (optional)
			//MasterCardApiConfiguration.Proxy = new WebProxy("http://127.0.0.1:8989/", true);
		}

		private void _executePaymentData()
		{
			var queryParams = new QueryParams()
				  .Add("checkoutId", checkoutId)
				  .Add("cartId", cartId);
            PaymentData paymentData = PaymentDataApi.Show(oauth_verifier, queryParams);
		}

		private void _executePostback()
		{
            var postback = new Postback
            {
                Currency = "USD",
                PaymentCode = "001122",
                PaymentDate = DateTime.UtcNow,
                PaymentSuccessful = true,
                Amount = double.Parse("100"),
                TransactionId = oauth_verifier
			};

			PostbackApi.Create(postback);
		}
    }
}
