using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BittrexAPI.Structures
{
    public class Currency : IEnumerable
    {
        private List<object> _currencyList = new List<object>();

        /// <summary>
        /// Implements IEnumerator to allow for itterating using foreach
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            return (_currencyList as IEnumerable).GetEnumerator();
        }

        public string Currency { get; private set; }





    }
}
