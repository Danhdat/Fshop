using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FashionShop.Models.EF;
using PagedList;
using System.IO;

namespace FashionShop.Areas.Admin.Controllers
{
    public class SlidesController : BaseController
    {
        private FashionShopDB db = new FashionShopDB();

        // GET: Admin/Slides
        public ActionResult Index(int? page)
        {
            var pageNumber = page ?? 1;
            var slide = new FashionShop.Models.DAO.Slide().Slides();
            return View(slide.ToPagedList(pageNumber, 5));
        }

        // GET: Admin/Slides/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slide slide = db.Slide.Find(id);
            if (slide == null)
            {
                return HttpNotFound();
            }
            return View(slide);
        }

        // GET: Admin/Slides/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Slides/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "ID,SlideName,SlideImg,Description,Link,Meta,Hide,Order,Datebegin")] Slide slide, HttpPostedFileBase img)
        {
            if (ModelState.IsValid)
            {
                var fileName = "";
                var path = "";

                if(img != null)
                {
                    fileName = img.FileName;
                    path = Path.Combine(Server.MapPath("~/Images/Slides/"), fileName);

                    img.SaveAs(path);
                    slide.SlideImg = fileName;
                }
                else
                {
                    slide.SlideImg = "Slide";
                }
                db.Slide.Add(slide);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(slide);
        }

        // GET: Admin/Slides/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slide slide = db.Slide.Find(id);
            if (slide == null)
            {
                return HttpNotFound();
            }
            return View(slide);
        }

        // POST: Admin/Slides/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "ID,SlideName,SlideImg,Description,Link,Meta,Hide,Order,Datebegin")] Slide slide, HttpPostedFileBase img)
        {
            var fileName = "";
            var path = "";

            if (ModelState.IsValid)
            {
                if(img != null)
                {
                    fileName = img.FileName;
                    path = Path.Combine(Server.MapPath("~/Images/Slides/"), fileName);

                    img.SaveAs(path);
                    slide.SlideImg = fileName;
                }
                db.Entry(slide).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(slide);
        }

        // GET: Admin/Slides/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slide slide = db.Slide.Find(id);
            if (slide == null)
            {
                return HttpNotFound();
            }
            return View(slide);
        }

        // POST: Admin/Slides/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Slide slide = db.Slide.Find(id);
            db.Slide.Remove(slide);
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
