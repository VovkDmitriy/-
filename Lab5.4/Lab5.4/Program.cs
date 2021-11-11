using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace Lab5._4
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

        public static byte[] HashPasswordMD5(byte[] toBeHashed, byte[] salt, int numberOfRounds)
        {
            using (var rfc2898 = new Rfc2898DeriveBytes(toBeHashed, salt, numberOfRounds, HashAlgorithmName.MD5))
            {
                return rfc2898.GetBytes(16);
            }
        }

        public static byte[] HashPasswordSHA1(byte[] toBeHashed, byte[] salt, int numberOfRounds)
        {
            using (var rfc2898 = new Rfc2898DeriveBytes(toBeHashed, salt, numberOfRounds, HashAlgorithmName.SHA1))
            {
                return rfc2898.GetBytes(20);
            }
        }

        public static byte[] HashPasswordSHA256(byte[] toBeHashed, byte[] salt, int numberOfRounds)
        {
            using (var rfc2898 = new Rfc2898DeriveBytes(toBeHashed, salt, numberOfRounds, HashAlgorithmName.SHA256))
            {
                return rfc2898.GetBytes(32);
            }
        }

        public static byte[] HashPasswordSHA384(byte[] toBeHashed, byte[] salt, int numberOfRounds)
        {
            using (var rfc2898 = new Rfc2898DeriveBytes(toBeHashed, salt, numberOfRounds, HashAlgorithmName.SHA384))
            {
                return rfc2898.GetBytes(48);
            }
        }

        public static byte[] HashPasswordSHA512(byte[] toBeHashed, byte[] salt, int numberOfRounds)
        {
            using (var rfc2898 = new Rfc2898DeriveBytes(toBeHashed, salt, numberOfRounds, HashAlgorithmName.SHA512))
            {
                return rfc2898.GetBytes(64);
            }
        }
    }

    class Program
    {
        private static void HashPassword(string passwordToHash, int numberOfRounds)
        {
            var sw = new Stopwatch();
            sw.Start();

            var hashedPassword = PBKDF2.HashPasswordSHA256(Encoding.UTF8.GetBytes(passwordToHash), PBKDF2.GenerateSalt(), numberOfRounds);

            sw.Stop();

            Console.WriteLine();
            Console.WriteLine("Password to hash: " + passwordToHash);
            Console.WriteLine("Hashed Password: " + Convert.ToBase64String(hashedPassword));
            Console.WriteLine("Iterations <" + numberOfRounds + "> Elapsed Time: " + sw.ElapsedMilliseconds + "ms");
        }
        static void Main(string[] args)
        {
            const string passwordToHash = "VovkDmytro";

            HashPassword(passwordToHash, 60000);
            HashPassword(passwordToHash, 110000);
            HashPassword(passwordToHash, 160000);
            HashPassword(passwordToHash, 210000);
            HashPassword(passwordToHash, 260000);
            HashPassword(passwordToHash, 310000);
            HashPassword(passwordToHash, 360000);
            HashPassword(passwordToHash, 410000);
            HashPassword(passwordToHash, 460000);
            HashPassword(passwordToHash, 510000);
            Console.ReadLine();
        }

       
    }
}