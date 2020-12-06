using OnurShopSecond.DataAccess;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace OnurShopSecond.Models
{
    // Full of properties that have used in views (hardCoded)
    public class ViewGroupModel : IViewGroupModel
    {
        public List<UrunGurubuModel> urunGurubuListesi { get; set; }
        public List<UrunModel> urunListesi { get; set; }
        public string EklenecekUrunGurubununAdi { get; set; }
        public string UrunGurubununAdi { get; set; }
        public UrunModel urunModel { get; set; }
        public int UrunGurubununId { get; set; }
        public string YeniGurupAdi { get; set; }
        public UrunModel GuncelUrunDegerleri { get; set; }
        public List<string> UrunGuruplariAdiListesi { get; set; }
        public string arananText { get; set; }
        public byte[] qrCode { get; set; }
        public IDataAccess dataAccess { get; set; }
    }
}
