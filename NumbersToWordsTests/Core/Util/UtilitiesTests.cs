using System;
using System.Globalization;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NumbersToWords.Core.Services.Utility;

namespace NumbersToWordsTests.Core.Util
{
    [TestClass]
    public class UtilitiesTests
    {
        private UtilityService utils = new UtilityService();

        [TestMethod]
        public void CountDecimalDigits__CountsWithNoDecimalPlaces()
        {
            // arrange
            var testNumber = 1;

            // act
            var result = utils.CountDecimalDigits(testNumber);

            // assert:
            Assert.IsTrue(result == 0);
        }

        [TestMethod]
        public void CountDecimalDigits__CountsWithDecimalPlaces()
        {
            // arrange
            var testNumber = 1.0123000M;

            // act
            var result = utils.CountDecimalDigits(testNumber);

            // assert:
            Assert.IsTrue(result == 4);
        }

        [TestMethod]
        public void CountDecimalDigits__CountsLeadingZeros_IfSetToTrue()
        {
            // arrange
            var testNumber = 1.0123000M;

            // act
            var result = utils.CountDecimalDigits(testNumber, true);

            // assert:
            Assert.IsTrue(result == 7);
        }


        [TestMethod]
        public void GetWrappedWords__WrapsOnOverflowWord()
        {
            // arrange
            var testString = "test words here";

            // act
            var result = utils.GetWrappedWords(testString, 9);

            var before = string.Join(" ", result.BeforeWrap);
            var after = string.Join(" ", result.AfterWrap);

            // assert
            Assert.IsTrue(before == "test");
            Assert.IsTrue(after == "words here");


            // act
            var result2 = utils.GetWrappedWords(testString, 10);

            before = string.Join(" ", result2.BeforeWrap);
            after = string.Join(" ", result2.AfterWrap);

            // assert
            Assert.IsTrue(before == "test words");
            Assert.IsTrue(after == "here");


            // act
            var result3 = utils.GetWrappedWords(testString, 11);

            before = string.Join(" ", result3.BeforeWrap);
            after = string.Join(" ", result3.AfterWrap);

            // assert
            Assert.IsTrue(before == "test words");
            Assert.IsTrue(after == "here");
        }

        [TestMethod]
        public void GetWrappedWords__AllowsGreaterWrapCharacter()
        {
            // arrange
            var testString = "test words here";

            // act
            var result = utils.GetWrappedWords(testString, testString.Length + 1);

            var before = string.Join(" ", result.BeforeWrap);

            // assert
            Assert.IsTrue(before == "test words here");
        }
    }
}
