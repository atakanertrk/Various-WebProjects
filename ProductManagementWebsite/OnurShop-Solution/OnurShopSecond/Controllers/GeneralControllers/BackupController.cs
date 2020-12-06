using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnurShopSecond.DataAccess;
using OnurShopSecond.Models;

namespace OnurShopSecond.Controllers
{
    public class BackupController : Controller
    {
        private IDataAccess _dataAccess;
        private IViewGroupModel _modelGroup;
        public BackupController(IDataAccess sqlDataAccess, IViewGroupModel viewGroupModel)
        {
            _dataAccess = sqlDataAccess;
            _modelGroup = viewGroupModel;
        }
        public async Task<ActionResult> Index()
        {
            return await Task.Run(() => {
                _modelGroup.urunGurubuListesi = _dataAccess.GetAllProductGroups();
                _modelGroup.urunListesi = _dataAccess.GetAllProducts();
                var gurupListesi = _modelGroup.urunGurubuListesi;
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("TümÜrünlerGuruplar");
                    int currentRow = 1;
                    worksheet.Cell(currentRow, 1).Value = $"Dosyanın Üretildiği Tarih : {DateTime.UtcNow.AddHours(3).Date}";
                    worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                    currentRow += 2;
                    foreach (var gurup in gurupListesi)
                    {
                        var urunler = _dataAccess.GetProductsByGroupId(gurup.GroupId);
                        foreach (var urun in urunler)
                        {
                            worksheet.ColumnWidth = 35;
                            worksheet.RowHeight = 30;
                            worksheet.Style.Font.FontSize = 18;
                            worksheet.Cell(currentRow, 1).Value = $"Gurubu : {gurup.GroupName} ";
                            worksheet.Cell(currentRow, 2).Value = $"Ürün Adı: {urun.ProductName}";
                            worksheet.Cell(currentRow, 3).Value = $"Ürün Özellikleri : {urun.Properties}";
                            worksheet.Cell(currentRow, 4).Value = $"Ürün Miktarı : {urun.Amount}";
                            worksheet.Cell(currentRow, 5).Value = $"Ürün Fiyarı : {urun.Price}";
                            worksheet.Cell(currentRow, 6).Value = $"Ürün Numarası : {urun.ProductId} ";
                            worksheet.Cell(currentRow, 7).Value = $"Gurup Numarası : {urun.GroupId}";
                            worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 1).Style.Font.FontColor = XLColor.Blue;
                            worksheet.Cell(currentRow, 2).Style.Font.FontColor = XLColor.Green;
                            currentRow += 1;
                        }
                        currentRow += 1;
                    }

                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        return File(content,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            "UrunlerVeGuruplariListe.xlsx"
                            );
                    }

                }
            });
        }
    }
}
