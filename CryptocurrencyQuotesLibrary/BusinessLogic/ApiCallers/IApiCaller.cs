using System;
using System.Net;
using System.Threading.Tasks;

namespace CryptocurrencyQuotesLibrary.BusinessLogic.ApiCallers
{
    public interface IApiCaller
    {
        Task<string> MakeApiCall(UriBuilder url);
    }
}
