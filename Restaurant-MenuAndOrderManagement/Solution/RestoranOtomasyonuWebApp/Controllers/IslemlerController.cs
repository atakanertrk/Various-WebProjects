using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestoranOtomasyonuWebApp.DataAccess;
using RestoranOtomasyonuWebApp.Models;
using System;

namespace RestoranOtomasyonuWebApp.Controllers
{
    public class IslemlerController : Controller
    {
        MyDataProtector dataProtector;
        IDataAccess dataAccess;
        public IslemlerController()
        {
            dataProtector = new MyDataProtector();
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
            string rv = Request.Cookies["siparis"];
            if (rv != null && AuthenticationControl(rv))
            {
                return View();
            }
            else
            {
                return RedirectToAction("SiparisIslemleriGiris");
            }
        }
        public IActionResult IslemlerCikisYap()
        {
            Response.Cookies.Append("siparis", "");
            return RedirectToAction("Index");
        }
        public IActionResult SiparisIslemleriGiris()
        {
            if (Request.Cookies["siparis"] != null)
            {
                if (AuthenticationControl(Request.Cookies["siparis"]))
                {
                    return RedirectToAction("Index");
                }
            }
            return View();

        }
        [HttpPost]
        public IActionResult SiparisIslemleriGiris(SiparisIslemiSifreModel siparisIslemiSifre)
        {
            if (siparisIslemiSifre.SiparisIslemiSifresi == dataAccess.GetSiparisIslemleriGirisSifresi())
            {
                string encryptedSifre = dataProtector.Protect(siparisIslemiSifre.SiparisIslemiSifresi.ToString());
                CookieOptions cookieOptions = new CookieOptions();
                cookieOptions.Expires = DateTime.Now.AddDays(7);
                Response.Cookies.Append("siparis", encryptedSifre);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
        public IActionResult Masalar()
        {
            string rv = Request.Cookies["siparis"];
            if (AuthenticationControl(rv))
            {
                MasalarModel masalarModel = new MasalarModel();
                masalarModel.masaNoList = dataAccess.GetMasaNumaralariList();
                return View(masalarModel);
            }
            else
            {
                return RedirectToAction("SiparisIslemleriGiris");
            }
        }

        public IActionResult Masa(int masaNo)
        {
            string rv = Request.Cookies["siparis"];
            if (AuthenticationControl(rv))
            {
                SiparisModel siparisModel = new SiparisModel();
                siparisModel.masaNo = masaNo;

                siparisModel.siparisler = dataAccess.GetMasaSiparisleri(masaNo);
                siparisModel.MasaNotu = dataAccess.GetMasaNotu(masaNo);

                foreach (var siparis in siparisModel.siparisler)
                {
                    var urun = dataAccess.UrunBilgileriByUrunId(siparis.urunId);
                    urun.adet = siparis.adet;
                    urun.siparisId = siparis.Id;
                    urun.teslimEdildiMi = siparis.teslimEdildiMi;
                    urun.siparisNotu = dataAccess.GetSiparisNotu(siparis.Id);
                    siparisModel.siparisEdilenUrunList.Add(urun);
                }

                return View(siparisModel);
            }
            else
            {
                return RedirectToAction("SiparisIslemleriGiris");
            }
        }

        public IActionResult MasayaSiparisEkle(int masaNo)
        {
            string rv = Request.Cookies["siparis"];
            if (AuthenticationControl(rv))
            {
                SiparisModel siparisModel = new SiparisModel();
                siparisModel.masaNo = masaNo;
                return View(siparisModel);
            }
            else
            {
                return RedirectToAction("SiparisIslemleriGiris");
            }

        }
        [HttpPost]
        public IActionResult MasaNotuEkle(SiparisModel siparis)
        {
            string rv = Request.Cookies["siparis"];
            if (AuthenticationControl(rv))
            {
                dataAccess.MasaNotuEkle(siparis.masaNo, siparis.MasaNotu);
                return RedirectToAction("Masa", new { masaNo = siparis.masaNo });
            }
            else
            {
                return RedirectToAction("SiparisIslemleriGiris");
            }
        }

        [HttpPost]
        public IActionResult SiparisEklePost(SiparisModel siparis)
        {
            string rv = Request.Cookies["siparis"];
            if (AuthenticationControl(rv))
            {
                for (int i = 0; i < siparis.adetList.Count; i++)
                {
                    if (siparis.adetList[i] != 0)
                    {
                        dataAccess.SiparisEkle(siparis.masaNo, siparis.urunNoList[i], siparis.adetList[i], siparis.siparisNotlari[i]);
                    }
                    // şu üründen: siparis.urunNoList[i];
                    // şu kadar: siparis.adetList[i];
                }
                return RedirectToAction("Masa", new { masaNo = siparis.masaNo });
            }
            return RedirectToAction("SiparisIslemleriGiris");

        }

        public IActionResult SiparisiIptalEt(int siparisId, int masaNo)
        {
            string rv = Request.Cookies["siparis"];
            if (AuthenticationControl(rv))
            {
                dataAccess.SiparisSil(siparisId);
                return RedirectToAction("Masa", new { masaNo = masaNo });
            }
            else
            {
                return RedirectToAction("SiparisIslemleriGiris");
            }
        }

        public IActionResult SiparisTeslimEdildi(int siparisId, int masaNo)
        {
            string rv = Request.Cookies["siparis"];
            if (AuthenticationControl(rv))
            {
                dataAccess.SiparisTeslimEdildi(siparisId);
                return RedirectToAction("Masa", new { masaNo = masaNo });
            }
            else
            {
                return RedirectToAction("SiparisIslemleriGiris");
            }
        }

        public IActionResult MasaOdemesiAlDogrulama(int masaNo,  decimal total)
        {
            string rv = Request.Cookies["siparis"];
            if (AuthenticationControl(rv))
            {
                MasaUcretiModel masaUcretiModel = new MasaUcretiModel();
                masaUcretiModel.masaNo = masaNo;
                masaUcretiModel.masaUcreti = total;
                return View(masaUcretiModel);
            }
            else
            {
                return RedirectToAction("SiparisIslemleriGiris");
            }
        }

        public IActionResult MasaOdemesiAl(int masaNo)
        {
            string rv = Request.Cookies["siparis"];
            if (AuthenticationControl(rv))
            {
                //--- geçmiş siparişlere ekleme --- UrunBilgileriByUrunId(urunleri alır) GetMasaSiparisleri(aktif siparisleri alır)
                var masaSiparisleri = dataAccess.GetMasaSiparisleri(masaNo); //aktif siparisleri tablosunu alır
                foreach (var siparis in masaSiparisleri)
                {
                    //  siparis.adet; siparis.masaNo; siparis.urunId;
                    var siparisVerilenUrununBilgileri = dataAccess.UrunBilgileriByUrunId(siparis.urunId);
                    // siparisVerilenUrununBilgileri.urunAdi; siparisVerilenUrununBilgileri.urunFiyati;
                    dataAccess.TamamlanmislaraEkle(siparisVerilenUrununBilgileri.urunAdi, siparisVerilenUrununBilgileri.urunFiyati, siparis.adet, siparis.masaNo);
                }
                //--- end ---

                dataAccess.MasaSiparisleriniSil(masaNo);
                dataAccess.MasaNotuSil(masaNo);
                return RedirectToAction("Masalar");
            }
            else
            {
                return RedirectToAction("SiparisIslemleriGiris");
            }
        }
        public IActionResult EskiTarihtekiSiparisler(SiparisIslemiSifreModel model)
        {
            string rv = Request.Cookies["siparis"];
            if (AuthenticationControl(rv))
            {
                return View(model);
            }
            else
            {
                return RedirectToAction("SiparisIslemleriGiris");
            }
        }


        public bool AuthenticationControl(string rv)
        {
            string decryptedSifre = dataProtector.Unprotect(rv);
            if (decryptedSifre == dataAccess.GetSiparisIslemleriGirisSifresi())
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
