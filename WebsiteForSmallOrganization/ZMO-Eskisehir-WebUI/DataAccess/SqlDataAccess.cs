using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Dapper;
using ZMO_Eskisehir_WebUI.Models;
using Microsoft.Extensions.Configuration;

namespace ZMO_Eskisehir_WebUI.DataAccess
{
    public class SqlDataAccess : IDataAccess
    {
        private static string connstr;
        public SqlDataAccess(IConfiguration config)
        {
            connstr = config.GetConnectionString("SqlConnection");
        }
        public List<HaberModel> GetHaberler()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(connstr))
            {
                var output = connection.Query<HaberModel>($"select * from Haberler").ToList();
                return output;
            }
        }
        public List<EtkinlikModel> GetEtkinlikler()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(connstr))
            {
                var output = connection.Query<EtkinlikModel>($"select * from Etkinlikler").ToList();
                return output;
            }
        }

        public List<DuyuruModel> GetDuyurular()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(connstr))
            {
                var output = connection.Query<DuyuruModel>($"select * from Duyurular").ToList();
                return output;
            }
        }

        public List<EgitimlerModel> GetEgitimler()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(connstr))
            {
                var output = connection.Query<EgitimlerModel>($"select * from Egitimler").ToList();
                return output;
            }
        }
        public List<BasindaBizModel> GetBasindaBizKayitlar()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(connstr))
            {
                var output = connection.Query<BasindaBizModel>($"select * from BasindaBizKayitlar").ToList();
                return output;
            }
        }
        public List<BasinBultenlerimizModel> GetBasinBultenlerimiz()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(connstr))
            {
                var output = connection.Query<BasinBultenlerimizModel>($"select * from BasinBultenlerimiz").ToList();
                return output;
            }
        }
        public List<LoginModel> GetSifre()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(connstr))
            {
                var output = connection.Query<LoginModel>($"select * from Password").ToList();
                return output;
            }
        }

        /*   Admin İşlemleri */
        public int InsertHaber(HaberModel haberModel)
        {
            int id;
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(connstr))
            {
                id = connection.QuerySingle<int>("INSERT INTO Haberler (Baslik,Metin,Tarih,Link) OUTPUT INSERTED.Id VALUES (@Baslik,@Metin,@Tarih,@Link)", haberModel);
            }
            return id;
        }

        public void DeleteHaber(int id)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(connstr))
            {
                string command = $"DELETE FROM Haberler WHERE Id={id}";
                connection.Execute(command);
            }
        }
        public int InsertEtkinlik(EtkinlikModel etkinlikModel)
        {
            int id;
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(connstr))
            {
                id = connection.QuerySingle<int>("INSERT INTO Etkinlikler (Baslik,Metin,Tarih,Link) OUTPUT INSERTED.Id VALUES (@Baslik,@Metin,@Tarih,@Link)", etkinlikModel);
            }
            return id;
        }
        public void DeleteEtkinlik(int id)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(connstr))
            {
                string command = $"DELETE FROM Etkinlikler WHERE Id={id}";
                connection.Execute(command);
            }
        }
        public int InsertDuyuru(DuyuruModel duyuru)
        {
            int id;
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(connstr))
            {
                id = connection.QuerySingle<int>("INSERT INTO Duyurular (Baslik,Metin,Tarih,Link) OUTPUT INSERTED.Id VALUES (@Baslik,@Metin,@Tarih,@Link)", duyuru);
            }
            return id;
        }
        public void DeleteDuyuru(int id)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(connstr))
            {
                string command = $"DELETE FROM Duyurular WHERE Id={id}";
                connection.Execute(command);
            }
        }

        public int InsertEgitim(EgitimlerModel egitim)
        {
            int id;
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(connstr))
            {
                id = connection.QuerySingle<int>("INSERT INTO Egitimler (Baslik,Metin,Tarih,Link) OUTPUT INSERTED.Id VALUES (@Baslik,@Metin,@Tarih,@Link)", egitim);
            }
            return id;
        }
        public void DeleteEgitim(int id)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(connstr))
            {
                string command = $"DELETE FROM Egitimler WHERE Id={id}";
                connection.Execute(command);
            }
        }

        public int InsertBasindaBiz(BasindaBizModel basindaBiz)
        {
            int id;
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(connstr))
            {
                id = connection.QuerySingle<int>("INSERT INTO BasindaBizKayitlar (Baslik,Tarih,Link) OUTPUT INSERTED.Id VALUES (@Baslik,@Tarih,@Link)", basindaBiz);
            }
            return id;
        }
        public void DeleteBasindaBiz(int id)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(connstr))
            {
                string command = $"DELETE FROM BasindaBizKayitlar WHERE Id={id}";
                connection.Execute(command);
            }
        }

        public int InsertBasinBulteni(BasinBultenlerimizModel basinBulteni)
        {
            int id;
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(connstr))
            {
                id = connection.QuerySingle<int>("INSERT INTO BasinBultenlerimiz (Baslik,Tarih,Link) OUTPUT INSERTED.Id VALUES (@Baslik,@Tarih,@Link)", basinBulteni);
            }
            return id;
        }
        public void DeleteBasinBulteni(int id)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(connstr))
            {
                string command = $"DELETE FROM BasinBultenlerimiz WHERE Id={id}";
                connection.Execute(command);
            }
        }

        /* get by id */
        public List<HaberModel> ByIdGetHaber(int id)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(connstr))
            {
                string command = $"SELECT * FROM Haberler WHERE Id={id}";
                var output = connection.Query<HaberModel>(command).ToList();
                return output;
            }
        }
        public List<EtkinlikModel> ByIdGetEtkinlik(int id)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(connstr))
            {
                string command = $"SELECT * FROM Etkinlikler WHERE Id={id}";
                var output = connection.Query<EtkinlikModel>(command).ToList();
                return output;
            }
        }
        public List<BasindaBizModel> ByIdGetBasindaBiz(int id)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(connstr))
            {
                string command = $"SELECT * FROM BasindaBizKayitlar WHERE Id={id}";
                var output = connection.Query<BasindaBizModel>(command).ToList();
                return output;
            }
        }
        public List<BasinBultenlerimizModel> ByIdGetBasinBulteni(int id)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(connstr))
            {
                string command = $"SELECT * FROM BasinBultenlerimiz WHERE Id={id}";
                var output = connection.Query<BasinBultenlerimizModel>(command).ToList();
                return output;
            }
        }
        public List<DuyuruModel> ByIdGetDuyuru(int id)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(connstr))
            {
                string command = $"SELECT * FROM Duyurular WHERE Id={id}";
                var output = connection.Query<DuyuruModel>(command).ToList();
                return output;
            }
        }
        public List<EgitimlerModel> ByIdGetEgitim(int id)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(connstr))
            {
                string command = $"SELECT * FROM Egitimler WHERE Id={id}";
                var output = connection.Query<EgitimlerModel>(command).ToList();
                return output;
            }
        }

        /* UPDATE */
        public void UpdateHaber(HaberModel haber)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(connstr))
            {
                connection.Execute("UPDATE Haberler SET Baslik=@Baslik,Metin=@Metin,Tarih=@Tarih,Link=@Link WHERE Id=@Id", haber);
            }
        }
        public void UpdateEtkinlik(EtkinlikModel etkinlik)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(connstr))
            {
                connection.Execute("UPDATE Etkinlikler SET Baslik=@Baslik,Metin=@Metin,Tarih=@Tarih,Link=@Link WHERE Id=@Id", etkinlik);
            }
        }
        public void UpdateDuyuru(DuyuruModel duyuru)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(connstr))
            {
                connection.Execute("UPDATE Duyurular SET Baslik=@Baslik,Metin=@Metin,Tarih=@Tarih,Link=@Link WHERE Id=@Id", duyuru);
            }
        }
        public void UpdateEgitim(EgitimlerModel egitim)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(connstr))
            {
                connection.Execute("UPDATE Egitimler SET Baslik=@Baslik,Metin=@Metin,Tarih=@Tarih,Link=@Link WHERE Id=@Id", egitim);
            }
        }
        public void UpdateBasindaBiz(BasindaBizModel basindaBiz)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(connstr))
            {
                connection.Execute("UPDATE BasindaBizKayitlar SET Baslik=@Baslik,Tarih=@Tarih,Link=@Link WHERE Id=@Id", basindaBiz);
            }
        }
        public void UpdateBasinBulteni(BasinBultenlerimizModel basinBulteni)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(connstr))
            {
                connection.Execute("UPDATE BasinBultenlerimiz SET Baslik=@Baslik,Tarih=@Tarih,Link=@Link WHERE Id=@Id", basinBulteni);
            }
        }

        /* SEARCH */

        public List<HaberModel> SearchHaber(string searchValue)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(connstr))
            {
                var output = connection.Query<HaberModel>($"SELECT * FROM Haberler WHERE Baslik LIKE @x OR Metin LIKE @x", new { x = "%" + searchValue + "%" }).ToList();
                return output;
            }
        }
        public List<EtkinlikModel> SearchEtkinlik(string searchValue)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(connstr))
            {
                var output = connection.Query<EtkinlikModel>($"SELECT * FROM Etkinlikler WHERE Baslik LIKE @x OR Metin LIKE @x", new { x = "%" + searchValue + "%" }).ToList();
                return output;
            }
        }
        public List<BasindaBizModel> SearchBasindaBiz(string searchValue)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(connstr))
            {
                var output = connection.Query<BasindaBizModel>($"SELECT * FROM BasindaBizKayitlar WHERE Baslik LIKE @x", new { x = "%" + searchValue + "%" }).ToList();
                return output;
            }
        }
        public List<BasinBultenlerimizModel> SearchBasinBulteni(string searchValue)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(connstr))
            {
                var output = connection.Query<BasinBultenlerimizModel>($"SELECT * FROM BasinBultenlerimiz WHERE Baslik LIKE @x", new { x = "%" + searchValue + "%" }).ToList();
                return output;
            }
        }
        public List<EgitimlerModel> SearchEgitim(string searchValue)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(connstr))
            {
                var output = connection.Query<EgitimlerModel>($"SELECT * FROM Egitimler WHERE Baslik LIKE @x OR Metin LIKE @x", new { x = "%" + searchValue + "%" }).ToList();
                return output;
            }
        }
        public List<DuyuruModel> SearchDuyuru(string searchValue)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(connstr))
            {
                var output = connection.Query<DuyuruModel>($"SELECT * FROM Duyurular WHERE Baslik LIKE @x OR Metin LIKE @x", new { x = "%" + searchValue + "%" }).ToList();
                return output;
            }
        }

    }
}
