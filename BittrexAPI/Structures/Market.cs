using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BittrexAPI.Structures
{
    /// <summary>
    /// Used for the GetMarkets() method
    /// </summary>
    public class Market : IEnumerable
    {
        private List<object> _marketList = new List<object>();

        public string MarketCurrency { get; private set; }
        public string BaseCurrency { get; private set; }
        public string MarketCurrencyLong { get; private set; }
        public string BaseCurrencyLong { get; private set; }
        public double MinTradeSize { get; private set; }
        public string MarketName { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime Created { get; private set; }
        public string Notice { get; private set; }
        public bool IsSponsored { get; private set; }
        public string LogoUrl { get; private set; }

        public Market(string marketCurrency, string baseCurrency, string marketCurrencyLong, string baseCurrencyLong, double minTradeSize, string marketName, string isActive,
            DateTime created, string notice, string isSponsored, string logoUrl)
        {
            MarketCurrency = marketCurrency;
            BaseCurrency = baseCurrency;
            MarketCurrencyLong = marketCurrencyLong;
            BaseCurrencyLong = baseCurrencyLong;
            MinTradeSize = minTradeSize;
            MarketName = marketName;
            IsActive = Convert.ToBoolean(isActive);
            Created = created;
            Notice = notice;

            if (isSponsored == null || isSponsored == "")
            {
                IsSponsored = false;
            }
            else
            {
                IsSponsored = Convert.ToBoolean(isSponsored);
            }

            LogoUrl = logoUrl;

            //Adds all fields to the list to allow for implementation of IEnumerable
            _marketList.Add(MarketCurrency);
            _marketList.Add(BaseCurrency);
            _marketList.Add(MarketCurrencyLong);
            _marketList.Add(BaseCurrencyLong);
            _marketList.Add(MinTradeSize);
            _marketList.Add(MarketName);
            _marketList.Add(IsActive);
            _marketList.Add(Created);
            _marketList.Add(Notice);
            _marketList.Add(IsSponsored);
            _marketList.Add(LogoUrl);
        }

        /// <summary>
        /// Implements IEnumerator to allow for itterating using foreach
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            return (_marketList as IEnumerable).GetEnumerator();
        }

    }
}
