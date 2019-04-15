using FashionShop.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace FashionShop.Models.DAO
{
    public class MenuBar
    {
        FashionShopDB db = null;
        public MenuBar()
        {
            db = new FashionShopDB();
        }

        //xuất danh sách menubar
        public List<Menu> menu()
        {
            return db.Menu.OrderBy(x => x.Order).ToList();
        }
    }
}