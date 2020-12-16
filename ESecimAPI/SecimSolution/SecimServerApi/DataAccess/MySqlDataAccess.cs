using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using SecimServerApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SecimServerApi.DataAccess
{
    public class MySqlDataAccess : IDataAccess
    {
        private string _conStr;
        public MySqlDataAccess(IConfiguration config)
        {
            _conStr = ConfigurationExtensions.GetConnectionString(config, "MySqlConnection");
        }

        public List<AdayModel> GetAdayList()
        {
            using (IDbConnection cnn = new MySqlConnection(_conStr))
            {
                string sql = "SELECT * FROM Adaylar;";

                var output = cnn.Query<AdayModel>(sql).ToList();
                return output;
            }
        }

        public List<SecmenModel> GetAllSecmenler()
        {
            using (IDbConnection cnn = new MySqlConnection(_conStr))
            {
                string sql = "SELECT * FROM Secmenler;";

                var output = cnn.Query<SecmenModel>(sql).ToList();
                return output;
            }
        }

        public void InsertOy(string oy)
        {
            using (IDbConnection cnn = new MySqlConnection(_conStr))
            {
                var p = new DynamicParameters();
                p.Add("@Oy", oy);

                string sql = "INSERT INTO Oylar (Oy) VALUES (@Oy);";

                cnn.Execute(sql, p);
            }
        }
        public void UpdateSecmenOyDurumu(int durum, string tcno)
        {
            using (IDbConnection cnn = new MySqlConnection(_conStr))
            {
                var p = new DynamicParameters();
                p.Add("@OyKullandiMi", durum);
                p.Add("@SecmenTcNo", tcno);
                string sql = "UPDATE Secmenler SET OyKullandiMi=@OyKullandiMi WHERE TcNo=@SecmenTcNo;";

                cnn.Execute(sql, p);
            }
        }

        public List<OyModel> GetAllOylar()
        {
            using (IDbConnection cnn = new MySqlConnection(_conStr))
            {
                string sql = "SELECT * FROM Oylar";

                return cnn.Query<OyModel>(sql).ToList();
            }
        }

        public void InsertSecmen(SecmenModel model)
        {
            using (IDbConnection cnn = new MySqlConnection(_conStr))
            {
                var p = new DynamicParameters();
                p.Add("@TcNo", model.TcNo);
                p.Add("@Sifre", model.Sifre);

                string sql = "INSERT INTO Secmenler (TcNo,Sifre) VALUES (@TcNo,@Sifre);";

                cnn.Execute(sql, p);
            }
        }
        public void OylariTemizle()
        {
            using (IDbConnection cnn = new MySqlConnection(_conStr))
            {
                var p = new DynamicParameters();
                p.Add("@OyKullandiMi", 0);
                string sql = "DELETE FROM Oylar; UPDATE Secmenler SET OyKullandiMi=@OyKullandiMi";

                cnn.Execute(sql, p);
            }
        }

    }
}
