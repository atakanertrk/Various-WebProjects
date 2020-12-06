using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestoranOtomasyonuWebApp.Models
{
    public class TamamlanmisSiparisModel
    {
        public int Id { get; set; }
        public string urunAdi { get; set; }
        public decimal urunFiyati { get; set; }
        public decimal adet { get; set; }
        public DateTime tarih { get; set; }
        public int masaNo { get; set; }
    }
}
