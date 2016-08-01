using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace CryptographicAlgorithms
{
    public class Program
    {
        /// <summary>
        /// Measures the time it takes to compute cryptographic HASH algorithm
        /// </summary>
        /// <param name="description"> The name of the cryptographic HASH algorithm </param>
        /// <param name="iterations">  Number of times the cryptographic  HASH algorithm will be run </param>
        /// <param name="func"> Encapsulates a method which will be used to calculate cryptographic HASH algorithm </param>
        static void TimeAction(string description, int iterations, Action func)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            for (int i = 0; i < iterations; i++)
            {
                func();
            }
            watch.Stop();
            Console.WriteLine($"{description} Time Elapsed {watch.ElapsedMilliseconds} ms");
            Console.WriteLine($"{description} Elapsed Ticks {watch.ElapsedTicks} ticks");
            Console.WriteLine();
        }

        /// <summary>
        /// Generate a cryptographically strong Guid
        /// </summary>
        /// <returns> A random Guid </returns>
        private static Guid GenerateNewGuid()
        {
            byte[] guidBytes = new byte[16]; // Guids are 16 bytes long
            RandomNumberGenerator random = RandomNumberGenerator.Create();
            random.GetBytes(guidBytes);
            return new Guid(guidBytes);
        }

        /// <summary>
        /// Gets an unique string at given lenght
        /// </summary>
        /// <param name="maxSize"> The lenght of your random unique string </param>
        /// <returns> Randomized unique string </returns>
        private static string GetUniqueKey(int maxSize)
        {
            char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890~`!@#$%^&*()_+".ToCharArray();
            byte[] data = new byte[1];
            RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(data);
            data = new byte[maxSize];
            randomNumberGenerator.GetBytes(data);
            StringBuilder result = new StringBuilder(maxSize);
            foreach (byte b in data)
            {
                result.Append(chars[b % chars.Length]);
            }
            return result.ToString();
        }

        public static void Main(string[] args)
        {
            // Create an instance of the default implementation of cryptographic HASH algorithm
            MD5 md5 = MD5.Create();
            SHA1 sha1 = SHA1.Create();
            SHA256 sha256 = SHA256.Create();
            SHA384 sha384 = SHA384.Create();
            SHA512 sha512 = SHA512.Create();
            // Set cryptographic HASH algorithm dictionary
            Dictionary<string, HashAlgorithm> algorithms = new Dictionary<string, HashAlgorithm>
            {
                ["md5"] = md5,
                ["sha1"] = sha1,
                ["sha256"] = sha256,
                ["sha384"] = sha384,
                ["sha512"] = sha512
            };
            // Create Password and Salt, use user size defined
            string pwd = GetUniqueKey(25);
            string salt = GetUniqueKey(10);
            Console.WriteLine($"pwd => {pwd}");
            Console.WriteLine($"salt=> {salt}");
            Console.WriteLine();
            Console.WriteLine($"Password lenght => {pwd.Length}, Salt lenght => {salt.Length} combined => {pwd.Length + salt.Length}");
            Console.WriteLine();
            // Source for HASH compute
            byte[] source = Encoding.UTF8.GetBytes(pwd + salt);
            // Prints out the HASH lenght for each cryptographic HASH algorithm along with the hash string
            foreach (KeyValuePair<string, HashAlgorithm> keyValuePair in algorithms)
            {
                Console.WriteLine($"{keyValuePair.Key} is {pair.Value.ComputeHash(source).Length} bytes => {Convert.ToBase64String(keyValuePair.Value.ComputeHash(source))}");
                Console.WriteLine();
            }
            // Number of iterations
            const int iterations = 1000;
            Console.WriteLine($"Iterations => {iterations}");
            Console.WriteLine();
            // Runs thro each cryptographic HASH algorithm and prints out the time it took to compute X hash computations
            foreach (KeyValuePair<string, HashAlgorithm> keyValuePair in algorithms)
            {
                TimeAction(keyValuePair.Key + " calculation", iterations, () => { keyValuePair.Value.ComputeHash(source); });
            }
            Console.ReadLine();
        }
    }
}
