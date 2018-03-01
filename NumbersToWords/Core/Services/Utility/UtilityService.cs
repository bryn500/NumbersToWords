using NumbersToWords.Core.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;

namespace NumbersToWords.Core.Services.Utility
{
    public class UtilityService : IUtilityService
    {
        public int CountDecimalDigits(decimal n, bool includeTrailingZeros = false)
        {
            var number = n.ToString(CultureInfo.InvariantCulture);

            if (!includeTrailingZeros)
                number = number.TrimEnd('0');

            return number.SkipWhile(c => c != '.')
                    .Skip(1)
                    .Count();
        }

        public WrappedWords GetWrappedWords(string words, int wrapOnCharacter)
        {
            var result = new WrappedWords
            {
                AfterWrap = words.Split(' ').ToList()
            };

            // add words to list while they don't overflow
            var characterCount = 0;

            while (characterCount < wrapOnCharacter)
            {
                var currentWord = result.AfterWrap.FirstOrDefault();

                // break
                if (String.IsNullOrEmpty(currentWord) || characterCount + currentWord.Length >= wrapOnCharacter)
                    break;

                result.BeforeWrap.Add(currentWord);
                result.AfterWrap.RemoveAt(0);
                characterCount += currentWord.Length;
            }

            return result;
        }
    }
}