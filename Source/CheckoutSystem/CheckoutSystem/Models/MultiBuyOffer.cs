using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckoutSystem.Models
{
    public class MultiBuyOffer:IOffer
    {
        public MultiBuyOffer(double unitPrice, int threshold)
        {
            UnitPrice = unitPrice;
            Threshold = threshold;
        }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double UnitPrice { get; set; }
        public int Threshold { get; set; }
    }
}
