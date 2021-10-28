using System;
using System.Security.Cryptography;
using System.Text;

namespace Lab4_4
{
    class Program
    {
        static byte[] ComputeHashSHA256(byte[] dataForHash)
        {
            using (var sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(dataForHash);
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Введіть логін: ");
            string log = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Введіть пароль: ");
            string pass = Convert.ToBase64String(ComputeHashSHA256(Encoding.Unicode.GetBytes(Convert.ToString(Console.ReadLine()))));

            Console.WriteLine("---------------------------------");
            Console.WriteLine("Введіть логін: ");
            string eLog = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Введіть пароль: ");
            string ePass = Convert.ToBase64String(ComputeHashSHA256(Encoding.Unicode.GetBytes(Convert.ToString(Console.ReadLine()))));

            if (log != eLog)
            {
                Console.WriteLine("Неправильний логін");
            }
            else if (pass != ePass)
            {
                Console.WriteLine("Неправильний пароль");
            }
            else
            {
                Console.WriteLine("Логін і пароль вірні");
            }
            Console.ReadKey();
        }
    }
}
