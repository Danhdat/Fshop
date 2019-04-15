using FashionShop.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FashionShop.Models.DAO
{
    public class News
    {
        FashionShopDB db = null;

        public News()
        {
            db = new FashionShopDB();
        }

        //show news
        public List<EF.News> showNews()
        {
            return db.News.Take(3).ToList();
        }

        public List<EF.News> show()
        {
            return db.News.ToList();
        }

        public EF.News newsDetail(int id)
        {
            var item = from t in db.News
                       where t.ID == id
                       select t;
            return item.FirstOrDefault();
        }

        public EF.News getById(int id)
        {
            return db.News.Where(x => x.ID == id).FirstOrDefault();
        }
    }
}