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
        public static string nonce = Guid.NewGuid().ToString("N");

        #region PublicApi
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
                throw new Exception("Unable to get data from API: " + response.message.ToString());
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
                throw new Exception("Unable to get data from API: " + response.message.ToString());
            }

            foreach (var item in response.result)
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

            if (response.success == false)
            {
                throw new Exception("Unable to get data from API: " + response.message.ToString());
            }

            if (response.message == "INVALID_MARKET")
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

            if (response.success == false)
            {
                throw new Exception("Unable to get data from API: " + response.message.ToString());
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
                throw new Exception("Unable to get data from API: " + response.message.ToString());
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
        /// <param name="orderType">requires a Order.Type enum to pick the type of order (e.g buy, sell or both)</param>
        /// <returns>The order book for the specified market</returns>
        public static OrderBook GetOrderBook(string market, Order.Type orderType)
        {
            dynamic response = JsonConvert.DeserializeObject(HTTPMethods.HttpGet(Constants.baseUrl + "/public/getorderbook?market=" + market + "&type=" + orderType.ToString()));

            if (response.success == false)
            {
                throw new Exception("Unable to get data from API: " + response.message.ToString());
            }

            if (response.message == "INVALID_MARKET")
            {
                throw new ArgumentException("This is not a valid market. Use GetMarkets() to get a list of valid markets.");
            }

            List<Order> buyList = new List<Order>();
            List<Order> sellList = new List<Order>();

            if (orderType == Order.Type.buy)
            {
                foreach (var item in response.result)
                {
                    Order order = new Order(Convert.ToDouble(item.Quantity), Convert.ToDouble(item.Rate));

                    buyList.Add(order);
                }

                OrderBook orderBook = new OrderBook(buyList, orderType);
                return orderBook;
            }
            else if (orderType == Order.Type.sell)
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
                foreach (var item in response.result.buy)
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

        /// <summary>
        /// Used to retrieve the latest trades that have occured for a specific market.
        /// </summary>
        /// <param name="market">requires a string literal for the market (ex: BTC-LTC)</param>
        /// <returns>The market history for the specified market</returns>
        public static List<MarketHistory> GetMarketHistory(string market)
        {
            List<MarketHistory> marketHistoryList = new List<MarketHistory>();

            dynamic response = JsonConvert.DeserializeObject(HTTPMethods.HttpGet(Constants.baseUrl + "/public/getmarkethistory?market=" + market));

            if (response.success == false)
            {
                throw new Exception("Unable to get data from API: " + response.message.ToString());
            }

            foreach (var item in response.result)
            {

                int id = Convert.ToInt32(item.Id);
                DateTime timeStamp = Convert.ToDateTime(item.TimeStamp);
                double quantity = Convert.ToDouble(item.Quantity);
                double price = Convert.ToDouble(item.Price);
                double total = Convert.ToDouble(item.Total);
                string fillType = item.FillType.ToString();
                string orderType = item.OrderType.ToString();

                MarketHistory marketHistroy = new MarketHistory(id, timeStamp, quantity, price, total, fillType, orderType);

                marketHistoryList.Add(marketHistroy);
            }

            return marketHistoryList;
        }

        #endregion

        #region MarketAPI

        /// <summary>
        /// Used to place a buy order in a specific market. Use buylimit to place limit orders. Make sure you have the proper permissions set on your API keys for this call to work
        /// </summary>
        /// <param name="market">requires a string literal for the market (ex: BTC-LTC)</param>
        /// <param name="quantity">Amount of coins to buy</param>
        /// <param name="rate">The rate per coin</param>
        /// <returns>The UUID for the buy order<returns>
        public static string PlaceBuyLimitOrder(string market, double quantity, double rate)
        {
            string url = Constants.baseUrl + "market/buylimit?apikey=" + Constants.ApiKey + "&market=" + market + "&quantity=" +
                quantity.ToString() + "&rate=" + rate.ToString() + "&nonce=" + nonce;

            dynamic response = JsonConvert.DeserializeObject(HTTPMethods.HttpSignAndGet(url));

            if (response.success == "false")
            {
                throw new Exception("Unable to place buy limit order: " + response.message.ToString());
            }

            Console.WriteLine("Buy limit order placed: " + response.result.ToString());

            return response.result.ToString();
        }

        /// <summary>
        /// Used to place a sell order in a specific market. Use selllimit to place limit orders. Make sure you have the proper permissions set on your API keys for this call to work
        /// </summary>
        /// <param name="market">requires a string literal for the market (ex: BTC-LTC)</param>
        /// <param name="quantity">Amount of coins to buy</param>
        /// <param name="rate">The rate per coin</param>
        /// <returns>The UUID for the sell order<returns>
        public static string PlaceSellLimitOrder(string market, double quantity, double rate)
        {
            string url = Constants.baseUrl + "market/selllimit?apikey=" + Constants.ApiKey + "&market=" + market + "&quantity=" +
                quantity.ToString() + "&rate=" + rate.ToString() + "&nonce=" + nonce;

            dynamic response = JsonConvert.DeserializeObject(HTTPMethods.HttpSignAndGet(url));

            if (response.success == "false")
            {
                throw new Exception("Unable to place sell limit order: " + response.message.ToString());
            }

            Console.WriteLine("Sell limit order placed: " + response.result.ToString());

            return response.result.ToString();
        }

        /// <summary>
        /// Used to cancel a buy or sell order.
        /// </summary>
        /// <param name="uuid">The uuid of the order you want to cancel</param>
        public static void CancelOrder(string uuid)
        {
            string url = Constants.baseUrl + "market/cancel?apikey=" + Constants.ApiKey + "Y&uuid=" + uuid + "&nonce=" + nonce;

            dynamic response = JsonConvert.DeserializeObject(HTTPMethods.HttpSignAndGet(url));

            if (response.success == "false")
            {
                Console.WriteLine("*Unable to cancel order" + "\n" +
                    "Error: " + response.message
                    );
                return;
            }

            Console.WriteLine("Order: " + uuid + " has been canceled");
        }

        /// <summary>
        /// Get all orders that you currently have opened. A specific market can be requested
        /// </summary>
        /// <param name="market">requires a string literal for the market (ex: BTC-LTC)</param>
        /// <returns>A list of open orders<returns>
        public static List<OpenOrder> GetOpenOrders(string market)
        {
            string url = Constants.baseUrl + "market/getopenorders?apikey=" + Constants.ApiKey + "&market=" + market + "&nonce=" + nonce;

            dynamic response = JsonConvert.DeserializeObject(HTTPMethods.HttpSignAndGet(url));

            if (response.success == false)
            {
                if (response.success == "false")
                {
                    Console.WriteLine("*Unable to get open orders" + "\n" +
                        "Error: " + response.message + "\n"
                        );
                    throw new Exception("Unable to get data from API: " + response.message.ToString());
                }
            }

            if (response.message == "INVALID_MARKET")
            {
                throw new ArgumentException("This is not a valid market. Use GetMarkets() to get a list of valid markets.");
            }

            List<OpenOrder> openOrdersList = new List<OpenOrder>();

            if (response.result == null)
            {
                throw new NoOpenOrdersException();
            }
            else
            {
                foreach (var item in response.result)
                {
                    string uuid = item.Uuid.ToString();
                    string orderUuid = item.OrderUuid.ToString();
                    string exchange = item.Exchange.ToString();
                    string orderType = item.OrderType.ToString();
                    double quantity = Convert.ToDouble(item.Quantity);
                    double quantityRemaining = Convert.ToDouble(item.QuantityRemaining);
                    double limit = Convert.ToDouble(item.Limit);
                    double commissionPaid = Convert.ToDouble(item.CommissionPaid);
                    double price = Convert.ToDouble(item.Price);
                    string pricePerUnit = item.PricePerUnit.ToString();
                    DateTime opened = Convert.ToDateTime(item.Opened);
                    string closed = item.Closed.ToString();
                    bool cancelInitiated = Convert.ToBoolean(item.CancelInitiated);
                    bool immediateOrCancel = Convert.ToBoolean(item.ImmediateOrCancel);
                    bool isConditional = Convert.ToBoolean(item.IsConditional);
                    string condition = item.Condition.ToString();
                    string conditionTarget = item.ConditionTarget.ToString();

                    OpenOrder openOrder = new OpenOrder(uuid, orderUuid, exchange, orderType, quantity, quantityRemaining, limit, commissionPaid, price, pricePerUnit, opened, closed,
                        cancelInitiated, immediateOrCancel, isConditional, condition, conditionTarget);

                    openOrdersList.Add(openOrder);
                }
            }
            return openOrdersList;
        }

        #endregion

        #region AccountApi

        /// <summary>
        /// Used to retrieve all balances from your account
        /// </summary>
        /// <returns>A list of balances</returns>
        public static List<Balance> GetBalances()
        {
            List<Balance> balanceList = new List<Balance>();

            string url = Constants.baseUrl + "account/getbalances?apikey=" + Constants.ApiKey + "&nonce=" + nonce;

            dynamic response = JsonConvert.DeserializeObject(HTTPMethods.HttpSignAndGet(url));

            if (response.success == false)
            {
                if (response.success == "false")
                {
                    Console.WriteLine("*Unable to get balances" + "\n" +
                        "Error: " + response.message + "\n"
                        );
                    throw new Exception("Unable to get data from API: " + response.message.ToString());
                }
            }

            if (response.result == null)
            {
                throw new NoCurrentBalancesException();
            }

            foreach (var item in response.result)
            {
                string currency = item.Currency.ToString();
                string balance = item.Balance.ToString();
                string available = item.Available.ToString();
                string pending = item.Pending.ToString();
                string cryptoAddress = item.CryptoAddress.ToString();
                bool requested = Convert.ToBoolean(item.Requested);
                string uuid = item.uuid;

                Balance b = new Balance(currency, balance, available, pending, cryptoAddress, requested, uuid);

                balanceList.Add(b);
            }

            return balanceList;
        }

        /// <summary>
        /// Used to retrieve the balance from your account for a specific currency.
        /// </summary>
        /// <param name="currency">equired	a string literal for the currency (ex: LTC)</param>
        /// <returns>The balance from your account for a specific currency</returns>
        public static Balance GetBalance(string currency)
        {
            string url = Constants.baseUrl + "account/getbalance?apikey=" + Constants.ApiKey + "&currency=" + currency + "&nonce=" + nonce;

            dynamic response = JsonConvert.DeserializeObject(HTTPMethods.HttpSignAndGet(url));

            if (response.success == false)
            {
                if (response.success == "false")
                {
                    Console.WriteLine("*Unable to get balances" + "\n" +
                        "Error: " + response.message + "\n"
                        );
                    throw new Exception("Unable to get data from API: " + response.message.ToString());
                }
            }

            if (response.result == null)
            {
                throw new NoCurrentBalancesException();
            }

            Balance b = null;

            string curr = response.result.Currency.ToString();
            string balance = response.result.Balance.ToString();
            string available = response.result.Available.ToString();
            string pending = response.result.Pending.ToString();
            string cryptoAddress = response.result.CryptoAddress.ToString();
            bool requested = Convert.ToBoolean(response.result.Requested);
            string uuid = response.result.uuid;

            b = new Balance(curr, balance, available, pending, cryptoAddress, requested, uuid);


            return b;
        }

        /// <summary>
        /// Used to retrieve or generate an address for a specific currency. If one does not exist, the call will fail and return ADDRESS_GENERATING until one is available.
        /// </summary>
        /// <param name="currency">required	a string literal for the currency (ie. BTC)</param>
        /// <returns>The address, or if the call fails will return ADDRESS_GENERATING until address is available</returns>
        public static string GetDepositAddress(string currency)
        {
            string url = Constants.baseUrl + "account/getdepositaddress?apikey=" + Constants.ApiKey + "&currency=" + currency + "&nonce=" + nonce;

            dynamic response = JsonConvert.DeserializeObject(HTTPMethods.HttpSignAndGet(url));

            if (response.success == false)
            {
                if (response.success == "false")
                {
                    Console.WriteLine("*Unable to get balances" + "\n" +
                        "Error: " + response.message + "\n"
                        );
                    throw new Exception("Unable to get data from API: " + response.message.ToString());
                }
            }

            return response.result.address.ToString();
        }

        /// <summary>
        /// Used to withdraw funds from your account. note: please account for txfee.
        /// </summary>
        /// <param name="currency">A string literal for the currency (ie. BTC)</param>
        /// <param name="quantity">The quantity of coins to withdraw</param>
        /// <param name="address">The address to send the funds to</param>
        /// <returns>The UUID of the transcation</returns>
        public static string Withdraw(string currency, double quantity, string address)
        {
            string url = Constants.baseUrl + "account/withdraw?apikey=" + Constants.ApiKey + "&currency=" + currency + "&quantity=" + quantity + "&address=" + address + " & nonce=" + nonce;

            dynamic response = JsonConvert.DeserializeObject(HTTPMethods.HttpSignAndGet(url));

            if (response.success == false)
            {
                if (response.success == "false")
                {
                    Console.WriteLine("*Unable to get balances" + "\n" +
                        "Error: " + response.message + "\n"
                        );
                    throw new Exception("Unable to get data from API: " + response.message.ToString());
                }
            }

            return response.result.uuid.ToString();

        }

        /// <summary>
        /// Used to withdraw funds from your account. note: please account for txfee.
        /// </summary>
        /// <param name="currency">A string literal for the currency (ie. BTC)</param>
        /// <param name="quantity">The quantity of coins to withdraw</param>
        /// <param name="address">The address to send the funds to</param>
        /// <param name="paymentID">Used for CryptoNotes/BitShareX/Nxt optional field (memo/paymentid)</param>
        /// <returns>The UUID of the transcation</returns>
        public static string Withdraw(string currency, double quantity, string address, string paymentID)
        {
            string url = Constants.baseUrl + "account/withdraw?apikey=" + Constants.ApiKey + "&currency=" + currency + "&quantity=" + quantity + "&address=" + address + 
                "&paymentid=" + paymentID + " & nonce=" + nonce;

            dynamic response = JsonConvert.DeserializeObject(HTTPMethods.HttpSignAndGet(url));

            if (response.success == false)
            {
                if (response.success == "false")
                {
                    Console.WriteLine("*Unable to get balances" + "\n" +
                        "Error: " + response.message + "\n"
                        );
                    throw new Exception("Unable to get data from API: " + response.message.ToString());
                }
            }

            return response.result.uuid.ToString();
        }

        /// <summary>
        /// Used to retrieve a single order by uuid.
        /// </summary>
        /// <param name="uuid">The uuid of the buy or sell order</param>
        /// <returns>The details of an specific order</returns>
        public static AccountOrder GetOrder(string uuid)
        {
            string url = Constants.baseUrl + "account/getorder&uuid=" + uuid + "&nonce=" + nonce;

            dynamic response = JsonConvert.DeserializeObject(HTTPMethods.HttpSignAndGet(url));

            if (response.success == false)
            {
                if (response.success == "false")
                {
                    Console.WriteLine("*Unable to get balances" + "\n" +
                        "Error: " + response.message + "\n"
                        );
                    throw new Exception("Unable to get data from API: " + response.message.ToString());
                }
            }

            string accountId = response.result.AccountId.ToString();
            string orderUuid = response.result.OrderUuid.ToString();
            string exchange = response.result.Exchange.ToString();
            string type = response.result.Type.ToString();
            string quantity = response.result.Quantity.ToString();
            string quantityRemaing = response.result.QuantityRemaining.ToString();
            string limit = response.result.Limit.ToString();
            string reserved = response.result.Reserved.ToString();
            string reservedRemaining = response.result.ReservedRemaining.ToString();
            string commissionReserved = response.result.ReserveRemaining.ToString();
            string commissionReservedRemaining = response.result.CommissionReserveRemaining.ToString();
            string commissionPaid = response.result.CommissionPaid.ToString();
            string price = response.result.Price.ToString();
            string pricePerUnit = response.result.PricePerUnit.ToString();
            string opened = response.result.Opened.ToString();
            string closed = response.result.Closed.ToString();
            string isOpen = response.result.IsOpen.ToString();
            string sentinel = response.result.Sentinel.ToString();
            string cancelInitiated = response.result.CancelInitiated.ToString();
            string immediateOrCancel = response.result.ImmediateOrCancel.ToString();
            string isConditional = response.result.IsConditional.ToString();
            string condiition = response.result.Condition.ToString();
            string conditionTarget = response.result.ConditionTarget.ToString();

            AccountOrder order = new AccountOrder(accountId, orderUuid, exchange, type, quantity, quantityRemaing, limit, reserved, reservedRemaining, commissionReserved,
                commissionReservedRemaining, commissionPaid, price, pricePerUnit, opened, closed, isOpen, sentinel, cancelInitiated, immediateOrCancel, isConditional, condiition, conditionTarget);

            return order;
        }



        /// <summary>
        /// Used to retrieve your order history.
        /// </summary>
        /// <returns></returns>
        public static List<HistoryOrder> GetOrderHistory()
        {
            List<HistoryOrder> historyOrdersList = new List<HistoryOrder>();

            string url = Constants.baseUrl + "account/getorderhistory?apikey=" + Constants.ApiKey + "&nonce=" + nonce;

            dynamic response = JsonConvert.DeserializeObject(HTTPMethods.HttpSignAndGet(url));

            if (response.success == false)
            {
                if (response.success == "false")
                {
                    Console.WriteLine("*Unable to get balances" + "\n" +
                        "Error: " + response.message + "\n"
                        );
                    throw new Exception("Unable to get data from API: " + response.message.ToString());
                }
            }

            foreach (var item in response.result)
            {
                string orderUuid = item.OrderUuid.ToString();
                string exchange = item.Exchange.ToString();
                string timeStamp = item.TimeStamp.ToString();
                string ordertype = item.OrderType.ToString();
                string limit = item.Limit.ToString();
                string quantity = item.Quantity.ToString();
                string quantityRemaining = item.QuantityRemaining.ToString();
                string commission = item.Commission.ToString();
                string price = item.Price.ToString();
                string pricePerUnit = item.PricePerUnit.ToString();
                string isConditional = item.IsConditional.ToString();
                string condition = item.Condition.ToString();
                string conditionTarget = item.ConditionTarget.ToString();
                string immediateOrCancel = item.ImmediateOrCancel.ToString();

                HistoryOrder order = new HistoryOrder(orderUuid, exchange, timeStamp, ordertype, limit, quantity, quantityRemaining, commission, price, pricePerUnit, isConditional, condition, conditionTarget,
                    immediateOrCancel);

                historyOrdersList.Add(order);
            }

            return historyOrdersList;

        }

        /// <summary>
        /// Used to retrieve your order history for a specific market.
        /// </summary>
        /// <param name="market">a string literal for the market (ie. BTC-LTC).</param>
        /// <returns></returns>
        public static List<HistoryOrder> GetOrderHistory(string market)
        {
            List<HistoryOrder> historyOrdersList = new List<HistoryOrder>();

            string url = Constants.baseUrl + "account/getorderhistory?apikey=" + Constants.ApiKey + "&market=" + market + "&nonce=" + nonce;

            dynamic response = JsonConvert.DeserializeObject(HTTPMethods.HttpSignAndGet(url));

            if (response.success == false)
            {
                if (response.success == "false")
                {
                    Console.WriteLine("*Unable to get balances" + "\n" +
                        "Error: " + response.message + "\n"
                        );
                    throw new Exception("Unable to get data from API: " + response.message.ToString());
                }
            }

            foreach (var item in response.result)
            {
                string orderUuid = item.OrderUuid.ToString();
                string exchange = item.Exchange.ToString();
                string timeStamp = item.TimeStamp.ToString();
                string ordertype = item.OrderType.ToString();
                string limit = item.Limit.ToString();
                string quantity = item.Quantity.ToString();
                string quantityRemaining = item.QuantityRemaining.ToString();
                string commission = item.Commission.ToString();
                string price = item.Price.ToString();
                string pricePerUnit = item.PricePerUnit.ToString();
                string isConditional = item.IsConditional.ToString();
                string condition = item.Condition.ToString();
                string conditionTarget = item.ConditionTarget.ToString();
                string immediateOrCancel = item.ImmediateOrCancel.ToString();

                HistoryOrder order = new HistoryOrder(orderUuid, exchange, timeStamp, ordertype, limit, quantity, quantityRemaining, commission, price, pricePerUnit, isConditional, condition, conditionTarget,
                    immediateOrCancel);

                historyOrdersList.Add(order);
            }

            return historyOrdersList;
        }



        #endregion
    }

    [Serializable()]
    public class NoOpenOrdersException : Exception
    {
        public NoOpenOrdersException()
        {
        }

        public NoOpenOrdersException(string message)
        : base(message)
        {
        }
    }

    [Serializable()]
    public class NoCurrentBalancesException : Exception
    {
        public NoCurrentBalancesException()
        {
        }

        public NoCurrentBalancesException(string message)
        : base(message)
        {
        }
    }
}

