using System;
using System.Security.Cryptography;
using System.Text;

namespace Lab5._5
{
    public class PBKDF2
    {
        public static byte[] GenerateSalt()
        {
            using (var randomNumberGenerator = new RNGCryptoServiceProvider())
            {
                var randomNumber = new byte[32];
                randomNumberGenerator.GetBytes(randomNumber);
                return randomNumber;
            }
        }

        public static byte[] HashPasswordSHA256(byte[] toBeHashed, byte[] salt, int numberOfRounds)
        {
            using (var rfc2898 = new Rfc2898DeriveBytes(toBeHashed, salt, numberOfRounds, HashAlgorithmName.SHA256))
            {
                return rfc2898.GetBytes(32);
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Для регистрации введите логин и пароль");
            Console.WriteLine("Введите логин: ");
            string login = Convert.ToString(Console.ReadLine());
            byte[] salt = PBKDF2.GenerateSalt();
            Console.WriteLine("Введите пароль: ");
            string password = Convert.ToBase64String(PBKDF2.HashPasswordSHA256(Encoding.Unicode.GetBytes(Convert.ToString(Console.ReadLine())), salt, 60000));

            Console.WriteLine("Для входа введите логин и пароль");
            Console.WriteLine("Введите логин: ");
            string enteredLogin = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Введите пароль: ");
            string enteredPassword = Convert.ToBase64String(PBKDF2.HashPasswordSHA256(Encoding.Unicode.GetBytes(Convert.ToString(Console.ReadLine())), salt, 60000));
            if (login != enteredLogin)
            {
                Console.WriteLine("Неправильный логин");
                Console.ReadKey();
            }
            else if (password != enteredPassword)
            {
                Console.WriteLine("Неправильный пароль");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Вы вошли");
                Console.ReadKey();
            }
        }
    }
}