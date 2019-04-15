using FashionShop.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FashionShop.Areas.Admin.Models
{
    public class UserDao
    {
        FashionShopDB db = null;
        public UserDao()
        {
            db = new FashionShopDB();
        }

        public FashionShop.Models.EF.Admin getByID(string username)
        {
            return db.Admin.SingleOrDefault(x => x.UserName == username);
        }

        public bool Login(string username, string password)
        {
            var result = db.Admin.Count(x => x.UserName == username && x.Password == password);

            if (result > 0)
                return true;
            return false;
        }
    }
}