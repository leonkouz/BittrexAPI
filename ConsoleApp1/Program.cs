using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BittrexAPI;
using BittrexAPI.Structures;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

            Constants.ApiKey = "Enter api key here";
            Constants.SecretKey = "Enter Secret key here";

            
            #region PublicAPI

            //Get Markets test
            List<Market> listOfMarkets = APIMethods.GetMarkets();
            
            //Get all supported currencies
            List<MarketCurrency> listOfCurrencies = APIMethods.GetCurrencies();

            //Get the current tick value for the specified market
            Ticker tick = APIMethods.GetTicker("BTC-LTC");

            //Gets the summary of all markets
            List<MarketSummary> listOfMarketSummaries = APIMethods.GetMarketSummaries();

            //Gets the summary of a specificed market
            MarketSummary marketSummary = APIMethods.GetMarketSummary("BTC-LTC");

            //Gets the Order book for the specified market
            OrderBook book = APIMethods.GetOrderBook("BTC-LTC", Order.Type.both);

            List<MarketHistory> marketHistory = APIMethods.GetMarketHistory("BTC-LTC");

            #endregion

            
            #region MarketAPI

            APIMethods.PlaceBuyLimitOrder("BTC-LTC", 5, 0.17);
            
            APIMethods.PlaceSellLimitOrder("BTC-LTC", 5, 0.17);

            APIMethods.CancelOrder("1231aweqweqwe123");

            List<OpenOrder> orders = APIMethods.GetOpenOrders("BTC-GRS");
        
            #endregion

            #region AccountAPI

            List<Balance> balanceList = APIMethods.GetBalances();

            Balance balance = APIMethods.GetBalance("GRS");

            //APIMethods.Withdraw("GRS", 20.23222, "Address to withdraw GRS to");

            AccountOrder accountOrder = APIMethods.GetOrder("uuid here");

            List<HistoryOrder> listOfOrderHistory = APIMethods.GetOrderHistory();

            List<HistoryOrder> listOfSpecificOrderHistory = APIMethods.GetOrderHistory("BTC-LTC");


            #endregion

        }
    }
}
