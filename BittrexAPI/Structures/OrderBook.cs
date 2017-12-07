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
        
        public OrderBook(List<Order> order, Order.Type orderType)
        {
            if (orderType == Order.Type.Buy)
            {
                Buy = order;
            }
            else if (orderType == Order.Type.Sell)
            {
                Sell = order;
            }
        }
    }
}
