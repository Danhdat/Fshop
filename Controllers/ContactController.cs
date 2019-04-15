using FashionShop.Models.DAO;
using FashionShop.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FashionShop.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddContect(string name, string address, string phone, string email, string message)
        {
            var addcontact = new AddContact();
            var contact = new Contact();

            contact.ContactName = name;
            contact.Address = address;
            contact.Phone = phone;
            contact.Email = email;
            contact.Message = message;
            addcontact.AddContacts(contact);

            return View("Index");
        }
    }
}