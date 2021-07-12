using CryptocurrencyQuotesLibrary.BusinessLogic.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptocurrencyQuotesLibrary.Utils
{
    public static class ImageUrl
    {
        public static string GetImageUrl(int id)
        {
            return $"{ URLs.BASE_IMAGE }{ id }.png";
        }
    }
}
