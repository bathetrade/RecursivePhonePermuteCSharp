using System;

namespace RecursivePhonePermute
{
    public class Phone
    {
        private static readonly string[] _letterGroups = new string[] { "abc", "def", "ghi", "jkl", "mno", "pqrs", "tuv", "wxyz" };

        private int _numberOfLetterGroups;
        private string _phoneNumbers;

        private static bool OutOfRange(uint digit)
        {
            // 0 and 1 are not associated with letters on a typical phone keypad.
            return digit < 2 || digit > 9;
        }

        private static void AssertDigitIsConvertibleToLetterGroup(uint digit)
        {
            if (OutOfRange(digit))
            {
                throw new ArgumentOutOfRangeException("Only the numbers 2-9 are associated with letters on a typical phone keypad.");
            }
        }

        private static bool PhoneNumbersConvertibleToLetterGroups(string phoneNumbers)
        {
            if (String.IsNullOrEmpty(phoneNumbers))
            {
                return false;
            }

            // Make sure each character is a number that is in range.
            foreach (char number in phoneNumbers)
            {
                if (!uint.TryParse(number.ToString(), out uint iNumber) || OutOfRange(iNumber))
                {
                    return false;
                }
            }

            return true;
        }

        /*  Terminology:
        Assume the user enters "258". These numbers on a phone correspond to the 
        letter groups "abc", "jkl", and "tuv", respectively.
        "abc" has a letterGroupIndex of 0, "jkl" has a letterGroupIndex of 1, etc.
        
        Algorithm explanation:
        The permutation algorithm runs like a nested loop, in the sense that the innermost loops
        run more frequently than outer loops. Letter groups with a higher index are 
        analogous to inner loops.
        
        For instance, in the example above with an input of "258", the output would be:
        
        ajt
        aju
        ajv
        akt
        aku
        akv
        
        ...and so on. Notice that the last letter group "tuv" cycles more frequently
        than the second, and the second cycles more frequently than the first.
        */
        private bool RecursivePermute(char[] permutation, int letterGroupIndex)
        {
            var finished = (letterGroupIndex == _numberOfLetterGroups);
            if (finished)
            {
                return false;
            }

            var currentDigit = uint.Parse(_phoneNumbers[letterGroupIndex].ToString());
            string currentLetterGroup = GetLetterGroup(currentDigit);

            foreach(char letter in currentLetterGroup)
            {
                permutation[letterGroupIndex] = letter;
                if (!RecursivePermute(permutation, letterGroupIndex + 1))
                {
                    var permutationString = new String(permutation);
                    OnEachPermutation?.Invoke(permutationString);
                }
            }

            return true;
        }

        /// <summary>
        /// Converts digit on a phone's keypad to a letter group. For example, 2 would map to the letter group "abc".
        /// </summary>
        /// <param name="digit">A uint between 2 and 9. 0 and 1 do not map to any letters on a typical phone keypad. If the digit is invalid, an ArgumentOutOfRangeException is thrown.</param>
        /// <returns>A letter group. For example, the digit 5 would return "jkl".</returns>
        public static string GetLetterGroup(uint digit)
        {
            AssertDigitIsConvertibleToLetterGroup(digit);
            return _letterGroups[digit - 2];
        }

        /// <summary>
        /// <para>Converts each digit in phoneNumbers to its corresponding letter group. Then, permutes all combinations of letters by selecting one letter from each group.</para>
        /// <para>For example, "23" maps to "abc" and "def" respectively. Therefore, the permutations would be:</para>
        /// 
        /// <para>
        /// ad
        /// ae
        /// af
        /// bd
        /// be
        /// bf
        /// cd
        /// ce
        /// cf
        /// </para>
        /// 
        /// The OnEachPermutation event is fired on each permutation.
        /// </summary>
        /// <param name="phoneNumbers">A string consisting of the digits 2 through 9. An ArgumentException will be thrown if phoneNumbers is empty, null, or one of its characters is not a digit between 2 and 9.</param>
        public void PermutePhoneNumberLetterGroups(string phoneNumbers)
        {
            if (!PhoneNumbersConvertibleToLetterGroups(phoneNumbers))
            {
                throw new ArgumentException($"Each character in {nameof(phoneNumbers)} must be a number between 2 and 9. Also, {nameof(phoneNumbers)} cannot be null or empty.");
            }

            _phoneNumbers = phoneNumbers;
            _numberOfLetterGroups = _phoneNumbers.Length;
            var permutation = new char[_numberOfLetterGroups];
            RecursivePermute(permutation, 0);
        }

        public event Action<string> OnEachPermutation;
    }
}
