using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FashionShop.Models.EF;
using System.IO;
using PagedList;

namespace FashionShop.Areas.Admin.Controllers
{
    public class ProductsController : BaseController
    {
        private FashionShopDB db = new FashionShopDB();

        // GET: Admin/Products
        public ActionResult Index(int? id = null)
        {
            getMenuCategory(id);
            return View();
        }

        public void getMenuCategory(int? selectedID)
        {
            ViewBag.MenuCate = new SelectList(db.MenuCategory, "ID", "MenuCateName", selectedID);
        }

        public ActionResult getProduct(int? id, int? page)
        {
            int pageNumber = page ?? 1;
            int pageSize = 10;

            if (id == null)
            {
                var v = db.Product.OrderBy(x => x.ID).ToList();
                return PartialView(v.ToPagedList(pageNumber, pageSize));
            }

            var m = db.Product.Where(x => x.MenuCateID == id).OrderBy(x => x.ID).ToList();
            return PartialView(m.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            ViewBag.MenuCateID = new SelectList(db.MenuCategory, "ID", "MenuCateName");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "ID,ProductName,ProductPrice,ProductSize,ProductColor,Quantity,ProductImg,Meta,Hide,Order,Datebegin,ProductDescription,MenuCateID")] Product product, HttpPostedFileBase img)
        {
            var path = "";
            var filename = "";
            if (ModelState.IsValid)
            {
                if(img != null)
                {
                    filename = img.FileName;
                    path = Path.Combine(Server.MapPath("~/Images/Products/"), filename);
                    img.SaveAs(path);
                    product.ProductImg = filename;
                }
                else
                {
                    product.ProductImg = "logo.png";
                }

                db.Product.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MenuCateID = new SelectList(db.MenuCategory, "ID", "MenuCateName", product.MenuCateID);
            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.MenuCateID = new SelectList(db.MenuCategory, "ID", "MenuCateName", product.MenuCateID);
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "ID,ProductName,ProductPrice,ProductSize,ProductColor,Quantity,ProductImg,Meta,Hide,Order,Datebegin,ProductDescription,MenuCateID")] Product product, HttpPostedFileBase img)
        {
            var filename = "";
            var path = "";
            if (ModelState.IsValid)
            {
                if(img != null)
                {
                    filename = img.FileName;
                    path = Path.Combine(Server.MapPath("~/Images/Products/"), filename);

                    img.SaveAs(path);
                    product.ProductImg = filename;
                }
                else
                {
                    product.ProductImg = "";
                }
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MenuCateID = new SelectList(db.MenuCategory, "ID", "MenuCateName", product.MenuCateID);
            return View(product);
        }

        // GET: Admin/Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Product.Find(id);
            db.Product.Remove(product);
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
