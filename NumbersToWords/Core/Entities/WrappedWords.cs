using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NumbersToWords.Core.Entities
{
    public class WrappedWords
    {
        public List<string> BeforeWrap { get; set; }
        public List<string> AfterWrap { get; set; }

        public WrappedWords()
        {
            BeforeWrap = new List<string>();
            AfterWrap = new List<string>();
        }
    }
}