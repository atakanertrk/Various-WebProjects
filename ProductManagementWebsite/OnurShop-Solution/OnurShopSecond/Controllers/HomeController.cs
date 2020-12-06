using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Office2013.Drawing.ChartStyle;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnurShopSecond.DataAccess;
using OnurShopSecond.Models;

namespace OnurShopSecond.Controllers
{

    public class HomeController : Controller
    {
        private IDataAccess _dataAccess;
        private IViewGroupModel _vgm;
        public HomeController(IDataAccess sqlDataAccess, IViewGroupModel viewGroupModel)
        {
            _dataAccess = sqlDataAccess;
            _vgm = viewGroupModel;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TumUrunGuruplariniGoster()
        {
            _vgm.urunGurubuListesi = _dataAccess.GetAllProductGroups();
            return View(_vgm);
        }

        public IActionResult UrunGurubunuGoster(int selectedGroupId)
        {
            _vgm.urunListesi = _dataAccess.GetProductsByGroupId(selectedGroupId);
            _vgm.UrunGurubununAdi = _dataAccess.GetGroupNameFromGroupId(selectedGroupId);
            return View(_vgm);
        }
        public IActionResult UrunuGoster(int selectedProductId)
        {
            _vgm.urunModel = _dataAccess.GetProductByProductId(selectedProductId);
            _vgm.UrunGurubununAdi = _dataAccess.GetGroupNameFromGroupId(_vgm.urunModel.GroupId);
            QRCodeGenerator generalOperations = new QRCodeGenerator($"https://onur.azurewebsites.net/Home/UrunuGoster?selectedProductId={selectedProductId}", selectedProductId);
            _vgm.qrCode = generalOperations._imageByte;
            return View(_vgm);
        }
        public IActionResult HUrunAra(ViewGroupModel vgm)
        {
            vgm.urunListesi = _dataAccess.SearchProduct(vgm.arananText);
            vgm.dataAccess = _dataAccess;
            return View(vgm);
        }


    }
}
