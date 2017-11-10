using System;
using Nancy;

namespace ConsoleApp
{
    public class RootRoutes: NancyModule
    {
		public RootRoutes()
		{
            Get["/shop/order/payment/process/"] = StandardCheckout;
		}

		private dynamic StandardCheckout(dynamic parameters)
		{
			var cartId = this.Request.Query["cartId"];
            var oauth_verifier = this.Request.Query["oauth_verifier"];

            var SC = new StandardCheckout(cartId, oauth_verifier);

			return "END....";
		}

    }
}
