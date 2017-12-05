using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BittrexAPI
{
    class Encryption
    {
        /// <summary>
        /// Signs the string in HmacSHA512 format
        /// </summary>
        public static string HmacSHA512Sign(string StringToSign, string KeyToSignWith)
        {
            var encoding = Encoding.UTF8;

            using (var hasher = new HMACSHA512(Convert.FromBase64String(KeyToSignWith)))
            {
                return Convert.ToBase64String(hasher.ComputeHash(encoding.GetBytes(StringToSign)));
            }
        }
    }
}
