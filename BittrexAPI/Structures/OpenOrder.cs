using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BittrexAPI.Structures
{
    public class OpenOrder
    {
        private List<object> _openOrderList = new List<object>();

        public string Uuid { get; private set; }
        public string OrderUuid { get; private set; }
        public string Exchange { get; private set; }
        public string OrderType { get; private set; }
        public double Quantity { get; private set; }
        public double QuantityRemaining { get; private set; }
        public double Limit { get; private set; }
        public double ComissionPaid { get; private set; }
        public double Price { get; private set; }
        public double PricePerUnit { get; private set; }
        public DateTime Opened { get; private set; }
        public DateTime Closed { get; private set; }
        public bool CancelInitiated { get; private set; }
        public bool ImmediateOrCancel { get; private set; }
        public bool IsConditional { get; private set; }
        public string Condition { get; private set; }
        public double ConditionTarget { get; private set; }

        public OpenOrder(string uuid, string orderUuid, string exchange, string orderType, double quantity, double quantityRemaining, double limit, double comissionPaid, double price,
            double pricePerUnit, DateTime opened, DateTime closed, bool cancelInitiatied, bool immediateOrCancel, bool isConditional, string condition, double conditionTarget)
        {
            Uuid = uuid;
            OrderUuid = orderUuid;
            Exchange = exchange;
            OrderType = orderType;
            Quantity = quantity;
            QuantityRemaining = quantityRemaining;
            Limit = limit;
            ComissionPaid = comissionPaid;
            Price = price;
            PricePerUnit = pricePerUnit;
            Opened = opened;
            Closed = closed;
            CancelInitiated = cancelInitiatied;
            ImmediateOrCancel = immediateOrCancel;
            IsConditional = isConditional;
            Condition = condition;
            ConditionTarget = conditionTarget;

            _openOrderList.Add(Uuid);
            _openOrderList.Add(OrderUuid);
            _openOrderList.Add(Exchange);
            _openOrderList.Add(OrderType);
            _openOrderList.Add(Quantity);
            _openOrderList.Add(QuantityRemaining);
            _openOrderList.Add(Limit);
            _openOrderList.Add(ComissionPaid);
            _openOrderList.Add(Price);
            _openOrderList.Add(PricePerUnit);
            _openOrderList.Add(Opened);
            _openOrderList.Add(Closed);
            _openOrderList.Add(CancelInitiated);
            _openOrderList.Add(ImmediateOrCancel);
            _openOrderList.Add(IsConditional);
            _openOrderList.Add(Condition);
            _openOrderList.Add(ConditionTarget);
        }
        
        /// <summary>
        /// Implements IEnumerator to allow for itterating using foreach
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            return (_openOrderList as IEnumerable).GetEnumerator();
        }


    }
}
