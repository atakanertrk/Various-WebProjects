using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SecimServerApi.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SecimServerApi.Models;
using SecimServerApi.Operations;

namespace SecimServerApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SecimController : ControllerBase
    {
        IDataAccess _dataAccess;
        IConfiguration _config;
        SecimIslemleri _islem;
        public SecimController(IConfiguration config)
        {
            _islem = new SecimIslemleri(config);
            _config = config;
            _dataAccess = new MySqlDataAccess(config);
        }

        [HttpPost]
        public IActionResult IsUserValid([FromBody] UserInfo userInfo)
        {
            if (_islem.UserValidationControl(userInfo.hashedUserInfo))
            {
                return Ok();
            }
            return StatusCode(401); // not authorized
        }


        [HttpGet]
        public IActionResult GetListOfAday()
        {
            var response = _dataAccess.GetAdayList();
            return Ok(response);
        }
        public class OyKullanModel
        {
            // SHA256(TcNo+Sifre)
            public string AuthenticationValue { get; set; }
            // SHA256(tcno+adayno)
            public string SifreliOyu { get; set; }
        }
        [HttpPost]
        public IActionResult OyKullan([FromBody] OyKullanModel shaSifreliOy)
        {
            if (_islem.UserValidationControl(shaSifreliOy.AuthenticationValue))
            {
                var adaylar = _dataAccess.GetAdayList();
                var secmenler = _dataAccess.GetAllSecmenler();

                foreach (var secmen in secmenler)
                {
                    foreach (var aday in adaylar)
                    {
                        string possibility = CryptoOperations.EncryptSHA256(secmen.TcNo + aday.AdayNo);
                        if (possibility == shaSifreliOy.SifreliOyu)
                        {
                            if (secmen.OyKullandiMi == 0)
                            {
                                string rsaEncryptedOy = CryptoOperations.EncryptRSA(aday.AdayNo.ToString(), Keys.PubKey, "utf8");
                                _dataAccess.InsertOy(rsaEncryptedOy);
                                _dataAccess.UpdateSecmenOyDurumu(1, secmen.TcNo);
                                return Ok();
                            }
                        }
                    }
                }
            }
            return StatusCode(400);
        }

        [HttpGet]
        public IActionResult SecimSonuclari()
        {
            var secimIslemleri = new SecimIslemleri(_config);

            var output = secimIslemleri.SayimYap();

            return Ok(output);
        }

    }
}
