using System;
using System.Security.Cryptography;
using System.Text;
namespace Lab4_2
{
    class Program
    {
        static byte[] ComputeHashMd5(byte[] data)
        {
            using (var md5 = MD5.Create())
            {
                return md5.ComputeHash(data);
            }

        }
        static void Main(string[] args)
        {
            Guid guid1 = new Guid("564c8da6-0440-88ec-d453-0bbad57c6036");
            string a = "po1MVkAE7IjUUwu61XxgNg==";
            string Hash;
            for (int i = 1; i <= 999999999; i++)
            {
                string pass = i.ToString();
                for (int q = pass.Length; q < 8; q++)
                {
                    pass = "0" + pass;
                }
                Hash = Convert.ToBase64String(ComputeHashMd5(Encoding.Unicode.GetBytes(pass)));
                if (Hash == a)
                {
                    Console.WriteLine(Hash);
                    Console.WriteLine(pass);
                    break;
                }
                Console.WriteLine(pass);
            }
            Console.ReadKey();
        }
    }
}
