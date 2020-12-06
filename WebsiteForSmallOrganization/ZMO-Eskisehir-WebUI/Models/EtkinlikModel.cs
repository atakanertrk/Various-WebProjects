using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZMO_Eskisehir_WebUI.Models
{
    public class EtkinlikModel
    {
        public int Id { get; set; }
        public string Baslik { get; set; } // 150
        public string Metin { get; set; } // 1000
        public DateTime Tarih { get; set; } // date
        public string Link { get; set; } // 200
        public List<EtkinlikModel> etkinlikler { get; set; }
    }
}
