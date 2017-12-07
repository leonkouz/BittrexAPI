using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BittrexAPI.Structures
{
    public class MarketHistory
    {
        private List<object> _marketHistoryList = new List<object>();

        public int Id { get; private set; }
        public DateTime TimeStamp { get; private set; }
        public double Quantity { get; private set; }
        public double Price { get; private set; }
        public double Total { get; private set; }
        public string FillType { get; private set; }
        public Order.Type OrderType { get; private set; }

        public MarketHistory(int id, DateTime timeStamp, double quantity, double price, double total, string fillType, string orderType)
        {
            Id = id;
            TimeStamp = timeStamp;
            Quantity = quantity;
            Price = price;
            Total = total;
            FillType = fillType;

            if (orderType == "SELL")
            {
                OrderType = Order.Type.sell;
            }
            else 
            {
                OrderType = Order.Type.buy;
            }

            _marketHistoryList.Add(Id);
            _marketHistoryList.Add(TimeStamp);
            _marketHistoryList.Add(Quantity);
            _marketHistoryList.Add(Price);
            _marketHistoryList.Add(Total);
            _marketHistoryList.Add(FillType);
            _marketHistoryList.Add(OrderType);
        }

        /// <summary>
        /// Implements IEnumerator to allow for itterating using foreach
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            return (_marketHistoryList as IEnumerable).GetEnumerator();
        }


    }
}
