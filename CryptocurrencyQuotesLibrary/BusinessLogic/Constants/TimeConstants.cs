using System;
using System.Collections.Generic;
using System.Text;

namespace CryptocurrencyQuotesLibrary.BusinessLogic.Constants
{
    public static class TimeConstants
    {
        private const int CREDITS_FOR_MONTH = 10000;
        private const int CREDITS_PER_UPDATE = 25;

        private const int MIN_DAYS_IN_MONTH = 28;
        private const int MINUTES_IN_MONTH = MIN_DAYS_IN_MONTH * 24 * 60; // 40320
        private const int AVAILABLE_UPDATES_PER_MONTH = CREDITS_FOR_MONTH / CREDITS_PER_UPDATE; // 400

        public const int MINUTES_PER_UPDATE = MINUTES_IN_MONTH / AVAILABLE_UPDATES_PER_MONTH; // 100
    }
}
