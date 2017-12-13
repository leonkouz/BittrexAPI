using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BittrexAPI.Structures
{
    public class HistoryOrder
    {
        private List<object> _historyOrderList = new List<object>();

        /// <summary>
        /// Implements IEnumerator to allow for itterating using foreach
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            return (_historyOrderList as IEnumerable).GetEnumerator();
        }

        public string OrderUuid { get; private set; }
        public string Exchange { get; private set; }
        public DateTime TimeStamp { get; private set; }
        public string OrderType { get; private set; }
        public double Limit { get; private set; }
        public double Quantity { get; private set; }
        public double QuantityRemaining { get; private set; }
        public double Commission { get; private set; }
        public double Price { get; private set; }
        public double PricePerUnit { get; private set; }
        public bool IsConditional { get; private set; }
        public string Condition { get; private set; }
        public double ConditionTarget { get; private set; }
        public bool ImmediateOrCancel { get; private set; }

        public HistoryOrder(string orderUuid, string exchange, string timeStamp, string orderType, string limit, string quantity, string quantityRemaining, string commission,
            string price, string pricePerUnit, string isConditional, string condition, string conditionTarget, string immediateOrCancel)
        {
            OrderUuid = orderUuid;
            Exchange = exchange;
            TimeStamp = Convert.ToDateTime(timeStamp);
            OrderType = orderType;
            Limit = Convert.ToDouble(limit);
            Quantity = Convert.ToDouble(quantity);
            QuantityRemaining = Convert.ToDouble(quantityRemaining);
            Commission = Convert.ToDouble(commission);
            Price = Convert.ToDouble(price);
            PricePerUnit = Convert.ToDouble(pricePerUnit);
            IsConditional = Convert.ToBoolean(isConditional);
            Condition = condition;
            ConditionTarget = Convert.ToDouble(conditionTarget);
            ImmediateOrCancel = Convert.ToBoolean(immediateOrCancel);

            _historyOrderList.Add(OrderUuid);
            _historyOrderList.Add(Exchange);
            _historyOrderList.Add(TimeStamp);
            _historyOrderList.Add(OrderType);
            _historyOrderList.Add(Limit);
            _historyOrderList.Add(Quantity);
            _historyOrderList.Add(QuantityRemaining);
            _historyOrderList.Add(Commission);
            _historyOrderList.Add(Price);
            _historyOrderList.Add(PricePerUnit);
            _historyOrderList.Add(IsConditional);
            _historyOrderList.Add(Condition);
            _historyOrderList.Add(ConditionTarget);
            _historyOrderList.Add(ImmediateOrCancel);

        }



    }
}
