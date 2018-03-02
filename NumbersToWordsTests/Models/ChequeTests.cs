using System;
using NumbersToWordsTests.TestUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using NumbersToWords.Models.Cheque;

namespace NumbersToWordsTests.Models
{
    [TestClass]
    public class ChequeTests
    {
        private Cheque validCheque = new Cheque()
        {
            Amount = 1.99M,
            Date = DateTime.Today,
            Name = "Test"
        };

        [TestMethod]
        public void Name_ShouldBeRequired()
        {
            // arrange
            var model = new Cheque()
            {
                Name = "",
                Date = validCheque.Date,
                Amount = validCheque.Amount
            };
            var model2 = validCheque;

            // act
            var results = ValidationHelper.ValidateModel(model);
            var results2 = ValidationHelper.ValidateModel(model2);

            // assert:
            Assert.IsTrue(results.Any(x => x.MemberNames.Contains("Name") && x.ErrorMessage.Contains("required")));
            Assert.IsFalse(results2.Any(x => x.MemberNames.Contains("Name") && x.ErrorMessage.Contains("required")));
        }

        [TestMethod]
        public void DateWritten_ShouldValidateMaxDate()
        {
            // arrange
            var model = new Cheque()
            {
                Name = validCheque.Name,
                Date = Cheque.MaxDate.AddDays(1),
                Amount = validCheque.Amount
            };
            var model2 = validCheque;

            // act
            var results = ValidationHelper.ValidateModel(model);
            var results2 = ValidationHelper.ValidateModel(model2);

            // assert:
            Assert.IsTrue(results.Any(x => x.MemberNames.Contains("DateWritten")));
            Assert.IsFalse(results2.Any(x => x.MemberNames.Contains("DateWritten")));
        }

        [TestMethod]
        public void DateWritten_ShouldValidateMinDate()
        {
            // arrange
            var model = new Cheque()
            {
                Name = validCheque.Name,
                Date = Cheque.MinDate.AddDays(-1),
                Amount = validCheque.Amount
            };
            var model2 = validCheque;

            // act
            var results = ValidationHelper.ValidateModel(model);
            var results2 = ValidationHelper.ValidateModel(model2);

            // assert:
            Assert.IsTrue(results.Any(x => x.MemberNames.Contains("DateWritten")));
            Assert.IsFalse(results2.Any(x => x.MemberNames.Contains("DateWritten")));
        }

        [TestMethod]
        public void Amount_ShouldValidateMaxValue()
        {
            // arrange
            var model = new Cheque()
            {
                Name = validCheque.Name,
                Date = validCheque.Date,
                Amount = Cheque.MaxAcceptedAmount + 1
            };
            var model2 = validCheque;

            // act
            var results = ValidationHelper.ValidateModel(model);
            var results2 = ValidationHelper.ValidateModel(model2);

            // assert:
            Assert.IsTrue(results.Any(x => x.MemberNames.Contains("Amount")));
            Assert.IsFalse(results2.Any(x => x.MemberNames.Contains("Amount")));
        }

        [TestMethod]
        public void Amount_ShouldValidateMinValue()
        {
            // arrange
            var model = new Cheque()
            {
                Name = validCheque.Name,
                Date = validCheque.Date,
                Amount = (decimal)Cheque.MinAcceptedAmount - 1
            };
            var model2 = validCheque;

            // act
            var results = ValidationHelper.ValidateModel(model);
            var results2 = ValidationHelper.ValidateModel(model2);

            // assert:
            Assert.IsTrue(results.Any(x => x.MemberNames.Contains("Amount")));
            Assert.IsFalse(results2.Any(x => x.MemberNames.Contains("Amount")));
        }

        [TestMethod]
        public void Amount_ShouldValidateDecimalPlaces()
        {
            // arrange
            var model = new Cheque()
            {
                Name = validCheque.Name,
                Date = validCheque.Date,
                Amount = 1.999M
            };
            var model2 = validCheque;

            // act
            var results = ValidationHelper.ValidateModel(model);
            var results2 = ValidationHelper.ValidateModel(model2);

            // assert:
            Assert.IsTrue(results.Any(x => x.MemberNames.Contains("Amount")));
            Assert.IsFalse(results2.Any(x => x.MemberNames.Contains("Amount")));
        }
    }
} 
