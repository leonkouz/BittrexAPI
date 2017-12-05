using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BittrexAPI.Structures
{
    public class MarketCurrency : IEnumerable
    {
        private List<object> _currencyList = new List<object>();

        /// <summary>
        /// Implements IEnumerator to allow for itterating using foreach
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            return (_currencyList as IEnumerable).GetEnumerator();
        }

        public string Currency { get; private set; }
        public string CurrencyLong { get; private set; }
        public float MinConfirmations { get; private set; }
        public double TxFee { get; private set; }
        public bool IsActive { get; private set; }
        public string CoinType { get; private set; }
        public string BaseAddress { get; private set; }

        public MarketCurrency(string currency, string currencyLong, float minConfirmations, double txFee, bool isActive, string coinType, string baseAddress)
        {
            Currency = currency;
            CurrencyLong = currencyLong;
            MinConfirmations = minConfirmations;
            TxFee = txFee;
            IsActive = isActive;
            CoinType = coinType;
            BaseAddress = baseAddress;

            //Adds all fields to the list to allow for implementation of IEnumerable
            _currencyList.Add(Currency);
            _currencyList.Add(CurrencyLong);
            _currencyList.Add(MinConfirmations);
            _currencyList.Add(TxFee);
            _currencyList.Add(IsActive);
            _currencyList.Add(CoinType);
            _currencyList.Add(BaseAddress);
        }
    }
}
