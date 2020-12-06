using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZMO_Eskisehir_WebUI.Models
{
    public class BasindaBizModel
    {
        public int Id { get; set; }
        public string Baslik { get; set; }
        public DateTime Tarih { get; set; }
        public string Link { get; set; }
        public List<BasindaBizModel> basindaBizKayitlar { get; set; }

    }
}
