using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZMO_Eskisehir_WebUI.DataAccess;

namespace ZMO_Eskisehir_WebUI.Helpers
{
    public class PasswordControl
    {
        private SqlDataAccess dataAccess;
        public PasswordControl(IConfiguration config)
        {
            dataAccess = new SqlDataAccess(config);
        }
        public bool IsPasswordCorrect(string password)
        {
            if (password != null && password == dataAccess.GetSifre().FirstOrDefault().Password)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
