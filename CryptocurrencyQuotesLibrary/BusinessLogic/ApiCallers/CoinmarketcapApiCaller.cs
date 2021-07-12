using CryptocurrencyQuotesLibrary.BusinessLogic.Constants;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using System.Web;

namespace CryptocurrencyQuotesLibrary.BusinessLogic.ApiCallers
{
    public class CoinmarketcapApiCaller : IApiCaller
    {
        public async Task<string> MakeApiCall(UriBuilder url)
        {
            using (var response = await ApiHelper.Client.GetAsync(url.ToString()))
            {
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
