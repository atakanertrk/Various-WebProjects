using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnurShopSecond.Models
{
    public class ErrorModel
    {
        public string ErrorMessage { get; set; }
        public string ExtraMessage { get; set; }
        public string InnerMessage { get; set; }
    }
}
