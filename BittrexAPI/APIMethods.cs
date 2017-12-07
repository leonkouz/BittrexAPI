using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using BittrexAPI.Structures;

namespace BittrexAPI
{
    public class APIMethods
    {
        /// <summary>
        /// Used to get the open and available trading markets at Bittrex along with other meta data.
        /// </summary>
        /// <returns>A list of all active markets</returns>
        public static List<Market> GetMarkets()
        {
            List<Market> MarketList = new List<Market>();

            dynamic response = JsonConvert.DeserializeObject(HTTPMethods.HttpGet(Constants.baseUrl + "/public/getmarkets"));

            if (response.success == false)
            {
                throw new Exception("Unable to retrieve data from API");
            }

            foreach (var item in response.result)
            {
                Market point = new Market(
                item.MarketCurrency.ToString(),
                item.BaseCurrency.ToString(),
                item.MarketCurrencyLong.ToString(),
                item.BaseCurrencyLong.ToString(),
                Convert.ToDouble(item.MinTradeSize),
                item.MarketName.ToString(),
                item.IsActive.ToString(),
                Convert.ToDateTime(item.Created),
                item.Notice.ToString(),
                item.IsSponsored.ToString(),
                item.LogoUrl.ToString()
                );

                MarketList.Add(point);
            }

            return MarketList;

        }

        /// <summary>
        /// Used to get all supported currencies at Bittrex along with other meta data.
        /// </summary>
        /// <returns>A list of all supported currencies</returns>
        public static List<MarketCurrency> GetCurrencies()
        {
            List<MarketCurrency> MarketCurrencyList = new List<MarketCurrency>();

            dynamic response = JsonConvert.DeserializeObject(HTTPMethods.HttpGet(Constants.baseUrl + "/public/getcurrencies"));

            if (response.success == false)
            {
                throw new Exception("Unable to retrieve data from API");
            }

            foreach(var item in response.result)
            {
                MarketCurrency currency = new MarketCurrency(
                    item.Currency.ToString(),
                    item.CurrencyLong.ToString(),
                    item.MinConfirmation.ToString(),
                    Convert.ToDouble(item.TxFee),
                    Convert.ToBoolean(item.IsActive),
                    item.CoinType.ToString(),
                    item.BaseAddress.ToString()
                    );

                MarketCurrencyList.Add(currency);
            }

            return MarketCurrencyList;
        }

        /// <summary>
        /// Used to get the current tick values for a market.
        /// </summary>
        /// <param name="market">requires a string literal for the market (ex: BTC-LTC)</param>
        /// <returns>The ticker data</returns>
        public static Ticker GetTicker(string market)
        {
            dynamic response = JsonConvert.DeserializeObject(HTTPMethods.HttpGet(Constants.baseUrl + "public/getticker?market=" + market));

            if(response.success == false)
            {
                throw new Exception("Unable to retreive data from API");
            }

            if(response.message == "INVALID_MARKET")
            {
                throw new ArgumentException("This is not a valid market. Use GetMarkets() to get a list of valid markets.");
            }
            
            double bid = Convert.ToDouble(response.result.Bid);
            double ask = Convert.ToDouble(response.result.Ask);
            double last = Convert.ToDouble(response.result.Last);

            return new Ticker(bid, ask, last);
        }

        /// <summary>
        /// Used to get the last 24 hour summary of all active exchanges
        /// </summary>
        /// <returns>A List of summaries for all markets</returns>
        public static List<MarketSummary> GetMarketSummaries()
        {
            List<MarketSummary> marketSummaryList = new List<MarketSummary>();

            dynamic response = JsonConvert.DeserializeObject(HTTPMethods.HttpGet(Constants.baseUrl + "/public/getmarketsummaries"));

            if(response.success == false)
            {
                throw new Exception("Unable to retreive data from API");
            }

            foreach (var item in response.result)
            {
                MarketSummary marketSummary = new MarketSummary(
                    item.MarketName.ToString(),
                    Convert.ToDouble(item.High),
                    Convert.ToDouble(item.Low),
                    Convert.ToDouble(item.Volume),
                    Convert.ToDouble(item.Last),
                    Convert.ToDouble(item.BaseVolume),
                    Convert.ToDateTime(item.TimeStamp),
                    Convert.ToDouble(item.Bid),
                    Convert.ToDouble(item.Ask),
                    Convert.ToInt32(item.OpenBuyOrders),
                    Convert.ToInt32(item.OpenSellOrders),
                    Convert.ToDouble(item.PrevDay),
                    Convert.ToDateTime(item.Created),
                    item.DisplayMarketName
                    );

                marketSummaryList.Add(marketSummary);
            }

            return marketSummaryList;
        }

        /// <summary>
        /// Used to get the last 24 hour summary of specified exchange
        /// </summary>
        /// <param name="market">requires a string literal for the market (ex: BTC-LTC)</param>
        /// <returns>The market summary for the specified market</returns>
        public static MarketSummary GetMarketSummary(string market)
        {
            dynamic response = JsonConvert.DeserializeObject(HTTPMethods.HttpGet(Constants.baseUrl + "/public/getmarketsummary?market=" + market));

            if (response.success == false)
            {
                throw new Exception("Unable to retreive data from API");
            }

            if (response.message == "INVALID_MARKET")
            {
                throw new ArgumentException("This is not a valid market. Use GetMarkets() to get a list of valid markets.");
            }

            var item = response.result[0];
                        
            string marketName = item.MarketName.ToString();
            double high = Convert.ToDouble(item.High);
            double low = Convert.ToDouble(item.Low);
            double volume = Convert.ToDouble(item.Volume);
            double last = Convert.ToDouble(item.Last);
            double baseVolume = Convert.ToDouble(item.BaseVolume);
            DateTime timeStamp = Convert.ToDateTime(item.TimeStamp);
            double bid = Convert.ToDouble(item.Bid);
            double ask = Convert.ToDouble(item.Ask);
            int openBuyOrders = Convert.ToInt32(item.OpenBuyOrders);
            int openSellOrders = Convert.ToInt32(item.OpenSellOrders);
            double prevDay = Convert.ToDouble(item.PrevDay);
            DateTime created = Convert.ToDateTime(item.Created);
            string displayMarketName = item.DisplayMarketName;

            MarketSummary marketSummary = new MarketSummary(marketName, high, low, volume, last, baseVolume, timeStamp, bid, ask, openBuyOrders, 
              openSellOrders, prevDay, created, displayMarketName);
            
            return marketSummary;
        }

        /// <summary>
        /// Used to get retrieve the orderbook for a given market
        /// </summary>
        /// <param name="market">requires a string literal for the market (ex: BTC-LTC)</param>
        /// <returns>The market summary for the specified market</returns>
        public static OrderBook GetOrderBook(string market, Order.Type orderType)
        {
            dynamic response = JsonConvert.DeserializeObject(HTTPMethods.HttpGet(Constants.baseUrl + "/public/getorderbook?market=" + market + "&type=" + orderType.ToString()));

            if (response.success == false)
            {
                throw new Exception("Unable to retreive data from API");
            }

            if (response.message == "INVALID_MARKET")
            {
                throw new ArgumentException("This is not a valid market. Use GetMarkets() to get a list of valid markets.");
            }

            List<Order> buyList = new List<Order>();
            List<Order> sellList = new List<Order>();

            if(orderType == Order.Type.buy)
            {
                foreach (var item in response.result)
                {
                    Order order = new Order(Convert.ToDouble(item.Quantity), Convert.ToDouble(item.Rate));

                    buyList.Add(order);
                }

                OrderBook orderBook = new OrderBook(buyList, orderType);
                return orderBook;

            }
            else if(orderType == Order.Type.sell)
            {

                foreach (var item in response.result)
                {
                    Order order = new Order(Convert.ToDouble(item.Quantity), Convert.ToDouble(item.Rate));

                    sellList.Add(order);
                }

                OrderBook orderBook = new OrderBook(sellList, orderType);
                return orderBook;
            }
            else //else the order type will be 'both'
            {

                foreach(var item in response.result.buy)
                {
                    Order order = new Order(Convert.ToDouble(item.Quantity), Convert.ToDouble(item.Rate));

                    buyList.Add(order);
                }
                foreach (var item in response.result.sell)
                {
                    Order order = new Order(Convert.ToDouble(item.Quantity), Convert.ToDouble(item.Rate));

                    buyList.Add(order);
                }

                OrderBook orderBook = new OrderBook(buyList, sellList);
                return orderBook;
            }

            throw new Exception("Error: Should not have got to this point");
        }



    }
}

