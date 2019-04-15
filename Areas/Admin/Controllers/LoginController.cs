using FashionShop.Areas.Admin.Models;
using FashionShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FashionShop.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(FashionShop.Models.EF.Admin admin)
        {

            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.Login(admin.UserName, admin.Password);

                if (result)
                {
                    var user = dao.getByID(admin.UserName);
                    var userSession = new adminLogin();

                    userSession.ID = user.ID;
                    userSession.username = user.UserName;
                    Session.Add(commonstants.User_Session, userSession);

                    return RedirectToAction("Index", "Default");
                }
                else
                {
                    ModelState.AddModelError("", "Đăng nhập không đúng");
                }
            }

            return View("Index");
        }
    }
}