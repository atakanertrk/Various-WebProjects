using OnurShopSecond.DataAccess;
using System.Collections.Generic;

namespace OnurShopSecond.Models
{
    public interface IViewGroupModel
    {
        // Full of properties that have used in views (hardCoded)
        string arananText { get; set; }
        string EklenecekUrunGurubununAdi { get; set; }
        UrunModel GuncelUrunDegerleri { get; set; }
        byte[] qrCode { get; set; }
        List<UrunGurubuModel> urunGurubuListesi { get; set; }
        string UrunGurubununAdi { get; set; }
        int UrunGurubununId { get; set; }
        List<string> UrunGuruplariAdiListesi { get; set; }
        List<UrunModel> urunListesi { get; set; }
        UrunModel urunModel { get; set; }
        string YeniGurupAdi { get; set; }
        IDataAccess dataAccess { get; set; }
    }
}