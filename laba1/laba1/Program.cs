using System;
using System.Security.Cryptography;

namespace laba1
{
    class Program
    {
            static byte[] func(int length = 10)
            {
                var rng = new RNGCryptoServiceProvider();
                var rnd = new byte[length];
                rng.GetBytes(rnd);
                return rnd;
            }

            static void Main(string[] args)
            {
                for (int j = 0; j < 2; j++)
                {
                    Console.WriteLine("-------------New random------------------");
                    Random rnd = new Random(245);
                    for (int i = 0; i < 10; i++)
                    {
                        Console.WriteLine(rnd.Next(0, 100));
                    }

                }

                Console.WriteLine("------------------next task-------------------------");
                for (int i = 0; i < 4; i++)
                {
                    string text = Convert.ToBase64String(func());
                    Console.WriteLine(text);
                }
                Console.ReadKey();

            }
        
    }
}
