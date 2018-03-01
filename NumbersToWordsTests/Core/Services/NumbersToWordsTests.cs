using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NumbersToWords.Core.Services.NumbersToWords;

namespace NumbersToWordsTests.Core.Services
{
    [TestClass]
    public class NumbersToWordsTests
    {
        private NumbersToWordsService _service = new NumbersToWordsService();

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void ConvertDecimalToPriceString__NegativeNumberThrowsException()
        {
            // arrange
            var test = -0.01M;

            // act
            var result = _service.ConvertDecimalToPriceString(test);
        }

        [TestMethod]
        public void ConvertDecimalToPriceString__JustDollars()
        {
            // arrange
            var test = 2.0M;

            // act
            var result = _service.ConvertDecimalToPriceString(test);

            // assert
            Assert.IsTrue(result == "two dollars and zero cents");
        }

        [TestMethod]
        public void ConvertDecimalToPriceString__JustCents()
        {
            // arrange
            var test = .2M;

            // act
            var result = _service.ConvertDecimalToPriceString(test);

            // assert
            Assert.IsTrue(result == "zero dollars and twenty cents");
        }

        [TestMethod]
        public void ConvertDecimalToPriceString__DollarsAndCents()
        {
            // arrange
            var test = 02.20M;

            // act
            var result = _service.ConvertDecimalToPriceString(test);

            // assert
            Assert.IsTrue(result == "two dollars and twenty cents");
        }

        [TestMethod]
        public void ConvertDecimalToPriceString__PluralisesDollars()
        {
            // arrange
            var test = 1M;
            var test2 = 2M;
            var test3 = 10M;

            // act
            var result = _service.ConvertDecimalToPriceString(test);
            var result2 = _service.ConvertDecimalToPriceString(test2);
            var result3 = _service.ConvertDecimalToPriceString(test3);

            // assert
            Assert.IsTrue(result == "one dollar and zero cents");
            Assert.IsTrue(result2 == "two dollars and zero cents");
            Assert.IsTrue(result3 == "ten dollars and zero cents");
        }

        [TestMethod]
        public void ConvertDecimalToPriceString__PluralisesCents()
        {
            // arrange
            var test = 0.01M;
            var test2 = 0.02M;
            var test3 = 0.1M;

            // act
            var result = _service.ConvertDecimalToPriceString(test);
            var result2 = _service.ConvertDecimalToPriceString(test2);
            var result3 = _service.ConvertDecimalToPriceString(test3);

            // assert
            Assert.IsTrue(result == "zero dollars and one cent");
            Assert.IsTrue(result2 == "zero dollars and two cents");
            Assert.IsTrue(result3 == "zero dollars and ten cents");
        }       


        [TestMethod]
        public void GetIntegerAndFractionalNamesFromDecimal__ReturnsZeroFractionalPartWhenNoDecimal()
        {
            // arrange
            decimal test = 15M;

            // act
            var result = _service.GetIntegerAndFractionalNamesFromDecimal(test);

            // assert
            Assert.IsTrue(!String.IsNullOrEmpty(result.Item1));
            Assert.IsTrue(result.Item2 == "zero");
        }

        [TestMethod]
        public void GetIntegerAndFractionalNamesFromDecimal__ReturnsFractionalPartWithDecimal()
        {
            // arrange
            decimal test = 15.5M;

            // act
            var result = _service.GetIntegerAndFractionalNamesFromDecimal(test);

            // assert
            Assert.IsFalse(String.IsNullOrEmpty(result.Item1));
            Assert.IsFalse(String.IsNullOrEmpty(result.Item2));
        }

        [TestMethod]
        public void GetIntegerAndFractionalNamesFromDecimal__ReturnsTwoPartFractional()
        {
            // arrange
            decimal test = 15.10M;

            // act
            var result = _service.GetIntegerAndFractionalNamesFromDecimal(test);

            // assert
            Assert.IsTrue(result.Item2 == "ten");            
        }

        [TestMethod]
        public void GetIntegerAndFractionalNamesFromDecimal__NegativeNumbersAddsMinus()
        {
            // arrange
            decimal test = -15.10M;

            // act
            var result = _service.GetIntegerAndFractionalNamesFromDecimal(test);

            // assert
            Assert.IsTrue(result.Item1 == "minus fifteen");
            Assert.IsTrue(result.Item2 == "ten");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Min value exceeded")]
        public void TurnNumberIntoWords__MinValueThrowsException()
        {
            // arrange
            var test = int.MinValue;

            // act
            var result = _service.ConvertIntToStringRepresentation(test);
        }

        // check a range of outputs
        [TestMethod]
        public void TurnNumberIntoWords__SelectiveTests()
        {
            // arrange
            var numbers = new List<Tuple<int, string>>() {                
                new Tuple<int, string>(-5678, "minus five thousand six hundred and seventy eight"),
                new Tuple<int, string>(-0, "zero"),
                new Tuple<int, string>(0, "zero"),
                new Tuple<int, string>(1, "one"),
                new Tuple<int, string>(2, "two"),
                new Tuple<int, string>(3, "three"),
                new Tuple<int, string>(4, "four"),
                new Tuple<int, string>(5, "five"),
                new Tuple<int, string>(6, "six"),
                new Tuple<int, string>(7, "seven"),
                new Tuple<int, string>(8, "eight"),
                new Tuple<int, string>(9, "nine"),
                new Tuple<int, string>(10, "ten"),
                new Tuple<int, string>(11, "eleven"),
                new Tuple<int, string>(12, "twelve"),
                new Tuple<int, string>(13, "thirteen"),
                new Tuple<int, string>(14, "fourteen"),
                new Tuple<int, string>(15, "fifteen"),
                new Tuple<int, string>(16, "sixteen"),
                new Tuple<int, string>(17, "seventeen"),
                new Tuple<int, string>(18, "eighteen"),
                new Tuple<int, string>(19, "nineteen"),
                new Tuple<int, string>(20, "twenty"),
                new Tuple<int, string>(21, "twenty one"),
                new Tuple<int, string>(99, "ninety nine"),
                new Tuple<int, string>(100, "one hundred"),
                new Tuple<int, string>(999, "nine hundred and ninety nine"),
                new Tuple<int, string>(1000, "one thousand"),
                new Tuple<int, string>(9999, "nine thousand nine hundred and ninety nine"),
                new Tuple<int, string>(10000, "ten thousand"),
                new Tuple<int, string>(99999, "ninety nine thousand nine hundred and ninety nine"),
                new Tuple<int, string>(100000, "one hundred thousand"),
                new Tuple<int, string>(999999, "nine hundred and ninety nine thousand nine hundred and ninety nine"),
                new Tuple<int, string>(1000000, "one million"),
                new Tuple<int, string>(9999999, "nine million nine hundred and ninety nine thousand nine hundred and ninety nine"),
                new Tuple<int, string>(10000000, "ten million"),
                new Tuple<int, string>(99999999, "ninety nine million nine hundred and ninety nine thousand nine hundred and ninety nine"),
                new Tuple<int, string>(100000000, "one hundred million"),
                new Tuple<int, string>(999999999, "nine hundred and ninety nine million nine hundred and ninety nine thousand nine hundred and ninety nine"),
                new Tuple<int, string>(1000000000, "one billion"),
                new Tuple<int, string>(int.MaxValue, "two billion one hundred and forty seven million four hundred and eighty three thousand six hundred and forty seven")
            };

            foreach (var number in numbers)
            {
                // act
                var result = _service.ConvertIntToStringRepresentation(number.Item1);

                // assert
                Assert.IsTrue(result == number.Item2);
            }
        }
    }
}
