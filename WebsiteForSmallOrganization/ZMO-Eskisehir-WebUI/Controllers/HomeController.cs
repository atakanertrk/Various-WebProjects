using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ZMO_Eskisehir_WebUI.Models;
using ZMO_Eskisehir_WebUI.DataAccess;
using Microsoft.Extensions.Configuration;

namespace ZMO_Eskisehir_WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private int counter;
        private SqlDataAccess dataAccess;
        public HomeController(ILogger<HomeController> logger, IConfiguration config)
        {
            counter = 0;
            _logger = logger;
            dataAccess = new SqlDataAccess(config);
        }

        public IActionResult Index()
        {
            // Loading all information to show the user on index page
            GroupOfModels groupOfModels = new GroupOfModels();

            groupOfModels.haberModel.haberler = dataAccess.GetHaberler();
            groupOfModels.haberModel.haberler = groupOfModels.haberModel.haberler.OrderByDescending(x=>x.Tarih).ToList();

            groupOfModels.etkinlikModel.etkinlikler = dataAccess.GetEtkinlikler();
            groupOfModels.etkinlikModel.etkinlikler = groupOfModels.etkinlikModel.etkinlikler.OrderByDescending(x=>x.Tarih).ToList();

            groupOfModels.duyuruModel.duyurular = dataAccess.GetDuyurular();
            groupOfModels.duyuruModel.duyurular = groupOfModels.duyuruModel.duyurular.OrderByDescending(x=>x.Tarih).ToList();

            groupOfModels.egitimlerModel.egitimler = dataAccess.GetEgitimler();
            groupOfModels.egitimlerModel.egitimler = groupOfModels.egitimlerModel.egitimler.OrderByDescending(x=>x.Tarih).ToList();

            groupOfModels.BasindaBizModel.basindaBizKayitlar = dataAccess.GetBasindaBizKayitlar();
            groupOfModels.BasindaBizModel.basindaBizKayitlar = groupOfModels.BasindaBizModel.basindaBizKayitlar.OrderByDescending(x => x.Tarih).ToList();

            groupOfModels.basinBultenlerimizModel.bultenler = dataAccess.GetBasinBultenlerimiz();
            groupOfModels.basinBultenlerimizModel.bultenler = groupOfModels.basinBultenlerimizModel.bultenler.OrderByDescending(x => x.Tarih).ToList();

            return View(groupOfModels);
        }
        public IActionResult SubeTarihcesi()
        {
            return View();
        }
        public IActionResult YonetimKuruluUyeleri()
        {
            return View();
        }
        public IActionResult Haberler()
        {
            HaberModel haberModel = new HaberModel();
            haberModel.haberler = dataAccess.GetHaberler();
            haberModel.haberler = haberModel.haberler.OrderByDescending(x => x.Tarih).ToList();
            return View(haberModel);
        }
        public IActionResult Etkinlikler()
        {
            EtkinlikModel etkinlikModel = new EtkinlikModel();
            etkinlikModel.etkinlikler = dataAccess.GetEtkinlikler();
            etkinlikModel.etkinlikler = etkinlikModel.etkinlikler.OrderByDescending(x => x.Tarih).ToList();
            return View(etkinlikModel);
        }
        public IActionResult Duyurular()
        {
            DuyuruModel duyuruModel = new DuyuruModel();
            duyuruModel.duyurular = dataAccess.GetDuyurular();
            duyuruModel.duyurular = duyuruModel.duyurular.OrderByDescending(x => x.Tarih).ToList();
            return View(duyuruModel);
        }
        public IActionResult Egitimler()
        {
            EgitimlerModel egitimlerModel = new EgitimlerModel();
            egitimlerModel.egitimler = dataAccess.GetEgitimler();
            egitimlerModel.egitimler = egitimlerModel.egitimler.OrderByDescending(x => x.Tarih).ToList();
            return View(egitimlerModel);
        }
        public IActionResult UyelikIslemleri()
        {
            return View();
        }
        public IActionResult Haber(int id)
        {
            GroupOfModels group = new GroupOfModels();
            group.haberModel.haberler = dataAccess.ByIdGetHaber(id);
            return View(group);
        }
        public IActionResult Etkinlik(int id)
        {
            GroupOfModels group = new GroupOfModels();
            group.etkinlikModel.etkinlikler = dataAccess.ByIdGetEtkinlik(id);
            return View(group);
        }

        public IActionResult BasindaBizTumKayitlar()
        {
            BasindaBizModel basindaBizModel = new BasindaBizModel();
            basindaBizModel.basindaBizKayitlar = dataAccess.GetBasindaBizKayitlar();
            basindaBizModel.basindaBizKayitlar = basindaBizModel.basindaBizKayitlar.OrderByDescending(x => x.Tarih).ToList();
            return View(basindaBizModel);
        }
        public IActionResult BasindaBiz(int id)
        {
            GroupOfModels group = new GroupOfModels();
            group.BasindaBizModel.basindaBizKayitlar = dataAccess.ByIdGetBasindaBiz(id);
            return View(group);
        }

        public IActionResult TumBasinBultenleri()
        {
            BasinBultenlerimizModel basinBultenlerimiz = new BasinBultenlerimizModel();
            basinBultenlerimiz.bultenler = dataAccess.GetBasinBultenlerimiz();
            basinBultenlerimiz.bultenler = basinBultenlerimiz.bultenler.OrderByDescending(x => x.Tarih).ToList();
            return View(basinBultenlerimiz);
        }

        public IActionResult Duyuru(int id)
        {
            GroupOfModels group = new GroupOfModels();
            group.duyuruModel.duyurular = dataAccess.ByIdGetDuyuru(id);
            return View(group);
        }
        public IActionResult Egitim(int id)
        {
            GroupOfModels group = new GroupOfModels();
            group.egitimlerModel.egitimler = dataAccess.ByIdGetEgitim(id);
            return View(group);
        }

        [HttpPost]
        public IActionResult SitedeAra(GroupOfModels group)
        {
            if (group.searchValue != null && group.searchValue.Length > 3)
            {
                string searchValue = group.searchValue;
                group.haberModel.haberler = dataAccess.SearchHaber(searchValue);
                group.etkinlikModel.etkinlikler = dataAccess.SearchEtkinlik(searchValue);
                group.BasindaBizModel.basindaBizKayitlar = dataAccess.SearchBasindaBiz(searchValue);
                group.basinBultenlerimizModel.bultenler = dataAccess.SearchBasinBulteni(searchValue);
                group.egitimlerModel.egitimler = dataAccess.SearchEgitim(searchValue);
                group.duyuruModel.duyurular = dataAccess.SearchDuyuru(searchValue);
            }
            return View(group);
        }
    }
}
