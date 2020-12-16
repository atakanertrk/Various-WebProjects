using Microsoft.Extensions.Configuration;
using SecimServerApi.DataAccess;
using SecimServerApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecimServerApi.Operations
{
    public class SecimIslemleri
    {
        IDataAccess _dataAccess;
        public SecimIslemleri(IConfiguration config)
        {
            _dataAccess = new MySqlDataAccess(config);
        }
        public bool UserValidationControl(string hashedUserInfo)
        {
            var secmenler = _dataAccess.GetAllSecmenler();
            foreach (var secmen in secmenler)
            {
                if (secmen.Sifre == hashedUserInfo)
                {
                    return true;
                }
            }
            return false;
        }

        public List<SayimModel> SayimYap()
        {
            var output = new List<SayimModel>();

            var encryptedOylar = _dataAccess.GetAllOylar();
            var decryptedOylar = new List<OyModel>();

            var adaylar = _dataAccess.GetAdayList();

            #region Şifreli Oyların Deşifrelenmesi
            foreach (var oyModel in encryptedOylar)
            {
                string decryptedOy = CryptoOperations.DecryptRSA(oyModel.Oy, Keys.PrivKey, "utf8");
                decryptedOylar.Add(new OyModel { Oy = decryptedOy });
            }
            #endregion

            #region Aday'a ait oyların sayılması
            foreach (var aday in adaylar)
            {
                var sayim = new SayimModel();
                sayim.Aday = aday;
                sayim.AdligiOySayisi = decryptedOylar.Where(x => x.Oy == aday.AdayNo.ToString()).Count();
                output.Add(sayim);
            }
            #endregion

            return output;
        }
    }
}