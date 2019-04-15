using FashionShop.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FashionShop.Models.DAO
{
    public class Category
    {
        FashionShopDB db = null;

        public Category()
        {
            db = new FashionShopDB();
        }

        //List Category
        public List<EF.Category> Cate()
        {
            return db.Category.ToList();
        }

        //List Products belong with Category
        public List<MenuCategory> MenuCate(Int32 id)
        {
            return db.MenuCategory.Where(x => x.CategoryID == id).ToList();
        }

        //list Size
        public List<Size> Size()
        {
            return db.Size.OrderBy(x => x.ID).ToList();
        }

        public Size SizeID(int id)
        {
            return db.Size.Where(x => x.ID == id).FirstOrDefault();
        }
    }
}