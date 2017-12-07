using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BittrexAPI.Structures
{
    public class Order
    {
        public double Quantity { get; private set; }
        public double Rate { get; private set; }

        public Order(double quantity, double rate)
        {
            Quantity = quantity;
            Rate = rate;
        }

        public enum Type
        {
            Buy,
            Sell
        };

    }
}

