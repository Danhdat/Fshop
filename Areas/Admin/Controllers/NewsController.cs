using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FashionShop.Models.EF;
using System.ComponentModel.DataAnnotations;
using System.IO;
using Microsoft.Diagnostics.Instrumentation.Extensions.Intercept;
using PagedList;

namespace FashionShop.Areas.Admin.Controllers
{
    public class NewsController : BaseController
    {
        private FashionShopDB db = new FashionShopDB();

        // GET: Admin/News
        public ActionResult Index(int? page)
        {
            var pageNumber = page ?? 1;
            var news = new FashionShop.Models.DAO.News().show();
            return View(news.ToPagedList(pageNumber, 3));
        }

        // GET: Admin/News/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FashionShop.Models.EF.News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // GET: Admin/News/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/News/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NewName,NewImg,Desciption,Link,Detail,Meta,Hide,Order,Datebegin")] FashionShop.Models.EF.News news)
        {
            if (ModelState.IsValid)
            {
                db.News.Add(news);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(news);
        }

        // GET: Admin/News/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FashionShop.Models.EF.News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: Admin/News/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "ID,NewName,NewImg,Desciption,Link,Detail,Meta,Hide,Order,Datebegin")] FashionShop.Models.EF.News news, HttpPostedFileBase img)
        {
            var filename = "";
            var path = "";
            FashionShop.Models.EF.News temp = new FashionShop.Models.DAO.News().getById(news.ID);
            if (ModelState.IsValid)
            {
                if(img != null)
                {
                    filename = img.FileName;
                    path = Path.Combine(Server.MapPath("~/Images/Products/"), filename);

                    img.SaveAs(path);
                    temp.NewImg = filename;
                }

                temp.NewName = news.NewName;
                temp.Desciption = news.Desciption;
                temp.Detail = news.Detail;
                temp.Meta = news.Meta;
                temp.Hide = news.Hide;
                temp.Order = news.Order;
                temp.Datebegin = news.Datebegin;

                db.Entry(temp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(news);
        }

        // GET: Admin/News/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FashionShop.Models.EF.News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: Admin/News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FashionShop.Models.EF.News news = db.News.Find(id);
            db.News.Remove(news);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
