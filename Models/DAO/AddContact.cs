using FashionShop.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FashionShop.Models.DAO
{
    public class AddContact
    {
        FashionShopDB db = null;

        public AddContact()
        {
            db = new FashionShopDB();
        }

        public void AddContacts(Contact addcontect)
        {
            db.Contact.Add(addcontect);
            db.SaveChanges();
        }
    }
}