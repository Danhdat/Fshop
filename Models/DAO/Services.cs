using FashionShop.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FashionShop.Models.DAO
{
    public class Services
    {
        FashionShopDB db = null;
        public Services()
        {
            db = new FashionShopDB();
        }

        public Country Country(int id)
        {
            var r = from p in db.Country
                    where p.Id == id
                    select p;
            return r.FirstOrDefault();
        }

        public Province Province(int id)
        {
            var r = from p in db.Province
                    where p.Id == id
                    select p;
            return r.FirstOrDefault();
        }

        public District District(int id)
        {
            var r = from p in db.District
                    where p.Id == id
                    select p;
            return r.FirstOrDefault();
        }

        public Ward Ward(int id)
        {
            var r = from p in db.Ward
                    where p.Id == id
                    select p;
            return r.FirstOrDefault();
        }
    }
}