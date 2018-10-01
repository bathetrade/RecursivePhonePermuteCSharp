using RecursivePhonePermute;
using System;

namespace RecursivePhonePermutationConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This is a program that takes in a series of digits from a phone keypad, and recursively permutes all possible letter combinations associated with those digits." +
                "For example, the numbers '2' and '5' are associated with the letters 'abc' and 'jkl', respectively. Therefore, the permutations would be 'aj', 'ak', 'al', 'bj', 'bk', 'bl', 'cj', 'ck', 'cl'.");
            Console.WriteLine();
            Console.WriteLine("Enter a string of numbers between 2 and 9.");

            var phoneNumbers = Console.ReadLine();

            var phone = new Phone();
            var firstPermutation = true;
            phone.OnEachPermutation += (permutation) =>
            {
                if (!firstPermutation)
                {
                    Console.Write(", ");
                }
                Console.Write(permutation);
                firstPermutation = false;
            };

            try
            {
                Console.WriteLine();
                phone.PermutePhoneNumberLetterGroups(phoneNumbers);
                Console.WriteLine();
                Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }
        }
    }
}
