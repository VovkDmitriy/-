
using System;
using System.Collections.Generic;
using System.Security;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;


namespace Lab11
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
    class User
    {
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public byte[] Salt { get; set; }
        public string[] Roles { get; set; }
    }

    class Protector
    {
        private static Dictionary<string, User> _users = new Dictionary<string, User>();

        public static void Register(string userName, string password, string[] roles = null)
        {
            if (_users.ContainsKey(userName))
            {
                Console.WriteLine("Такой пользователь уже существует");
            }
            else
            {
                byte[] salt = PBKDF2.GenerateSalt();
                byte[] hashedPassword = PBKDF2.HashPasswordSHA256(Encoding.UTF8.GetBytes(password), salt, 5000);

                User new_user = new User();
                new_user.Login = userName;
                new_user.PasswordHash = Convert.ToBase64String(hashedPassword);
                new_user.Salt = salt;
                new_user.Roles = roles;
                _users.Add(userName, new_user);

                Console.WriteLine("Новый пользователь добавлен");
            }
        }

        public static bool CheckPassword(string userName, string password)
        {
            if (_users.ContainsKey(userName))
            {
                User user = _users[userName];
                byte[] hashedPassword = PBKDF2.HashPasswordSHA256(Encoding.UTF8.GetBytes(password), user.Salt, 5000);

                if (Convert.ToBase64String(hashedPassword) == user.PasswordHash)
                {
                    Console.WriteLine("This password is correct!");
                    return true;
                }
                else
                {
                    Console.WriteLine("This password is incorrect!");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("There is no registered user with this name!");
                return false;
            }
        }
        public static void LogIn(string userName, string password)
        {
            if (CheckPassword(userName, password))
            {
                var identity = new GenericIdentity(userName, "OIAuth");
                var principal = new GenericPrincipal(identity, _users[userName].Roles);
                System.Threading.Thread.CurrentPrincipal = principal;
            }
        }
        public static void OnlyForAdminsFeature()
        {
            if (Thread.CurrentPrincipal == null)
            {
                throw new SecurityException("Thread.CurrentPrincipal cannot be null.");
            }

            if (!Thread.CurrentPrincipal.IsInRole("Admins"))
            {
                throw new SecurityException("User must be a member of Admins to access this feature.");
            }

            Console.WriteLine("Вы авторизировались как администратор");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            int numUsers = 3;
            for (int i = 0; i < numUsers; i++)
            {
                Console.WriteLine("Введите логин: ");
                string login = Console.ReadLine();
                Console.WriteLine("Введите пароль: ");
                string password = Console.ReadLine();
                Console.WriteLine("Введите роли через запятую(,): ");
                string rolesString = Console.ReadLine();
                Regex sWhitespace = new Regex(@"\s+");
                string rolesWithoutSpaces = sWhitespace.Replace(rolesString, "");
                string[] roles = rolesWithoutSpaces.Split(',');
                Protector.Register(login, password, roles);
                Console.WriteLine();
            }
            Console.WriteLine("Готово");
            Console.WriteLine();
            int k = 1;
            while (k != 0) {
                Console.WriteLine("Вход");
                Console.WriteLine("Введите логин: ");
                string enteredLogin = Console.ReadLine();
                Console.WriteLine("Введите палоль: ");
                string enteredPassword = Console.ReadLine();
                if (Protector.CheckPassword(enteredLogin, enteredPassword))
                {
                    Protector.LogIn(enteredLogin, enteredPassword);

                    try
                    {
                        Protector.OnlyForAdminsFeature();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"{ex.GetType()}: {ex.Message}");
                    }
                }
                Console.WriteLine("Для выхода нажмите 0!");
                k = Convert.ToInt32(Console.ReadLine());
            }
            Console.ReadKey();
        }
    }
}
