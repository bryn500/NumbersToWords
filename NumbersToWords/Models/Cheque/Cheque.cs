using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NumbersToWords.Core.Services.Utility;

namespace NumbersToWords.Models.Cheque
{
    public class Cheque : IValidatableObject
    {
        public static DateTime MinDate { get { return DateTime.Today.AddYears(-1); } }
        public static DateTime MaxDate { get { return DateTime.Today.AddYears(1); } }
        public const int MaxNameLength = 100;
        public const double MinAcceptedAmount = 0.01;
        public const int MaxAcceptedAmount = 1000000000;

        [Required]
        [MaxLength(MaxNameLength)]
        public string Name { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [Range(
            MinAcceptedAmount,
            MaxAcceptedAmount,
            ErrorMessage = "Please enter an amount greater than 0 and less than 1 billion."
        )]
        public decimal Amount { get; set; }

        // Custom Validation
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // No future dates
            if (Date > MaxDate)
                yield return new ValidationResult("Date cannot be in the future.", new string[] { "DateWritten" });

            // No dates less than defined minimum
            if (Date < MinDate)
                yield return new ValidationResult("Only dates within the last 20 years are allowed.", new string[] { "DateWritten" });

            var utils = new UtilityService();

            if (utils.CountDecimalDigits(Amount) > 2)
                yield return new ValidationResult("Amount can only contain 2 decimal places.", new string[] { "Amount" });            
        }
    }
}