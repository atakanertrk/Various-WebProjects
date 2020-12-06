using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace RestoranOtomasyonuWebApp.Models
{
    public class MenuDuzenleModel
    {
        public int gurupId { get; set; }
        public string yeniGurupAdi { get; set; }
        public UrunModel urun { get; set; }
        public UrunModel guncelUrun { get; set; }
        public List<UrunModel> urunlerlist { get; set; }
    }
}
