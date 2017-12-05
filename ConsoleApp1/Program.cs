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
            //Get Markets test
            List<Market> listOfMarkets = BittrexAPI.APIMethods.GetMarkets();
            
            //Get all supported currencies
            List<MarketCurrency> listofCurrencies = BittrexAPI.APIMethods.GetCurrencies();

            //Get the current tick value for the specified market
            Ticker tick = BittrexAPI.APIMethods.GetTicker("BTC-LTC");


        }
    }
}
