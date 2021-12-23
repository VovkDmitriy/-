using System;
using System.Security.Cryptography;
using System.Text;

namespace exam
{
    class Program
    {
        private RSAParameters _publicKey;
        private RSAParameters _privateKey;

        public void AssignNewKey()
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                _publicKey = rsa.ExportParameters(false);
                _privateKey = rsa.ExportParameters(true);
            }
        }
        public byte[] EncryptData(byte[] dataToEncrypt)
        {
            byte[] cipherbytes;
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.PersistKeyInCsp = false;
                rsa.ImportParameters(_publicKey);
                cipherbytes = rsa.Encrypt(dataToEncrypt, true);
            }
            return cipherbytes;
        }
        public byte[] DecryptData(byte[] dataToEncrypt)
        {
            byte[] plain;
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.PersistKeyInCsp = false;
                rsa.ImportParameters(_privateKey);
                plain = rsa.Decrypt(dataToEncrypt, true);
            }
            return plain;
        }
        static void Main(string[] args)
        {

            var rsaParams = new Program();
            const string original = "Exam done";
            Console.WriteLine("Original text: " + original);

            //генерація ключів
            rsaParams.AssignNewKey();

            //Зашифрування
            var encrypted = rsaParams.EncryptData(Encoding.UTF8.GetBytes(original));
            Console.WriteLine("Encrypted: " + Convert.ToBase64String(encrypted));

            //Розшифруваня
            var decrypted = rsaParams.DecryptData(encrypted);
            Console.WriteLine("Decrypted: " + Encoding.Default.GetString(decrypted));
            Console.ReadKey();
        }
    }
}
