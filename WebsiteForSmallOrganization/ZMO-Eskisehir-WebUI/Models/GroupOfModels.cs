using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZMO_Eskisehir_WebUI.Models
{
    public class GroupOfModels
    {
        public DuyuruModel duyuruModel { get; set; } = new DuyuruModel();
        public EgitimlerModel egitimlerModel { get; set; } = new EgitimlerModel();
        public EtkinlikModel etkinlikModel { get; set; } = new EtkinlikModel();
        public HaberModel haberModel { get; set; } = new HaberModel();
        public BasindaBizModel BasindaBizModel { get; set; } = new BasindaBizModel();
        public BasinBultenlerimizModel basinBultenlerimizModel { get; set; } = new BasinBultenlerimizModel();
        public int id { get; set; }
        public string password { get; set; }
        public string searchValue { get; set; }
    }
}
