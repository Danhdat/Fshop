using FashionShop.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FashionShop.Models.DAO
{
    public class Slide
    {
        FashionShopDB db = null;

        public Slide()
        {
            db = new FashionShopDB();
        }

        public List<EF.Slide> Slides()
        {
            return db.Slide.OrderBy(x => x.ID).ToList();
        }
    }
}