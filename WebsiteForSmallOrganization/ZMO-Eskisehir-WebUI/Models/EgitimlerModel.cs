using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZMO_Eskisehir_WebUI.Models
{
    public class EgitimlerModel
    {
        public int Id { get; set; }
        public string Baslik { get; set; } // 100
        public DateTime Tarih { get; set; }
        public string Metin { get; set; } // 1000
        public string Link { get; set; } // 200
        public List<EgitimlerModel> egitimler { get; set; }
    }
}
