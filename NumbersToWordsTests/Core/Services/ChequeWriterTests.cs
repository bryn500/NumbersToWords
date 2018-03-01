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

namespace NumbersToWordsTests.Core.Services
{
    [TestClass]
    public class ChequeWriterTests
    {
        private ChequeImage _testImageModel = new ChequeImage()
        {
            
        };

        [TestMethod]
        public void ChequeWriter__Works()
        {
            // arrange            
            var mockNumbersToWords = new Mock<INumbersToWordsService>();
            var mockUtilities = new Mock<IUtilityService>();
            var service = new ChequeWriterService(mockNumbersToWords.Object, mockUtilities.Object);

            var model = new Cheque() { Amount = 123 };
            var image = new ChequeImage()
            {
                NameLocation = new PointF(70f, 125f),
                AmountInWordsLocation = new PointF(135f, 170f),
                AmountInWordsOverflowLocation = new PointF(60f, 210f),
                DateLocation = new PointF(620f, 92f),
                AmountLocation = new PointF(610f, 158f),

                ImageFormat = ImageFormat.Jpeg,
                ImagePath = Server.MapPath("/Content/img/cheques/example.jpg"),
                WrapCharacter = 3
            };

            // act
            var result = service.WriteCheque(model, image);

            // assert
            mockNumbersToWords.Verify(x => x.ConvertDecimalToPriceString(model.Amount));
            mockUtilities.Verify(x => x.GetWrappedWords());
        }
    }
}
