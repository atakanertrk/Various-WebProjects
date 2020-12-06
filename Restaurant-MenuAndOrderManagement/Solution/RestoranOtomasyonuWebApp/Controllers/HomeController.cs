using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestoranOtomasyonuWebApp.Models;
using RestoranOtomasyonuWebApp.DataAccess;

namespace RestoranOtomasyonuWebApp.Controllers
{
    public class HomeController : Controller
    {
        IDataAccess dataAccess;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            if (Storage.isDevelopment)
            {
                dataAccess = new SqlDataAccess();
            }
            else
            {
                dataAccess = new MySqlDataAccess();
            }
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult aos()
        {
            return View();
        }

        [Route("/menu")]
        public IActionResult Menu()
        {
            //MySqlDataAccess dataAccess = new MySqlDataAccess();
            UrunGurubuModel urunGuruplari = new UrunGurubuModel();

            List<UrunGurubuModel> guruplar = dataAccess.UrunGuruplari();
            urunGuruplari.UrunGuruplariList = guruplar;

            return View(urunGuruplari);
        }

        [Route("menu/GurupIcerigindekiUrunler")]
        // /menu/gurupicerigindekiurunler?id=3
        public IActionResult GurupIcerigindekiUrunler([FromQuery] int id)
        {
            //MySqlDataAccess dataAccess = new MySqlDataAccess();
            string gurupAdi = dataAccess.GetGurupNameByGurupId(id);
            UrunModel urunmodel = new UrunModel();
            List<UrunModel> urunler = dataAccess.GetUrunByGurupId(id);
            urunmodel.urunlerlist = urunler;
            urunmodel.gurubununAdi = gurupAdi;
            return View(urunmodel);
        }

        [Route("/admin")]
        public IActionResult Admin()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
