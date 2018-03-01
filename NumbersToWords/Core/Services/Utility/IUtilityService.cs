using NumbersToWords.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumbersToWords.Core.Services.Utility
{
    public interface IUtilityService
    {
        int CountDecimalDigits(decimal n, bool includeTrailingZeros);
        WrappedWords GetWrappedWords(string words, int wrapOnCharacter);
    }
}
