using System;

namespace CheckoutSystem.Models
{
    public interface IOffer
    {
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }

    }
}