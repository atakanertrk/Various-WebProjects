using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecimServerApi.Models
{
    public class SecmenModel
    {
        public string TcNo { get; set; }
        public string Sifre { get; set; }
        public int OyKullandiMi { get; set; } // 0 hayir , 1 evet
    }
}
