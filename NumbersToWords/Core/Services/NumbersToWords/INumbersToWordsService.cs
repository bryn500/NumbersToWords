using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumbersToWords.Core.Services.NumbersToWords
{
    public interface INumbersToWordsService
    {
        string ConvertDecimalToPriceString(decimal amount);
        Tuple<string, string> GetIntegerAndFractionalNamesFromDecimal(decimal number);
        string ConvertIntToStringRepresentation(int number);
    }
}
