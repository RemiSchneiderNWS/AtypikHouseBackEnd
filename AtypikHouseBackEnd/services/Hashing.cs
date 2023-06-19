using System;
using System.Security.Cryptography;
using System.Text;


namespace BackEnd_TocTroc.Services
{
    public class Hashing
    {
        public static string Sha512(string get)
        {
            using (SHA512 sha512hash = SHA512.Create())
            {
                byte[] textBytes = Encoding.UTF8.GetBytes(get);
                byte[] hashBytes = sha512hash.ComputeHash(textBytes);

                get = BitConverter.ToString(hashBytes).Replace("-", String.Empty);
            }

            return get;
        }
    }
}
