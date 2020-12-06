
using System.Collections.Generic;

namespace RestoranOtomasyonuWebApp.Models
{
    public class SiparisModel
    {
        public int Id { get; set; }
        public int masaNo { get; set; }
        public int urunId { get; set; }
        public decimal adet { get; set; }
        public List<SiparisModel> siparisler { get; set; }
        public List<UrunModel> siparisEdilenUrunList { get; set; } = new List<UrunModel>();
        public List<int> urunNoList { get; set; }
        public List<decimal> adetList { get; set; }
        public string MasaNotu { get; set; } // masaya not eklemede kullanılacak
        public List<string> siparisNotlari { get; set; }
        public string teslimEdildiMi { get; set; } // evet hayir
    }
}
