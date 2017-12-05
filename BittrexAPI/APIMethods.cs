using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;

namespace BittrexAPI
{
    public class APIMethods
    {
        /// <summary>
        /// Used to get the open and available trading markets at Bittrex along with other meta data.
        /// </summary>
        /// <returns></returns>
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


        public static List<object> GetCurrencies()
        {
            dynamic response = HTTPMethods.HttpGet(Constants.baseUrl + "/public/getcurrencies");

            if (response.success == false)
            {
                throw new Exception("Unable to retrieve data from API");
            }



        }




    }
}

