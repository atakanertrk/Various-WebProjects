using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SecimServerApi.DataAccess;
using SecimServerApi.Models;
using SecimServerApi.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecimServerApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DeveloperHelperController : ControllerBase
    {
        IDataAccess _dataAccess;
        IConfiguration _config;
        public DeveloperHelperController(IConfiguration config)
        {
            _config = config;
            _dataAccess = new MySqlDataAccess(config);
        }

        [HttpPost]
        public IActionResult SecmenEkle([FromBody] SecmenModel model)
        {
            model.Sifre = CryptoOperations.EncryptSHA256(model.TcNo + model.Sifre);
            _dataAccess.InsertSecmen(model);
            return Ok();
        }

        [HttpPost]
        public IActionResult OylariTemizle()
        {
            _dataAccess.OylariTemizle();
            return Ok();
        }

        [HttpGet]
        public IActionResult GetAllSecmenler()
        {
            var secmenler = _dataAccess.GetAllSecmenler();
            return Ok(secmenler);
        }
    }
}
