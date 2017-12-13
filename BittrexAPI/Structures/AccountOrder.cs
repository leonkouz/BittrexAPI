using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BittrexAPI.Structures
{
    public class AccountOrder
    {
        private List<object> _accountOrderList = new List<object>();

        /// <summary>
        /// Implements IEnumerator to allow for itterating using foreach
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            return (_accountOrderList as IEnumerable).GetEnumerator();
        }

        public string AccountId { get; private set;  }
        public string OrderUuid { get; private set; }
        public string Exchange { get; private set; }
        public string Type { get; private set; }
        public double Quantity { get; private set; }
        public double QuantityRemaining { get; private set; }
        public double Limit { get; private set; }
        public double Reserved { get; private set; }
        public double ReserveRemaining { get; private set; }
        public double CommissionReserved { get; private set; }
        public double CommissionReserveRemaining { get; private set; }
        public double CommissionPaid { get; private set; }
        public double Price { get; private set; }
        public double PricePerUnit { get; private set; }
        public DateTime Opened { get; private set; }
        public DateTime Closed { get; private set; }
        public bool IsOpen { get; private set; }
        public string Sentinel { get; private set; }
        public bool CancelInitiated { get; private set; }
        public bool ImmediateOrCancel { get; private set; }
        public bool IsConditional { get; private set; }
        public string Condition { get; private set; }
        public double ConditionTarget { get; private set; }

        public AccountOrder(string accountId, string orderUuid, string exchange, string type, string quantity, string quantityRemaining, string limit, string reserved, string reserveRemaining,
            string commissionReserved, string commissionReservedRemaining, string commissionPaid, string price, string pricePerUnit, string opened, string closed, string isOpen,
            string sentinel, string cancelInitiated, string immediateOrCancel, string isConditional, string condition, string conditionTarget)
        {

            AccountId = accountId;
            OrderUuid = orderUuid;
            Exchange = exchange;
            Type = type;

            if (quantity == "" || quantity == null || quantity == "null")
                Quantity = 0;
            else
                Quantity = Convert.ToDouble(quantity);

            if (quantityRemaining == "" || quantityRemaining == null || quantityRemaining == "null")
                QuantityRemaining = 0;
            else
                QuantityRemaining = Convert.ToDouble(quantityRemaining);

            if (limit == "" || limit == null || limit == "null")
                Limit = 0;
            else
                Limit = Convert.ToDouble(limit);

            if (reserved == "" || reserved == null || reserved == "null")
                Reserved = 0;
            else
                Reserved = Convert.ToDouble(reserved);

            if (reserveRemaining == "" || reserveRemaining == null || reserveRemaining == "null")
                ReserveRemaining = 0;
            else
                ReserveRemaining = Convert.ToDouble(reserveRemaining);

            if (commissionReserved == "" || commissionReserved == null || commissionReserved == "null")
                CommissionReserved = 0;
            else
                CommissionReserved = Convert.ToDouble(commissionReserved);

            if (commissionReservedRemaining == "" || commissionReservedRemaining == null || commissionReservedRemaining == "null")
                CommissionReserveRemaining = 0;
            else
                CommissionReserveRemaining = Convert.ToDouble(commissionReservedRemaining);

            if (commissionPaid == "" || commissionPaid == null || commissionPaid == "null")
                CommissionPaid = 0;
            else
                CommissionPaid = Convert.ToDouble(commissionPaid);

            if (price == "" || price == null || price == "null")
                Price = 0;
            else
                Price = Convert.ToDouble(price);

            if (pricePerUnit == "" || pricePerUnit == null || pricePerUnit == "null")
                PricePerUnit = 0;
            else
                PricePerUnit = Convert.ToDouble(pricePerUnit);

            if (opened == "" || opened == null || opened == "null")
                Opened = DateTime.MinValue;
            else
                Opened = Convert.ToDateTime(opened);

            if (closed == "" || closed == null || closed == "null")
                Closed = DateTime.MinValue;
            else
                Closed = Convert.ToDateTime(closed);

            if (isOpen == "" || isOpen == null || isOpen == "null")
                IsOpen = false;
            else
                IsOpen = Convert.ToBoolean(isOpen);

            Sentinel = sentinel;

            if (cancelInitiated == "" || cancelInitiated == null || cancelInitiated == "null")
                CancelInitiated = false;
            else
                CancelInitiated = Convert.ToBoolean(cancelInitiated);

            if (immediateOrCancel == "" || immediateOrCancel == null || immediateOrCancel == "null")
                ImmediateOrCancel = false;
            else
                ImmediateOrCancel = Convert.ToBoolean(immediateOrCancel);

            if (isConditional == "" || isConditional == null || isConditional == "null")
                IsConditional = false;
            else
                IsConditional = Convert.ToBoolean(isConditional);

            Condition = condition;

            if (conditionTarget == "" || conditionTarget == null || conditionTarget == "null")
                ConditionTarget = 0;
            else
                ConditionTarget = Convert.ToDouble(conditionTarget);

            _accountOrderList.Add(AccountId);
            _accountOrderList.Add(OrderUuid);
            _accountOrderList.Add(Exchange);
            _accountOrderList.Add(Type);
            _accountOrderList.Add(Quantity);
            _accountOrderList.Add(QuantityRemaining);
            _accountOrderList.Add(Limit);
            _accountOrderList.Add(Reserved);
            _accountOrderList.Add(ReserveRemaining);
            _accountOrderList.Add(CommissionReserved);
            _accountOrderList.Add(CommissionReserveRemaining);
            _accountOrderList.Add(CommissionPaid);
            _accountOrderList.Add(Price);
            _accountOrderList.Add(PricePerUnit);
            _accountOrderList.Add(Opened);
            _accountOrderList.Add(Closed);
            _accountOrderList.Add(IsOpen);
            _accountOrderList.Add(Sentinel);
            _accountOrderList.Add(CancelInitiated);
            _accountOrderList.Add(ImmediateOrCancel);
            _accountOrderList.Add(IsConditional);
            _accountOrderList.Add(Condition);
            _accountOrderList.Add(ConditionTarget);


        }
        
    }
}
