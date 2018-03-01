using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NumbersToWords.Core.Services.ChequeWriter;
using Moq;
using NumbersToWords.Core.Services.Utility;
using NumbersToWords.Core.Services.NumbersToWords;
using NumbersToWords.Models.Cheque;
using NumbersToWords.Core.Entities;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using NumbersToWords.Core;

namespace NumbersToWordsTests.Core.Services
{
    [TestClass]
    public class ChequeWriterTests
    {
        private ChequeImage _testImageModel = new ChequeImage()
        {
            NameLocation = new PointF(70f, 125f),
            AmountInWordsLocation = new PointF(135f, 170f),
            AmountInWordsOverflowLocation = new PointF(60f, 210f),
            DateLocation = new PointF(620f, 92f),
            AmountLocation = new PointF(610f, 158f),

            ImageFormat = ImageFormat.Jpeg,
            ImagePath = @"Content/img/cheques/example.jpg",
            WrapCharacter = 3
        };

        [TestMethod]
        public void ChequeWriter__ReturnsErrorIfImageIsNotFound()
        {
            // arrange
            var mockNumbersToWords = new Mock<INumbersToWordsService>();
            var mockUtilities = new Mock<IUtilityService>();

            var service = new ChequeWriterService(mockNumbersToWords.Object, mockUtilities.Object);

            _testImageModel.ImagePath = @"Content/img/cheques/missing.jpg";

            // act
            var result = service.WriteCheque(new Cheque(), _testImageModel);

            // assert
            Assert.IsTrue(result.IsError == true);
            Assert.IsTrue(result.ErrorMessage == Consts.ErrMsg_MissingImage);
        }

        [TestMethod]
        public void ChequeWriter__Runs()
        {
            // arrange
            var number = 123;
            var text = "one hundred and twenty three";

            var mockNumbersToWords = new Mock<INumbersToWordsService>();
            mockNumbersToWords.Setup(x => x.ConvertDecimalToPriceString(It.IsAny<decimal>()))
                .Returns(text);

            var mockUtilities = new Mock<IUtilityService>();
            mockUtilities.Setup(x => x.GetWrappedWords(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(new WrappedWords() { });

            var service = new ChequeWriterService(mockNumbersToWords.Object, mockUtilities.Object);

            var model = new Cheque() { Amount = number };

            // act
            var result = service.WriteCheque(model, _testImageModel);

            // assert
            mockNumbersToWords.Verify(x => x.ConvertDecimalToPriceString(model.Amount));
            mockUtilities.Verify(x => x.GetWrappedWords(text, _testImageModel.WrapCharacter));
            Assert.IsTrue(result.Item.GetType() == typeof(byte[]));
        }
    }
}
