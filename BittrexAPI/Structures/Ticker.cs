using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BittrexAPI.Structures
{
    public class Ticker
    {
        public double Bid { get; private set; }
        public double Ask { get; private set; }
        public double Last { get; private set; }

        public Ticker(double bid, double ask, double last)
        {
            Bid = bid;
            Ask = ask;
            Last = last;
        }
    }
}
