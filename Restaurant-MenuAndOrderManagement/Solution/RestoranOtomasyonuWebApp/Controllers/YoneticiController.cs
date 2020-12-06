using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestoranOtomasyonuWebApp.DataAccess;
using RestoranOtomasyonuWebApp.Models;

namespace RestoranOtomasyonuWebApp.Controllers
{
    public class YoneticiController : Controller
    {
        private readonly IHostingEnvironment _environment;
        IDataAccess dataAccess;
        MyDataProtector dataProtector;
        public YoneticiController(IHostingEnvironment IHostingEnvironment)
        {
            dataProtector = new MyDataProtector();
            _environment = IHostingEnvironment;
            if (Storage.isDevelopment)
            {
                dataAccess = new SqlDataAccess();
            }
            else
            {
                dataAccess = new MySqlDataAccess();
            }
        }

        public IActionResult Index()
        {
            YoneticiGirisModel girismodel = new YoneticiGirisModel();
            string rv = Request.Cookies["yonetici"];
            if (rv == null)
            {
                return RedirectToAction("YoneticiGiris");
            }
            else
            {
                if (AuthenticationControl(rv))
                {
                    girismodel.YoneticiSifresi = rv;
                    return View(girismodel);

                }
                else
                {
                    return RedirectToAction("YoneticiGiris");
                }
            }

        }

        public IActionResult YoneticiGiris()
        {
            if (Request.Cookies["yonetici"] != null)
            {
                if (AuthenticationControl(Request.Cookies["yonetici"]))
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        [HttpPost]
        public IActionResult YoneticiGiris(YoneticiGirisModel yoneticiGirisModel)
        {
            if (yoneticiGirisModel.YoneticiSifresi == dataAccess.GetYoneticiSifresi())
            {
                string encryptedSifre = dataProtector.Protect(yoneticiGirisModel.YoneticiSifresi.ToString());
                CookieOptions cookieOptions = new CookieOptions();
                cookieOptions.Expires = DateTime.Now.AddDays(7);
                Response.Cookies.Append("yonetici", encryptedSifre);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public IActionResult MenuDuzenle()
        {
            string rv = Request.Cookies["yonetici"];
            if (AuthenticationControl(rv))
            {
                MenuDuzenleModel menuDuzenleModel = new MenuDuzenleModel();
                return View(menuDuzenleModel);
            }
            else
            {
                return RedirectToAction("YoneticiGiris");
            }
        }
        [HttpPost]
        public IActionResult GurupAdiDegistir(MenuDuzenleModel menuDuzenleModel)
        {
            string rv = Request.Cookies["yonetici"];
            if (AuthenticationControl(rv))
            {
                dataAccess.ChangeGurupNameById(menuDuzenleModel.gurupId, menuDuzenleModel.yeniGurupAdi);
                return RedirectToAction("MenuDuzenle");
            }
            else
            {
                return RedirectToAction("YoneticiGiris");
            }

        }

        [HttpPost]
        public IActionResult GurupResmiDegistir(MenuDuzenleModel menuDuzenleModel)
        {
            string rv = Request.Cookies["yonetici"];
            if (AuthenticationControl(rv))
            {
                //string fileName = Path.Combine(_environment.WebRootPath + @"\images\urungurubufoto") + $@"\{menuDuzenleModel.gurupId}.jpg";
                //System.IO.File.Delete(fileName);

                var files = HttpContext.Request.Form.Files;
                if (HttpContext.Request.Form.Files != null)
                {
                    foreach (var file in files)
                    {
                        if (file.Length > 0)
                        {
                            var myUniqueFileName = menuDuzenleModel.gurupId.ToString();

                            string newFileName = myUniqueFileName + ".jpg";

                            string fileName2 = Path.Combine(_environment.WebRootPath + @"\images\urungurubufoto") + $@"\{newFileName}";

                            using (FileStream fs = System.IO.File.Create(fileName2))
                            {
                                file.CopyTo(fs);
                                fs.Flush();
                            }
                        }
                    }
                }

                return RedirectToAction("MenuDuzenle");
            }
            else
            {
                return RedirectToAction("YoneticiGiris");
            }
        }

        [HttpPost]
        public IActionResult YeniGurupOlustur(MenuDuzenleModel menuDuzenleModel)
        {
            string rv = Request.Cookies["yonetici"];
            if (AuthenticationControl(rv)) 
            {
                string insertedId = dataAccess.YeniGurupEkle(menuDuzenleModel.yeniGurupAdi).ToString();

                var files = HttpContext.Request.Form.Files;
                if (HttpContext.Request.Form.Files != null)
                {
                    foreach (var file in files)
                    {
                        if (file.Length > 0)
                        {
                            var myUniqueFileName = menuDuzenleModel.gurupId.ToString();

                            string newFileName = insertedId + ".jpg";

                            string fileName2 = Path.Combine(_environment.WebRootPath + @"\images\urungurubufoto") + $@"\{newFileName}";

                            using (FileStream fs = System.IO.File.Create(fileName2))
                            {
                                file.CopyTo(fs);
                                fs.Flush();
                            }
                        }
                    }
                }

                return RedirectToAction("MenuDuzenle");


            }
            else
            {
                return RedirectToAction("YoneticiGiris");
            }
        }

        [HttpPost]
        public IActionResult GurubuSil(MenuDuzenleModel menuDuzenleModel)
        {
            string rv = Request.Cookies["yonetici"];
            if (AuthenticationControl(rv))
            {
                var gurupIcindekiUrunler = dataAccess.GurupIcindekiUrunlerinIdListesi(menuDuzenleModel.gurupId);//sadece id değeri yüklü, diğer değerler null veya 0
                foreach (var urun in gurupIcindekiUrunler)
                {
                    string fileName = Path.Combine(_environment.WebRootPath + @"\images\urunfoto") + $@"\{urun.Id}.jpg";
                    System.IO.File.Delete(fileName);
                    dataAccess.UrunSilById(urun.Id);
                }
                //içeriğindeki ürünler silindi, şimdi ürün gurubu siliniyor
                string fileName2 = Path.Combine(_environment.WebRootPath + @"\images\urungurubufoto") + $@"\{menuDuzenleModel.gurupId}.jpg";
                System.IO.File.Delete(fileName2);

                dataAccess.UrunGurubunuSil(menuDuzenleModel.gurupId);

                return RedirectToAction("MenuDuzenle");
            }
            else
            {
                return RedirectToAction("YoneticiGiris");
            }
        }


        public IActionResult YoneticiCikisYap()
        {
            Response.Cookies.Append("yonetici", "");
            return RedirectToAction("Index");
        }

        public IActionResult GurupIcerigi(int gurupId)
        {
            string rv = Request.Cookies["yonetici"];
            MenuDuzenleModel menuDuzenleModel = new MenuDuzenleModel();
            menuDuzenleModel.gurupId = gurupId;
            if (AuthenticationControl(rv)) 
            {
                menuDuzenleModel.urunlerlist = dataAccess.GetUrunByGurupId(menuDuzenleModel.gurupId);
                return View(menuDuzenleModel);
            }
            else
            {
                return RedirectToAction("YoneticiGiris");
            }
        }

        [HttpPost]
        public IActionResult GurubaUrunEkle(MenuDuzenleModel menuDuzenleModel)
        {
            string rv = Request.Cookies["yonetici"];
            if (AuthenticationControl(rv))
            {
                int insertedUrunId = dataAccess.UrunEkle(menuDuzenleModel.urun);
                var files = HttpContext.Request.Form.Files;
                if (HttpContext.Request.Form.Files != null)
                {
                    foreach (var file in files)
                    {
                        if (file.Length > 0)
                        {
                            string newFileName = insertedUrunId + ".jpg";

                            string fileName2 = Path.Combine(_environment.WebRootPath + @"\images\urunfoto") + $@"\{newFileName}";

                            using (FileStream fs = System.IO.File.Create(fileName2))
                            {
                                file.CopyTo(fs);
                                fs.Flush();
                            }
                        }
                    }
                }
                return RedirectToAction("GurupIcerigi", new { gurupId=menuDuzenleModel.urun.gurupId });
            }
            else
            {
                return RedirectToAction("YoneticiGiris");
            }
        }

        [HttpPost]
        public IActionResult UrunuSil(int urunId, int gurupId)
        {
            string rv = Request.Cookies["yonetici"];
            if (AuthenticationControl(rv))
            {
                string fileName = Path.Combine(_environment.WebRootPath + @"\images\urunfoto") + $@"\{urunId}.jpg";
                System.IO.File.Delete(fileName);
                dataAccess.UrunSilById(urunId);
                return RedirectToAction("GurupIcerigi", new {  gurupId = gurupId });
            }
            else
            {
                return RedirectToAction("YoneticiGiris");
            }
        }
        [HttpPost]
        public IActionResult StokDurumuDegistir( int urunId, int gurupId, string stokDurumu)
        {
            string rv = Request.Cookies["yonetici"];
            if (AuthenticationControl(rv))
            {
                if (stokDurumu == "var")
                {
                    dataAccess.StokDurumuDegistir(urunId,"yok");
                }
                else
                {
                    dataAccess.StokDurumuDegistir(urunId, "var");
                }
                return RedirectToAction("GurupIcerigi", new { gurupId = gurupId });
            }
            else
            {
                return RedirectToAction("YoneticiGiris");
            }
        }
        [HttpPost]
        public IActionResult UrunBilgileriGuncelle(MenuDuzenleModel menuDuzenleModel)
        {
            string rv = Request.Cookies["yonetici"];
            if (AuthenticationControl(rv))
            {
                UrunModel urunModel = new UrunModel();
                urunModel.urunAdi = menuDuzenleModel.guncelUrun.urunAdi;
                urunModel.urunAciklamasi = menuDuzenleModel.guncelUrun.urunAciklamasi;
                urunModel.urunFiyati = menuDuzenleModel.guncelUrun.urunFiyati;
                urunModel.Id = menuDuzenleModel.guncelUrun.Id;
                dataAccess.UrunBilgileriGuncelle(urunModel);
                return RedirectToAction("GurupIcerigi",new {gurupId=menuDuzenleModel.gurupId});
            }
            else
            {
                return RedirectToAction("YoneticiGiris");
            }
        }
        [HttpPost]
        public IActionResult UrunFotosunuDegistir( int urunId, int gurupId)
        {
            string rv = Request.Cookies["yonetici"];
            if (AuthenticationControl(rv))
            {
                var files = HttpContext.Request.Form.Files;
                if (HttpContext.Request.Form.Files != null)
                {
                    foreach (var file in files)
                    {
                        if (file.Length > 0)
                        {
                            string newFileName = urunId.ToString() + ".jpg";

                            string fileName2 = Path.Combine(_environment.WebRootPath + @"\images\urunfoto") + $@"\{newFileName}";

                            using (FileStream fs = System.IO.File.Create(fileName2))
                            {
                                file.CopyTo(fs);
                                fs.Flush();
                            }
                        }
                    }
                }
                return RedirectToAction("GurupIcerigi", new { gurupId = gurupId });
            }
            else
            {
                return RedirectToAction("YoneticiGiris");
            }
        }


        

       
        public IActionResult MasaIslemleri()
        {
            string rv = Request.Cookies["yonetici"];
            if (AuthenticationControl(rv))
            {
                MasaEkleSilModel masaEkleSilModel = new MasaEkleSilModel();
                masaEkleSilModel.tumMasaNumaralari = dataAccess.GetMasaNumaralariList();
                return View(masaEkleSilModel);
            }
            else
            {
                return RedirectToAction("YoneticiGiris");
            }
        }

        [HttpPost]
        public IActionResult MasaEkle(MasaEkleSilModel masaEkleSilModel)
        {
            string rv = Request.Cookies["yonetici"];
            if (AuthenticationControl(rv))
            {
                dataAccess.MasaEkle(masaEkleSilModel.MasaNo);
                return RedirectToAction("MasaIslemleri");
            }
            else
            {
                return RedirectToAction("YoneticiGiris");
            }
        }
        [HttpPost]
        public IActionResult MasaSil(MasaEkleSilModel masaEkleSilModel)
        {
            string rv = Request.Cookies["yonetici"];
            if (AuthenticationControl(rv))
            {
                dataAccess.MasaSil(masaEkleSilModel.MasaNo);
                return RedirectToAction("MasaIslemleri");
            }
            else
            {
                return RedirectToAction("YoneticiGiris");
            }
        }

        public IActionResult SiteEkstra()
        {
            string rv = Request.Cookies["yonetici"];
            if (AuthenticationControl(rv))
            {
                SiteEkstraModel siteEkstra = new SiteEkstraModel();
                return View(siteEkstra);
            }
            else
            {
                return RedirectToAction("YoneticiGiris");
            }
        }
        [HttpPost]
        public IActionResult SiteEkstraGuncelle(SiteEkstraModel guncelSiteEkstra)
        {
            string rv = Request.Cookies["yonetici"];
            if (AuthenticationControl(rv))
            {
                dataAccess.SetSiteEkstra(guncelSiteEkstra);
                return Redirect(Storage.baseURL);
            }
            else
            {
                return RedirectToAction("YoneticiGiris");
            }
        }

        public bool AuthenticationControl(string rv)
        {
            string decryptedSifre = dataProtector.Unprotect(rv);
            if (decryptedSifre == dataAccess.GetYoneticiSifresi())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
