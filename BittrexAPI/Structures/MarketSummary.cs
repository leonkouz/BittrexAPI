using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BittrexAPI.Structures
{
    public class MarketSummary
    {
        private List<object> _marketSummaryList = new List<object>();

        /// <summary>
        /// Implements IEnumerator to allow for itterating using foreach
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            return (_marketSummaryList as IEnumerable).GetEnumerator();
        }

        public string MarketName { get; private set; }
        public double High { get; private set; }
        public double Low { get; private set; }
        public double Volume { get; private set; }
        public double Last { get; private set; }
        public double BaseVolume { get; private set; }
        public DateTime TimeStamp { get; private set; }
        public double Bid { get; private set; }
        public double Ask { get; private set; }
        public int OpenBuyOrders { get; private set; }
        public int OpenSellOrders { get; private set; }
        public double PrevDay { get; private set; }
        public DateTime Created { get; private set; }
        public string DisplayMarketName { get; private set; }

        public MarketSummary(string marketName, double high, double low, double volume, double last, double baseVolume, DateTime timeStamp, double bid, double ask, int openBuyorders,
            int openSellOrders, double prevDay, DateTime created, string displayMarketName)
        {
            MarketName = marketName;
            High = high;
            Low = low;
            Volume = volume;
            Last = last;
            BaseVolume = baseVolume;
            TimeStamp = timeStamp;
            Bid = bid;
            Ask = ask;
            OpenBuyOrders = openBuyorders;
            OpenSellOrders = openSellOrders;
            PrevDay = prevDay;
            Created = created;

            if (displayMarketName == "" || displayMarketName == null)
            {
                DisplayMarketName = "";
            }
            else
            {
                DisplayMarketName = displayMarketName;
            }

            _marketSummaryList.Add(MarketName);
            _marketSummaryList.Add(High);
            _marketSummaryList.Add(Low);
            _marketSummaryList.Add(Volume);
            _marketSummaryList.Add(Last);
            _marketSummaryList.Add(BaseVolume);
            _marketSummaryList.Add(TimeStamp);
            _marketSummaryList.Add(Bid);
            _marketSummaryList.Add(Ask);
            _marketSummaryList.Add(OpenBuyOrders);
            _marketSummaryList.Add(OpenSellOrders);
            _marketSummaryList.Add(PrevDay);
            _marketSummaryList.Add(Created);
            _marketSummaryList.Add(DisplayMarketName);
        }
    }
}
