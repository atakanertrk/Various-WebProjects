using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnurShopSecond.Models
{
    public class UrunModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Amount { get; set; }
        public string Properties { get; set; }
        public int GroupId { get; set; }
        public decimal Price { get; set; }
    }
}
