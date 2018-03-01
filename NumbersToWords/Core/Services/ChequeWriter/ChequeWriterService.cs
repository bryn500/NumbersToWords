using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using NumbersToWords.Core.Entities;
using NumbersToWords.Core.Services.NumbersToWords;
using NumbersToWords.Core.Services.Utility;
using NumbersToWords.Models.Cheque;

namespace NumbersToWords.Core.Services.ChequeWriter
{
    public class ChequeWriterService : IChequeWriterService
    {
        private IUtilityService _utilityService;
        private INumbersToWordsService _numbersToWordsService;

        public ChequeWriterService(INumbersToWordsService numbersToWordsService, IUtilityService utilityService)
        {
            _numbersToWordsService = numbersToWordsService;
            _utilityService = utilityService;
        }

        public Result<byte[]> WriteCheque(Cheque model, ChequeImage chequeImage)
        {
            if (!File.Exists(chequeImage.ImagePath))
                return new Result<byte[]>()
                {
                    IsError = true,
                    ErrorMessage = "Could not find image file"
                };

            byte[] imageAsByteArray;

            using (var image = (Bitmap)Image.FromFile(chequeImage.ImagePath))
            using (var graphics = Graphics.FromImage(image))
            using (var smallFont = new Font("Arial", 12))
            using (var bigFont = new Font("Arial", 16))
            using (var fontColour = Brushes.Black)
            {
                graphics.DrawString(model.Amount.ToString("N2"), bigFont, fontColour, chequeImage.AmountLocation);
                graphics.DrawString(model.Name, bigFont, fontColour, chequeImage.NameLocation);
                graphics.DrawString(model.Date.ToShortDateString(), bigFont, fontColour, chequeImage.DateLocation);

                var amountAsWords = _numbersToWordsService.ConvertDecimalToPriceString(model.Amount);

                var wrappedWords = _utilityService.GetWrappedWords(amountAsWords, chequeImage.WrapCharacter);

                graphics.DrawString(string.Join(" ", wrappedWords.BeforeWrap), smallFont, fontColour, chequeImage.AmountInWordsLocation);

                if (wrappedWords.AfterWrap.Any())
                    graphics.DrawString(string.Join(" ", wrappedWords.AfterWrap), smallFont, fontColour, chequeImage.AmountInWordsOverflowLocation);

                using (var ms = new MemoryStream())
                {
                    image.Save(ms, chequeImage.ImageFormat);
                    imageAsByteArray = ms.ToArray();
                }
            }

            return new Result<byte[]>()
            {
                Item = imageAsByteArray
            };
        }
    }
}