using System.IO;

namespace CryptocurrencyQuotesLibrary.BusinessLogic.Constants
{
    public static class WebKeys
    {
        public static readonly string API_KEY = File.ReadAllText("./api_key.txt");
        public const string CUSTOM_HEADER = "X-CMC_PRO_API_KEY";
    }
}
