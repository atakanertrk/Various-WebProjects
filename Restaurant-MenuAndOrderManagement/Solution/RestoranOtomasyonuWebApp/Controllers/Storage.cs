

namespace RestoranOtomasyonuWebApp.Controllers
{
    public static class Storage
    {
        // https://localhost:44329    https://kaptancafe.webde.biz.tr    https://kardeslerlokanta.webde.biz.tr   https://kardeslerlokanta.webde.biz.tr
        public static bool isDevelopment { get; set; } = false;
        public static string GetUrl()
        {
            if (isDevelopment)
            {
                return "https://localhost:44329";
            }
            else
            {
                return "https://kaptancafe.webde.biz.tr";
            }
        }
        public static string baseURL { get; set; } = GetUrl();
        // ayrıca development değil ise mysql kullanıyor
        
    }
}
