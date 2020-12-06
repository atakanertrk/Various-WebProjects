using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Drawing.Charts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using OnurShopSecond.DataAccess;
using OnurShopSecond.Models;
using OnurShopSecond.GeneralOperations.FluentDataValidation;
using FluentValidation.Results;

namespace OnurShopSecond.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IDataAccess _dataAccess;
        private IViewGroupModel _viewGroupModel;
        private ProductValidation productValidation;
        private GroupNameValidation groupNameValidation;
        public AdminController(IDataAccess sqlDataAccess, IViewGroupModel viewGroupModel)
        {
            _dataAccess = sqlDataAccess;
            _viewGroupModel = viewGroupModel;
            productValidation = new ProductValidation();
            groupNameValidation = new GroupNameValidation();
        }

        public IActionResult UrunGuruplari()
        {
            _viewGroupModel.urunGurubuListesi = _dataAccess.GetAllProductGroups() ;
            return View(_viewGroupModel);
        }
        [HttpPost]
        public IActionResult UrunGurubuEkle(ViewGroupModel vgm)
        {
            UrunGurubuModel urunGurubuModel = new UrunGurubuModel();
            urunGurubuModel.GroupName = vgm.EklenecekUrunGurubununAdi;
            ValidationResult result = groupNameValidation.Validate(urunGurubuModel);
            if (result.IsValid == false)
            {
                throw new InvalidOperationException(result.Errors.FirstOrDefault().ToString());
            }
            else
            {
                _dataAccess.AddNewProductGroup(vgm.EklenecekUrunGurubununAdi);
            }
            return RedirectToAction("UrunGuruplari");
        }

        public IActionResult UrunGurubu(int selectedGroupId)
        {
            var urunGrubununAdi = _dataAccess.GetGroupNameFromGroupId(selectedGroupId);
            _viewGroupModel.UrunGurubununAdi = urunGrubununAdi;
            _viewGroupModel.UrunGurubununId = selectedGroupId;
            _viewGroupModel.urunListesi = _dataAccess.GetProductsByGroupId(selectedGroupId);
            return View(_viewGroupModel);
        }
        public IActionResult UrunuDuzenle(int selectedProductId)
        {
            _viewGroupModel.urunModel = _dataAccess.GetProductByProductId(selectedProductId);
            _viewGroupModel.UrunGurubununAdi = _dataAccess.GetGroupNameFromGroupId(_viewGroupModel.urunModel.GroupId);
            _viewGroupModel.urunGurubuListesi = _dataAccess.GetAllProductGroups();
            return View(_viewGroupModel);
        }
        [HttpPost]
        public IActionResult UrunuDuzenle(ViewGroupModel vgm)
        {
            ValidationResult result = productValidation.Validate(vgm.GuncelUrunDegerleri);
            if (result.IsValid == false)
            {
                throw new InvalidOperationException(result.Errors.FirstOrDefault().ToString());
            }
            else
            {
                _dataAccess.UpdateProduct(vgm.GuncelUrunDegerleri);
            }
            int selectedGroupId = vgm.GuncelUrunDegerleri.GroupId;
            return RedirectToAction("UrunGurubu", new { selectedGroupId });
        }
        public IActionResult UrunGurubundakiUrunuSil(int selectedProductId)
        {
            UrunModel urunModel = _dataAccess.GetProductByProductId(selectedProductId);
            int selectedGroupId = urunModel.GroupId;
            _dataAccess.DeleteProductProductId(selectedProductId);
            return RedirectToAction("UrunGurubu", new { selectedGroupId });
        }
        public IActionResult UrunGurubunuSil(int selectedGroupId)
        {
            _dataAccess.DeleteGroupByGroupId(selectedGroupId);
            return RedirectToAction("UrunGuruplari");
        }
        public IActionResult UrunGurubunaUrunEkle(int selectedGroupId)
        {
            _viewGroupModel.UrunGurubununAdi = _dataAccess.GetGroupNameFromGroupId(selectedGroupId);
            _viewGroupModel.UrunGurubununId = selectedGroupId;
            return View(_viewGroupModel);
        }
        [HttpPost]
        public IActionResult UrunGurubunaUrunEkle(ViewGroupModel viewGroupModel)
        {
            ValidationResult validationResult = productValidation.Validate(viewGroupModel.urunModel);
            if (validationResult.IsValid == false)
            {
                throw new InvalidOperationException(validationResult.Errors.FirstOrDefault().ToString());
            }
            else
            {
                _dataAccess.AddNewProduct(viewGroupModel.urunModel);
            }
            int selectedGroupId = viewGroupModel.urunModel.GroupId;
            return RedirectToAction("UrunGurubu", new { selectedGroupId });
        }

        public IActionResult UrunGurubununAdiniDegistir(int selectedGroupId)
        {
            _viewGroupModel.UrunGurubununAdi = _dataAccess.GetGroupNameFromGroupId(selectedGroupId);
            _viewGroupModel.UrunGurubununId = selectedGroupId;
            return View(_viewGroupModel);
        }
        [HttpPost]
        public IActionResult UrunGurubununAdiniDegistir(ViewGroupModel vgm)
        {
            UrunGurubuModel urunGurubu = new UrunGurubuModel();
            urunGurubu.GroupName = vgm.YeniGurupAdi;
            ValidationResult result = groupNameValidation.Validate(urunGurubu);
            if (result.IsValid == false)
            {
                throw new InvalidOperationException(result.Errors.FirstOrDefault().ToString());
            }
            else
            {
                _dataAccess.ChangeGroupNameByGroupId(vgm.UrunGurubununId, vgm.YeniGurupAdi);
            }
            int selectedGroupId = vgm.UrunGurubununId;
            return RedirectToAction("UrunGurubu", new { selectedGroupId });
        }
        public IActionResult UrunAra(ViewGroupModel vgm)
        {
            vgm.urunListesi = _dataAccess.SearchProduct(vgm.arananText);
            vgm.dataAccess = _dataAccess;
            return View(vgm);
        }
    }
}
