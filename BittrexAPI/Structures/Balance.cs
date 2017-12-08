using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BittrexAPI.Structures
{
    public class Balance
    {
        private List<object> _balanceList = new List<object>();

        /// <summary>
        /// Implements IEnumerator to allow for itterating using foreach
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            return (_balanceList as IEnumerable).GetEnumerator();
        }

        public string Currency { get; private set; }
        public double balance { get; private set; }
        public double Available { get; private set; }
        public double Pending { get; private set; }
        public string CryptoAddress { get; private set; }
        public bool Requested { get; private set; }
        public string Uuid { get; private set; }

        public Balance(string currency, string Balance, string available, string pending, string cryptoAddress, bool requested, string uuid)
        {
            Currency = currency;

            if(Balance == "" || Balance == null || Balance == "null")
            {
                balance = 0;
            }
            else
            {
                balance = Convert.ToDouble(Balance);
            }

            if (available == "" || available == null || available == "null")
            {
                Available = 0;
            }
            else
            {
                Available = Convert.ToDouble(available);
            }

            if (pending == "" || pending == null || pending == "null")
            {
                Pending = 0;
            }
            else
            {
                Pending = Convert.ToDouble(pending);
            }

            CryptoAddress = cryptoAddress;
            Requested = requested;
            Uuid = uuid;

            _balanceList.Add(Currency);
            _balanceList.Add(balance);
            _balanceList.Add(Available);
            _balanceList.Add(Pending);
            _balanceList.Add(CryptoAddress);
            _balanceList.Add(Requested);
            _balanceList.Add(Uuid);
        }
    }
}
