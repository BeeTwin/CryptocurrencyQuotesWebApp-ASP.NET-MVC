using CryptocurrencyQuotesLibrary.BusinessLogic.ApiCallers;
using CryptocurrencyQuotesLibrary.Data.Contexts;
using CryptocurrencyQuotesLibrary.BusinessLogic.JsonConverters;
using Quartz;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CryptocurrencyQuotesLibrary.BusinessLogic.Constants;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using CryptocurrencyQuotesLibrary.BusinessLogic.Schedulers.DataUpdaters;

namespace CryptocurrencyQuotesLibrary.BusinessLogic.Schedulers
{
    public class DataUpdateJob : IJob
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public DataUpdateJob(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }


        public async Task Execute(IJobExecutionContext context)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var dataUpdater = scope.ServiceProvider.GetService<IDataUpdater>();

                await dataUpdater.UpdateData();
            }
        }
    }
}
