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

            var hmac = new HMACSHA512(Encoding.ASCII.GetBytes(KeyToSignWith));
            var messagebyte = Encoding.ASCII.GetBytes(StringToSign);
            var hashmessage = hmac.ComputeHash(messagebyte);
            var sign = BitConverter.ToString(hashmessage).Replace("-", "");

            return sign;

            /*var encoding = Encoding.ASCII;

            using (var hasher = new HMACSHA512(Convert.FromBase64String(KeyToSignWith)))
            {
                return Convert.ToBase64String(hasher.ComputeHash(encoding.GetBytes(StringToSign)));
            }*/
        }
    }
}
