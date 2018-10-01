using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RecursivePhonePermute;

namespace RecursivePhonePermutationTests
{
    [TestClass]
    public class PhoneTests
    {
        private void TestPermutation(string phoneNumbers, ISet<string> expectedPermutations)
        {
            var permutations = new HashSet<string>();
            var phone = new Phone();
            phone.OnEachPermutation += (permutation) =>
            {
                permutations.Add(permutation);
            };
            phone.PermutePhoneNumberLetterGroups(phoneNumbers);
            if (expectedPermutations != null)
            {
                Assert.IsTrue(expectedPermutations.SetEquals(permutations));
            }
        }

        [TestMethod]
        public void PhoneShouldPermute258Correctly()
        {
            var phoneNumbers = "258";
            var expectedPermutations = new HashSet<string>() {
                "ajt", "aju", "ajv", "akt", "aku", "akv", "alt", "alu", "alv",
                "bjt", "bju", "bjv", "bkt", "bku", "bkv", "blt", "blu", "blv",
                "cjt", "cju", "cjv", "ckt", "cku", "ckv", "clt", "clu", "clv"
            };
            TestPermutation(phoneNumbers, expectedPermutations);
        }

        [TestMethod]
        public void PhoneShouldPermute2Correctly()
        {
            var phoneNumber = "2";
            var expectedPermutations = new HashSet<string>() { "a", "b", "c" };
            TestPermutation(phoneNumber, expectedPermutations);
        }

        [TestMethod]
        public void PhoneShouldPermute9Correctly()
        {
            var phoneNumber = "9";
            var expectedPermutations = new HashSet<string>() { "w", "x", "y", "z" };
            TestPermutation(phoneNumber, expectedPermutations);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Trying to permute 1 should throw an exception")]
        public void Phone1ShouldBeOutOfRange()
        {
            var phoneNumber = "1";
            TestPermutation(phoneNumber, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Trying to permute 0 should throw an exception")]
        public void Phone0ShouldBeOutOfRange()
        {
            var phoneNumber = "0";
            TestPermutation(phoneNumber, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Trying to permute an empty string should throw an exception")]
        public void PhoneEmptyPhoneNumberShouldBeOutOfRange()
        {
            var phoneNumber = "";
            TestPermutation(phoneNumber, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Trying to permute an empty string should throw an exception")]
        public void PhoneNullPhoneNumberShouldBeOutOfRange()
        {
            TestPermutation(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Trying to permute non-numeric string should throw an exception")]
        public void PhonePhoneNumberWithLettersInItShouldBeOutOfRange()
        {
            var phoneNumber = "ksdjhf";
            TestPermutation(phoneNumber, null);
        }
    }
}
