using CryptocurrencyQuotesLibrary.Models.DbModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CryptocurrencyQuotesLibrary.BusinessLogic.JsonConverters
{
    public interface IJsonConverter<T>
    {
        Task<T> Deserialize(string jsonString);
    }
}
