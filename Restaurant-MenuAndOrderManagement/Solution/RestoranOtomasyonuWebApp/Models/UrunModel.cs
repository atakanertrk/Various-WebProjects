using System;
using System.Collections.Generic;

namespace RestoranOtomasyonuWebApp.Models
{
    public class UrunModel
    {
        public int Id { get; set; }
        public int gurupId { get; set; }
        public string urunAdi { get; set; }
        public string urunAciklamasi { get; set; }
        public decimal urunFiyati { get; set; }
        public string stokVarYok { get; set; }
        public List<UrunModel> urunlerlist { get; set; }
        public string gurubununAdi { get; set; }
        public decimal adet { get; set; } //siparis islemi için kullanılacak 
        public int siparisId { get; set; } //siparis islemi için kullanılacak 
        public string siparisNotu { get; set; }
        public string teslimEdildiMi { get; set; } // evet hayir
    }
}
