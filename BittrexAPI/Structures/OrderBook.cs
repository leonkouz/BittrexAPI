using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BittrexAPI.Structures
{
    public class OrderBook
    {
        private readonly List<Order> _buys;
        private readonly List<Order> _sells;

        public IReadOnlyCollection<Order> Buys { get; private set; } 
        public IReadOnlyCollection<Order> Sells { get; private set; }

        public OrderBook(List<Order> buys, List<Order> sells)
        {
            _buys = buys;
            _sells = sells;

            Buys = _buys.AsReadOnly();
            Sells = _sells.AsReadOnly();
          
        }
        
        public OrderBook(List<Order> order, Order.Type orderType)
        {
            if (orderType == Order.Type.buy)
            {
                Buys = order;
            }
            else if (orderType == Order.Type.sell)
            {
                Sells = order;
            }
        }
    }
}
