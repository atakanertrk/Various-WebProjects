using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnurShopSecond.DataAccess
{
    public class LoginCookie : ILoginCookie
    {
        private IDataAccess _dataAccess;
        public LoginCookie(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public bool LogIn(string password)
        {

            if (_dataAccess.isPasswordValid(password))
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
