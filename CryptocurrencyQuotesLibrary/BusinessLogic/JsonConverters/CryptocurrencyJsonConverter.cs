using CryptocurrencyQuotesLibrary.Models.DbModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CryptocurrencyQuotesLibrary.BusinessLogic.JsonConverters
{
    public class CryptocurrencyJsonConverter : IJsonConverter<List<Cryptocurrency>>
    {
        public async Task<List<Cryptocurrency>> Deserialize(string jsonString)
        {
            return await Task.Run(() =>
            {
                var result = new List<Cryptocurrency>();

                var jobject = JObject.Parse(jsonString);
                var data = (JArray)jobject["data"];

                foreach (var jsonCrypt in data)
                {
                    var id = jsonCrypt["id"].ToObject<int>();
                    var name = jsonCrypt["name"].ToObject<string>();
                    var symbol = jsonCrypt["symbol"].ToObject<string>();

                    var quote = jsonCrypt["quote"]["USD"];
                    var price = quote["price"].ToObject<decimal>();
                    var percent_change_1h = quote["percent_change_1h"].ToObject<decimal>();
                    var percent_change_24h = quote["percent_change_24h"].ToObject<decimal>();
                    var market_cap = quote["market_cap"].ToObject<decimal>();
                    var last_updated = quote["last_updated"].ToObject<DateTime>();

                    result.Add(new Cryptocurrency
                    {
                        Id = id,
                        Name = name,
                        Symbol = symbol,
                        Price = price,
                        PercentChange1h = percent_change_1h,
                        PercentChange24h = percent_change_24h,
                        MarketCap = market_cap,
                        LastUpdated = last_updated
                    });
                }

                return result;
            });          
        }
    }
}
