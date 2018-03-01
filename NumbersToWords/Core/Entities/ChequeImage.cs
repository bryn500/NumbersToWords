using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;

namespace NumbersToWords.Core.Entities
{
    public class ChequeImage
    {
        public string ImagePath { get; set; }
        public PointF NameLocation { get; set; }
        public PointF DateLocation { get; set; }
        public PointF AmountLocation { get; set; }
        public PointF AmountInWordsLocation { get; set; }
        public PointF AmountInWordsOverflowLocation { get; set; }
        public int WrapCharacter { get; set; }
        public ImageFormat ImageFormat { get; set; }
    }
}