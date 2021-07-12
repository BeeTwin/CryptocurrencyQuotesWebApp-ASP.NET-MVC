using CryptocurrencyQuotesLibrary.BusinessLogic.ApiCallers;
using CryptocurrencyQuotesLibrary.BusinessLogic.Constants;
using CryptocurrencyQuotesLibrary.BusinessLogic.JsonConverters;
using CryptocurrencyQuotesLibrary.Data.Contexts;
using CryptocurrencyQuotesLibrary.Models.DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CryptocurrencyQuotesLibrary.BusinessLogic.Schedulers.DataUpdaters
{
    public class DataUpdater : IDataUpdater
    {
        private readonly IApiCaller _apiCaller;
        private readonly IJsonConverter<List<Cryptocurrency>> _jsonConverter;
        private readonly CryptocurrencyDbContext _dbContext;
        private readonly WebClient _client;

        public DataUpdater(
            IApiCaller apiCaller, 
            IJsonConverter<List<Cryptocurrency>> jsonConverter,
            CryptocurrencyDbContext dbContext,
            WebClient client)
        {
            _apiCaller = apiCaller;
            _jsonConverter = jsonConverter;
            _dbContext = dbContext;
            _client = client;
        }

        public async Task UpdateData()
        {
            var URL = new UriBuilder(URLs.LATEST);

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["start"] = QueryStringSettings.START;
            queryString["limit"] = QueryStringSettings.LIMIT;
            queryString["convert"] = QueryStringSettings.CONVERT;

            URL.Query = queryString.ToString();

            var jsonString = await _apiCaller.MakeApiCall(URL);
            var crypts = await _jsonConverter.Deserialize(jsonString);

            foreach (var crypt in crypts)
            {
                var existingCrypt = await _dbContext.Cryptocurrency.FindAsync(crypt.Id);
                if (existingCrypt == null)
                {
                    await _dbContext.Cryptocurrency.AddAsync(crypt);
                }
                if (existingCrypt != null)
                {
                    if (existingCrypt.LastUpdated != crypt.LastUpdated)
                    {
                        existingCrypt.Price =            crypt.Price;
                        existingCrypt.PercentChange1h =  crypt.PercentChange1h;
                        existingCrypt.PercentChange24h = crypt.PercentChange24h;
                        existingCrypt.MarketCap =        crypt.MarketCap;
                        existingCrypt.LastUpdated =      crypt.LastUpdated;
                    }
                }
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
