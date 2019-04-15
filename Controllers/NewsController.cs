using FashionShop.Models;
using FashionShop.Models.DAO;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace FashionShop.Controllers
{
    public class NewsController : Controller
    {
        // GET: News
        public ActionResult News(int? page)
        {
            var showNews = new News().show();

            int pageSize = 3;
            int pageNumber = page ?? 1;

            if (showNews == null)
                return View("Error");
            return View(showNews.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult NewsDetail(string meta, int id)
        {
            var showNews = new News().newsDetail(id);
            return View(showNews);
        }

        //Category
        public ActionResult Category()
        {
            var cate = new Category().Cate();
            return PartialView(cate);
        }

        //Product
        public ActionResult Product()
        {
            var Prod = new ShowProduct().ProductHot();
            return PartialView(Prod);
        }

        //rss news
        public ActionResult ReadRss()
        {
            string url = "https://vnexpress.net/rss/tin-moi-nhat.rss";
            WebClient wclient = new WebClient();
            wclient.Encoding = ASCIIEncoding.UTF8;
            string RssData = wclient.DownloadString(url);

            XDocument xml = XDocument.Parse(RssData, LoadOptions.PreserveWhitespace);
            var RssFeedData = (from i in xml.Descendants("item")
                               select new RSSFeed
                               {
                                   Title = ((string)i.Element("title")),
                                   Link = ((string)i.Element("link")),
                                   Description = ((string)i.Element("description")),
                                   PubDate = ((string)i.Element("pudDate"))
                               });

            ViewBag.RSSFeed = RssFeedData;
            ViewBag.URL = url;
            return PartialView();
        }
    }
}