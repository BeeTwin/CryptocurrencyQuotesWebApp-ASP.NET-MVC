using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CryptocurrencyQuotesLibrary.BusinessLogic.Schedulers.DataUpdaters
{
    public interface IDataUpdater
    {
        Task UpdateData();
    }
}
