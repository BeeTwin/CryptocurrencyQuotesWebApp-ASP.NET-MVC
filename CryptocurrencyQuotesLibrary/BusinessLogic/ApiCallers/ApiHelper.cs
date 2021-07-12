using CryptocurrencyQuotesLibrary.BusinessLogic.Constants;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace CryptocurrencyQuotesLibrary.BusinessLogic.ApiCallers
{
    public static class ApiHelper
    {
        public static HttpClient Client;

        public static void Initialize()
        {
            Client = new HttpClient();
            Client.DefaultRequestHeaders.Add(WebKeys.CUSTOM_HEADER, WebKeys.API_KEY);
            Client.DefaultRequestHeaders.Add("Accepts", "application/json");
        }
    }
}
