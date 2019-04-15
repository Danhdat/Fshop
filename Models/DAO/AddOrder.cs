using FashionShop.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FashionShop.Models.DAO
{
    public class AddOrder
    {
        FashionShopDB db = null;

        public AddOrder()
        {
            db = new FashionShopDB();
        }

        public int Insert(Order order)
        {
            db.Order.Add(order);
            db.SaveChanges();

            return order.ID;
        }
    }
}