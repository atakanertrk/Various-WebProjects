using Dapper;
using RestoranOtomasyonuWebApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace RestoranOtomasyonuWebApp.DataAccess
{
    public class SqlDataAccess : IDataAccess
    {
        string connstr = @"";
        public void ChangeGurupNameById(int gurupId, string yeniGurupAdi)
        {
            using (IDbConnection connection = new SqlConnection(connstr))
            {
                var p = new DynamicParameters();
                p.Add("@gurupid", gurupId);
                p.Add("@yeniGurupAdi", yeniGurupAdi);

                string sql = "update UrunGuruplari SET gurupAdi=@yeniGurupAdi WHERE Id=@gurupid";

                connection.Execute(sql, p);
            }
        }
        public string GetGurupNameByGurupId(int id)
        {
            using (IDbConnection connection = new SqlConnection(connstr))
            {
                var p = new DynamicParameters();
                p.Add("@gurupId", id);

                string sql = "select gurupAdi from UrunGuruplari WHERE Id=@gurupId";

                var gurupAdi = connection.Query<string>(sql, p).ToList().First();
                return gurupAdi;
            }
        }
        public List<UrunModel> GetUrunByGurupId(int gurupid)
        {
            using (IDbConnection connection = new SqlConnection(connstr))
            {
                var p = new DynamicParameters();
                p.Add("@gurupid", gurupid);

                string sql = "select * from Urunler where gurupId=@gurupid";

                var output = connection.Query<UrunModel>(sql, p).ToList();
                return output;
            }
        }
        public string GetYoneticiSifresi()
        {
            using (IDbConnection connection = new SqlConnection(connstr))
            {
                var sifre = connection.Query<string>($"select YoneticiSifresi from YoneticiSifre").ToList().First();
                return sifre;
            }
        }
        public SiteEkstraModel GetSiteEkstra() 
        {
            using (IDbConnection connection = new SqlConnection(connstr))
            {
                string sql = " SELECT * from SiteEkstra;";

                var result = connection.Query<SiteEkstraModel>(sql).ToList().FirstOrDefault();
                return result;
            }
        }
        public void SetSiteEkstra(SiteEkstraModel model) 
        {
            using (IDbConnection connection = new SqlConnection(connstr))
            {
                var p = new DynamicParameters();
                p.Add("@adresaciklama", model.adresAciklama);
                p.Add("@telefon", model.telefon);
                p.Add("@mesaj", model.anaSayfaMesaji);

                string sql = "UPDATE SiteEkstra SET anaSayfaMesaji=@mesaj, telefon=@telefon, adresAciklama=@adresaciklama";

                connection.Execute(sql, p);
            }
        }
        public List<UrunModel> GurupIcindekiUrunlerinIdListesi(int gurupId)
        {
            using (IDbConnection connection = new SqlConnection(connstr))
            {
                var p = new DynamicParameters();
                p.Add("@gurupId", gurupId);

                string sql = "SELECT Id FROM Urunler WHERE gurupId=@gurupId";

                var id = connection.Query<UrunModel>(sql, p).ToList();
                return id;
            }
        }
        public UrunModel UrunBilgileriByUrunId(int urunId)
        {
            using (IDbConnection connection = new SqlConnection(connstr))
            {
                var p = new DynamicParameters();
                p.Add("@urunId", urunId);

                string sql = "SELECT * FROM Urunler WHERE Id=@urunId";

                var id = connection.Query<UrunModel>(sql, p).ToList();
                return id.First();
            }
        }
        public void MasaEkle(int masaNo)
        {
            using (IDbConnection connection = new SqlConnection(connstr))
            {
                var p = new DynamicParameters();
                p.Add("@masano", masaNo);

                string sql = "INSERT INTO Masalar (masaNo) VALUES (@masano);";

                connection.Execute(sql, p);
            }
        }
        public void MasaSil(int masaNo)
        {
            MasaNotuSil(masaNo);
            using (IDbConnection connection = new SqlConnection(connstr))
            {
                var p = new DynamicParameters();
                p.Add("@masano", masaNo);

                string sql = " DELETE FROM AktifSiparisler WHERE masaNo=@masano; DELETE FROM Masalar WHERE masaNo=@masano ;";

                connection.Execute(sql, p);
            }
        }
        public void MasaNotuEkle(int masaNo, string masaNotu)
        {
            using (IDbConnection connection = new SqlConnection(connstr))
            {
                var p = new DynamicParameters();
                p.Add("@masano", masaNo);
                p.Add("@masanotu", masaNotu);

                string sql = "UPDATE Masalar SET masaNotu=@masanotu WHERE masaNo=@masano";

                connection.Execute(sql, p);
            }
        }
        public void MasaNotuSil(int masaNo)
        {
            using (IDbConnection connection = new SqlConnection(connstr))
            {
                var p = new DynamicParameters();
                p.Add("@masano", masaNo);
                string x = "";
                p.Add("@masanotu",x);
                string sql = "UPDATE Masalar SET masaNotu=@masanotu WHERE masaNo=@masano;";

                connection.Execute(sql, p);
            }
        }
        public void MasaSiparisleriniSil(int masaNo)
        {
            using (IDbConnection connection = new SqlConnection(connstr))
            {
                var p = new DynamicParameters();
                p.Add("@masano", masaNo);

                string sql = " DELETE FROM AktifSiparisler WHERE masaNo=@masano;";

                connection.Execute(sql, p);
            }
        }
        public string GetMasaNotu(int masaNo)
        {
            using (IDbConnection connection = new SqlConnection(connstr))
            {
                var p = new DynamicParameters();
                p.Add("@masaNo", masaNo);

                string sql = " SELECT masaNotu from Masalar WHERE masaNo=@masaNo;";

                string not = connection.Query<string>(sql,p).First();
                return not;
            }
        }
        public string GetSiparisIslemleriGirisSifresi()
        {
            using (IDbConnection connection = new SqlConnection(connstr))
            {
                var sifre = connection.Query<string>($"select SiparisIslemiSifresi from SiparisIslemiSifre").ToList().First();
                return sifre;
            }
        }
        public List<int> GetMasaNumaralariList()
        {
            using (IDbConnection connection = new SqlConnection(connstr))
            {
                string sql = "SELECT masaNo FROM Masalar";

                var numaralar = connection.Query<int>(sql).ToList();
                return numaralar;
            }
        }
        public List<SiparisModel> GetMasaSiparisleri(int masaNo)
        {
            using (IDbConnection connection = new SqlConnection(connstr))
            {
                var p = new DynamicParameters();
                p.Add("@masano", masaNo);

                string sql = "SELECT * FROM AktifSiparisler WHERE masaNo=@masano";

                var masayaAitSiparisler = connection.Query<SiparisModel>(sql, p).ToList();
                return masayaAitSiparisler;
            }
        }
        public void SiparisEkle(int masaNo,int urunId,decimal adet,string siparisNotu)
        {
            using (IDbConnection connection = new SqlConnection(connstr))
            {
                var p = new DynamicParameters();
                p.Add("@masano", masaNo);
                p.Add("@urunId", urunId);
                p.Add("@adet", adet);
                p.Add("@siparisNotu", siparisNotu);
                // default olarak teslimEdildiMi "hayir" olarak set ediliyor
                string sql = "INSERT INTO AktifSiparisler (masaNo,urunId,adet,siparisNotu) VALUES (@masano,@urunId,@adet,@siparisNotu) ;";

                connection.Execute(sql, p);
            }
        }
        public string GetSiparisNotu(int siparisId)
        {
            using (IDbConnection connection = new SqlConnection(connstr))
            {
                var p = new DynamicParameters();
                p.Add("@siparisId", siparisId);

                string sql = " SELECT siparisNotu from AktifSiparisler WHERE Id=@siparisId;";

                string not = connection.Query<string>(sql, p).First();
                return not;
            }
        }
        public void SiparisSil(int siparisId)
        {
            using (IDbConnection connection = new SqlConnection(connstr))
            {
                var p = new DynamicParameters();
                p.Add("@siparisId", siparisId);

                string sql = "DELETE FROM AktifSiparisler WHERE Id=@siparisId";

                connection.Execute(sql, p);
            }
        }
        public void TamamlanmislaraEkle(string urunAdi, decimal urunFiyati,decimal adet,int masaNo)
        {
            /* ONLY DATE
             DateTime dateTime = DateTime.UtcNow.AddHours(3).Date;
             string tarih = dateTime.ToString("MM/dd/yyyy");
             */
            DateTime date = DateTime.UtcNow.AddHours(3);
            using (IDbConnection connection = new SqlConnection(connstr))
            {
                var p = new DynamicParameters();
                p.Add("@urunAdi", urunAdi);
                p.Add("@urunFiyati", urunFiyati);
                p.Add("@adet", adet);
                p.Add("@tarih", date);
                p.Add("@masaNo", masaNo);

                string sql = "INSERT INTO TamamlanmisSiparisler (urunAdi,urunFiyati,adet,tarih,masaNo) VALUES (@urunAdi,@urunFiyati,@adet,@tarih,@masaNo) ;";

                connection.Execute(sql, p);
            }
        }
        public void StokDurumuDegistir(int urunId, string yeniStokDurumu)
        {
            using (IDbConnection connection = new SqlConnection(connstr))
            {
                var p = new DynamicParameters();
                p.Add("@urunId", urunId);
                p.Add("@yeniStokDurumu", yeniStokDurumu);
               
                string sql = "UPDATE Urunler SET stokVarYok=@yeniStokDurumu WHERE Id=@urunId";

                connection.Execute(sql, p);
            }
        }
        public List<TamamlanmisSiparisModel> GetTamamlanmisSiparislerByDate(DateTime date)
        {
            string tarih = date.ToString("yyyy-MM-dd");
            string tarihstart = tarih + " 00:00:00";
            string tarihend = tarih + " 23:59:00";
            //select * from tamamlanmissiparisler where tarih>'2020-11-22 00:00:00' and tarih<'2020-11-22 23:59:00'
            using (IDbConnection connection = new SqlConnection(connstr))
            {
                var p = new DynamicParameters();
                p.Add("@tarihstart", tarihstart);
                p.Add("@tarihend", tarihend);
                string sql = "SELECT * FROM TamamlanmisSiparisler WHERE tarih>=@tarihstart and tarih<=@tarihend";

                var output = connection.Query<TamamlanmisSiparisModel>(sql, p).ToList();

                output = output.OrderByDescending(o => o.Id).ToList();

                return output;
            }
        }
        // şu tarihteki herbir farklı ürün adı ne
        public List<string> TamamlanmisUrunAdlariByDate(DateTime date)
        {
            string tarih = date.ToString("yyyy-MM-dd");
            string tarihstart = tarih + " 00:00:00";
            string tarihend = tarih + " 23:59:00";
            //select * from tamamlanmissiparisler where tarih>'2020-11-22 00:00:00' and tarih<'2020-11-22 23:59:00'
            using (IDbConnection connection = new SqlConnection(connstr))
            {
                var p = new DynamicParameters();
                p.Add("@tarihstart", tarihstart);
                p.Add("@tarihend", tarihend);
                string sql = "select Distinct(urunAdi) from TamamlanmisSiparisler WHERE tarih>=@tarihstart and tarih<=@tarihend;";

                var output = connection.Query<string>(sql, p).ToList();

                return output;
            }
        }
        // şu tarihte şu üründen kaç adet
        public decimal TamamlanmisUrununMiktariByDate(DateTime date, string urunadi)
        {
            string tarih = date.ToString("yyyy-MM-dd");
            string tarihstart = tarih + " 00:00:00";
            string tarihend = tarih + " 23:59:00";
            //select * from tamamlanmissiparisler where tarih>'2020-11-22 00:00:00' and tarih<'2020-11-22 23:59:00'
            using (IDbConnection connection = new SqlConnection(connstr))
            {
                var p = new DynamicParameters();
                p.Add("@tarihstart", tarihstart);
                p.Add("@tarihend", tarihend);
                p.Add("@urunadi", urunadi);
                string sql = "select SUM(adet) from tamamlanmissiparisler WHERE urunAdi=@urunadi and tarih>=@tarihstart and tarih<=@tarihend;";

                var output = connection.Query<decimal>(sql, p).ToList().FirstOrDefault();

                return output;
            }
        }
        public decimal MasaToplamUcreti(int masaNo)
        {
            var p = new DynamicParameters();
            p.Add("@masaNo", masaNo);
            using (IDbConnection connection = new SqlConnection(connstr))
            {
                string sql = "SELECT COALESCE(SUM(urunFiyati),0) FROM Urunler WHERE Id IN (SELECT urunId FROM AktifSiparisler WHERE masaNo=@masaNo);";
                decimal toplam = connection.Query<decimal>(sql,p).ToList().First();
                return toplam;
            }
        }
        public void UrunBilgileriGuncelle(UrunModel urunModel)
        {
            using (IDbConnection connection = new SqlConnection(connstr))
            {
                var p = new DynamicParameters();
                p.Add("@urunAdi", urunModel.urunAdi);
                p.Add("@urunAciklamasi", urunModel.urunAciklamasi);
                p.Add("@urunFiyati", urunModel.urunFiyati);
                p.Add("@Id", urunModel.Id);

                string sql = "UPDATE Urunler SET urunAdi=@urunAdi, urunAciklamasi=@urunAciklamasi, urunFiyati=@urunFiyati WHERE Id=@Id";

                connection.Execute(sql, p);
            }
        }
        public int UrunEkle(UrunModel urunModel)
        {
            using (IDbConnection connection = new SqlConnection(connstr))
            {
                var p = new DynamicParameters();
                p.Add("@gurupId", urunModel.gurupId);
                p.Add("@urunAdi", urunModel.urunAdi);
                p.Add("@urunAciklamasi", urunModel.urunAciklamasi);
                p.Add("@urunFiyati", urunModel.urunFiyati);
                p.Add("@stokVarYok", "var");

                string sql = "INSERT INTO Urunler (gurupId,urunAdi,urunAciklamasi,urunFiyati,stokVarYok) VALUES (@gurupId,@urunAdi,@urunAciklamasi,@urunFiyati,@stokVarYok); SELECT SCOPE_IDENTITY();";

                var insertedId = connection.Query<int>(sql, p).ToList().First();
                return insertedId;
            }
        }
        public void UrunGurubunuSil(int id)
        {
            using (IDbConnection connection = new SqlConnection(connstr))
            {
                var p = new DynamicParameters();
                p.Add("@gurupId", id);

                string sql = "DELETE FROM UrunGuruplari WHERE Id=@gurupId";

                connection.Execute(sql, p);
            }
        }
        public List<UrunGurubuModel> UrunGuruplari()
        {
            using (IDbConnection connection = new SqlConnection(connstr))
            {
                var output = connection.Query<UrunGurubuModel>($"select * from UrunGuruplari").ToList();
                return output;
            }
        }
        public void UrunSilById(int id)
        {
            using (IDbConnection connection = new SqlConnection(connstr))
            {
                var p1 = new DynamicParameters();
                p1.Add("@urunId", id);

                string sql1 = "SELECT COUNT(*) FROM AktifSiparisler WHERE urunId=@urunId";

                int count = connection.Query<int>(sql1, p1).ToList().FirstOrDefault();
                if (count > 0)
                {
                    throw new InvalidOperationException("bu ürüne ait aktif bir sipariş var, silme işlemi başarısız");
                }
                else
                {
                    var p = new DynamicParameters();
                    p.Add("@urunId", id);

                    string sql = "DELETE FROM Urunler WHERE Id=@urunId";

                    connection.Execute(sql, p);
                }
            }
        }

        public void SiparisTeslimEdildi(int siparisId)
        {
            using (IDbConnection connection = new SqlConnection(connstr))
            {
                string teslim = "evet";
                var p = new DynamicParameters();
                p.Add("@siparisId", siparisId);
                p.Add("@teslim", teslim);

                string sql = "UPDATE AktifSiparisler SET teslimEdildiMi=@teslim WHERE Id=@siparisId";

                connection.Execute(sql, p);
            }
        }

        public int YeniGurupEkle(string yeniGurupAdi)
        {
            using (IDbConnection connection = new SqlConnection(connstr))
            {
                var p = new DynamicParameters();
                p.Add("@yeniGurupAdi", yeniGurupAdi);

                string sql = "INSERT INTO UrunGuruplari (gurupAdi) VALUES (@yeniGurupAdi); SELECT SCOPE_IDENTITY();";

                int id = connection.QuerySingle<int>(sql, p);
                return id;
            }
        }
       
    }
}