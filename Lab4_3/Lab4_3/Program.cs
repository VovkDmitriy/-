using System;
using System.Security.Cryptography;
using System.Text;

namespace Lab4_3
{
    class Program
    {
        public static byte[] ComputeHmacsha1(byte[] toBeHashed, byte[] key)
        {
            using (var hmac = new HMACSHA1(key))
            {
                return hmac.ComputeHash(toBeHashed);
            }
        }

        static void Main(string[] args)
        {
            const string message = "Hello World!";
            const string password = "DV0702";
            const string message1 = "Hello World!";
            const string message2 = "hello world";

            var heshkode = ComputeHmacsha1(Encoding.Unicode.GetBytes(message), Encoding.Unicode.GetBytes(password));

            Console.WriteLine($"Hash HMAC: {Convert.ToBase64String(heshkode)}");

            
            var pHesh = heshkode;

            var heshkode1 = ComputeHmacsha1(Encoding.Unicode.GetBytes(message1), Encoding.Unicode.GetBytes(password));

            if (Convert.ToBase64String(pHesh) == Convert.ToBase64String(heshkode1))
            {
                Console.WriteLine("Повiдомлення 1 не змiнене");
            }
            else
            {
                Console.WriteLine("Повiдомлення 1 змiнене");
            }

            var heshkode2 = ComputeHmacsha1(Encoding.Unicode.GetBytes(message2), Encoding.Unicode.GetBytes(password));

            if (Convert.ToBase64String(heshkode2) == Convert.ToBase64String(pHesh))
            {
                Console.WriteLine("Повiдомлення 2 не змiнене");
            }
            else
            {
                Console.WriteLine("Повiдомлення 2 змiнене");
            }
            Console.ReadKey();
        }
    }
}
