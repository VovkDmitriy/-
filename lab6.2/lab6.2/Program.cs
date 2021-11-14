using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace lab6._2
{
    public class aesChipher
    {
        public byte[] GenerateRandomNumber(int length)
        {
            using (var randomNumberGenerator = new RNGCryptoServiceProvider())
            {
                var randomNumber = new byte[length];
                randomNumberGenerator.GetBytes(randomNumber);
                return randomNumber;
            }
        }
        public byte[] Encrypt(byte[] dataToEncrypt, byte[] key, byte[] iv)
        {
            using (var aes = new AesCryptoServiceProvider())
            {
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = key;
                aes.IV = iv;
                using (var memoryStream = new MemoryStream())
                {
                    var CryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
                    CryptoStream.Write(dataToEncrypt, 0, dataToEncrypt.Length);
                    CryptoStream.FlushFinalBlock();
                    return memoryStream.ToArray();
                }
            }
        }


        public byte[] Decrypt(byte[] dataToDecrypt, byte[] key, byte[] iv)
        {
            using (var aes = new AesCryptoServiceProvider())
            {
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = key;
                aes.IV = iv;
                using (var memoryStream = new MemoryStream())
                {
                    var CryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Write);
                    CryptoStream.Write(dataToDecrypt, 0, dataToDecrypt.Length);
                    CryptoStream.FlushFinalBlock();
                    return memoryStream.ToArray();
                }
            }
        }
    }

    public class desChipher
    {
        public byte[] GenerateRandomNumber(int length)
        {
            using (var randomNumberGenerator = new RNGCryptoServiceProvider())
            {
                var randomNumber = new byte[length];
                randomNumberGenerator.GetBytes(randomNumber);
                return randomNumber;
            }
        }
        public byte[] Encrypt(byte[] dataToEncrypt, byte[] key, byte[] iv)
        {
            using (var des = new DESCryptoServiceProvider())
            {
                des.Mode = CipherMode.CBC;
                des.Padding = PaddingMode.PKCS7;
                des.Key = key;
                des.IV = iv;
                using (var memoryStream = new MemoryStream())
                {
                    var CryptoStream = new CryptoStream(memoryStream, des.CreateEncryptor(), CryptoStreamMode.Write);
                    CryptoStream.Write(dataToEncrypt, 0, dataToEncrypt.Length);
                    CryptoStream.FlushFinalBlock();
                    return memoryStream.ToArray();
                }
            }
        }


        public byte[] Decrypt(byte[] dataToDecrypt, byte[] key, byte[] iv)
        {
            using (var des = new DESCryptoServiceProvider())
            {
                des.Mode = CipherMode.CBC;
                des.Padding = PaddingMode.PKCS7;
                des.Key = key;
                des.IV = iv;
                using (var memoryStream = new MemoryStream())
                {
                    var CryptoStream = new CryptoStream(memoryStream, des.CreateDecryptor(), CryptoStreamMode.Write);
                    CryptoStream.Write(dataToDecrypt, 0, dataToDecrypt.Length);
                    CryptoStream.FlushFinalBlock();
                    return memoryStream.ToArray();
                }
            }
        }
    }


    public class tripleDesChipher
    {
        public byte[] GenerateRandomNumber(int length)
        {
            using (var randomNumberGenerator = new RNGCryptoServiceProvider())
            {
                var randomNumber = new byte[length];
                randomNumberGenerator.GetBytes(randomNumber);
                return randomNumber;
            }
        }
        public byte[] Encrypt(byte[] dataToEncrypt, byte[] key, byte[] iv)
        {
            using (var tripleDes = new TripleDESCryptoServiceProvider())
            {
                tripleDes.Mode = CipherMode.CBC;
                tripleDes.Padding = PaddingMode.PKCS7;
                tripleDes.Key = key;
                tripleDes.IV = iv;
                using (var memoryStream = new MemoryStream())
                {
                    var CryptoStream = new CryptoStream(memoryStream, tripleDes.CreateEncryptor(), CryptoStreamMode.Write);
                    CryptoStream.Write(dataToEncrypt, 0, dataToEncrypt.Length);
                    CryptoStream.FlushFinalBlock();
                    return memoryStream.ToArray();
                }
            }
        }


        public byte[] Decrypt(byte[] dataToDecrypt, byte[] key, byte[] iv)
        {
            using (var tripleDes = new DESCryptoServiceProvider())
            {
                tripleDes.Mode = CipherMode.CBC;
                tripleDes.Padding = PaddingMode.PKCS7;
                tripleDes.Key = key;
                tripleDes.IV = iv;
                using (var memoryStream = new MemoryStream())
                {
                    var CryptoStream = new CryptoStream(memoryStream, tripleDes.CreateDecryptor(), CryptoStreamMode.Write);
                    CryptoStream.Write(dataToDecrypt, 0, dataToDecrypt.Length);
                    CryptoStream.FlushFinalBlock();
                    return memoryStream.ToArray();
                }
            }
        }
    }

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

        public static byte[] HashPasswordSHA256(byte[] toBeHashed, byte[] salt, int numberOfRounds, int length)
        {
            using (var rfc2898 = new Rfc2898DeriveBytes(toBeHashed, salt, numberOfRounds, HashAlgorithmName.SHA256))
            {
                return rfc2898.GetBytes(length);
            }
        }
    }


    class Program
    {


        static void Main(string[] args)
        {

            Random randomKey = new Random(1234);
            Random randomIV = new Random(5678);

            int Key = randomKey.Next(100000, 1000000);
            Console.WriteLine("Pseudo-random key: " + Key);

            int IV = randomIV.Next(100000, 1000000);
            Console.WriteLine("Pseudo-random IV: " + IV);

            byte[] salt = PBKDF2.GenerateSalt();

            int numberOfRounds = 60000;
            const string text = "VovkDmytro";
            var tripleDes = new desChipher();
            var des = new desChipher();
            var aes = new aesChipher();
            Console.WriteLine();
            var keyTripleDes = PBKDF2.HashPasswordSHA256(BitConverter.GetBytes(Key), salt, numberOfRounds, 8);
            var ivTripleDes = PBKDF2.HashPasswordSHA256(BitConverter.GetBytes(IV), salt, numberOfRounds, 8);
            var encryptedTripleDes = tripleDes.Encrypt(Encoding.UTF8.GetBytes(text), keyTripleDes, ivTripleDes);
            var decryptedTripleDes = tripleDes.Decrypt(encryptedTripleDes, keyTripleDes, ivTripleDes);
            var decryptedMessageTripleDes = Encoding.UTF8.GetString(decryptedTripleDes);
            Console.WriteLine("Triple DES Encryption");
            Console.WriteLine("Original Text = " + text);
            Console.WriteLine("Encrypted Text = " + Convert.ToBase64String(encryptedTripleDes));
            Console.WriteLine("Decrypted Text = " + decryptedMessageTripleDes);
            Console.WriteLine();
            var keyDes = PBKDF2.HashPasswordSHA256(BitConverter.GetBytes(Key), salt, numberOfRounds, 8);
            var ivDes = PBKDF2.HashPasswordSHA256(BitConverter.GetBytes(IV), salt, numberOfRounds, 8);
            var encryptedDes = des.Encrypt(Encoding.UTF8.GetBytes(text), keyDes, ivDes);
            var decryptedDes = des.Decrypt(encryptedDes, keyDes, ivDes);
            var decryptedMessageDes = Encoding.UTF8.GetString(decryptedDes);
            Console.WriteLine("DES Encryption");
            Console.WriteLine("Original Text = " + text);
            Console.WriteLine("Encrypted Text = " + Convert.ToBase64String(encryptedDes));
            Console.WriteLine("Decrypted Text = " + decryptedMessageDes);
            Console.WriteLine();
            var key = PBKDF2.HashPasswordSHA256(BitConverter.GetBytes(Key), salt, numberOfRounds, 32);
            var iv = PBKDF2.HashPasswordSHA256(BitConverter.GetBytes(IV), salt, numberOfRounds, 16);
            var encrypted = aes.Encrypt(Encoding.UTF8.GetBytes(text), key, iv);
            var decrypted = aes.Decrypt(encrypted, key, iv);
            var decryptedMessage = Encoding.UTF8.GetString(decrypted);
            Console.WriteLine("AES Encryption");
            Console.WriteLine("Original Text = " + text);
            Console.WriteLine("Encrypted Text = " + Convert.ToBase64String(encrypted));
            Console.WriteLine("Decrypted Text = " + decryptedMessage);
            Console.ReadKey();
        }
    }
}