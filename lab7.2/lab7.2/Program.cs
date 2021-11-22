using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace lab7._2
{
    class Program
    {
        public void AssignNewKey(string publicKeyPath = "public.xml", string privateKeyPath = "private.xml")
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                File.WriteAllText(publicKeyPath, rsa.ToXmlString(false));
                File.WriteAllText(privateKeyPath, rsa.ToXmlString(true));
            }
        }

        public byte[] EncryptData(byte[] dataToEncrypt, string publicKeyPath = "public.xml")
        {
            byte[] cipherbytes;
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                rsa.FromXmlString(File.ReadAllText(publicKeyPath));
                cipherbytes = rsa.Encrypt(dataToEncrypt, false);
            }
            return cipherbytes;
        }
        public byte[] DecryptData(byte[] dataToEncrypt, string privateKeyPath = "private.xml")
        {
            byte[] plain;
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                rsa.FromXmlString(File.ReadAllText(privateKeyPath));
                plain = rsa.Decrypt(dataToEncrypt, false);
            }
            return plain;
        }
        static void Main(string[] args)
        {
            var rsaParams = new Program();
            const string original = "Dmytro Vovk";
            rsaParams.AssignNewKey();
            var encrypted = rsaParams.EncryptData(Encoding.UTF8.GetBytes(original));
            var decrypted = rsaParams.DecryptData(encrypted);
            Console.WriteLine("Text: " + original);
            Console.WriteLine("Encrypted: " + Convert.ToBase64String(encrypted));
            Console.WriteLine("Decrypted: " + Encoding.Default.GetString(decrypted));
            Console.ReadKey();
        }
    }
}