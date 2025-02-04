using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandyShop.Models
{
    public class ValidationResponse
    {
        public bool IsValid { get; set; } 

        public string ErrorMessage { get; set; }  
    }

    public class CocoaValidationResponse : ValidationResponse
    {
        internal int CocoaPercentage { get; set; }
    }

    public class PriceValidationResponse : ValidationResponse
    {
        internal decimal Price { get; set; }
    }
}
