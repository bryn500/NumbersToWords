using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NumbersToWords.Models.Shared
{
    public enum AlertType
    {
        Success,
        Failure
    }

    public class Alert
    {
        public AlertType AlertType { get; set; }
        public string Message { get; set; }
    }
}