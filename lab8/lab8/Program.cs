using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;


namespace lab8
{
    class Program
    {

        private readonly static string CspContainerName = "container";
        public static void AssignNewKey(string publicKeyPath)
        {
            CspParameters cspParameters = new CspParameters(1)
            {
                KeyContainerName = CspContainerName,

                ProviderName = "Microsoft Strong Cryptographic Provider"
            };
            var rsa = new RSACryptoServiceProvider(cspParameters)
            {
                PersistKeyInCsp = true
            };
            File.WriteAllText(publicKeyPath, rsa.ToXmlString(false));
        }
        public static byte[] DecryptData(byte[] dataToDecrypt)
        {
            byte[] plainBytes;
            var cspParameters = new CspParameters
            {
                KeyContainerName = CspContainerName,

            };
            using (var rsa = new RSACryptoServiceProvider(cspParameters))
            {
                rsa.PersistKeyInCsp = true;
                plainBytes = rsa.Decrypt(dataToDecrypt, false);
            }
            return plainBytes;
        }
        public static byte[] EncryptData(string publicKeyPath, byte[] dataToEncrypt)
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
        static void Main(string[] args)
        {

            string otherPublicKey = "MaksymovIvan.xml";
            string otherMessage = "myMassage.dat";
            string original = "Vovk Dmytro";
            int i = 5;
            while(i > 0)
            {
                Console.WriteLine("Press 1 to create a key");
                Console.WriteLine("Press 2 to encrypt message");
                Console.WriteLine("Press 3 to dencrypt message");
                Console.WriteLine("Press 0 to exit");
                int k = Convert.ToInt32(Console.ReadLine());

                if (k == 0) { Console.WriteLine("Press Enter to close app"); Console.ReadKey(); break; }
                else if (k == 1)
                {
                    AssignNewKey("publicVovk.xml");
                    Console.WriteLine("The key has been successfully created!");
                    Console.ReadKey();
                }
                else if (k == 2)
                {
                    var encrypted = EncryptData(otherPublicKey, Encoding.Unicode.GetBytes(original));
                    File.WriteAllBytes("MyMessage.dat", encrypted);
                    Console.WriteLine("Encrypted message = " + Convert.ToBase64String(encrypted));
                    Console.ReadKey();
                    Console.WriteLine();
                }
                else if (k==3)
                {
                    byte[] data = File.ReadAllBytes(otherMessage).ToArray();
                    var decrypted = DecryptData(data);
                    string dat = BitConverter.ToString(data);
                    Console.WriteLine("Message = " + dat);
                    Console.WriteLine();
                    Console.WriteLine("Decrypted message = " + Encoding.Unicode.GetString(decrypted));
                    Console.ReadKey();
                }
                else { Console.WriteLine("Please enter a valid value!"); }
            }
        }
    }
}
