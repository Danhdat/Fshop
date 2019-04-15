using FashionShop.Models;
using FashionShop.Models.DAO;
using FashionShop.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace FashionShop.Controllers
{
    public class CartController : Controller
    {
        FashionShopDB db = null;
        public CartController()
        {
            db = new FashionShopDB();
        }

        private const string CartSession = "CartSession";
        // GET: Cart
        public ActionResult Index()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();

            if (cart != null)
            {
                list = (List<CartItem>)cart;
                ViewBag.totalCost = list.Sum(x => x.Product.ProductPrice);
            }

            return View(list);
        }

        //get Size
        public ActionResult listSize()
        {
            var listSize = new Models.DAO.Category().Size();
            return PartialView(listSize);
        }

        //Thêm mới sản phẩm
        public ActionResult AddItem(int productID, int Quantity, int sizeID)
        {
            var size = new Models.DAO.Category().SizeID(sizeID);
            var product = new ShowProduct().ProductDetail(productID);
            var Cart = Session[CartSession];
            if (Cart != null)
            {
                var list = (List<CartItem>)Cart;

                if (list.Exists(x => x.Product.ID == productID))
                {
                    foreach (var item in list)
                    {
                        if (item.Product.ID == productID)
                        {
                            item.Quantity += Quantity;
                        }
                    }
                }
                else
                {
                    var item = new CartItem();
                    item.Product = product;
                    item.Quantity = Quantity;
                    item.Size = size;
                    list.Add(item);
                }

                Session[CartSession] = list;
            }
            else
            {
                var item = new CartItem();
                item.Product = product;
                item.Quantity = Quantity;
                item.Size = size;

                var list = new List<CartItem>();
                list.Add(item);
                Session[CartSession] = list;
            }

            return RedirectToAction("Index");
        }

        //Update giỏ hàng
        public JsonResult UpdateCart(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var sessionCart = (List<CartItem>)Session[CartSession];

            foreach (var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.Product.ID == item.Product.ID);
                if (jsonItem != null)
                {
                    item.Quantity = jsonItem.Quantity;
                }
            }

            Session[CartSession] = sessionCart; // cập nhật lại giỏ hàng

            return Json(new
            {
                status = true
            });
        }

        public JsonResult updateSize(int sizes, int productID)
        {
            var listSize = new Models.DAO.Category().SizeID(sizes);
            var sessionCart = (List<CartItem>)Session[CartSession];

            foreach(var item in sessionCart)
            {
                if(item.Product.ID == productID)
                {
                    item.Size = listSize;
                }
            }

            return Json(new
            {
                status = true
            });
        }

        //Delete giỏ hàng
        public JsonResult DeteleItem(int id)
        {
            var list = (List<CartItem>)Session[CartSession];

            list.RemoveAll(x => x.Product.ID == id);
            Session[CartSession] = list;

            return Json(new
            {
                status = true
            });
        }

        //partialView Information Custumer
        public ActionResult InformationCustumer()
        {
            return PartialView();
        }

        //partialView total payment
        public ActionResult TotalPayment()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();

            if(cart != null)
            {
                list = (List<CartItem>)cart;
            }

            return PartialView(list);
        }

        //Payment
        [HttpPost]
        public ActionResult Payment(FormCollection fc)
        {
            string Name = fc["txtCustumer"];
            string Phone = fc["txtPhone"];
            string Road = fc["txtRoad"];

            int tempCountry = int.Parse(fc["textCountry"]);
            Country Country = new Services().Country(tempCountry);

            int tempProvince = int.Parse(fc["textProvince"]);
            Province Province = new Services().Province(tempProvince);

            int tempDistrict = int.Parse(fc["textDistrict"]);
            District District = new Services().District(tempDistrict);

            int tempWard = int.Parse(fc["textWard"]);
            Ward Ward = new Services().Ward(tempWard);

            string[] add = new string[] { Road, Ward.Name, District.Name, Province.Name, Country.CommonName };
            var order = new Order();
            order.ClientName = Name;
            order.Address = string.Join(" - ", add);
            order.PhoneNumber = Phone;
            order.Hide = false;
            order.OrderDate = DateTime.Now;

            decimal totalPrice = 0;
            var cart = (List<CartItem>)Session[CartSession];
            foreach (var i in cart)
            {
                totalPrice += (decimal)(i.Quantity * i.Product.ProductPrice);
            }
            order.Paid = totalPrice;

            var id = new AddOrder().Insert(order);
            
            var detail = new AddOrderDetail();
            try
            {
                foreach (var item in cart)
                {
                    var orderDetail = new OrderDetail();
                    orderDetail.OrderID = id;
                    orderDetail.ProductID = item.Product.ID;
                    orderDetail.Quantity = item.Quantity;
                    orderDetail.TotalPrice = (decimal)item.Product.ProductPrice;
                    detail.InsertOrderDetail(orderDetail);
                }
            }            
            catch (Exception ex)
            {
                ex.ToString();
            }
            Session[CartSession] = null;

            return Redirect("/thanh-cong");
        }

        //Success
        public ActionResult Success()
        {
            return View();
        }



        //Json get Address
        /// <summary>
        /// select(e => new {Id = e.Id, Name = e.Nam}): fix error circule references....
        /// </summary>
        /// <returns></returns>
        public JsonResult getAllCountries()
        {
            var data = db.Country.OrderBy(x => x.CommonName).
                Select(e => new {
                    Id = e.Id,
                    CommonName = e.CommonName
                });
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getAllProvincesByCountryId(int? id = 237)
        {
            var data = db.Province.Where(x => x.CountryId == id).OrderBy(x => x.Name).
                Select(e => new {
                    Id = e.Id,
                    Name = e.Name
                });

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getAllDistrictsByProvinceId(int? id = 1)
        {
            var data = db.District.Where(x => x.ProvinceId == id).OrderBy(x => x.Name).
                Select(e => new {
                    Id = e.Id,
                    Name = e.Name
                });

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getAllWardsByDistrictId(int? id=1)
        {
            var data = db.Ward.Where(x => x.DistrictID == id).OrderBy(x => x.Name).
                Select(e => new {
                    Id = e.Id,
                    Name = e.Name
                });

            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}