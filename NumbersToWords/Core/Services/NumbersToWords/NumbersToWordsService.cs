using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace NumbersToWords.Core.Services.NumbersToWords
{
    public class NumbersToWordsService : INumbersToWordsService
    {
        private const int Billion = 1000000000;
        private const int Million = 1000000;
        private const int Thousand = 1000;
        private const int Hundred = 100;

        private List<string> lowUnits = new List<string>
        {
                "zero", "one", "two", "three", "four",
                "five", "six", "seven", "eight",
                "nine", "ten", "eleven", "twelve",
                "thirteen", "fourteen", "fifteen",
                "sixteen", "seventeen", "eighteen", "nineteen"
        };
        private List<string> tensUnits = new List<string>
        {
                "ten", "twenty", "thirty",
                "forty", "fifty", "sixty",
                "seventy", "eighty", "ninety"
        };


        public string ConvertDecimalToPriceString(decimal amount)
        {
            if (amount < 0)
                throw new NotImplementedException();

            var numberParts = GetIntegerAndFractionalNamesFromDecimal(amount);
            var amountString = "";

            if (!String.IsNullOrEmpty(numberParts.Item1))
                amountString += numberParts.Item1 + (numberParts.Item1 == "one" ? " dollar" : " dollars");

            if (!String.IsNullOrEmpty(numberParts.Item1) && !String.IsNullOrEmpty(numberParts.Item2))
                amountString += " and ";

            if (!String.IsNullOrEmpty(numberParts.Item2))
                amountString += numberParts.Item2 + (numberParts.Item2 == "one" ? " cent" : " cents");

            return amountString;
        }

        public Tuple<string, string> GetIntegerAndFractionalNamesFromDecimal(decimal number)
        {
            var test = number.ToString("0.00", CultureInfo.InvariantCulture);

            var numberStrings = test.Split('.');

            var integer = ConvertIntToStringRepresentation(Convert.ToInt32(numberStrings[0]));

            var fractional = "";
            if (numberStrings.Length == 2)
                fractional = ConvertIntToStringRepresentation(Convert.ToInt32(numberStrings[1]));

            return new Tuple<string, string>(integer, fractional);
        }

        public string ConvertIntToStringRepresentation(int number)
        {
            // Can't handle negation of int.MinValue as that would be larger than the MaxValue
            if (number == int.MinValue)
                throw new Exception("Min value exceeded");

            // if negative - make positive and add minus
            if (number < 0)
                return "minus " + ConvertIntToStringRepresentation(Math.Abs(number));

            string result = "";

            // get billions part
            var billions = number / Billion;
            if (billions > 0)
            {
                // convert billions to words
                result += ConvertIntToStringRepresentation(billions) + " billion ";
                // work on remaining portion of number
                number = number % Billion;
            }

            // get millions part
            var millions = number / Million;
            if (millions > 0)
            {
                // convert millions to words
                result += ConvertIntToStringRepresentation(millions) + " million ";
                // work on remaining portion of number
                number = number % Million;
            }

            // get thousands part
            var thousands = number / Thousand;
            if (thousands > 0)
            {
                // convert thousands to words
                result += ConvertIntToStringRepresentation(thousands) + " thousand ";
                // work on remaining portion of number
                number = number % Thousand;
            }

            // get hundreds part
            var hundreds = number / Hundred;
            if (hundreds > 0)
            {
                // convert hundreds to words
                result += ConvertIntToStringRepresentation(hundreds) + " hundred ";
                // work on remaining portion of number
                number = number % Hundred;
            }

            // range left to work on is 99 - 0
            var tensDigit = number / 10;
            var onesDigit = number % 10;

            // if we already have a number and still have work to do append " and "
            if (!String.IsNullOrEmpty(result) && (tensDigit > 0 || onesDigit > 0))
                result += "and ";

            // if less than twenty lookup in table
            if (number < 20)
            {
                // don't add zero to existing result
                if (String.IsNullOrEmpty(result) || number > 0)
                    result += lowUnits[number];
            }
            else
            {
                // write tens
                result += tensUnits[tensDigit - 1];

                // add ones
                if (onesDigit > 0)
                    result += $" {lowUnits[onesDigit]}";
            }

            return result.Trim();
        }
    }
}