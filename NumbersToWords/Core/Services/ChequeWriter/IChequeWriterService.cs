using NumbersToWords.Core.Entities;
using NumbersToWords.Models.Cheque;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumbersToWords.Core.Services.ChequeWriter
{
    public interface IChequeWriterService
    {
        Result<byte[]> WriteCheque(Cheque model, ChequeImage chequeImage);
    }
}
