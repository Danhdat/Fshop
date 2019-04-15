using FashionShop.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FashionShop.Areas.Admin.Models
{
    public class Orders
    {
        FashionShopDB db = null;
        public Orders()
        {
            db = new FashionShopDB();
        }

        public List<OrderDetail> OrderDetail()
        {
            var OD = from od in db.OrderDetail
                     from o in db.Order
                     where od.OrderID == o.ID
                     orderby o.ID descending
                     select od;
            return OD.ToList();         
        }

        public void Delete(int id)
        {
            var d = db.OrderDetail.Find(id);
            db.OrderDetail.Remove(d);
            db.SaveChanges();
        }
    }
}