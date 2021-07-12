using System;
using Microsoft.AspNetCore.Mvc;

namespace CryptocurrencyQuotesWebApp.Models
{
    public class CryptocurrencyViewModel
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string Logo { get; set; }
        public string Price { get; set; }
        public string PercentChange1h { get; set; }
        public string PercentChange24h { get; set; }
        public string MarketCap { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
