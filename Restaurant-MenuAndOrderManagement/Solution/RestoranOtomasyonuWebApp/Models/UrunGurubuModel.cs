using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestoranOtomasyonuWebApp.Models
{
    public class UrunGurubuModel
    {
        public int Id { get; set; }
        public string gurupAdi { get; set; }
        public List<UrunGurubuModel> UrunGuruplariList { get; set; } = new List<UrunGurubuModel>();
    }
}
