using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ZMO_Eskisehir_WebUI.DataAccess;
using ZMO_Eskisehir_WebUI.Models;
using ZMO_Eskisehir_WebUI.Helpers;
using Microsoft.Extensions.Configuration;

namespace ZMO_Eskisehir_WebUI.Controllers
{
    // not using cookie based auth because of hosting issues, we transport password value for each call and check if the password is correct
    // [Authorize]
    public class AdminController : Controller
    {
        private readonly IHostingEnvironment _environment;
        private SqlDataAccess dataAccess;
        private PasswordControl pw;
        // Constructor
        public AdminController(IHostingEnvironment IHostingEnvironment, IConfiguration config)
        {
            _environment = IHostingEnvironment;
            dataAccess = new SqlDataAccess(config);
            pw = new PasswordControl(config);
        }

        public IActionResult Giris()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Giris(GroupOfModels group)
        {
            if (pw.IsPasswordCorrect(group.password))
            {
                return RedirectToAction("Index", group);
            }
            else
            {
                return View();
            }
        }

        public IActionResult Index(GroupOfModels group)
        {
            if (pw.IsPasswordCorrect(group.password))
            {
                return View(group);
            }
            else
            {
                return RedirectToAction("Giris");
            }
        }
        /* Haber Control */
        public IActionResult HaberlerDuzenle(GroupOfModels group)
        {
            if (pw.IsPasswordCorrect(group.password))
            {
                group.haberModel.haberler = dataAccess.GetHaberler();
                group.haberModel.haberler = group.haberModel.haberler.OrderByDescending(x => x.Tarih).ToList();
                return View(group);
            }
            else
            {
                return RedirectToAction("Giris");
            }
        }
        [HttpPost]
        public IActionResult HaberEkle(GroupOfModels group)
        {
            if (pw.IsPasswordCorrect(group.password))
            {
                int id = dataAccess.InsertHaber(group.haberModel);
                group.id = id;
                return RedirectToAction("HabereFotoEkle", group);
            }
            else
            {
                return RedirectToAction("Giris");
            }

        }
        public IActionResult HabereFotoEkle(GroupOfModels group)
        {
            if (pw.IsPasswordCorrect(group.password))
            {
                return View(group);
            }
            else
            {
                return RedirectToAction("Giris");
            }
        }
        [HttpPost]
        public IActionResult HabereFotoEklePost(GroupOfModels group)
        {
            if (pw.IsPasswordCorrect(group.password))
            {
                var files = HttpContext.Request.Form.Files;
                if (HttpContext.Request.Form.Files != null)
                {
                    foreach (var file in files)
                    {
                        if (file.Length > 0)
                        {
                            //Assigning Unique Filename (Guid)
                            var myUniqueFileName = group.id.ToString();
                            // concating  FileName + FileExtension
                            string newFileName = myUniqueFileName + ".jpg";

                            // Combines two strings into a path.
                            // string fileName = Path.Combine(_environment.WebRootPath, @"Resources\Images\Haberler") + $@"\{newFileName}";
                            // 
                            string fileName = Path.Combine(_environment.WebRootPath, @"Resources\Images\Haberler") + $@"\{newFileName}";
                            using (FileStream fs = System.IO.File.Create(fileName))
                            {
                                file.CopyTo(fs);
                                fs.Flush();
                            }
                        }
                    }
                }
                return RedirectToAction("HaberlerDuzenle", group);
            }
            else
            {
                return RedirectToAction("Giris");
            }

        }
        [HttpPost]
        public IActionResult HaberiSil(GroupOfModels group)
        {
            if (pw.IsPasswordCorrect(group.password))
            {
                dataAccess.DeleteHaber(group.id);
                string fileName = Path.Combine(_environment.WebRootPath, @"Resources\Images\Haberler") + $@"\{group.id}.jpg";
                System.IO.File.Delete(fileName);
                return RedirectToAction("HaberlerDuzenle", group);
            }
            else
            {
                return RedirectToAction("Giris");
            }
        }
        public IActionResult HaberUpdate(GroupOfModels group)
        {
            if (pw.IsPasswordCorrect(group.password))
            {
                group.haberModel.Id = group.id;
                group.haberModel = dataAccess.ByIdGetHaber(group.id).FirstOrDefault();
                return View(group);
            }
            else
            {
                return RedirectToAction("Giris");
            }
        }
        [HttpPost]
        public IActionResult HaberUpdatePost(GroupOfModels group)
        {
            if (pw.IsPasswordCorrect(group.password))
            {
                dataAccess.UpdateHaber(group.haberModel);
                return RedirectToAction("HaberlerDuzenle",group);
            }
            else
            {
                return RedirectToAction("Giris");
            }
        }

        /* Etkinlik Control */

        public IActionResult EtkinliklerDuzenle(GroupOfModels group)
        {
            if (pw.IsPasswordCorrect(group.password))
            {
                group.etkinlikModel.etkinlikler = dataAccess.GetEtkinlikler();
                group.etkinlikModel.etkinlikler = group.etkinlikModel.etkinlikler.OrderByDescending(x => x.Tarih).ToList();
                return View(group);
            }
            else
            {
                return RedirectToAction("Giris");
            }
        }
        [HttpPost]
        public IActionResult EtkinlikEkle(GroupOfModels group)
        {
            if (pw.IsPasswordCorrect(group.password))
            {
                int id = dataAccess.InsertEtkinlik(group.etkinlikModel);
                group.id = id;
                return RedirectToAction("EtkinlikFotoEkle", group);
            }
            else
            {
                return RedirectToAction("Giris");
            }
        }
        public IActionResult EtkinlikFotoEkle(GroupOfModels group)
        {
            if (pw.IsPasswordCorrect(group.password))
            {
                return View(group);
            }
            else
            {
                return RedirectToAction("Giris");
            }
            
        }
        [HttpPost]
        public IActionResult EtkinlikFotoEklePost(GroupOfModels group)
        {
            if (pw.IsPasswordCorrect(group.password))
            {
                var files = HttpContext.Request.Form.Files;
                if (HttpContext.Request.Form.Files != null)
                {
                    foreach (var file in files)
                    {
                        if (file.Length > 0)
                        {
                            //Assigning Unique Filename (Guid)
                            var myUniqueFileName = group.id.ToString();

                            // concating  FileName + FileExtension
                            string newFileName = myUniqueFileName + ".jpg";

                            // Combines two strings into a path.
                            string fileName = Path.Combine(_environment.WebRootPath, @"Resources\Images\Etkinlikler") + $@"\{newFileName}";

                            using (FileStream fs = System.IO.File.Create(fileName))
                            {
                                file.CopyTo(fs);
                                fs.Flush();
                            }
                        }
                    }
                }
                return RedirectToAction("EtkinliklerDuzenle",group);
            }
            else
            {
                return RedirectToAction("Giris");
            }
           
        }
        public IActionResult EtkinlikSil(GroupOfModels group)
        {
            if (pw.IsPasswordCorrect(group.password))
            {
                dataAccess.DeleteEtkinlik(group.id);
                string fileName = Path.Combine(_environment.WebRootPath, @"Resources\Images\Etkinlikler") + $@"\{group.id}.jpg";
                System.IO.File.Delete(fileName);
                return RedirectToAction("EtkinliklerDuzenle",group);
            }
            else
            {
                return RedirectToAction("Giris");
            }
           
        }

        public IActionResult EtkinlikUpdate(GroupOfModels group)
        {
            if (pw.IsPasswordCorrect(group.password))
            {
                group.etkinlikModel.Id = group.id;
                group.etkinlikModel = dataAccess.ByIdGetEtkinlik(group.id).FirstOrDefault();
                return View(group);
            }
            else
            {
                return RedirectToAction("Giris");
            }
        }
        [HttpPost]
        public IActionResult EtkinlikUpdatePost(GroupOfModels group)
        {
            if (pw.IsPasswordCorrect(group.password))
            {
                dataAccess.UpdateEtkinlik(group.etkinlikModel);
                return RedirectToAction("EtkinliklerDuzenle", group);
            }
            else
            {
                return RedirectToAction("Giris");
            }
        }

        /* BasindaBiz Control */

        public IActionResult BasindaBizDuzenle(GroupOfModels group)
        {
            if (pw.IsPasswordCorrect(group.password))
            {
                group.BasindaBizModel.basindaBizKayitlar = dataAccess.GetBasindaBizKayitlar();
                group.BasindaBizModel.basindaBizKayitlar = group.BasindaBizModel.basindaBizKayitlar.OrderByDescending(x => x.Tarih).ToList();
                return View(group);
            }
            else
            {
                return RedirectToAction("Giris");
            }
        }
        [HttpPost]
        public IActionResult BasindaBizEkle(GroupOfModels group)
        {
            if (pw.IsPasswordCorrect(group.password))
            {
                int id = dataAccess.InsertBasindaBiz(group.BasindaBizModel);
                group.id = id;
                return RedirectToAction("BasindaBizFotoEkle", group);
            }
            else
            {
                return RedirectToAction("Giris");
            }
        }
        public IActionResult BasindaBizFotoEkle(GroupOfModels group)
        {
            if (pw.IsPasswordCorrect(group.password))
            {
                return View(group);
            }
            else
            {
                return RedirectToAction("Giris");
            }
        }
        [HttpPost]
        public IActionResult BasindaBizFotoEklePost(GroupOfModels group)
        {
            if (pw.IsPasswordCorrect(group.password))
            {
                var files = HttpContext.Request.Form.Files;
                if (HttpContext.Request.Form.Files != null)
                {
                    foreach (var file in files)
                    {
                        if (file.Length > 0)
                        {
                            //Assigning Unique Filename (Guid)
                            var myUniqueFileName = group.id.ToString();

                            // concating  FileName + FileExtension
                            string newFileName = myUniqueFileName + ".jpg";

                            // Combines two strings into a path.
                            string fileName = Path.Combine(_environment.WebRootPath, @"Resources\Images\BasindaBiz") + $@"\{newFileName}";

                            using (FileStream fs = System.IO.File.Create(fileName))
                            {
                                file.CopyTo(fs);
                                fs.Flush();
                            }
                        }
                    }
                }
                return RedirectToAction("BasindaBizDuzenle", group);
            }
            else
            {
                return RedirectToAction("Giris");
            }
        }
        public IActionResult BasindaBizSil(GroupOfModels group)
        {
            if (pw.IsPasswordCorrect(group.password))
            {
                dataAccess.DeleteBasindaBiz(group.id);
                string fileName = Path.Combine(_environment.WebRootPath, @"Resources\Images\BasindaBiz") + $@"\{group.id}.jpg";
                System.IO.File.Delete(fileName);
                return RedirectToAction("BasindaBizDuzenle", group);
            }
            else
            {
                return RedirectToAction("Giris");
            }
        }

        public IActionResult BasindaBizUpdate(GroupOfModels group)
        {
            if (pw.IsPasswordCorrect(group.password))
            {
                group.BasindaBizModel.Id = group.id;
                group.BasindaBizModel = dataAccess.ByIdGetBasindaBiz(group.id).FirstOrDefault();
                return View(group);
            }
            else
            {
                return RedirectToAction("Giris");
            }
        }
        [HttpPost]
        public IActionResult BasindaBizUpdatePost(GroupOfModels group)
        {
            if (pw.IsPasswordCorrect(group.password))
            {
                dataAccess.UpdateBasindaBiz(group.BasindaBizModel);
                return RedirectToAction("BasindaBizDuzenle", group);
            }
            else
            {
                return RedirectToAction("Giris");
            }
        }


        /* BASİN BULTENLERİMİZ CONTROL */

        public IActionResult BasinBultenlerimizDuzenle(GroupOfModels group)
        {
            if (pw.IsPasswordCorrect(group.password))
            {
                group.basinBultenlerimizModel.bultenler = dataAccess.GetBasinBultenlerimiz();
                group.basinBultenlerimizModel.bultenler = group.basinBultenlerimizModel.bultenler.OrderByDescending(x => x.Tarih).ToList();
                return View(group);
            }
            else
            {
                return RedirectToAction("Giris");
            }
        }
        [HttpPost]
        public IActionResult BultenEkle(GroupOfModels group)
        {
            if (pw.IsPasswordCorrect(group.password))
            {
                int id = dataAccess.InsertBasinBulteni(group.basinBultenlerimizModel);
                group.id = id;
                return RedirectToAction("BasinBultenlerimizDosyaEkle", group);
            }
            else
            {
                return RedirectToAction("Giris");
            }
        }
        public IActionResult BasinBultenlerimizDosyaEkle(GroupOfModels group)
        {
            if (pw.IsPasswordCorrect(group.password))
            {
                return View(group);
            }
            else
            {
                return RedirectToAction("Giris");
            }
        }
        [HttpPost]
        public IActionResult BasinBultenlerimizDosyaEklePost(GroupOfModels group)
        {
            if (pw.IsPasswordCorrect(group.password))
            {
                var files = HttpContext.Request.Form.Files;
                if (HttpContext.Request.Form.Files != null)
                {
                    foreach (var file in files)
                    {
                        if (file.Length > 0)
                        {
                            //Assigning Unique Filename (Guid)
                            var myUniqueFileName = group.id.ToString();

                            // concating  FileName + FileExtension
                            string newFileName = myUniqueFileName + ".doc";

                            // Combines two strings into a path.
                            string fileName = Path.Combine(_environment.WebRootPath, @"Resources\Documents\BasinBultenleri") + $@"\{newFileName}";

                            using (FileStream fs = System.IO.File.Create(fileName))
                            {
                                file.CopyTo(fs);
                                fs.Flush();
                            }
                        }
                    }
                }
                return RedirectToAction("BasinBultenlerimizDuzenle", group);
            }
            else
            {
                return RedirectToAction("Giris");
            }
        }

        public IActionResult BulteniSil(GroupOfModels group)
        {
            if (pw.IsPasswordCorrect(group.password))
            {
                dataAccess.DeleteBasinBulteni(group.id);
                string fileName = Path.Combine(_environment.WebRootPath, @"Resources\Documents\BasinBultenleri") + $@"\{group.id}.doc";
                System.IO.File.Delete(fileName);
                return RedirectToAction("BasinBultenlerimizDuzenle", group);
            }
            else
            {
                return RedirectToAction("Giris");
            }
        }

        public IActionResult BasinBulteniUpdate(GroupOfModels group)
        {
            if (pw.IsPasswordCorrect(group.password))
            {
                group.basinBultenlerimizModel.Id = group.id;
                group.basinBultenlerimizModel = dataAccess.ByIdGetBasinBulteni(group.id).FirstOrDefault();
                return View(group);
            }
            else
            {
                return RedirectToAction("Giris");
            }
        }
        [HttpPost]
        public IActionResult BasinBulteniUpdatePost(GroupOfModels group)
        {
            if (pw.IsPasswordCorrect(group.password))
            {
                dataAccess.UpdateBasinBulteni(group.basinBultenlerimizModel);
                return RedirectToAction("BasinBultenlerimizDuzenle", group);
            }
            else
            {
                return RedirectToAction("Giris");
            }
        }

        /* Duyuru Control */

        public IActionResult DuyurularDuzenle(GroupOfModels group)
        {
            if (pw.IsPasswordCorrect(group.password))
            {
                group.duyuruModel.duyurular = dataAccess.GetDuyurular();
                group.duyuruModel.duyurular = group.duyuruModel.duyurular.OrderByDescending(x => x.Tarih).ToList();
                return View(group);
            }
            else
            {
                return RedirectToAction("Giris");
            }
        }
        [HttpPost]
        public IActionResult DuyuruEkle(GroupOfModels group)
        {
            if (pw.IsPasswordCorrect(group.password))
            {
                dataAccess.InsertDuyuru(group.duyuruModel);
                return RedirectToAction("DuyurularDuzenle",group);
            }
            else
            {
                return RedirectToAction("Giris");
            }
        }
        [HttpPost]
        public IActionResult DuyuruSil(GroupOfModels group)
        {
            if (pw.IsPasswordCorrect(group.password))
            {
                dataAccess.DeleteDuyuru(group.id);
                return RedirectToAction("DuyurularDuzenle",group);
            }
            else
            {
                return RedirectToAction("Giris");
            }
            
        }
        public IActionResult DuyuruUpdate(GroupOfModels group)
        {
            if (pw.IsPasswordCorrect(group.password))
            {
                group.duyuruModel.Id = group.id;
                group.duyuruModel = dataAccess.ByIdGetDuyuru(group.id).FirstOrDefault();
                return View(group);
            }
            else
            {
                return RedirectToAction("Giris");
            }
        }
        [HttpPost]
        public IActionResult DuyuruUpdatePost(GroupOfModels group)
        {
            if (pw.IsPasswordCorrect(group.password))
            {
                dataAccess.UpdateDuyuru(group.duyuruModel);
                return RedirectToAction("DuyurularDuzenle", group);
            }
            else
            {
                return RedirectToAction("Giris");
            }
        }

        /* Eğitim Control */
        public IActionResult EgitimlerDuzenle(GroupOfModels group)
        {
            if (pw.IsPasswordCorrect(group.password))
            {
                group.egitimlerModel.egitimler = dataAccess.GetEgitimler();
                group.egitimlerModel.egitimler = group.egitimlerModel.egitimler.OrderByDescending(x => x.Tarih).ToList();
                return View(group);
            }
            else
            {
                return RedirectToAction("Giris");
            }

        }
        [HttpPost]
        public IActionResult EgitimEkle(GroupOfModels group)
        {
            if (pw.IsPasswordCorrect(group.password))
            {
                int id = dataAccess.InsertEgitim(group.egitimlerModel);
                group.id = id;
                return RedirectToAction("EgitimFotoEkle",group);
            }
            else
            {
                return RedirectToAction("Giris");
            }

        }
        public IActionResult EgitimFotoEkle(GroupOfModels group)
        {
            if (pw.IsPasswordCorrect(group.password))
            {
                return View(group);
            }
            else
            {
                return RedirectToAction("Giris");
            }
        }
        [HttpPost]
        public IActionResult EgitimFotoEklePost(GroupOfModels group)
        {
            if (pw.IsPasswordCorrect(group.password))
            {
                var files = HttpContext.Request.Form.Files;
                if (HttpContext.Request.Form.Files != null)
                {
                    foreach (var file in files)
                    {
                        if (file.Length > 0)
                        {
                            //Assigning Unique Filename (Guid)
                            var myUniqueFileName = group.id.ToString();

                            // concating  FileName + FileExtension
                            string newFileName = myUniqueFileName + ".jpg";

                            // Combines two strings into a path.
                            string fileName = Path.Combine(_environment.WebRootPath, @"Resources\Images\Egitimlerimiz") + $@"\{newFileName}";

                            using (FileStream fs = System.IO.File.Create(fileName))
                            {
                                file.CopyTo(fs);
                                fs.Flush();
                            }
                        }
                    }
                }
                return RedirectToAction("EgitimlerDuzenle", group);
            }
            else
            {
                return RedirectToAction("Giris");
            }
        }
        [HttpPost]
        public IActionResult EgitimSil(GroupOfModels group)
        {
            if (pw.IsPasswordCorrect(group.password))
            {
                dataAccess.DeleteEgitim(group.id);
                string fileName = Path.Combine(_environment.WebRootPath, @"Resources\Images\Egitimlerimiz") + $@"\{group.id}.jpg";
                System.IO.File.Delete(fileName);
                return RedirectToAction("EgitimlerDuzenle", group);
            }
            else
            {
                return RedirectToAction("Giris");
            }

        }

        public IActionResult EgitimUpdate(GroupOfModels group)
        {
            if (pw.IsPasswordCorrect(group.password))
            {
                group.egitimlerModel.Id = group.id;
                group.egitimlerModel = dataAccess.ByIdGetEgitim(group.id).FirstOrDefault();
                return View(group);
            }
            else
            {
                return RedirectToAction("Giris");
            }
        }
        [HttpPost]
        public IActionResult EgitimUpdatePost(GroupOfModels group)
        {
            if (pw.IsPasswordCorrect(group.password))
            {
                dataAccess.UpdateEgitim(group.egitimlerModel);
                return RedirectToAction("EgitimlerDuzenle", group);
            }
            else
            {
                return RedirectToAction("Giris");
            }
        }

        /* diğer */

        public IActionResult CikisYap()
        {
            return RedirectToAction("Giris");
        }

    }
}