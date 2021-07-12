namespace CryptocurrencyQuotesLibrary.BusinessLogic.Constants
{
    public static class URLs
    {
        private const string SITE = "coinmarketcap.com/";
        private const string BASE_API = "https://pro-api." + SITE + "v1/cryptocurrency/";

        public const string LATEST = BASE_API + "listings/latest";
        public const string BASE_IMAGE = "https://s2." + SITE + "static/img/coins/64x64/";
    }
}
