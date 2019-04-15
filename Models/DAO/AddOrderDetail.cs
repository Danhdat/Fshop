using FashionShop.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FashionShop.Models.DAO
{
    public class AddOrderDetail
    {
        FashionShopDB db = null;

        public AddOrderDetail()
        {
            db = new FashionShopDB();
        }

        public void InsertOrderDetail(OrderDetail orderDetail)
        {
            try
            {
                db.OrderDetail.Add(orderDetail);
                db.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
    }
}