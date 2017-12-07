using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BittrexAPI.Structures
{
    class OrderBook
    {
        public List<Order> Buy { get; private set; } 
        public List<Order> Sell { get; private set; }

        public OrderBook(List<Order> buy, List<Order> sell)
        {
            Buy = buy;
            Sell = sell;
        }
        
        public OrderBook(List<Order> order, string orderType)
        {
            if (orderType.ToLower() == "buy")
            {
                Buy = order;
            }
            else if (orderType.ToLower() == "sell")
            {
                Sell = order;
            }
            else
            {
                throw new ArgumentException("Wrong order type");
            }
        }
    }
}
