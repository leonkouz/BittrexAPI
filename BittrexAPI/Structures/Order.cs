using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BittrexAPI.Structures
{
    public class Order : IComparer
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
            buy,
            sell,
            both
        };

        public int Compare(object x, object y)
        {
            Order o1 = (Order)x;
            Order o2 = (Order)x;

            if (o1.Rate > o2.Rate)
                return 1;
            if (o1.Rate < o2.Rate)
                return -1;
            else
                return 0;
        }

    }
}

