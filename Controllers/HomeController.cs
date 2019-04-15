using FashionShop.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FashionShop.Models;
using PagedList;
using System.Xml.Linq;
using System.Globalization;
using System.Text;
using FashionShop.Models.EF;

namespace FashionShop.Controllers
{
    public class HomeController : Controller
    {
        private const string CartSession = "CartSession";
        FashionShopDB db = null;

        public HomeController()
        {
            db = new FashionShopDB();
        }


        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        //Header Icon
        public ActionResult HeaderIcon()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();

            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }

            return PartialView(list);
        }

        //Header quantity
        public ActionResult HeaderQuantity()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();

            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }

            return PartialView(list);
        }

        //Show menubar
        public ActionResult menubar()
        {
            var bar = new MenuBar().menu();
            return PartialView(bar);
        }

        //Show data banner
        public ActionResult showBanner()
        {
            var banner = new Models.DAO.Category().Cate();
            return PartialView(banner);
        }

        //Show new products
        public ActionResult newProducts()
        {
            var _newProduct = new ShowProduct().newProduct();
            return PartialView(_newProduct);
        }
        // ===========//

        //Show the same product
        public ActionResult SameProduct(string name)
        {
            string NAME = name;

            var _sameProduct = new ShowProduct().sameProduct(name);
            return PartialView(_sameProduct);
        }

        //show news
        public ActionResult news()
        {
            var _news = new Models.DAO.News().showNews();

            return PartialView(_news);
        }
        // create sitemap auto
        public class sitemapNode
        {
            public SitemapFrequency? Frequency { get; set; }
            public DateTime? LastModifiled { get; set; }
            public double? Priority { get; set; }
            public string Url { get; set; }
        }

        public enum SitemapFrequency
        {
            Never,
            Yearly,
            Monthly,
            Weekly,
            Daily,
            Hourly,
            Always,
        }

        public IReadOnlyCollection<sitemapNode> GetSitemapNodes(UrlHelper urlHelper)
        {
            List<sitemapNode> nodes = new List<sitemapNode>();
            nodes.Add(
                new sitemapNode()
                {
                    Url = Url.Action("Index", "Home", null, Request.Url.Scheme),
                    LastModifiled = DateTime.Now,
                    Priority = 1
                });
            nodes.Add(
               new sitemapNode()
               {
                   Url = Url.Action("Index", "About", null, Request.Url.Scheme),
                   LastModifiled = DateTime.Now,
                   Priority = 0.9
               });
            nodes.Add(
               new sitemapNode()
               {
                   Url = Url.Action("Index", "Contact", null, Request.Url.Scheme),
                   LastModifiled = DateTime.Now,
                   Priority = 0.9
               });
          // contronller news
            nodes.Add(
               new sitemapNode()
               {
                   Url = Url.Action("news", "News", null, Request.Url.Scheme),
                   LastModifiled = DateTime.Now,
                   Priority = 0.9
               });

            foreach (var product in db.News)
            {
                nodes.Add(
                   new sitemapNode()
                   {
                       Url = Url.Action("news", "News",new{product.ID,product.Meta}, Request.Url.Scheme),
                       LastModifiled = DateTime.Now,
                       Priority = 0.8
                   });
            }




            // contronller product
            foreach (var product in db.Product)
            {
                nodes.Add(
                   new sitemapNode()
                   {
                       Url = Url.Action("Product", "Product", new { product.Meta,product.ID }, Request.Url.Scheme),
                       LastModifiled = DateTime.Now,
                       Priority = 0.8
                   });
            }
            int countPage = (int)Math.Ceiling(db.Product.Count() / 10.0);
            for (int page = 1; page <= countPage; page++)
            {
                foreach (var product in db.Product)
                {
                    nodes.Add(
                       new sitemapNode()
                       {
                           Url = Url.Action("Product", "Product", new { id = product.ID, page = page, product.Meta }, Request.Url.Scheme),
                             LastModifiled = DateTime.Now,
                           Priority = 0.8
                       });
                }
            }
           
            return nodes;

        }
        public string GetSitemapDocument(IEnumerable<sitemapNode> sitemapNodes)
        {
            XNamespace xmlns = "http://www.sitemaps.org/schemas/sitemap/0.9";
            XElement root = new XElement(xmlns + "urlset");

            foreach (sitemapNode sitemapNode in sitemapNodes)
            {
                XElement urlElement = new XElement(
                    xmlns + "url",
                    new XElement(xmlns + "loc", Uri.EscapeUriString(sitemapNode.Url)),
                    sitemapNode.LastModifiled == null ? null : new XElement(
                        xmlns + "lastmod",
                        sitemapNode.LastModifiled.Value.ToLocalTime().ToString("yyyy-MM-ddTHH:mm:sszzz")),
                    sitemapNode.Frequency == null ? null : new XElement(
                        xmlns + "changefreq",
                        sitemapNode.Frequency.Value.ToString().ToLowerInvariant()),
                    sitemapNode.Priority == null ? null : new XElement(
                        xmlns + "priority",
                        sitemapNode.Priority.Value.ToString("F1", CultureInfo.InvariantCulture)));
                root.Add(urlElement);
            }

            XDocument document = new XDocument(root);
            return document.ToString();
        }
        [Route("sitemap.xml")]
        public ActionResult SitemapXml()
        {
            var sitemapNodes = GetSitemapNodes(this.Url);
            string xml = GetSitemapDocument(sitemapNodes);
            return this.Content(xml, "text/xml", Encoding.UTF8);
        }

        //robot.txt
        #region --Robots() Method --
        public ActionResult Robots()
        {
            Response.ContentType = "text/plain";
            return View();
        }
        #endregion


    }
}